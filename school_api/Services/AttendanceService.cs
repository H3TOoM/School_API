using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class AttendanceService : IAttendanceService
    {
        private readonly IMainRepoistory<Attendance> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttendanceService( IMainRepoistory<Attendance> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }




        public async Task<IEnumerable<AttendanceReadDto>> GetAllAttendancesAsync()
        {
            var attendances = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<AttendanceReadDto>>( attendances );
        }

        public async Task<AttendanceReadDto> GetAttendanceByIdAsync( int id )
        {
            var attendance = await _mainRepoistory.GetByIdAsync( id );
            return _mapper.Map<AttendanceReadDto>( attendance );
        }


        public async Task<AttendanceReadDto> CreateAttendance( AttendanceCreateDto dto )
        {

            var attendance = new Attendance
            {
                IsPresent = dto.IsPresent,
                StudentId = dto.StudentId,
                ScheduleId = dto.ScheduleId,
                Date = dto.Date,
            };

            await _mainRepoistory.CreateAsync( attendance );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AttendanceReadDto>( attendance );
        }



        public async Task<AttendanceReadDto> UpdateAttendanceAsync( int id, AttendanceUpdateDto dto )
        {
            var attendance = await _mainRepoistory.GetByIdAsync( id );
            if (attendance == null)
                throw new KeyNotFoundException( "Attendance Not Found!" );

            attendance.IsPresent = dto.IsPresent ?? attendance.IsPresent;
            attendance.Date = dto.Date ?? attendance.Date;

            await _mainRepoistory.UpdateAsync( id, attendance );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<AttendanceReadDto>( attendance );
        }


        public async Task<bool> DeleteAttendanceAsync( int id )
        {
            var attendance = await _mainRepoistory.GetByIdAsync( id );
            if (id == 0)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }




    }
}
