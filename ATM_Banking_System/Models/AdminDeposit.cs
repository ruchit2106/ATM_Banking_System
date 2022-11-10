using System.ComponentModel.DataAnnotations;

namespace ATM_Banking_System.Models
{
    public class AdminDeposit
    {
        public string? Type { get; set; }
        [Range(1000000000, 9999999999)]
        public long AccNo { get; set; }
        public DateTime dtOfTransaction { get; set; }
        public int Amount { get; set; }
        public int currAmountOfATM { get; set; }

    }
}
