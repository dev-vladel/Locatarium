using Locatarium.Models;
using System.Collections.Generic;

namespace Locatarium.BLL.IBusinessLogic
{
    public interface IResidenceBLL
    {
        bool CreateResidence(ResidenceModel model);

        bool UpdateResidence(ResidenceModel model);

        bool RemoveResidence(int residenceId);

        void RemoveResidenceImage(string path, int id);

        ResidenceModel GetResidence(int residenceId);

        List<ResidenceModel> GetMyResidences(int userId, int residencesPageNumber);

        int GetMyResidencesTotal(int userId);

        List<ResidenceModel> GetResidencesSkip(int toSkipNumber);

        List<ResidenceModel> GetResidencesFiltered(string name, int minPrice, int? maxPrice);
    }
}