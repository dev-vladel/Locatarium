using Locatarium.BLL.Converters;
using Locatarium.BLL.IBusinessLogic;
using Locatarium.DAL.IDAO;
using Locatarium.Models;
using System.Collections.Generic;

namespace Locatarium.BLL.BusinessLogic
{
    public class UserBLL : IUserBLL
    {
        private IUserDAO _iUserDAO;

        public UserBLL(IUserDAO iUserDAO)
        {
            _iUserDAO = iUserDAO;
        }

        public bool RegisterUser(UserModel model)
        {
            var entity = UserConverter.ModelToEntity(model);

            entity.IsBanned = false;
            entity.Role = "user";

            return _iUserDAO.RegisterUser(entity);
        }

        public bool CredentialsExist(string email, string password)
        {
            return _iUserDAO.CredentialsExist(email, password);
        }

        public string GetUserName(string email)
        {
            return _iUserDAO.GetUserName(email);
        }

        public string GetUserRole(string email)
        {
            return _iUserDAO.GetUserRole(email);
        }

        public int GetUserId(string email)
        {
            return _iUserDAO.GetUserId(email);
        }

        public UserModel GetUserById(int id)
        {
            var entity = _iUserDAO.GetUserById(id);
            var model = UserConverter.EntityToModel(entity);

            return model;
        }

        public List<UserModel> GetUsers(int usersPageNumber)
        {
            var entities = _iUserDAO.GetUsers(usersPageNumber);
            var models = UserConverter.EntitiesToModels(entities);

            return models;
        }

        public int GetUsersTotal()
        {
            return _iUserDAO.GetUsersTotal();
        }

        public bool Ban(int id)
        {
            var entity = _iUserDAO.GetUserById(id);

            entity.IsBanned = true;

            return _iUserDAO.Ban(entity);
        }

        public bool Unban(int id)
        {
            var entity = _iUserDAO.GetUserById(id);

            entity.IsBanned = false;

            return _iUserDAO.Unban(entity);
        }

        public bool CheckStatus(string email)
        {
            return _iUserDAO.CheckStatus(email); 
        }

        public bool UpdateDetails(int id, UserModel model, bool changePassword)
        {
            var entity = _iUserDAO.GetUserById(id);

            if (model.FirstName != entity.FirstName && model.FirstName != null)
            {
                entity.FirstName = model.FirstName;
            }

            if (model.LastName != entity.LastName && model.LastName != null)
            {
                entity.LastName = model.LastName;
            }

            if (model.Phone != entity.Phone && model.Phone != null)
            {
                entity.Phone = model.Phone;
            }

            if (model.Email != entity.Email && model.Email != null)
            {
                entity.Email = model.Email;
            }

            if (changePassword)
            {
                if (model.Password != entity.Password && model.Password != null)
                {
                    entity.Password = model.Password;
                }
            }

            return _iUserDAO.UpdateDetails(entity);
        }
    }
}