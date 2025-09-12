using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class StudentService : IStudentService
    {

        // Inject Repos And Unit of work
        private readonly IMainRepoistory<Student> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;

        // Inject AutoMapper
        private readonly IMapper _mapper;
        public StudentService( IMainRepoistory<Student> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }




        public async Task<IEnumerable<StudentReadDto>> GetAllStudentsAsync()
        {
            var students = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<StudentReadDto>>( students );
        }

        public async Task<StudentReadDto> GetStudentByIdAsync( int id )
        {
            var student = await _mainRepoistory.GetByIdAsync( id );
            return _mapper.Map<StudentReadDto>( student );
        }

        public async Task<StudentReadDto> CreateStudentAsync( StudentCreateDto dto )
        {
            var student = new Student
            {
                Name = dto.Name,
                DateOfBirth = dto.DateOfBirth,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                UserId = dto.UserId,
                ParentId = (int)dto.ParentId,
                StudentClassId = dto.StudentClassId,
            };

            await _mainRepoistory.CreateAsync( student );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<StudentReadDto>( student );

        }

        public async Task<StudentReadDto> UpdateStudentAsync( int id, StudentUpdateDto dto )
        {
            var student = await _mainRepoistory.GetByIdAsync( id );

            if (student == null)
                throw new KeyNotFoundException( "Student not found." );

            student.Name = dto.Name ?? student.Name;
            student.Email = dto.Email ?? student.Email;
            student.PhoneNumber = dto.PhoneNumber ?? student.PhoneNumber;
            student.DateOfBirth = dto.DateOfBirth ?? student.DateOfBirth;
            student.ParentId = dto.ParentId ?? student.ParentId;
            student.StudentClassId = dto.StudentClassId ?? student.StudentClassId;


            await _mainRepoistory.UpdateAsync( id, student );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<StudentReadDto>( student );

        }



        public async Task<bool> DeleteStudentAsync( int id )
        {
            var student = await _mainRepoistory.GetByIdAsync( id );
            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }




    }
}
