using UnitOfWork.Models;

namespace UnitOfWork.Repoistory.Base
{
    public interface IUnitOfWork : IDisposable
    {
        IRepoistory<Customer> Customers { get; }

        Task<int> SaveChangesAsync();
    }
}
