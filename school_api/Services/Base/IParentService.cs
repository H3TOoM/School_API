using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IParentService
    {
        Task<IEnumerable<ParentReadDto>> GetAllParentsAsync();
        Task<ParentReadDto> GetParentByIdAsync(int id);

        Task<ParentReadDto> CreateParentAsync(ParentCreateDto dto);

        Task<ParentReadDto> UpdateParentAsync(int id , ParentUpdateDto dto);

        Task<bool> DeleteParentAsync(int id);
    }
}
