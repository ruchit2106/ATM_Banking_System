using Microsoft.AspNetCore.Mvc;

namespace ATM_Banking_System.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult UserDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Deposit()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BalanceInquiry()
        {
            return View();
        }

        [HttpGet]
        public IActionResult TransactionLog()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangePIN()
        {
            return View();
        }

        [HttpGet]
        public IActionResult FastCash()
        {
            return View();
        }
    }
}
