namespace school_api.Repoistories.Base
{
    public interface IMainRepoistory<T> : IReadOnlyRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(int id , T entity);
        Task<bool> DeleteAsync(int id);
    }
}
