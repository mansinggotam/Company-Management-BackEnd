using BackEndDotNet.Models;

namespace BackEndDotNet.Repository
{
    public interface IUserRepository
    {
        public Task<bool> AddUser(User user);
        public Task<bool> UpdateUser(User user);
        public Task<bool> DeleteUser(User user);
        public Task<IList<User>> GetAllUsers();
        public Task<User> GetUserById(int id); 
        public Task<User> GetUserByEmail(string email);
        public Task<bool> SaveUser();
    }
} 
