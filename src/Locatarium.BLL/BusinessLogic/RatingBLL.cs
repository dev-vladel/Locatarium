using Locatarium.BLL.Converters;
using Locatarium.BLL.IBusinessLogic;
using Locatarium.DAL.IDAO;
using Locatarium.Models;
using System;

namespace Locatarium.BLL.BusinessLogic
{
    public class RatingBLL : IRatingBLL
    {
        private IRatingDAO _iRatingDAO;

        public RatingBLL(IRatingDAO iRatingDAO)
        {
            _iRatingDAO = iRatingDAO;
        }

        public bool RateResidence(int residenceId, int userLoggedId, int value)
        {
            var model = new RatingModel()
            {
                ResidenceId = residenceId,
                UserId = userLoggedId,
                Value = value
            };
            var entity = RatingConverter.ModelToEntity(model);

            return _iRatingDAO.RateResidence(entity);
        }

        public double CalculateAvarageResidence(int residenceId)
        {
            return _iRatingDAO.CalculateAvarageResidence(residenceId);
        }

        public int CalculateTotalOfRatingsResidence(int residenceId)
        {
            return _iRatingDAO.CalculateTotalOfRatingsResidence(residenceId);
        }
    }
}
