using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class TeacherService : ITeacherService
    {
        // Inject Repoistory & UOF
        private readonly IMainRepoistory<Teacher> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;

        // Inject AutoMapper
        private readonly IMapper _mapper;
        public TeacherService( IMainRepoistory<Teacher> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeacherReadDto>> GetAllTeachersAsync()
        {
            var teachers = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<TeacherReadDto>>( teachers );
        }


        public async Task<TeacherReadDto> GetTeacherByIdAsync( int id )
        {
            var teacher = await _mainRepoistory.GetByIdAsync( id );
            if (teacher == null)
                throw new KeyNotFoundException( "Teacher Not Found!" );

            return _mapper.Map<TeacherReadDto>( teacher );
        }

        public async Task<TeacherReadDto> CreateTeacherAsync( TeacherCreateDto dto )
        {
            if (dto == null)
                throw new InvalidDataException( "Complete data!" );

            var teacher = new Teacher
            {
                Name = dto.Name,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                UserId = dto.UserId,
                DepartmentId = dto.DepartmentId,
                SubjectId = dto.SubjectId,
            };


            await _mainRepoistory.CreateAsync( teacher );
            await _unitOfWork.SaveChangesAsync();


            return _mapper.Map<TeacherReadDto>( teacher );

        }



        public async Task<TeacherReadDto> UpdateTeacherAsync( int id, TeacherUpdateDto dto )
        {
            if (dto == null)
                throw new InvalidDataException( "Complete all Data!" );

            var teacher = await _mainRepoistory.GetByIdAsync( id );
            if (teacher == null)
                throw new KeyNotFoundException( "Teacher Not Found!" );

            teacher.Name = dto.Name ?? teacher.Name;
            teacher.Email = dto.Email ?? teacher.Email;
            teacher.PhoneNumber = dto.PhoneNumber ?? teacher.PhoneNumber;
            teacher.DepartmentId = dto.DepartmentId ?? teacher.DepartmentId;
            teacher.SubjectId = dto.SubjectId ?? teacher.SubjectId;

            await _mainRepoistory.UpdateAsync( id, teacher );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<TeacherReadDto>( teacher );
        }



        public async Task<bool> DeleteTeacherAsync( int id )
        {
            var teacher = await _mainRepoistory.GetByIdAsync( id );
            if (teacher == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }



    }
}
