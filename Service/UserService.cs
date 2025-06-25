using BackEndDotNet.Dtos;
using BackEndDotNet.Models;
using BackEndDotNet.Repository;

namespace BackEndDotNet.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository repo;
        public UserService(IUserRepository repo)
        {
            this.repo = repo;
        }
        public async Task<bool> Delete(int id)
        {
            var exist = await repo.GetUserById(id);
            if(exist == null)
            {
                return false;
            }
            return await repo.DeleteUser(exist);
        }

        public async Task<IList<User>> GetAllUsers()
        {
            return await repo.GetAllUsers();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await repo.GetUserByEmail(email);
        }

        public async Task<User> GetUserById(int id)
        {
            return await repo.GetUserById(id);
        }

        public async Task<User> Login(LoginDto loginDto)
        {
            var existing = await repo.GetUserByEmail(loginDto.Email);
            if (existing == null)
            {
                return null;
            }
            var res = BCrypt.Net.BCrypt.Verify(loginDto.Password, existing.Password);
            if (res)
            {
                existing.Password = "";
                return existing;
            }
            return null;
        }

        public async Task<bool> Register(RegisterDto registerDto)
        {
            var existing = await repo.GetUserByEmail(registerDto.Email);
            if(existing == null)
            {
                User user = this.ConvertDto(registerDto);
                return await repo.AddUser(user);
            }
            return false;
        }

        public async Task<bool> ResetPassword(ResetDto resetDto)
        {
            var exist = await repo.GetUserByEmail(resetDto.Email);
            if(exist== null)
            {
                return false;
            }
            exist.Password = BCrypt.Net.BCrypt.HashPassword(resetDto.Password);
            return await repo.UpdateUser(exist);
        }

        public async Task<bool> Update(int id, RegisterDto newObj)
        {
            var existing = await repo.GetUserById(id); 

            if (existing == null)
            {
                return false;
            }

            existing.Name = newObj.Name;
            existing.Email = newObj.Email;
            existing.Password = BCrypt.Net.BCrypt.HashPassword(newObj.Password);
            existing.Address = newObj.Address;
            existing.Gender = newObj.Gender;
            existing.Birthdate = newObj.Birthdate;

            return await repo.UpdateUser(existing); 
        }


        private User ConvertDto(RegisterDto obj)
        {
            var pass = BCrypt.Net.BCrypt.HashPassword(obj.Password);
            User u = new User
            {
                Name = obj.Name,
                Email = obj.Email,
                Password = pass,
                Address= obj.Address,
                Gender = obj.Gender,
                Birthdate = obj.Birthdate
            };
            return u;

        }
    }
}
