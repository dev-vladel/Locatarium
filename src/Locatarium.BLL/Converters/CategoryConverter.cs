using Locatarium.DAL.Entities;
using Locatarium.Models;
using System.Collections.Generic;

namespace Locatarium.BLL.Converters
{
    public static class CategoryConverter
    {
        public static CategoryModel EntityToModel(Category entity)
        {
            CategoryModel model = new CategoryModel();

            if (entity != null)
            {
                model.Id = entity.Id;
                model.Name = entity.Name;
            }

            return model;
        }

        public static List<CategoryModel> EntitiesToModels(List<Category> entities)
        {
            List<CategoryModel> models = new List<CategoryModel>();

            foreach (var entity in entities)
            {
                models.Add(EntityToModel(entity));
            }

            return models;
        }
    }
}