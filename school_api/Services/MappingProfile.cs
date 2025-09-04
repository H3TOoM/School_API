using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;

namespace school_api.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Subject
            CreateMap<Subject, SubjectReadDto>();
            CreateMap<SubjectCreateDto, Subject>();
            CreateMap<SubjectUpdateDto, Subject>();

            // Department
            CreateMap<Department, DepartmentReadDto>()
                .ForMember( dest => dest.Manager, opt => opt.MapFrom( src => new IdNameDto
                {
                    Id = src.Manager.Id,
                    Name = src.Manager.User != null ? src.Manager.User.Name : string.Empty
                } ) );
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();

            // Teacher
            CreateMap<Teacher, TeacherReadDto>()
                .ForMember( dest => dest.Department, opt => opt.MapFrom( src => new IdNameDto { Id = src.Department.Id, Name = src.Department.Name } ) )
                .ForMember( dest => dest.Subject, opt => opt.MapFrom( src => new IdNameDto { Id = src.Subject.Id, Name = src.Subject.Name } ) );
            CreateMap<TeacherCreateDto, Teacher>();
            CreateMap<TeacherUpdateDto, Teacher>();

            // Course
            CreateMap<Course, CourseReadDto>()
                .ForMember( dest => dest.Subject, opt => opt.MapFrom( src => new IdNameDto { Id = src.Subject.Id, Name = src.Subject.Name } ) )
                .ForMember( dest => dest.Teacher, opt => opt.MapFrom( src => src.Teacher == null
                    ? new IdNameOptionalDto { Id = null, Name = null }
                    : new IdNameOptionalDto { Id = src.Teacher.Id, Name = src.Teacher.Name } ) );
            CreateMap<CourseCreateDto, Course>();
            CreateMap<CourseUpdateDto, Course>();

            // User
            CreateMap<User, UserReadDto>();
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();

            // Manager
            CreateMap<Manager, ManagerReadDto>();
            CreateMap<ManagerCreateDto, Manager>();
            CreateMap<ManagerUpdateDto, Manager>();

            // Staff
            CreateMap<Staff, StaffReadDto>()
                .ForMember( dest => dest.Department, opt => opt.MapFrom( src => new IdNameDto { Id = src.Department.Id, Name = src.Department.Name } ) );
            CreateMap<StaffCreateDto, Staff>();
            CreateMap<StaffUpdateDto, Staff>();

            // Parent
            CreateMap<Parent, ParentReadDto>();
            CreateMap<ParentCreateDto, Parent>();
            CreateMap<ParentUpdateDto, Parent>();

            // Student
            CreateMap<Student, StudentReadDto>()
                .ForMember( dest => dest.StudentClass, opt => opt.MapFrom( src => new IdNameDto { Id = src.StudentClass.Id, Name = src.StudentClass.Name } ) );
            CreateMap<StudentCreateDto, Student>();
            CreateMap<StudentUpdateDto, Student>();

            // StudentClass
            CreateMap<StudentClass, StudentClassReadDto>()
                .ForMember( dest => dest.Department, opt => opt.MapFrom( src => new IdNameDto { Id = src.Department.Id, Name = src.Department.Name } ) )
                .ForMember( dest => dest.Teacher, opt => opt.MapFrom( src => src.Teacher == null
                    ? new IdNameOptionalDto { Id = null, Name = null }
                    : new IdNameOptionalDto { Id = src.Teacher.Id, Name = src.Teacher.Name } ) );
            CreateMap<StudentClassCreateDto, StudentClass>();
            CreateMap<StudentClassUpdateDto, StudentClass>();

            // Grade
            CreateMap<Grade, GradeReadDto>();
            CreateMap<GradeCreateDto, Grade>();
            CreateMap<GradeUpdateDto, Grade>();

            // Attendance
            CreateMap<Attendance, AttendanceReadDto>();
            CreateMap<AttendanceCreateDto, Attendance>();
            CreateMap<AttendanceUpdateDto, Attendance>();

            // Schedule
            CreateMap<Schedule, ScheduleReadDto>()
                .ForMember( dest => dest.Course, opt => opt.MapFrom( src => new IdNameDto { Id = src.Course.Id, Name = src.Course.Name } ) )
                .ForMember( dest => dest.Teacher, opt => opt.MapFrom( src => new IdNameDto { Id = src.Teacher.Id, Name = src.Teacher.Name } ) )
                .ForMember( dest => dest.StudentClass, opt => opt.MapFrom( src => new IdNameDto { Id = src.StudentClass.Id, Name = src.StudentClass.Name } ) );
            CreateMap<ScheduleCreateDto, Schedule>();
            CreateMap<ScheduleUpdateDto, Schedule>();
        }
    }
}


