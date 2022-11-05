using Microsoft.AspNetCore.Mvc;

namespace ATM_Banking_System.Controllers
{
    public class UsersController : Controller
    {
        [HttpPost]
        public IActionResult UserDashboard()
        {
            return View();
        }

        public IActionResult Deposit()
        {
            return View();
        }

        public IActionResult Withdraw()
        {
            return View();
        }

        public IActionResult BalanceInquiry()
        {
            return View();
        }

        public IActionResult TransactionLog()
        {
            return View();
        }
        public IActionResult ChangePIN()
        {
            return View();
        }

        public IActionResult FastCash()
        {
            return View();
        }
    }
}
