using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using SimpleProject.Data;
using SimpleProject.Models;
using SimpleProject.Repositories.Interfaces;
using SimpleProject.SharedRepository;

namespace SimpleProject.Repositories.implimintation
{
    public class ProductRepository : GenericRepository<Product> ,   IProductRepository
    {
        private readonly DbSet<Product> _products;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _products = context.Set<Product>();

        }
    }
}
