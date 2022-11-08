using ATM_Banking_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Diagnostics;

namespace ATM_Banking_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;
        public readonly IHttpContextAccessor httpContextAccessor;

        private readonly ILogger<HomeController> _logger;
        private readonly MyAppDbContext myAppDbContext;
        public string connectionString; 

        public HomeController(ILogger<HomeController> logger, MyAppDbContext myAppDbContext,IConfiguration iconfig,IHttpContextAccessor contxtAccessor)
        {
            _logger = logger;
            this.myAppDbContext = myAppDbContext;
            this.configuration = iconfig;
            this.httpContextAccessor = contxtAccessor;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult loginUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult loginUser(loginUser u)
        {
            connectionString = configuration.GetConnectionString("MyAppConnString");
            string query = "SELECT COUNT(*) FROM Users WHERE AccNo=@AccNo AND PIN=@PIN";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@AccNo",u.AccNo);
            cmd.Parameters.AddWithValue("@PIN",u.PIN);
            con.Open();
            int c = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if(c==1)
            {
                httpContextAccessor.HttpContext.Session.SetInt32("AccNo",u.AccNo);
                return RedirectToAction("UserDashboard","Users");
            }
            else
            {
                return RedirectToAction("loginUser");
            }
        }


        [HttpGet]
        public IActionResult loginAdmin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult loginAdmin(loginAdmin au)
        {
            connectionString = configuration.GetConnectionString("MyAppConnString");
            string query = "SELECT COUNT(*) FROM Admins WHERE Username=@Username AND Password=@Password";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username", au.Username);
            cmd.Parameters.AddWithValue("@Password", au.Password);
            con.Open();
            int c = Convert.ToInt32(cmd.ExecuteScalar());
            con.Close();
            if (c == 1)
            {
                httpContextAccessor.HttpContext.Session.SetString("Username",au.Username);
                //httpContextAccessor.HttpContext.Session.SetString("Password",au.Password);
                //httpContextAccessor.HttpContext.Session.SetInt32("AccNo",);
                
                return RedirectToAction("AdminDashboard", "Admins");
            }
            else
            {
                return RedirectToAction("loginAdmin");
            }
        }

        [HttpGet]
        public IActionResult registerUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> registerUser(AddUser u)
        {
            var user = new Users()
            {
                AccNo = u.AccNo,
                PIN = u.PIN,
                Balance = 0,
                FullName = u.FullName,
                Gender = u.Gender,
                DOB = u.DOB,
                Address = u.Address,
                PhoneNo = u.PhoneNo,
            };
            await myAppDbContext.Users.AddAsync(user);
            await myAppDbContext.SaveChangesAsync();
            return RedirectToAction("registerUser");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}