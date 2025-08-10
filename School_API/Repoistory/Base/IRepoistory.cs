using School_API.Models;

namespace School_API.Repoistory.Base
{
    public interface IRepoistory<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(int id);

        Task Add(T entity);

        Task UpdateAsync(T entity);

        Task DeleteByIdAsync(int id);



    }
}
