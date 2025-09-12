using AutoMapper;
using Microsoft.EntityFrameworkCore;
using school_api.Data;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class SearchService : ISearchService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public SearchService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PagedResultDto<StudentReadDto>> SearchStudentsAsync(StudentSearchFilterDto filter)
        {
            var query = _context.Students
                .Include(s => s.StudentClass)
                .Include(s => s.Parent)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(s => s.Name.Contains(filter.SearchTerm) ||
                                       s.Email.Contains(filter.SearchTerm));
            }

            if (filter.DepartmentId.HasValue)
            {
                query = query.Where(s => s.StudentClass.DepartmentId == filter.DepartmentId.Value);
            }

            if (filter.StudentClassId.HasValue)
            {
                query = query.Where(s => s.StudentClassId == filter.StudentClassId.Value);
            }

            if (filter.ParentId.HasValue)
            {
                query = query.Where(s => s.ParentId == filter.ParentId.Value);
            }

            if (filter.DateOfBirthFrom.HasValue)
            {
                query = query.Where(s => s.DateOfBirth >= filter.DateOfBirthFrom.Value);
            }

            if (filter.DateOfBirthTo.HasValue)
            {
                query = query.Where(s => s.DateOfBirth <= filter.DateOfBirthTo.Value);
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "name":
                        query = filter.SortDescending ? query.OrderByDescending(s => s.Name) : query.OrderBy(s => s.Name);
                        break;
                    case "dateofbirth":
                        query = filter.SortDescending ? query.OrderByDescending(s => s.DateOfBirth) : query.OrderBy(s => s.DateOfBirth);
                        break;
                    default:
                        query = query.OrderBy(s => s.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(s => s.Name);
            }

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply pagination
            var students = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var studentDtos = _mapper.Map<List<StudentReadDto>>(students);

            return new PagedResultDto<StudentReadDto>
            {
                Data = studentDtos,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        public async Task<PagedResultDto<TeacherReadDto>> SearchTeachersAsync(TeacherSearchFilterDto filter)
        {
            var query = _context.Teachers
                .Include(t => t.Department)
                .Include(t => t.Subject)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(t => t.Name.Contains(filter.SearchTerm) ||
                                       t.Email.Contains(filter.SearchTerm));
            }

            if (filter.DepartmentId.HasValue)
            {
                query = query.Where(t => t.DepartmentId == filter.DepartmentId.Value);
            }

            if (filter.SubjectId.HasValue)
            {
                query = query.Where(t => t.SubjectId == filter.SubjectId.Value);
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "name":
                        query = filter.SortDescending ? query.OrderByDescending(t => t.Name) : query.OrderBy(t => t.Name);
                        break;
                    default:
                        query = query.OrderBy(t => t.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(t => t.Name);
            }

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply pagination
            var teachers = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var teacherDtos = _mapper.Map<List<TeacherReadDto>>(teachers);

            return new PagedResultDto<TeacherReadDto>
            {
                Data = teacherDtos,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        public async Task<PagedResultDto<CourseReadDto>> SearchCoursesAsync(CourseSearchFilterDto filter)
        {
            var query = _context.Courses
                .Include(c => c.Subject)
                .Include(c => c.Teacher)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(c => c.Name.Contains(filter.SearchTerm) ||
                                       c.Description.Contains(filter.SearchTerm));
            }

            if (filter.SubjectId.HasValue)
            {
                query = query.Where(c => c.SubjectId == filter.SubjectId.Value);
            }

            if (filter.TeacherId.HasValue)
            {
                query = query.Where(c => c.TeacherId == filter.TeacherId.Value);
            }

            // Apply sorting
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "name":
                        query = filter.SortDescending ? query.OrderByDescending(c => c.Name) : query.OrderBy(c => c.Name);
                        break;
                    default:
                        query = query.OrderBy(c => c.Name);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(c => c.Name);
            }

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply pagination
            var courses = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var courseDtos = _mapper.Map<List<CourseReadDto>>(courses);

            return new PagedResultDto<CourseReadDto>
            {
                Data = courseDtos,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }

        public async Task<PagedResultDto<AttendanceReadDto>> SearchAttendancesAsync(AttendanceSearchFilterDto filter)
        {
            var query = _context.Attendances
                .Include(a => a.Student)
                .Include(a => a.Schedule)
                .AsQueryable();

            // Apply filters
            if (filter.StudentId.HasValue)
            {
                query = query.Where(a => a.StudentId == filter.StudentId.Value);
            }

            if (filter.ScheduleId.HasValue)
            {
                query = query.Where(a => a.ScheduleId == filter.ScheduleId.Value);
            }

            if (filter.DateFrom.HasValue)
            {
                query = query.Where(a => a.Date >= filter.DateFrom.Value);
            }

            if (filter.DateTo.HasValue)
            {
                query = query.Where(a => a.Date <= filter.DateTo.Value);
            }

           

            // Apply sorting
            if (!string.IsNullOrEmpty(filter.SortBy))
            {
                switch (filter.SortBy.ToLower())
                {
                    case "date":
                        query = filter.SortDescending ? query.OrderByDescending(a => a.Date) : query.OrderBy(a => a.Date);
                        break;
                    
                    default:
                        query = query.OrderByDescending(a => a.Date);
                        break;
                }
            }
            else
            {
                query = query.OrderByDescending(a => a.Date);
            }

            // Get total count
            var totalCount = await query.CountAsync();

            // Apply pagination
            var attendances = await query
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var attendanceDtos = _mapper.Map<List<AttendanceReadDto>>(attendances);

            return new PagedResultDto<AttendanceReadDto>
            {
                Data = attendanceDtos,
                TotalCount = totalCount,
                Page = filter.Page,
                PageSize = filter.PageSize
            };
        }
    }
}
