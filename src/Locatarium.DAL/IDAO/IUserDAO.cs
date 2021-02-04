using Locatarium.DAL.Entities;
using System.Collections.Generic;

namespace Locatarium.DAL.IDAO
{
    public interface IUserDAO
    {
        bool RegisterUser(User entity);

        bool CredentialsExist(string email, string password);

        string GetUserName(string email);

        string GetUserRole(string email);

        int GetUserId(string email);

        User GetUserById(int id);

        List<User> GetUsers(int usersPageNumber);

        int GetUsersTotal();

        bool Ban(User entity);

        bool Unban(User entity);

        bool CheckStatus(string email);

        bool UpdateDetails(User entity);
    }
}