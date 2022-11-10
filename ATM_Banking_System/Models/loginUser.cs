using System.ComponentModel.DataAnnotations;

namespace ATM_Banking_System.Models
{
    public class loginUser
    {
        [Required]
        [Range(1000000000,9999999999)]
        public long AccNo { get; set; }
        [Required]
        [Range(1000,9999)]
        public int PIN { get; set; }

    }
}
