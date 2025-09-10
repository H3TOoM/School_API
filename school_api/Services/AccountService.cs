using AutoMapper;
using school_api.Data.Models;
using school_api.DTOs;
using school_api.Repoistories.Base;
using school_api.Services.Base;

namespace school_api.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMainRepoistory<User> _mainRepoistory;
        private readonly IAccountRepoistory _accountRepoistory;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AccountService( IMainRepoistory<User> mainRepoistory, IAccountRepoistory accountRepoistory, IUnitOfWork unitOfWork , IMapper mapper)
        {
            _accountRepoistory = accountRepoistory;
            _mainRepoistory = mainRepoistory;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserReadDto> RegisterAsync( UserCreateDto dto )
        {
            var isUnique = await _accountRepoistory.IsEmailUniqueAsync( dto.Email );
            if (!isUnique)
                throw new InvalidDataException("Email Is invalid");

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

        public async Task<UserReadDto> LoginAsync( LoginUserDto dto )
        {
            var user = await _accountRepoistory.GetUserByEmailAsync( dto.Email );

            if (user == null || !BCrypt.Net.BCrypt.Verify( dto.Password, user.PasswordHash ))
            {
                throw new Exception( "Invalid email or password." );
            }

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
