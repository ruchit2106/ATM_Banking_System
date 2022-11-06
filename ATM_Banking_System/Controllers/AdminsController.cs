using Microsoft.AspNetCore.Mvc;

namespace ATM_Banking_System.Controllers
{
    public class AdminsController : Controller
    {
        [HttpGet]
        public IActionResult AdminDashboard()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ChangeProfile()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ManageUsers()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewATMBalance()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ViewTransactionHistory()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DepositMoney()
        {
            return View();
        }

        [HttpGet]
        public IActionResult WithdrawMoney()
        {
            return View();
        }

    }
}
