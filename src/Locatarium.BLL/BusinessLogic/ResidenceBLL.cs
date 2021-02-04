using Locatarium.BLL.Converters;
using Locatarium.BLL.IBusinessLogic;
using Locatarium.DAL.Entities;
using Locatarium.DAL.IDAO;
using Locatarium.Models;
using System.Collections.Generic;
using System.IO;

namespace Locatarium.BLL.BusinessLogic
{
    public class ResidenceBLL : IResidenceBLL
    {
        private IResidenceDAO _iResidenceDAO;

        public ResidenceBLL(IResidenceDAO iResidanceDAO)
        {
            _iResidenceDAO = iResidanceDAO;
        }

        public bool CreateResidence(ResidenceModel model)
        {
            var entity = ResidenceConverter.ModelToEntity(model);

            if (model.Images == null)
            {
                ResidenceImage residenceImage = new ResidenceImage()
                {
                    ResidenceId = entity.Id,
                    ImageName = "https://i.imgur.com/vd8iZDs.jpg"
                };

                entity.ResidenceImages.Add(residenceImage);
            }

            return _iResidenceDAO.CreateResidence(entity);
        }

        public bool UpdateResidence(ResidenceModel model)
        {
            var entity = _iResidenceDAO.GetResidence(model.Id);

            entity.Name = model.Name;
            entity.Price = model.Price;
            ResidenceConverter.AddressModelToEntity(model, entity);

            if (model.Images != null)
            {
                entity.ResidenceImages = new List<ResidenceImage>();
                ResidenceConverter.ImagesModelToEntity(model, entity);
            }

            entity.ResidenceCategories = new List<ResidenceCategory>();
            ResidenceConverter.CategoriesModelToEntity(model, entity);

            return _iResidenceDAO.UpdateResidence(entity);
        }

        public bool RemoveResidence(int residenceId)
        {
            return _iResidenceDAO.RemoveResidence(residenceId);
        }

        public void RemoveResidenceImage(string path, int residenceId)
        {
            var model = GetResidence(residenceId);

            foreach (var image in model.Images)
            {
                if(!(image.Contains("imgur")))
                {
                    File.Delete(path + image);
                }
            }
        }

        public ResidenceModel GetResidence(int residenceId)
        {
            var entity = _iResidenceDAO.GetResidence(residenceId);
            var model = ResidenceConverter.EntityToModel(entity);

            return model;
        }

        public List<ResidenceModel> GetMyResidences(int userId, int residencesPageNumber)
        {
            if (residencesPageNumber < 0)
            {
                residencesPageNumber = 0;
            }

            var entities = _iResidenceDAO.GetMyResidences(userId, residencesPageNumber);
            var models = ResidenceConverter.EntitiesToModels(entities);

            return models;
        }

        public int GetMyResidencesTotal(int userId)
        {
            return _iResidenceDAO.GetMyResidencesTotal(userId);
        }

        public List<ResidenceModel> GetResidencesSkip(int toSkipNumber)
        {
            var entities = _iResidenceDAO.GetResidencesSkip(toSkipNumber);
            var model = ResidenceConverter.EntitiesToModels(entities);

            return model;
        }

        public List<ResidenceModel> GetResidencesFiltered(string name, int minPrice, int? maxPrice)
        {
            var entities = _iResidenceDAO.GetResidencesFiltered(name, minPrice, maxPrice);

            return ResidenceConverter.EntitiesToModels(entities);
        }
    }
}