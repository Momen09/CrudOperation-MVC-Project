using Microsoft.EntityFrameworkCore.Storage;
using SimpleProject.SharedRepository;

namespace SimpleProject.UnitOfWorks
{
    public interface IUnitOfWork :IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
        public Task<IDbContextTransaction> BeginTransactionAsync();
        public Task CommitTransactionAsync();
        public Task RollbackTransactionAsync();
        public Task<int> CompleteAsync();
    }
}
