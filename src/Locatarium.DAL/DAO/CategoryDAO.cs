using Locatarium.DAL.Context;
using Locatarium.DAL.Entities;
using Locatarium.DAL.IDAO;
using System.Collections.Generic;
using System.Linq;

namespace Locatarium.DAL.DAO
{
    public class CategoryDAO : AbstractDAO<Category>, ICategoryDAO
    {
        public CategoryDAO(LocatariumDbContext dbContext) : base(dbContext)
        { }

        public List<Category> GetAllCategories()
        {
            return _dbSet.ToList();
        }
    }
}