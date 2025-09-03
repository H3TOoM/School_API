namespace school_api.Repoistories.Base
{
    public interface IUnitOfWork : IDisposable
    {
        // Commit changes to the database
        Task<int> SaveChangesAsync();

        // Repositories for different entities
        IMainRepoistory<T> GetRepository<T>() where T : class;
    }
}
