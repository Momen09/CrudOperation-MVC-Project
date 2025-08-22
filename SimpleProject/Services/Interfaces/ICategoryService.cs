using SimpleProject.Models;

namespace SimpleProject.Services.Interfaces
{
    public interface ICategoryService
    {
        public Task<List<Category>> GetCategoriesAsync();
        public IQueryable<Category> GetCategoryAsQueryable(string? search);
        public Task<Category?> GetCategoryByIdAsync(int id);
        public Task<Category?> GetCategoryByIdWithouIncludeAsync(int id);
        public Task<string> AddCategoryAsync(Category category);
        public Task<string> UpdateCategoryAsync(Category category);
        public Task<string> DeleteCategoryAsync(Category category);

        public Task<bool> IsCategoryNameArExistAsync(string NameAr);
        public Task<bool> IsCategoryNameArExistExcludeItselfAsync(string NameAr, int Id);
        public Task<bool> IsCategoryNameEnExistExcludeItselfAsync(string NameEn, int Id);
        public Task<bool> IsCategoryNameEnExistAsync(string NameEn);
    }
}
