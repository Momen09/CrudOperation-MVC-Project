using SimpleProject.Models;
using SimpleProject.ViewModels;

namespace SimpleProject.Services.Interfaces
{
    public interface IProductService
    {
        public Task<List<Product>> GetProducts();
        public IQueryable<Product> GetProductsAsQueryable(string? search);
        public Task<Product?> GetProductByIdAsync(int id);
        public Task<Product?> GetProductByIdWithouIncludeAsync(int id);
        public Task<string> AddProduct(Product product , List<IFormFile>? files);
        public Task<string> UpdateProduct(Product product, List<IFormFile>? files);
        public Task<string> DeleteProduct(Product product);

        public Task<bool> IsProductNameArExistAsync(string NameAr);
        public Task<bool> IsProductNameArExistExcludeItselfAsync(string NameAr,int Id);
        public Task<bool> IsProductNameEnExistAsync(string NameEn);
    }
}
