using SimpleProject.Models;
using SimpleProject.ViewModels;

namespace SimpleProject.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetProducts();
        public Task<Product?> GetProductById(int id);
        public Task<string> AddProduct(Product product , List<IFormFile>? files);
        public Task<string> UpdateProduct(Product product);
        public Task<string> DeleteProduct(Product product);

        public Task<bool> IsProductNameExistAsync(string ProductName);
    }
}
