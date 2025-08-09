using UnitOfWork.Data;
using UnitOfWork.Models;
using UnitOfWork.Repoistory.Base;

namespace UnitOfWork.Repoistory
{
    public class AppUnitOfWork : IUnitOfWork
    {

        private readonly AppDbContext _context;
        public AppUnitOfWork(AppDbContext context)
        {
            _context = context;
            Customers = new MainRepoistory<Customer>(context);
        }
        public IRepoistory<Customer> Customers { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
