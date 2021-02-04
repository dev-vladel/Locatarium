using Locatarium.Models;
using System.Collections.Generic;

namespace Locatarium.BLL.IBusinessLogic
{
    public interface IUserBLL
    {
        bool RegisterUser(UserModel userModel);

        bool CredentialsExist(string email, string password);

        string GetUserName(string email);

        string GetUserRole(string email);

        int GetUserId(string email);

        UserModel GetUserById(int id);

        List<UserModel> GetUsers(int usersPageNumber);

        int GetUsersTotal();

        bool Ban(int id);

        bool Unban(int id);

        bool CheckStatus(string email);

        bool UpdateDetails(int id, UserModel model, bool changePassword);
    }
}