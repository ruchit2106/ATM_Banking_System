using Microsoft.EntityFrameworkCore;
using ATM_Banking_System.Models;

namespace ATM_Banking_System.Models
{
    public class MyAppDbContext:DbContext
    {
        public MyAppDbContext(DbContextOptions<MyAppDbContext> options):base(options)
        {

        }

        public DbSet<Users> Users { get; set; }
        public DbSet<Admins> Admins { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<ATM_Banking_System.Models.UserViewModel> UserViewModel { get; set; }

    }
}
