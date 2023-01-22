namespace JWTWithCore.Model
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }

        public List<User> ActiveUsers()
        {
            return new List<User>
        {

            new User { Id = 1, UserName = "admin", Email = "admin@email.com",Password="admin", Role = "admin", RoleId = 1 },
            new User { Id = 1, UserName = "normaluser", Email = "normaluser@email.com",Password="normal", Role = "user", RoleId = 2 },
             new User { Id = 1, UserName = "second", Email = "seconduser@email.com",Password="second", Role = "user", RoleId = 2 }
        };
        }
    }

}
