using Microsoft.EntityFrameworkCore;
using school_api.Data;
using school_api.Repoistories.Base;
using System.Xml.Linq;

namespace school_api.Repoistories
{
    public class MainRepoistory<T> : IMainRepoistory<T> where T : class
    {
        // Inject AppDbContext 
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
        public async Task<T> GetByIdAsync( int id ) => await _dbSet.FindAsync( id ) ?? throw new InvalidOperationException("Id not found!");
        



        // Create new entity
        public async Task<T> CreateAsync( T entity )
        {
            await _dbSet.AddAsync( entity );
            return entity;
        }


        // Update entity
        public async Task<T> UpdateAsync( int id , T entity )
        {
            if (id <= 0)
            {
                throw new ArgumentException( "Invalid ID provided." );
            }

            var existingEntity = await GetByIdAsync( id );
            if (existingEntity == null)
            {
                throw new KeyNotFoundException( "Entity not found." );
            }

            _context.Entry( existingEntity ).CurrentValues.SetValues( entity );
            return existingEntity;
        }


        // Delete entity
        public async Task<bool> DeleteAsync( int id )
        {
            var entity = await GetByIdAsync( id );
             _dbSet.Remove( entity );

            return true;
        }

       
       
    }
}
