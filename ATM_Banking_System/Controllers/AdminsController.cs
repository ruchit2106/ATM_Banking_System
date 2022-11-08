using ATM_Banking_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ATM_Banking_System.Controllers
{
    public class AdminsController : Controller
    {
        private readonly IConfiguration configuration;
        public readonly IHttpContextAccessor httpContextAccessor;
        private readonly MyAppDbContext myAppDbContext;
        public string connectionString;

        public AdminsController(MyAppDbContext myAppDbContext, IConfiguration iconfig, IHttpContextAccessor contxtAccessor)
        {
            this.myAppDbContext = myAppDbContext;
            this.configuration = iconfig;
            this.httpContextAccessor = contxtAccessor;
            this.connectionString = configuration.GetConnectionString("MyAppConnString");
        }

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

        [HttpPost]
        public IActionResult ChangeProfile(AdminChangeProfile acp)//Ready
        {
            string username = acp.Username;
            string password= acp.Password;

            SqlConnection con = new SqlConnection(connectionString);
            string query = "UPDATE Admins SET Username=@Username,Password=@Password WHERE Username=@Username2";
            SqlCommand cmd=new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Username",username);
            cmd.Parameters.AddWithValue("@Password",password);
            cmd.Parameters.AddWithValue("@Username2",httpContextAccessor.HttpContext.Session.GetString("Username"));


            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            httpContextAccessor.HttpContext.Session.SetString("Username",username);

            return RedirectToAction("ChangeProfile");
        }

        [HttpGet]
        public IActionResult ManageUsers()
        {
            return View();
        }

        /*[HttpPost]
        public IActionResult ManageUsers()
        {
            return View();
        }*/

        [HttpGet]
        public IActionResult ViewATMBalance()//Ready
        {
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT currAmountOfATM FROM [Transaction] WHERE id= (SELECT MAX (Id) FROM [Transaction])";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt=new DataTable();
            sda.Fill(dt);
            con.Close();

            int balance = Convert.ToInt32(dt.Rows[0][0]);
            ViewData["Balance"] = balance;
            return View();
        }

        [HttpGet]
        public IActionResult ViewTransactionHistory()//Ready
        {
            List<Transaction> lT = new List<Transaction>();
            SqlConnection con=new SqlConnection(connectionString);
            string query = "SELECT * FROM [Transaction]";
            SqlCommand cmd = new SqlCommand(query,con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while(sdr.Read())
            {
                lT.Add(new Transaction()
                {
                    Type = sdr["Type"].ToString(),
                    AccNo = Convert.ToInt32(sdr["AccNo"]),
                    dtOfTransaction = Convert.ToDateTime(sdr["dtOfTransaction"]),
                    Amount = Convert.ToInt32(sdr["Amount"]),
                });
            }
            con.Close();
            ViewData["tData"] = lT;
            return View();
        }

        [HttpGet]
        public IActionResult DepositMoney()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> DepositMoney(AdminDeposit ad)//Ready
        {
            string query = "SELECT currAmountOfATM FROM [Transaction] WHERE id= (SELECT MAX (Id) FROM [Transaction])";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            sda.Fill(ds);
            con.Close();

            int amount = Convert.ToInt32(ds.Rows[0][0]);

            Transaction t = new Transaction();
            t.Type = "Deposit";
            t.AccNo = 0;
            t.dtOfTransaction = DateTime.Now;
            t.Amount = ad.Amount;
            t.currAmountOfATM = (amount + ad.Amount);
            await myAppDbContext.Transaction.AddAsync(t);
            await myAppDbContext.SaveChangesAsync();

            return RedirectToAction("DepositMoney");

            //return View();
        }

        [HttpGet]
        public IActionResult WithdrawMoney()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> WithdrawMoney(AdminDeposit ad)//Ready
        {
            string query = "SELECT currAmountOfATM FROM [Transaction] WHERE id= (SELECT MAX (Id) FROM [Transaction])";
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            sda.Fill(ds);
            con.Close();

            int amount = Convert.ToInt32(ds.Rows[0][0]);

            Transaction t = new Transaction();
            t.Type = "Withdraw";
            t.AccNo = 0;
            t.dtOfTransaction = DateTime.Now;
            t.Amount = ad.Amount;
            t.currAmountOfATM = (amount - ad.Amount);
            await myAppDbContext.Transaction.AddAsync(t);
            await myAppDbContext.SaveChangesAsync();

            return RedirectToAction("WithdrawMoney");

            //return View();
        }

    }
}
