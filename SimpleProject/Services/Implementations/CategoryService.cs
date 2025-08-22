using Microsoft.EntityFrameworkCore;
using SimpleProject.Data;
using SimpleProject.Models;
using SimpleProject.Repositories.Interfaces;
using SimpleProject.Services.Interfaces;
using SimpleProject.UnitOfWorks;

namespace SimpleProject.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(ApplicationDbContext context,IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> AddCategoryAsync(Category category)
        {
            try
            {
                await _unitOfWork.Repository<Category>().AddAsync(category);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message + "--" + ex.InnerException;
            }
        }

        public async Task<string> DeleteCategoryAsync(Category category)
        {
            try
            {
                await _unitOfWork.Repository<Category>().DeleteAsync(category);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message + "--" + ex.InnerException;
            }
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
           return await _unitOfWork.Repository<Category>().GetListAsync();
        }

        public IQueryable<Category> GetCategoryAsQueryable(string? search)
        {
            return _unitOfWork.Repository<Category>().GetQueryable();
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _unitOfWork.Repository<Category>().GetQueryable().FirstOrDefaultAsync(X => X.Id == id);
        }

        public async Task<Category?> GetCategoryByIdWithouIncludeAsync(int id)
        {
            return await _unitOfWork.Repository<Category>().GetByIdAsync(id);
        }

        public async Task<bool> IsCategoryNameArExistAsync(string NameAr)
        {
            return await _unitOfWork.Repository<Category>().GetQueryable()
                .AnyAsync(c => c.NameAr == NameAr);
        }

        public async Task<bool> IsCategoryNameArExistExcludeItselfAsync(string NameAr, int Id)
        {
            return await _unitOfWork.Repository<Category>().GetQueryable()
                .AnyAsync(c => c.NameAr == NameAr && c.Id != Id);
        }

        public async Task<bool> IsCategoryNameEnExistAsync(string NameEn)
        {
            return await _unitOfWork.Repository<Category>().GetQueryable()
                .AnyAsync(c => c.NameEn == NameEn);
        }

        public async Task<bool> IsCategoryNameEnExistExcludeItselfAsync(string NameEn, int Id)
        {
            return await _unitOfWork.Repository<Category>().GetQueryable()
                .AnyAsync(c => c.NameEn == NameEn && c.Id != Id);
        }

        public async Task<string> UpdateCategoryAsync(Category category)
        {
            try 
            {
            await _unitOfWork.Repository<Category>().UpdateAsync(category);
                return "Success";
            }
            catch (Exception ex)
            {
             return ex.Message+"--"+ex.InnerException;
            }
        }
    }
}
