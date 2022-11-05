using Microsoft.AspNetCore.Mvc;

namespace ATM_Banking_System.Controllers
{
    public class AdminsController : Controller
    {
        public IActionResult AdminDashboard()
        {
            return View();
        }

        public IActionResult ChangeProfile()
        {
            return View();
        }

        public IActionResult ManageUsers()
        {
            return View();
        }
        public IActionResult ViewATMBalance()
        {
            return View();
        }
        public IActionResult ViewTransactionHistory()
        {
            return View();
        }
        public IActionResult DepositMoney()
        {
            return View();
        }
        public IActionResult WithdrawMoney()
        {
            return View();
        }

    }
}
