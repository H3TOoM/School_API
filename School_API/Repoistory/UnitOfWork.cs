using School_API.Data;
using School_API.Repoistory.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitOfWork.Repoistory
{
    public class AppUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        // Store repositories for each entity type to avoid creating them multiple times
        private readonly Dictionary<Type, object> _repositories = new();

        public AppUnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get a generic repository for any entity type.
        /// </summary>
        public IRepoistory<TEntity> Repository<TEntity>() where TEntity : class
        {
            var type = typeof(TEntity);

            // If repository for this entity doesn't exist, create and store it
            if (!_repositories.ContainsKey(type))
            {
                var repoInstance = new IMainRepoistory<TEntity>(_context);
                _repositories[type] = repoInstance;
            }

            return (IRepoistory<TEntity>)_repositories[type];
        }

        /// <summary>
        /// Save all pending changes to the database asynchronously.
        /// </summary>
        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose the database context and release resources.
        /// </summary>
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
