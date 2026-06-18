using Microsoft.EntityFrameworkCore;
using WebApp.Models.Entities;
namespace WebApp.Data
{
    public class AppDbContext : DbContext
    {

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }

        public AppDbContext(DbContextOptions options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany(e => e.Employees)
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
        }


        //modelBuilder.Entity<Employee>()
        //        .HasIndex(e => e.DepartmentId);


    }
}
