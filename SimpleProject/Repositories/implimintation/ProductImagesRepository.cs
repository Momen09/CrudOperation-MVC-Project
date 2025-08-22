using Microsoft.EntityFrameworkCore;
using SimpleProject.Data;
using SimpleProject.Models;
using SimpleProject.Repositories.Interfaces;
using SimpleProject.SharedRepository;

namespace SimpleProject.Repositories.implimintation
{
    public class ProductImagesRepository : GenericRepository<ProductImages>, IProductImagesRepository
    {
        private readonly DbSet<ProductImages> _productImages;
        public ProductImagesRepository(ApplicationDbContext context) : base(context)
        {
            _productImages = context.Set<ProductImages>();

        }
    }
}
