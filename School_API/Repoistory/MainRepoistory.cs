using Microsoft.EntityFrameworkCore;
using UnitOfWork.Data;
using UnitOfWork.Repoistory.Base;

namespace UnitOfWork.Repoistory
{
    public class MainRepoistory<T> : IRepoistory<T> where T : class
    {

        

        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public MainRepoistory(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }





        // Get All

        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();


        // Get By Id
        public async Task<T> GetByIdAsync(int id) => await _dbSet.FindAsync(id);
       

        // Add
        public async Task Add(T entity)
        {
            await _dbSet.AddAsync(entity);
        }


        // Update
        public async Task UpdateAsync(T entity)
        {
             _dbSet.Update(entity);
        }

        // Delete
        public async Task DeleteByIdAsync(int id)
        {
            var entity =await GetByIdAsync(id);
            if(entity != null)
            {
                 _dbSet.Remove(entity);
            }
        }



    }
}
