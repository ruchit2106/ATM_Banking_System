using System.ComponentModel.DataAnnotations;

namespace ATM_Banking_System.Models
{
    public class UserChangePIN
    {
        [Range(1000,9999)]
        public int PIN { get; set; }
    }
}
