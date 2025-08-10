using School_API.DTOs;
using School_API.Models;

namespace School_API.Services.Base
{
    public interface IManagerService
    {
        Task<IEnumerable<Manager>> GetAllManagersAsync();
        Task<Manager> GetManagerByIdAsync(int id);

        Task<Manager> CreateManagerAsync(ManagerDto dto);

        Task<Manager> UpdateManagerAsync(int id , Manager manager);

        Task<string> DeleteManagerAsync(int id);
    }
}
