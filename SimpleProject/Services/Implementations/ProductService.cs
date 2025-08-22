using Microsoft.EntityFrameworkCore;
using SimpleProject.Data;
using SimpleProject.Models;
using SimpleProject.Repositories.Interfaces;
using SimpleProject.Services.Interfaces;
using SimpleProject.UnitOfWorks;

namespace SimpleProject.Services.Implementations
{
    public class ProductService : IProductService
    {
        #region fields
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileService _fileService;
        #endregion


        #region constructor
        public ProductService(IFileService fileService
            , IUnitOfWork unitOfWork
            )
        {
            _fileService = fileService;
            _unitOfWork = unitOfWork;
        }
        #endregion


        #region implementation functions
        public async Task<string> AddProduct(Product product, List<IFormFile> files)
        {
            var pathList = new List<string>();
            var trans = await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _unitOfWork.Repository<Product>().AddAsync(product);
                
                var result = await AddProductImages(files, product.Id);
                if (result.Item1 == null && result.Item2 !="Success")
                {
                    return result.Item2;
                }
                pathList = result.Item1;

                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                foreach (var path in pathList)
                {
                    _fileService.DeletePhysicalFile(path);
                }
                return ex.Message + "--" + ex.InnerException;
            }
        }


        public async Task<string> DeleteProduct(Product product)
        {
            try
            {

                //string path = product.Path;
                await _unitOfWork.Repository<Product>().DeleteAsync(product);
                
                //_fileService.DeletePhysicalFile(path);
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message + "--" + ex.InnerException;
            }


        }

        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Product>().GetQueryable().Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Product?> GetProductByIdWithouIncludeAsync(int id)
        {
            return await _unitOfWork.Repository<Product>().GetByIdAsync(id);
        }

        public async Task<List<Product>> GetProducts()
        {
            return await _unitOfWork.Repository<Product>().GetListAsync();
        }

        public IQueryable<Product> GetProductsAsQueryable(string? search)
        {
            var products = _unitOfWork.Repository<Product>().GetQueryable();
            if (!string.IsNullOrEmpty(search)) 
            {
                products = products.Where(x => x.NameAr.Contains(search) || x.NameEn.Contains(search));
            }
            return products;
        }

        public async Task<bool> IsProductNameArExistAsync(string NameAr)
        {
            return await _unitOfWork.Repository<Product>().GetQueryable().AnyAsync(x => x.NameAr == NameAr);
        }

        public async Task<bool> IsProductNameArExistExcludeItselfAsync(string NameAr, int Id)
        {
            return await _unitOfWork.Repository<Product>().GetQueryable().AnyAsync(x => x.NameAr == NameAr && x.Id!=Id);
        }

        public async Task<bool> IsProductNameEnExistAsync(string NameEn)
        {
            return await _unitOfWork.Repository<Product>().GetQueryable().AnyAsync(x => x.NameEn == NameEn);
        }
        public async Task<string> UpdateProduct(Product product, List<IFormFile>? files)
        {

            var pathList = new List<string>();
            var trans = await _unitOfWork.BeginTransactionAsync();
            try
            {

                await _unitOfWork.Repository<Product>().UpdateAsync(product);

                if (files != null && files.Count() > 0)
                {
                    var productImages = await _unitOfWork.Repository<ProductImages>().GetQueryable().Where(x => x.ProductId == product.Id).ToListAsync();
                    if (productImages.Count() > 0)
                    {
                        var pathes = productImages.Select(x => x.Path).ToList();
                        await _unitOfWork.Repository<ProductImages>().DeleteRangeAsync(productImages);
                        foreach (var image in pathes)
                        {
                            _fileService.DeletePhysicalFile(image);
                        }
                    }
                    var result = await AddProductImages(files, product.Id);
                    if (result.Item1 == null && result.Item2 != "Success")
                    {
                        return result.Item2;
                    }
                    pathList = result.Item1;
                }
                    await trans.CommitAsync();
                return "Success";
            }
            catch (Exception ex)
            {
                await trans.RollbackAsync();
                foreach (var path in pathList)
                {
                    _fileService.DeletePhysicalFile(path);
                }
                return ex.Message + "--" + ex.InnerException;
            }
        }
        private async Task<(List<string>,string)> AddProductImages(List<IFormFile> files, int productId)
        {
            var pathList = new List<string>();

            if (files != null && files.Count() > 0)
            {
                foreach (var file in files)
                {
                    var path = await _fileService.Upload(file, "/Images/");
                    if (!path.StartsWith("/Images/"))
                    {
                        return (null, path);
                    }
                    pathList.Add(path);
                }



                var productImages = new List<ProductImages>();
                foreach (var file in pathList)
                {
                    var productImage = new ProductImages();
                    productImage.ProductId = productId;
                    productImage.Path = file;
                    productImages.Add(productImage);

                }
                await _unitOfWork.Repository<ProductImages>().AddRangeAsync(productImages);
                    
            }
            return (pathList,"Success");
        }
        #endregion  
    }
}
