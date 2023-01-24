using BankAccountsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAccountsAPI.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}
