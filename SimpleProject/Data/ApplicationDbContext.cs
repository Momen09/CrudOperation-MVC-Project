using Microsoft.EntityFrameworkCore;
using SimpleProject.Models;
using SimpleProject.ViewModels.Categories;
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
        public DbSet<ProductImages>  ProductImages { get; set; }
        public DbSet<SimpleProject.ViewModels.Categories.GetCategoryListViewModel> GetCategoryListViewModel { get; set; } = default!;
        public DbSet<SimpleProject.ViewModels.Categories.GetCategoryByIdViewModel> GetCategoryByIdViewModel { get; set; } = default!;
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=.;Database=SimplePrjDb;TrustServerCertificate=True;UID=sa;Pwd=P@ssw0rd;");
        //        base.OnConfiguring(optionsBuilder);
        //}
    }
}
