using Locatarium.DAL.Context;
using Locatarium.DAL.Entities;
using Locatarium.DAL.IDAO;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Locatarium.DAL.DAO
{
    public class ResidenceDAO : AbstractDAO<Residence>, IResidenceDAO
    {
        public ResidenceDAO(LocatariumDbContext dbContext) : base(dbContext)
        {
        }

        public bool CreateResidence(Residence entity)
        {
            _dbSet.Add(entity);

            return _dbContext.SaveChanges() > 0;
        }

        public bool UpdateResidence(Residence entity)
        {
            _dbSet.Update(entity);

            return _dbContext.SaveChanges() > 0;
        }

        public bool RemoveResidence(int residenceId)
        {
            _dbSet.Remove(GetResidence(residenceId));

            return _dbContext.SaveChanges() > 0;
        }

        public Residence GetResidence(int residenceId)
        {
            return _dbSet.Where(r => r.Id == residenceId).Include(rc => rc.ResidenceCategories).ThenInclude(c => c.Category).Include(ri => ri.ResidenceImages).Include(a => a.Address).Include(u => u.User).FirstOrDefault();
        }

        public List<Residence> GetMyResidences(int userId, int residencesPageNumber)
        {
            return _dbSet.Where(u => u.UserId == userId).Include(rc => rc.ResidenceCategories).ThenInclude(c => c.Category).Include(ri => ri.ResidenceImages).Include(a => a.Address).Skip(residencesPageNumber * 8).Take(8).ToList();
        }

        public int GetMyResidencesTotal(int userId)
        {
            return _dbSet.Where(u => u.UserId == userId).Count();
        }

        public List<Residence> GetResidencesSkip(int toSkipNumber)
        {
            return _dbSet.Include(rc => rc.ResidenceCategories).ThenInclude(c => c.Category).Include(ri => ri.ResidenceImages).Include(a => a.Address).Skip(toSkipNumber).Take(8).ToList();
        }

        public List<Residence> GetResidencesFiltered(string name, int minPrice, int? maxPrice)
        {
            if (name == null && maxPrice == null && minPrice == 0)
            {
                return new List<Residence>();
            }

            var results = _dbSet.Include(rc => rc.ResidenceCategories).ThenInclude(c => c.Category).Include(ri => ri.ResidenceImages).Include(a => a.Address).Where(r => r.Name.Contains(name ?? string.Empty) && (r.Price >= minPrice));

            if (maxPrice != null)
            {
                results = results.Where(u => u.Price <= maxPrice.Value);
            }

            return results.ToList();
        }
    }
}