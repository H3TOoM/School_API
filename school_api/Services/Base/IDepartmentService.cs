using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IDepartmentService
    {
        Task<IEnumerable<DepartmentReadDto>> GetAllDepartmentsAsync();
        Task<DepartmentReadDto> GetDepartmentByIdAsync(int id);

        Task<DepartmentReadDto> CreateDepartmentAsync(DepartmentCreateDto dto);

        Task<DepartmentReadDto> UpdateDepartmentAsync(int id , DepartmentUpdateDto dto);

        Task<bool> DeleteDepartmentAsync(int id);
    }
}
