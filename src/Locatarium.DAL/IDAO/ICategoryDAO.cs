using Locatarium.DAL.Entities;
using System.Collections.Generic;

namespace Locatarium.DAL.IDAO
{
    public interface ICategoryDAO
    {
        List<Category> GetAllCategories();
    }
}