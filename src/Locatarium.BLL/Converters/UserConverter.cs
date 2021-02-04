using Locatarium.DAL.Entities;
using Locatarium.Models;
using System.Collections.Generic;

namespace Locatarium.BLL.Converters
{
    public static class UserConverter
    {
        public static UserModel EntityToModel(User entity)
        {
            UserModel model = new UserModel();

            if (entity != null)
            {
                model.Id = entity.Id;
                model.FirstName = entity.FirstName;
                model.LastName = entity.LastName;
                model.Password = entity.Password;
                model.Email = entity.Email;
                model.Phone = entity.Phone;
                model.IsBanned = entity.IsBanned;
                model.Role = entity.Role;
            }

            return model;
        }

        public static List<UserModel> EntitiesToModels(List<User> entities)
        {
            List<UserModel> models = new List<UserModel>();

            foreach (var item in entities)
            {
                models.Add(EntityToModel(item));
            }

            return models;
        }

        public static User ModelToEntity(UserModel model)
        {
            User entity = new User();

            if (model != null)
            {
                entity.Id = model.Id;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Password = model.Password;
                entity.Email = model.Email;
                entity.Phone = model.Phone;
                entity.IsBanned = model.IsBanned;
                entity.Role = model.Role;
            }

            return entity;
        }

        public static List<User> ModelsToEntities(List<UserModel> models)
        {
            List<User> entities = new List<User>();

            foreach (var item in models)
            {
                entities.Add(ModelToEntity(item));
            }

            return entities;
        }
    }
}