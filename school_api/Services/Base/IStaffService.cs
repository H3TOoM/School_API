using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IStaffService
    {
        Task<IEnumerable<StaffReadDto>> GetAllStaffAsync();
        Task<StaffReadDto> GetStaffByIdAsync(int id);

        Task<StaffReadDto> CreateStaffAsync( StaffCreateDto dto );
        
        Task<StaffReadDto> UpdateStaffAsync(int id , StaffUpdateDto dto );

        Task<bool> DeleteStaffAsync(int id );
    }
}
