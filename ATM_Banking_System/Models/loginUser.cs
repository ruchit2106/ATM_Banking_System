using System.ComponentModel.DataAnnotations;

namespace ATM_Banking_System.Models
{
    public class loginUser
    {
        [Required]
        public int AccNo { get; set; }
        [Required]
        public int PIN { get; set; }

    }
}
