using Microsoft.EntityFrameworkCore.Storage;

namespace SimpleProject.SharedRepository
{
    public interface IGenericRepository<T> where T : class
    {
        public IQueryable<T> GetQueryable();
        public Task<T> GetByIdAsync(int id);

        public Task<List<T>> GetListAsync();

        public Task AddAsync(T entity);
        public Task UpdateAsync(T entity);
        public Task DeleteAsync(T entity);
        public Task AddRangeAsync(IEnumerable<T> entities);
        public Task UpdateRangeAsync(IEnumerable<T> entities);
        public Task DeleteRangeAsync(IEnumerable<T> entities);
        //public Task<bool> AnyAsync(Func<T, bool> predicate);


        public Task<IDbContextTransaction> BeginTransactionAsync();
    }
}
