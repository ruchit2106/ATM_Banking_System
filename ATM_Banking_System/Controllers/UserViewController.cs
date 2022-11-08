using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ATM_Banking_System.Models;
using Microsoft.Data.SqlClient;

namespace ATM_Banking_System.Controllers
{
    public class UserViewController : Controller
    {
        private readonly MyAppDbContext _context;
        private readonly IConfiguration configuration;
        public readonly IHttpContextAccessor httpContextAccessor;
        public string connectionString;

        public UserViewController(MyAppDbContext context, IConfiguration iconfig, IHttpContextAccessor contxtAccessor)
        {
            _context = context;
            configuration = iconfig;
            httpContextAccessor = contxtAccessor;
            connectionString = configuration.GetConnectionString("MyAppConnString");
        }

        // GET: UserView
        public IActionResult Index()
        {
            List<UserViewModel> lU = new List<UserViewModel>();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM Users";
            SqlCommand cmd = new SqlCommand(query, con);
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            while (sdr.Read())
            {
                lU.Add(new UserViewModel()
                {
                    Id = Convert.ToInt32(sdr["Id"]),
                    AccNo = Convert.ToInt32(sdr["AccNo"]),
                    FullName = sdr["FullName"].ToString(),
                    Gender = Convert.ToChar(sdr["Gender"]),
                    DOB = Convert.ToDateTime(sdr["DOB"]),
                    Address = sdr["Address"].ToString(),
                    PhoneNo = sdr["PhoneNo"].ToString(),
                });
            }
            con.Close();

            ViewData["data"] = lU;
            return View();
        }

        // GET: UserView/Edit/5
        [HttpGet]
        public IActionResult Edit(int? id)//Ready
        {
            UserViewModel userViewModel = new UserViewModel();
            if(id>0)
            {
                userViewModel = FetchUserViewModelById(id);
            }
            return View(userViewModel);
        }

        [NonAction]
        private UserViewModel FetchUserViewModelById(int? id)
        {
            UserViewModel uVM = new UserViewModel();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM Users WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Id",id);
            con.Open();
            SqlDataReader sdr=cmd.ExecuteReader();
            while(sdr.Read())
            {
                uVM.Id = Convert.ToInt32(sdr["Id"]);
                uVM.AccNo = Convert.ToInt32(sdr["AccNo"]);
                uVM.FullName = sdr["FullName"].ToString();
                uVM.Gender = Convert.ToChar(sdr["Gender"]);
                uVM.DOB = Convert.ToDateTime(sdr["DOB"]);
                uVM.Address = sdr["Address"].ToString();
                uVM.PhoneNo = sdr["PhoneNo"].ToString();
            }
            con.Close();
            return uVM;
        }

        // POST: UserView/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,AccNo,FullName,Gender,DOB,Address,PhoneNo")] UserViewModel userViewModel)//Ready
        {
            if (id != userViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                SqlConnection con = new SqlConnection(connectionString);
                string query = "UPDATE Users SET AccNo=@AccNo,FullName=@FullName,Gender=@Gender,DOB=@DOB,Address=@Address,PhoneNo=@PhoneNo WHERE Id=@Id";
                SqlCommand cmd=new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@AccNo",userViewModel.AccNo);
                cmd.Parameters.AddWithValue("@FullName",userViewModel.FullName);
                cmd.Parameters.AddWithValue("@Gender",userViewModel.Gender);
                cmd.Parameters.AddWithValue("@DOB",userViewModel.DOB);
                cmd.Parameters.AddWithValue("@Address",userViewModel.Address);
                cmd.Parameters.AddWithValue("@PhoneNo",userViewModel.PhoneNo);
                cmd.Parameters.AddWithValue("@Id",userViewModel.Id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: UserView/Delete/5
        public IActionResult Delete(int? id)//Ready
        {
            UserViewModel uVM=new UserViewModel();
            SqlConnection con = new SqlConnection(connectionString);
            string query = "SELECT * FROM Users WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@Id",id);
            con.Open();
            SqlDataReader sdr=cmd.ExecuteReader();
            while(sdr.Read())
            {
                uVM.Id = (int)id;
                uVM.AccNo = Convert.ToInt32(sdr["AccNo"]);
                uVM.FullName = sdr["FullName"].ToString();
                uVM.Gender = Convert.ToChar(sdr["Gender"]);
                uVM.DOB = Convert.ToDateTime(sdr["DOB"]);
                uVM.Address = sdr["Address"].ToString();
                uVM.PhoneNo = sdr["PhoneNo"].ToString();
            }
            //ViewData["data"] = uVM;
            return View(uVM);
        }

        [HttpPost,ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)//Ready
        {
            SqlConnection con=new SqlConnection(connectionString);
            string query = "DELETE FROM Users WHERE Id=@Id";
            SqlCommand cmd = new SqlCommand(query,con);
            cmd.Parameters.AddWithValue("@Id", id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            return RedirectToAction("ManageUsers","Admins");
        }
    }
}
