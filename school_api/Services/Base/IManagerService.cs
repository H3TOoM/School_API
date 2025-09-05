using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IManagerService
    {
        Task<IEnumerable<ManagerReadDto>> GetAllManagersAsync();
        Task<ManagerReadDto> GetManagerByIdAsync(int id);

        Task<ManagerReadDto> CreateManagerAsync(ManagerCreateDto dto);

        Task<ManagerReadDto> UpdateManagerAsync(int id , ManagerUpdateDto dto);

        Task<bool> DeleteManagerAsync(int id);
    }
}
