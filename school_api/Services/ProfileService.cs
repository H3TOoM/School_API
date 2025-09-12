using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IMainRepoistory<User> _userRepository;
        private readonly IMainRepoistory<Student> _studentRepository;
        private readonly IMainRepoistory<Teacher> _teacherRepository;
        private readonly IMainRepoistory<StudentClass> _classRepository;
        private readonly IMainRepoistory<Subject> _subjectRepository;
        private readonly IMainRepoistory<Department> _departmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProfileService(
            IMainRepoistory<User> userRepository,
            IMainRepoistory<Student> studentRepository,
            IMainRepoistory<Teacher> teacherRepository,
            IMainRepoistory<StudentClass> classRepository,
            IMainRepoistory<Subject> subjectRepository,
            IMainRepoistory<Department> departmentRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _studentRepository = studentRepository;
            _teacherRepository = teacherRepository;
            _classRepository = classRepository;
            _subjectRepository = subjectRepository;
            _departmentRepository = departmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserProfileDto?> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return null;

            return _mapper.Map<UserProfileDto>(user);
        }

        public async Task<bool> UpdateUserProfileAsync(int userId, UpdateProfileDto dto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                return false;

            // Check if email is unique (if changed)
            if (!string.IsNullOrEmpty(dto.Email) && dto.Email != user.Email)
            {
                var existingUsers = await _userRepository.GetAllAsync();
                if (existingUsers.Any(u => u.Email == dto.Email && u.Id != userId))
                    return false;
            }

            // Update user
            user.Name = dto.Name;
            if (!string.IsNullOrEmpty(dto.Email))
                user.Email = dto.Email;

            await _userRepository.UpdateAsync(user.Id , user);
            await _unitOfWork.SaveChangesAsync();

            return true;
        }

        public async Task<UserStatsDto?> GetUserStatsAsync()
        {
            try
            {
                var students = await _studentRepository.GetAllAsync();
                var teachers = await _teacherRepository.GetAllAsync();
                var classes = await _classRepository.GetAllAsync();
                var subjects = await _subjectRepository.GetAllAsync();
                var departments = await _departmentRepository.GetAllAsync();

                return new UserStatsDto
                {
                    TotalStudents = students.Count(),
                    TotalTeachers = teachers.Count(),
                    TotalClasses = classes.Count(),
                    TotalSubjects = subjects.Count(),
                    TotalDepartments = departments.Count()
                };
            }
            catch
            {
                return null;
            }
        }
    }
}
