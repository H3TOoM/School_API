using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class GradeService : IGradeService
    {
        private readonly IMainRepoistory<Grade> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GradeService( IMainRepoistory<Grade> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       

        public async Task<IEnumerable<GradeReadDto>> GetAllGradesAsync()
        {
            var grades = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<GradeReadDto>>( grades );
        }



        public async Task<GradeReadDto> GetGradeByIdAsync( int id )
        {
            var grade = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<GradeReadDto>( grade );
        }


        public async Task<GradeReadDto> CreateGradeAsync( GradeCreateDto dto )
        {
            var grade = new Grade
            {
                Score = dto.Score,
                StudentId = dto.StudentId,
                CourserId = dto.CourserId
            };

            await _mainRepoistory.CreateAsync( grade );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GradeReadDto>( grade );
        }



        public async Task<GradeReadDto> UpdateGradeAsync( int id, GradeUpdateDto dto )
        {
            var grade = await _mainRepoistory.GetByIdAsync( id );
            if (grade == null)
                throw new KeyNotFoundException("Grade Not Found!");

            grade.Score = dto.Score ?? grade.Score;

            await _mainRepoistory.UpdateAsync( id, grade );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<GradeReadDto>(grade);
        }


        public async Task<bool> DeleteGradeAsync( int id )
        {
            var grade = await _mainRepoistory.GetByIdAsync( id );
            if (grade == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }



    }
}
