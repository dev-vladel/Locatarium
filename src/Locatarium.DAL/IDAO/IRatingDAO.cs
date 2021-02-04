using Locatarium.DAL.Entities;

namespace Locatarium.DAL.IDAO
{
    public interface IRatingDAO
    {
        bool RateResidence(Rating entity);

        double CalculateAvarageResidence(int residenceId);

        int CalculateTotalOfRatingsResidence(int residenceId);
    }
}
