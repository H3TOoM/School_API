using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class StaffService : IStaffService
    {
        private readonly IMainRepoistory<Staff> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StaffService( IMainRepoistory<Staff> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }



        public async Task<IEnumerable<StaffReadDto>> GetAllStaffAsync()
        {
            var staffes = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<StaffReadDto>>( staffes );
        }

        public async Task<StaffReadDto> GetStaffByIdAsync( int id )
        {
            var staff = await _mainRepoistory.GetByIdAsync( id );
            return _mapper.Map<StaffReadDto>( staff );
        }



        public async Task<StaffReadDto> CreateStaffAsync( StaffCreateDto dto )
        {
            var staff = new Staff
            {
                Name = dto.Name,
                Position = dto.Position,
                Salary = dto.Salary,
                PhoneNumber = dto.PhoneNumber,
                UserId = dto.UserId,
                DepartmentId = dto.DepartmentId,
            };

            await _mainRepoistory.CreateAsync( staff );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<StaffReadDto>( staff );
        }



        public async Task<StaffReadDto> UpdateStaffAsync( int id, StaffUpdateDto dto )
        {
            var staff = await _mainRepoistory.GetByIdAsync( id );
            if (staff == null)
                throw new KeyNotFoundException( "Staff Not Found!" );


            if (dto.DepartmentId != 0)
                staff.DepartmentId = dto.DepartmentId ?? staff.DepartmentId ;


            staff.Name = dto.Name ?? staff.Name;
            staff.Position = dto.Position ?? staff.Position;
            staff.Salary = dto.Salary ?? staff.Salary;
            staff.PhoneNumber = dto.PhoneNumber ?? staff.PhoneNumber;

            await _mainRepoistory.UpdateAsync( id, staff );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<StaffReadDto>( staff );

        }



        public async Task<bool> DeleteStaffAsync( int id )
        {
            var staff = await _mainRepoistory.GetByIdAsync( id );
            if (staff == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }




    }
}
