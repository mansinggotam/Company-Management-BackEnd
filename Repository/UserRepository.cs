using BackEndDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEndDotNet.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TechrelContext db;
        public UserRepository(TechrelContext db) 
        {
            this.db = db;
        }
        public async Task<bool> AddUser(User user)
        {   
            var exist = await db.Users.FirstOrDefaultAsync(u=> u.Email == user.Email);
            if (exist == null)
            {
                await db.Users.AddAsync(user);
                return await SaveUser();
            }
            return false;
        }

        public async Task<bool> DeleteUser(User user)
        {
            if(user != null)
            {
               db.Remove(user);
               return await SaveUser();
            }
            return false;
        }


        public async Task<IList<User>> GetAllUsers()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await db.Users.FirstOrDefaultAsync(u=> u.Email.ToLower() == email.ToLower());
        }

        public async Task<User> GetUserById(int id)
        {
            return await db.Users.FindAsync(id);
        }

        public async Task<bool> SaveUser()
        {
            return await db.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            db.Users.Update(user);
            return await SaveUser();
        }

    }
}
