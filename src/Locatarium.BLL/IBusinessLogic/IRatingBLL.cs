using Locatarium.Models;

namespace Locatarium.BLL.IBusinessLogic
{
    public interface IRatingBLL
    {
        bool RateResidence(int residenceId, int userId, int value);

        double CalculateAvarageResidence(int residenceId);

        int CalculateTotalOfRatingsResidence(int residenceId);
    }
}
