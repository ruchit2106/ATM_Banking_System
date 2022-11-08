namespace ATM_Banking_System.Models
{
    public class AdminDeposit
    {
        public string Type { get; set; }
        public int? AccNo { get; set; }
        public DateTime dtOfTransaction { get; set; }
        public int Amount { get; set; }
        public int currAmountOfATM { get; set; }

    }
}
