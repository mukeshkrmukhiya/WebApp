using Microsoft.EntityFrameworkCore;
using WebApp.Models.Entities;
namespace WebApp.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        





    }
}
