using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class CourseService : ICourseService
    {
        private readonly IMainRepoistory<Course> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CourseService( IMainRepoistory<Course> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       

       

        public async Task<IEnumerable<CourseReadDto>> GetAllCoursesAsync()
        {
            var courses = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<CourseReadDto>>( courses );
        }



        public async Task<CourseReadDto> GetCourseByIdAsync( int id )
        {
            var course = await _mainRepoistory.GetByIdAsync( id );
            return _mapper.Map<CourseReadDto>(course);
        }


        public async Task<CourseReadDto> CreateCourseAsync( CourseCreateDto dto )
        {
            var course = new Course
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                SubjectId = dto.SubjectId,
                TeacherId = dto.TeacherId,
            };

            await _mainRepoistory.CreateAsync( course );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CourseReadDto>( course );
        }



        public async Task<CourseReadDto> UpdateCourseAsync( int id, CourseUpdateDto dto )
        {
            var course = await _mainRepoistory.GetByIdAsync( id );
            if (course == null)
                throw new KeyNotFoundException("Course Not Found!");

            course.Name = dto.Name ?? course.Name;
            course.Description = dto.Description ?? course.Description;
            course.Price = dto.Price ?? course.Price;
            course.SubjectId = dto.SubjectId ?? course.SubjectId;
            course.TeacherId = dto.TeacherId ?? course.TeacherId;


            await _mainRepoistory.UpdateAsync(id , course);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CourseReadDto>( course );
        }

        public async Task<bool> DeleteCourseAsync( int id )
        {
            var course = await _mainRepoistory.GetByIdAsync( id );
            if (course == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }


    }
}
