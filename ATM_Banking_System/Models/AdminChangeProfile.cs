using System.ComponentModel.DataAnnotations;

namespace ATM_Banking_System.Models
{
    public class AdminChangeProfile
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
