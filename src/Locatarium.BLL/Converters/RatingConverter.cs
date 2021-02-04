using Locatarium.DAL.Entities;
using Locatarium.Models;

namespace Locatarium.BLL.Converters
{
    public static class RatingConverter
    {
        public static Rating ModelToEntity(RatingModel model)
        {
            var entity = new Rating();

            entity.Id = model.Id;
            entity.ResidenceId = model.ResidenceId;
            entity.UserId = model.UserId;
            entity.Value = model.Value;

            return entity;
        }

        public static RatingModel EntityToModel(Rating entity)
        {
            var model = new RatingModel();

            model.Id = entity.Id;
            model.ResidenceId = entity.ResidenceId;
            model.UserId = entity.UserId;
            model.Value = entity.Value;

            return model;
        }
    }
}
