using Microsoft.EntityFrameworkCore;
using SimpleProject.Models;
namespace SimpleProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
         
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {

        }

        public DbSet<Product> Product { get; set; }
        public DbSet<Category> Category { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=SimplePrjDb;TrustServerCertificate=True;UID=sa;Pwd=P@ssw0rd;");
        //        base.OnConfiguring(optionsBuilder);
        //}
    }
}
