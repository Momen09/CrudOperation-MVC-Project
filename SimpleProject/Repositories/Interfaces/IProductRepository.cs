using Microsoft.EntityFrameworkCore.Storage;
using SimpleProject.Models;
using SimpleProject.SharedRepository;

namespace SimpleProject.Repositories.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        
    }
}
