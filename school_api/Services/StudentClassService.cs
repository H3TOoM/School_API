using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class StudentClassService : IStudentClassService
    {
        private readonly IMainRepoistory<StudentClass> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public StudentClassService( IMainRepoistory<StudentClass> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       

        public async Task<IEnumerable<StudentClassReadDto>> GetAllStudentClassesAsync()
        {
            var studentClasses = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentClassReadDto>>( studentClasses ); 
        }

        public async Task<StudentClassReadDto> GetStudentClassByIdAsync( int id )
        {
            var studentClass = await _mainRepoistory.GetByIdAsync( id );
            return _mapper.Map<StudentClassReadDto>( studentClass );
        }


        public async Task<StudentClassReadDto> CreateStudentClassAsync( StudentClassCreateDto dto )
        {
            var studentClass = new StudentClass
            {
                Name = dto.Name,
                DepartmentId = dto.DepartmentId,
                TeacherId = dto.TeacherId,
            };

            await _mainRepoistory.CreateAsync( studentClass );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<StudentClassReadDto>( studentClass );
        }




        public async Task<StudentClassReadDto> UpdateStudentClassAsync( int id, StudentClassUpdateDto dto )
        {
            var studentClass = await _mainRepoistory.GetByIdAsync( id );
            if (studentClass == null)
                throw new KeyNotFoundException("class not found!");

            if(dto.TeacherId != 0)
                studentClass.TeacherId = dto.TeacherId ?? studentClass.TeacherId;

            if(dto.DepartmentId != 0)
                studentClass.DepartmentId = dto.DepartmentId ?? studentClass.DepartmentId;

            studentClass.Name = dto.Name ?? studentClass.Name;

            await _mainRepoistory.UpdateAsync(id, studentClass);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<StudentClassReadDto>( studentClass );
        }



        public async Task<bool> DeleteStudentClassAsync( int id )
        {
            var studentClass = await _mainRepoistory.GetByIdAsync( id );
            if (studentClass == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }


    }
}
