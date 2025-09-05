using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IMainRepoistory<Manager> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ManagerService(IMainRepoistory<Manager> mainRepoistory , IUnitOfWork unitOfWork , IMapper mapper)
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

       


        public async Task<IEnumerable<ManagerReadDto>> GetAllManagersAsync()
        {
            var managers = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<ManagerReadDto>>(managers);
        }

        public async Task<ManagerReadDto> GetManagerByIdAsync( int id )
        {
            var manager = await _mainRepoistory.GetByIdAsync( id );

            if (manager == null)
                throw new KeyNotFoundException( "Manager Not Found!" );

            return _mapper.Map<ManagerReadDto>( manager );

        }


        public async Task<ManagerReadDto> CreateManagerAsync( ManagerCreateDto dto )
        {
            var manager = new Manager
            {
                Name = dto.Name,
                UserId = dto.UserId,
            };

            await _mainRepoistory.CreateAsync( manager );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ManagerReadDto>( manager );

        }



        public async Task<ManagerReadDto> UpdateManagerAsync( int id, ManagerUpdateDto dto )
        {
            var manager = await _mainRepoistory.GetByIdAsync( id );

            if (manager == null)
                throw new KeyNotFoundException( "Manager Not Found!" );

            manager.Name = dto.Name ?? manager.Name;

            await _mainRepoistory.UpdateAsync( id, manager );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ManagerReadDto>( manager );
        }



        public async Task<bool> DeleteManagerAsync( int id )
        {
            var manager = await _mainRepoistory.GetByIdAsync( id );
            if (manager == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();
            return true;
        }



    }
}
