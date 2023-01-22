using JWTWithCore.DTO;
using JWTWithCore.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

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
        List<Claim> claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name,user.UserName),
            new Claim(ClaimTypes.Role,user.Role)
        };
        var key = new SymmetricSecurityKey(System.Text.ASCIIEncoding.UTF8.GetBytes(_configuration.GetSection("Token").Value));

        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddDays(1),
            signingCredentials: cred);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);

        return jwt;

    }
}
