using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class ParentService : IParentService
    {
        // Inject Repoistory & UOF
        private readonly IMainRepoistory<Parent> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;

        // Inject AutoMapper
        private readonly IMapper _mapper;
        public ParentService( IMainRepoistory<Parent> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ParentReadDto>> GetAllParentsAsync()
        {
            var parents = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<ParentReadDto>>( parents );
        }

        public async Task<ParentReadDto> GetParentByIdAsync( int id )
        {
            var parent = await _mainRepoistory.GetByIdAsync( id );
            if (parent == null)
                throw new KeyNotFoundException( "parent not found!" );

            return _mapper.Map<ParentReadDto>( parent );
        }


        public async Task<ParentReadDto> CreateParentAsync( ParentCreateDto dto )
        {

            var parent = new Parent
            {
                Name = dto.Name,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
                UserId = dto.UserId,
            };

            await _mainRepoistory.CreateAsync( parent );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ParentReadDto>( parent );
        }


        public async Task<ParentReadDto> UpdateParentAsync( int id, ParentUpdateDto dto )
        {
            var parent = await _mainRepoistory.GetByIdAsync( id );
            if (parent == null)
                throw new KeyNotFoundException( "Parent Is Not Found!" );

            parent.Name = dto.Name ?? parent.Name;
            parent.Address = dto.Address ?? parent.Address;
            parent.PhoneNumber = dto.PhoneNumber ?? parent.PhoneNumber;

            await _mainRepoistory.UpdateAsync( id, parent );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ParentReadDto>( parent );
        }

        public async Task<bool> DeleteParentAsync( int id )
        {
            var parent = await _mainRepoistory.GetByIdAsync( id );
            if (parent == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }




    }
}
