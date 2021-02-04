using Locatarium.BLL.Converters;
using Locatarium.BLL.IBusinessLogic;
using Locatarium.DAL.IDAO;
using Locatarium.Models;
using System.Collections.Generic;

namespace Locatarium.BLL.BusinessLogic
{
    public class CategoryBLL : ICategoryBLL
    {
        private ICategoryDAO _iCategoryDAO;

        public CategoryBLL(ICategoryDAO iCategoryDAO)
        {
            _iCategoryDAO = iCategoryDAO;
        }

        public List<CategoryModel> GetAllCategories()
        {
            var entities = _iCategoryDAO.GetAllCategories();
            var models = CategoryConverter.EntitiesToModels(entities);

            return models;
        }
    }
}