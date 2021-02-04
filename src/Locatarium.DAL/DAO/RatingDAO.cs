using Locatarium.DAL.Context;
using Locatarium.DAL.Entities;
using Locatarium.DAL.IDAO;
using System.Linq;

namespace Locatarium.DAL.DAO
{
    public class RatingDAO : AbstractDAO<Rating>, IRatingDAO
    {
        public RatingDAO(LocatariumDbContext dbContext) : base(dbContext)
        {

        }

        public bool CheckIfRatedAlready(int residenceId, int? userId)
        {
            return _dbSet.Where(r => r.ResidenceId == residenceId && r.UserId == userId).Select(r => r.Value).Count() > 0;
        }

        public bool RateResidence(Rating entity)
        {
            if (CheckIfRatedAlready(entity.ResidenceId, entity.UserId))
            {
                return false;
            }

            _dbSet.Add(entity);

            return _dbContext.SaveChanges() > 0;
        }

        public double CalculateAvarageResidence(int residenceId)
        {
            if (_dbSet.Where(r => r.ResidenceId == residenceId).Select(r => r.Value).Count() == 0)
            {
                return 0f;
            }
            else
            {
                return _dbSet.Where(r => r.ResidenceId == residenceId).Average(r => r.Value);
            }
        }

        public int CalculateTotalOfRatingsResidence(int residenceId)
        {
            return _dbSet.Count(r => r.ResidenceId == residenceId);
        }
    }
}
