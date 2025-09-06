using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IGradeService
    {
        Task<IEnumerable<GradeReadDto>> GetAllGradesAsync();
        Task<GradeReadDto> GetGradeByIdAsync(int id);
        Task<GradeReadDto> CreateGradeAsync(GradeCreateDto dto);
        Task<GradeReadDto> UpdateGradeAsync(int  id, GradeUpdateDto dto);

        Task<bool> DeleteGradeAsync(int id);
    }
}
