using System.ComponentModel.DataAnnotations;
using BackEndDotNet.Dtos;
using BackEndDotNet.Models;

namespace BackEndDotNet.Service
{
    public interface IUserService
    {
        public Task<bool> Register(RegisterDto registerDto);
        public Task<User> Login(LoginDto loginDto);
        public Task<bool> ResetPassword(ResetDto resetDto);
        public Task<bool> Delete(int id);
        public Task<bool> Update(int id, RegisterDto user);
        public Task<IList<User>> GetAllUsers();
        public Task<User> GetUserById(int id);
        public Task<User> GetUserByEmail(string email);
    }
}
