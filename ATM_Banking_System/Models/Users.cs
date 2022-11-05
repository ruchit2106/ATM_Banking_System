using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Numerics;

namespace ATM_Banking_System.Models
{
    public class Users
    {
        public int Id { get; set; }
        [Required]
        public int AccNo { get; set; }
        [Required]
        public int PIN { get; set; }
        public int Balance { get; set; }
        public string FullName { get; set; }

        public char Gender { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
    }
}
