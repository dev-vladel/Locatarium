using Locatarium.DAL.Context;
using Locatarium.DAL.Entities;
using Locatarium.DAL.IDAO;
using System.Collections.Generic;
using System.Linq;

namespace Locatarium.DAL.DAO
{
    public class UserDAO : AbstractDAO<User>, IUserDAO
    {
        public UserDAO(LocatariumDbContext dbContext) : base(dbContext)
        {
        }

        public bool RegisterUser(User entity)
        {
            _dbSet.Add(entity);

            return _dbContext.SaveChanges() > 0;
        }

        public bool CredentialsExist(string email, string password)
        {
            return _dbSet.FirstOrDefault(u => u.Email == email && u.Password == password) != null;
        }

        public string GetUserName(string email)
        {
            var user = _dbSet.FirstOrDefault(u => u.Email == email);

            return user.FirstName;
        }

        public string GetUserRole(string email)
        {
            var user = _dbSet.FirstOrDefault(u => u.Email == email);

            return user.Role;
        }

        public int GetUserId(string email)
        {
            var user = _dbSet.FirstOrDefault(u => u.Email == email);

            return user.Id;
        }

        public User GetUserById(int id)
        {
            return _dbSet.FirstOrDefault(u => u.Id == id);
        }

        public List<User> GetUsers(int usersPageNumber)
        {
            return _dbSet.Skip(usersPageNumber * 16).Take(16).ToList();
        }


        public int GetUsersTotal()
        {
            return _dbSet.Count();
        }

        public bool Ban(User entity)
        {
            _dbSet.Update(entity);

            return _dbContext.SaveChanges() > 0;
        }

        public bool Unban(User entity)
        {
            _dbSet.Update(entity);

            return _dbContext.SaveChanges() > 0;
        }

        public bool CheckStatus(string email)
        {
            return _dbSet.Where(u => u.Email == email).Select(u => u.IsBanned).FirstOrDefault() == true;
        }

        public bool UpdateDetails(User entity)
        {
            _dbSet.Update(entity);

            return _dbContext.SaveChanges() > 0;
        }
    }
}