using Microsoft.EntityFrameworkCore;

namespace ShedulerApp.Models
{
    public class CustomDbContext : DbContext
    {
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountStats> AccountStats { get; set; }
        public CustomDbContext(DbContextOptions<CustomDbContext> options) : base(options) { }
       
    }
}
