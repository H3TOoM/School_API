using System;
using System.Threading.Tasks;

namespace School_API.Repoistory.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepoistory<TEntity> Repository<TEntity>() where TEntity : class;

        Task<int> SaveChangesAsync();
    }
}
