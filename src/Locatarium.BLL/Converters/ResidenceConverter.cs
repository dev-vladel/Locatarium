using Locatarium.DAL.Entities;
using Locatarium.Models;
using System.Collections.Generic;

namespace Locatarium.BLL.Converters
{
    public class ResidenceConverter
    {
        public static ResidenceModel EntityToModel(Residence entity)
        {
            ResidenceModel model = new ResidenceModel()
            {
                ResidenceCategories = new List<CategoryModel>(),
                ResidenceCategoryId = new List<int>(),
                Images = new List<string>(),
                Address = new AddressModel(),
                UserDetails = new UserModel()
            };

            if (entity != null)
            {
                BaseEntityToModel(entity, model);
                AddressEntityToModel(entity, model);
                ImagesEntityToModel(entity, model);
                CategoriesEntityToModel(entity, model);

                if (entity.User != null)
                {
                    UserDetailsEntityToModel(entity, model);
                }
            }

            return model;
        }

        public static List<ResidenceModel> EntitiesToModels(List<Residence> entities)
        {
            List<ResidenceModel> models = new List<ResidenceModel>();

            foreach (var item in entities)
            {
                models.Add(EntityToModel(item));
            }

            return models;
        }

        public static Residence ModelToEntity(ResidenceModel model)
        {
            Residence entity = new Residence()
            {
                ResidenceCategories = new List<ResidenceCategory>(),
                Address = new Address(),
                ResidenceImages = new List<ResidenceImage>()
            };

            if (model != null)
            {
                BaseModelToEntity(model, entity);
                AddressModelToEntity(model, entity);
                ImagesModelToEntity(model, entity);
                CategoriesModelToEntity(model, entity);
            }

            return entity;
        }

        public static void BaseEntityToModel(Residence entity, ResidenceModel model)
        {
            model.Id = entity.Id;
            model.Name = entity.Name;
            model.Price = entity.Price;
            model.UserId = entity.UserId;
        }

        public static void BaseModelToEntity(ResidenceModel model, Residence entity)
        {
            entity.Id = model.Id;
            entity.Name = model.Name;
            entity.Price = model.Price;
            entity.UserId = model.UserId;
        }

        public static void AddressEntityToModel(Residence entity, ResidenceModel model)
        {
            model.Address.City = entity.Address.City;
            model.Address.Street = entity.Address.Street;
            model.Address.State = entity.Address.State;
            model.Address.PostalCode = entity.Address.PostalCode;
        }

        public static void AddressModelToEntity(ResidenceModel model, Residence entity)
        {
            entity.Address.City = model.Address.City;
            entity.Address.Street = model.Address.Street;
            entity.Address.State = model.Address.State;
            entity.Address.PostalCode = model.Address.PostalCode;
        }

        public static void UserDetailsEntityToModel(Residence entity, ResidenceModel model)
        {
            model.UserDetails.Email = entity.User.Email;
            model.UserDetails.FirstName = entity.User.FirstName;
            model.UserDetails.Phone = entity.User.Phone;
        }

        public static void ImagesEntityToModel(Residence entity, ResidenceModel model)
        {
            foreach (var item in entity.ResidenceImages)
            {
                model.Images.Add(item.ImageName);
            }
        }

        public static void ImagesModelToEntity(ResidenceModel model, Residence entity)
        {
            if (model.Images != null)
            {
                foreach (var image in model.Images)
                {
                    var itemImage = new ResidenceImage
                    {
                        ResidenceId = entity.Id
                    };

                    itemImage.ImageName = image;
                    entity.ResidenceImages.Add(itemImage);
                }
            }
        }

        public static void CategoriesEntityToModel(Residence entity, ResidenceModel model)
        {

            foreach (var item in entity.ResidenceCategories)
            {
                model.ResidenceCategoryId.Add(item.CategoryId);

                var itemCategory = new CategoryModel
                {
                    Id = item.CategoryId
                };

                itemCategory.Name = item.Category.Name;
                model.ResidenceCategories.Add(itemCategory);
            }
        }

        public static void CategoriesModelToEntity(ResidenceModel model, Residence entity)
        {
            foreach (var id in model.ResidenceCategoryId)
            {
                var itemCategory = new ResidenceCategory
                {
                    ResidenceId = entity.Id
                };

                itemCategory.CategoryId = id;
                entity.ResidenceCategories.Add(itemCategory);
            }
        }
    }
}