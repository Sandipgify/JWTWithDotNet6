using JWTWithCore.DTO;
using JWTWithCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTWithCore.Controllers;
[ApiController]
[Route("Authorization")]
public class AuthController : Controller
{
    private readonly IConfiguration _configuration;

    public AuthController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    [Route("SignIn")]
    public async Task<IActionResult> Login(UserDTO request)
    {

        var user = new User();
        var userExist = user.ActiveUsers().Any(x=>x.UserName==request.UserName && x.Password== request.Password);

        if (userExist)
        {
           user = user.ActiveUsers().Where(x => x.UserName == request.UserName && x.Password == request.Password).FirstOrDefault();
            var jwttoken = CreateToken(user);
            return Ok(jwttoken);
        }
        return BadRequest("Username or Password not matched");

    }

    private string CreateToken(User user)
    {
        var issuer = _configuration.GetSection("Jwt:Issuer").Value;
        var audience = _configuration.GetSection("Jwt:Audience").Value;
        var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt:Key").Value);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
             }),
            Expires = DateTime.UtcNow.AddMinutes(5),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials
            (new SymmetricSecurityKey(key),
            SecurityAlgorithms.HmacSha512Signature)
        };
        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);
        return stringToken;





        //List<Claim> claims = new List<Claim>
        //{
        //    new Claim(ClaimTypes.Name,user.UserName),
        //    new Claim(ClaimTypes.Role,user.Role)
        //};
        //var key = new SymmetricSecurityKey(System.Text.ASCIIEncoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));

        //var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        //var token = new JwtSecurityToken(
        //    claims: claims,
        //    expires: DateTime.Now.AddDays(1),
        //    signingCredentials: cred);

        //var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        //return jwt;

    }
}
