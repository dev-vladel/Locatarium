using Locatarium.Models;
using System.Collections.Generic;

namespace Locatarium.BLL.IBusinessLogic
{
    public interface ICategoryBLL
    {
        List<CategoryModel> GetAllCategories();
    }
}