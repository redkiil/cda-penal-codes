using System.Text.Json.Serialization;

namespace CDA.Models
{
    public class User : UserDto
    {
        public string Password { get; set; } = String.Empty;

    }
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; } = String.Empty;

    }
}
