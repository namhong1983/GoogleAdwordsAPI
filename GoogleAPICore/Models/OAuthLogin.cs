using System.ComponentModel.DataAnnotations;

namespace GoogleAPICore.Models
{
    public class OAuthLogin
    {
        [Key]
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
