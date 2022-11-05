using System.ComponentModel.DataAnnotations;

namespace ATM_Banking_System.Models
{
    public class Admins
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }

    }
}
