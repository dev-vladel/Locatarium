using Locatarium.DAL.Entities;
using System.Collections.Generic;

namespace Locatarium.DAL.IDAO
{
    public interface IResidenceDAO
    {
        bool CreateResidence(Residence entity);

        bool UpdateResidence(Residence entity);

        bool RemoveResidence(int residenceId);

        Residence GetResidence(int residenceId);

        List<Residence> GetMyResidences(int userId, int residencesPageNumber);

        int GetMyResidencesTotal(int userId);

        List<Residence> GetResidencesSkip(int toSkipNumber);

        List<Residence> GetResidencesFiltered(string name, int minPrice, int? maxPrice);
    }
}