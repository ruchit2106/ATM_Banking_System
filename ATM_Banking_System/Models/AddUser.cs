using System.ComponentModel.DataAnnotations;

namespace ATM_Banking_System.Models
{
    public class AddUser
    {
        [Required]
        [Range(1000000000,9999999999)]
        public long AccNo { get; set; }
        [Required]
        [Range(1000,9999)]
        public int PIN { get; set; }
        //public int Balance { get; set; }
        public string? FullName { get; set; }
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        public string? Address { get; set; }
        [Range(1000000000,9999999999)]
        public long PhoneNo { get; set; }

    }
}
