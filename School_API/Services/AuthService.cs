using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using School_API.Data;
using School_API.DTOs;
using School_API.Models;
using School_API.Repoistory.Base;
using School_API.Services.Base;

namespace School_API.Services
{
    public class AuthService : IAuthService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepoistory<User> _repoistory;

        public AuthService(IUnitOfWork unitOfWork, IRepoistory<User> repoistory)
        {
            _unitOfWork = unitOfWork;
            _repoistory = repoistory;
        }


        public async Task<User> Register(RegisterDto dto)
        {

            bool emailCheck = (await _repoistory.GetAllAsync()).Any(u => u.Email == dto.Email);


            if (emailCheck)
            {
                throw new Exception("Email is already registered");
            }


            // Create User
            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                RoleId = dto.RoleId,
            };

            // Add user to Database

            await _repoistory.Add(user);

            await _unitOfWork.SaveChangesAsync();


         
            return user;



        }



        public async Task<User> Login(LoginDto dto)
        {
            // find user by email
            var user = (await _repoistory.GetAllAsync())
                .FirstOrDefault(u => u.Email.ToLower() == dto.Email.ToLower());



            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
            {
                throw new Exception("Invalid email or password.");
            }


            return user;
        }


    }
}
