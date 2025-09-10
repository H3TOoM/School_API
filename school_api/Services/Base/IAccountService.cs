using school_api.DTOs;

namespace school_api.Services.Base
{
    public interface IAccountService
    {
        Task<UserReadDto> RegisterAsync(UserCreateDto dto);
        Task<UserReadDto> LoginAsync(LoginUserDto dto);

        Task<bool> DeleteUserAsync( int id );

        // To Do  ==>  Change Password
         

    }
}
