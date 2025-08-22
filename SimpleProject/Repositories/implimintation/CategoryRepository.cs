using Microsoft.EntityFrameworkCore;
using SimpleProject.Data;
using SimpleProject.Models;
using SimpleProject.Repositories.Interfaces;
using SimpleProject.SharedRepository;

namespace SimpleProject.Repositories.implimintation
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly DbSet<Category> _category;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _category = context.Set<Category>();

        }
    }
}
