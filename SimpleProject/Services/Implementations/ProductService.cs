using Microsoft.EntityFrameworkCore;
using SimpleProject.Data;
using SimpleProject.Models;
using SimpleProject.Services.Interfaces;

namespace SimpleProject.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region fields
        private readonly IFileService _fileService;
        private readonly ApplicationDbContext _context;
        #endregion


        #region constructor
        public ProductService(IFileService fileService, ApplicationDbContext context)
        {
            _fileService = fileService;
            _context = context;
        }
        #endregion


        #region implementation functions
        public async Task<string> AddProduct(Product product, List<IFormFile> files)
        {
            var pathList = new List<string>();
            var trans = await _context.Database.BeginTransactionAsync();
            try
            {
                await _context.Product.AddAsync(product);
                await _context.SaveChangesAsync();
                if (files != null && files.Count() > 0)
                {
                    foreach (var file in files)
                    {
                        var path = await _fileService.Upload(file, "/Images/");
                        if (!path.StartsWith("/Images/"))
                        {
                            return path;
                        }
                        pathList.Add(path);
                    }



                    var productImages = new List<ProductImages>();
                    foreach (var file in pathList)
                    {
                        var productImage = new ProductImages();
                        productImage.ProductId = product.Id;
                        productImage.Path = file;
                        productImages.Add(productImage);

                    }
                    _context.ProductImages.AddRange(productImages);
                    await _context.SaveChangesAsync();
                    await trans.CommitAsync();
                }
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                foreach (var file in pathList)
                {
                    _fileService.DeletePhysicalFile(file);
                } 
                return ex.Message + "--" + ex.InnerException;
            }
        }


        public async Task<string> DeleteProduct(Product product)
        {
            try
            {

                //string path = product.Path;
                _context.Product.Remove(product);
                await _context.SaveChangesAsync();
                //_fileService.DeletePhysicalFile(path);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message + "--" + ex.InnerException;
            }


        }

        public async Task<Product?> GetProductById(int id)
        {
            return await _context.Product.FindAsync(id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _context.Product.ToListAsync();
        }

        public async Task<bool> IsProductNameExistAsync(string ProductName)
        {
            return await _context.Product.AnyAsync(x => x.Name == ProductName);
        }

        public async Task<string> UpdateProduct(Product product)
        {
            try
            {

                _context.Product.Update(product);
                await _context.SaveChangesAsync();
                return "success";

            }
            catch (Exception ex)
            {
                return ex.Message + "--" + ex.InnerException;
            }

        }
        #endregion  
    }
}
