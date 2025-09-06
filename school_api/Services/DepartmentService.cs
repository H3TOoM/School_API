using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IMainRepoistory<Department> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepartmentService( IMainRepoistory<Department> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<DepartmentReadDto>> GetAllDepartmentsAsync()
        {
            var departments = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<DepartmentReadDto>>( departments );
        }

        public async Task<DepartmentReadDto> GetDepartmentByIdAsync( int id )
        {
            var department = await _mainRepoistory.GetByIdAsync( id );
            if (department == null)
                throw new KeyNotFoundException( "Department Not Found!" );

            return _mapper.Map<DepartmentReadDto>( department );

        }

        public async Task<DepartmentReadDto> CreateDepartmentAsync( DepartmentCreateDto dto )
        {
            if (dto == null)
                throw new InvalidDataException( "Complete All Data!" );

            var department = new Department
            {
                Name = dto.Name,
                ManagerId = dto.ManagerId,
            };

            await _mainRepoistory.CreateAsync( department );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DepartmentReadDto>( department );

        }

        public async Task<DepartmentReadDto> UpdateDepartmentAsync( int id, DepartmentUpdateDto dto )
        {

            var department = await _mainRepoistory.GetByIdAsync( id );
            if (department == null)
                throw new KeyNotFoundException( "Department Not Found!" );

            // Always allow updating Name
            department.Name = dto.Name ?? department.Name;

            // Only update ManagerId if not zero
            if (department.ManagerId != 0)
                department.ManagerId = dto.ManagerId ?? department.ManagerId;

            await _mainRepoistory.UpdateAsync( id, department );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<DepartmentReadDto>( department );

        }

        public async Task<bool> DeleteDepartmentAsync( int id )
        {
            var department = await _mainRepoistory.GetByIdAsync( id );
            if (department == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }




    }
}
