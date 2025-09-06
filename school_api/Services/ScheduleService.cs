using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IMainRepoistory<Schedule> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ScheduleService( IMainRepoistory<Schedule> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       

       

        public async Task<IEnumerable<ScheduleReadDto>> GetAllSchedulesAsync()
        {
            var schedules = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<ScheduleReadDto>>( schedules );  
        }



        public async Task<ScheduleReadDto> GetSchedulesByIdAsync( int id )
        {
            var schedule = await _mainRepoistory.GetByIdAsync(id);
            return _mapper.Map<ScheduleReadDto>(schedule);
        }


        public async Task<ScheduleReadDto> CreateScheduleAsync( ScheduleCreateDto dto )
        {
            var schedule = new Schedule
            {
                Day = dto.Day,
                StartTime = dto.StartTime,
                EndTime = dto.EndTime,
                CourseId = dto.CourseId,
                TeacherId = dto.TeacherId,
                StudentClassId = dto.StudentClassId,
            };

            await _mainRepoistory.CreateAsync( schedule );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ScheduleReadDto>( schedule );
        }



        public async Task<ScheduleReadDto> UpdateScheduleAsync(int id ,  ScheduleUpdateDto dto )
        {
            var schedule = await _mainRepoistory.GetByIdAsync( id );
            if (schedule == null)
                throw new KeyNotFoundException("Schedule Not Found!");

            schedule.Day = dto.Day ?? schedule.Day;
            schedule.StartTime = dto.StartTime ?? schedule.StartTime;
            schedule.EndTime = dto.EndTime ?? schedule.EndTime;
            schedule.CourseId = dto.CourseId ?? schedule.CourseId;
            schedule.TeacherId = dto.TeacherId ?? schedule.TeacherId;
            schedule.StudentClassId = dto.StudentClassId ?? schedule.StudentClassId;

            await _mainRepoistory.UpdateAsync(id, schedule);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ScheduleReadDto>( schedule );

        }


        public async Task<bool> DeleteScheduleAsync( int id )
        {
            var schedule = await _mainRepoistory.GetByIdAsync( id );
            if (schedule == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }




    }
}
