using Locatarium.Models;
using Locatarium.Web.ViewModels;

namespace Locatarium.Web.Utils
{
    public static class UserViewModelExtensions
    {
        public static UserModel ViewModelToModel(this UserViewModel viewModel)
        {
            return new UserModel
            {
                FirstName = viewModel.FirstNameVM,
                LastName = viewModel.LastNameVM,
                Email = viewModel.EmailVM,
                Phone = viewModel.PhoneVM,
                Password = viewModel.PasswordVM
            };
        }

        public static UserViewModel ModelToViewModel(this UserModel model)
        {
            return new UserViewModel
            {
                FirstNameVM = model.FirstName,
                LastNameVM = model.LastName,
                EmailVM = model.Email,
                PhoneVM = model.Phone,
            };
        }
    }
}
