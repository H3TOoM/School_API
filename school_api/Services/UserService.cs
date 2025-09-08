using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class UserService : IUserService
    {
        private readonly IMainRepoistory<User> _mainRepoistory;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService( IMainRepoistory<User> mainRepoistory, IUnitOfWork unitOfWork, IMapper mapper )
        {
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }




        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var users = await _mainRepoistory.GetAllAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>( users );
        }

        public async Task<UserReadDto> GetUserByIdAsync( int id )
        {
            var user = await _mainRepoistory.GetByIdAsync( id );
            return _mapper.Map<UserReadDto>( user );
        }


        public async Task<UserReadDto> CreateUserAsync( UserCreateDto dto )
        {
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword( dto.Password ),
                Role = dto.Role,
            };

            await _mainRepoistory.CreateAsync( user );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserReadDto>( user );

        }

        public async Task<UserReadDto> UpdateUserAsync( int id, UserUpdateDto dto )
        {
            var user = await _mainRepoistory.GetByIdAsync( id );
            if (user == null)
                throw new KeyNotFoundException( "User Not Found!" );

            user.Name = dto.Name ?? user.Name;
            user.Email = dto.Email ?? user.Email;
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword( dto.Password ) ?? user.PasswordHash;
            user.Role = dto.Role ?? user.Role;
            user.IsActive = dto.IsActive ?? user.IsActive;


            await _mainRepoistory.UpdateAsync( id, user );
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<UserReadDto>( user );
        }

        public async Task<bool> DeleteUserAsync( int id )
        {
            var user = await _mainRepoistory.GetByIdAsync( id );
            if (user == null)
                return false;

            await _mainRepoistory.DeleteAsync( id );
            await _unitOfWork.SaveChangesAsync();

            return true;
        }



    }
}
