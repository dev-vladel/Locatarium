using Locatarium.BLL.IBusinessLogic;
using Locatarium.Models;
using Locatarium.Web.Utils;
using Locatarium.Web.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Locatarium.Web.Controllers
{
    public class UsersController : Controller
    {
        private IUserBLL _iUserBLL;

        public UsersController(IUserBLL iUserBLL)
        {
            _iUserBLL = iUserBLL;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                if (_iUserBLL.RegisterUser(model))
                {
                    return RedirectToAction("Login");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            if (ModelState.IsValid)
            {
                if (_iUserBLL.CredentialsExist(email, password))
                {
                    if(!_iUserBLL.CheckStatus(email))
                    {
                        var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, _iUserBLL.GetUserName(email)),
                        new Claim(ClaimTypes.Role, _iUserBLL.GetUserRole(email)),
                        new Claim(ClaimTypes.SerialNumber, _iUserBLL.GetUserId(email).ToString()),
                        new Claim(ClaimTypes.Email, email)
                    };

                        ClaimsIdentity identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                        var authenticationProperties = new AuthenticationProperties
                        {
                            IsPersistent = false,
                        };

                        await HttpContext.SignInAsync(principal, authenticationProperties);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("Password", "Account is banned.");
                    }
                }
                else
                {
                    ModelState.AddModelError("Password", "Email and/or Password wrong");

                    return View();
                }
            }
            return View();
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Users");
        }

        [HttpGet]
        [Authorize]
        public IActionResult Details()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber).Value);
            var model = _iUserBLL.GetUserById(userId);
            var viewModel = model.ModelToViewModel();

            return View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public IActionResult Details(UserViewModel viewModel)
        {
            bool changePassword = new bool();
            bool updateStatus = new bool();
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber).Value);

            if (viewModel.PasswordVM != null && viewModel.ConfirmPasswordVM != null)
            {
                 changePassword = true;
            }

            if (ModelState.IsValid)
            {
                updateStatus = _iUserBLL.UpdateDetails(userId, viewModel.ViewModelToModel(), changePassword);
            }
            else return View();

            if (viewModel.PasswordVM != null && viewModel.ConfirmPasswordVM != null && updateStatus == true)
            {
                return RedirectToAction("Logout");
            }

            return RedirectToAction("Details");
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet]
        public IActionResult Items()
        {
            return View("Users");
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet]
        public int ItemsTotal()
        {
            return _iUserBLL.GetUsersTotal();
        }

        [Authorize(Policy = "Administrator")]
        [HttpGet]
        public IActionResult ItemsPartial(int usersPageNumber)
        {
            var models = _iUserBLL.GetUsers(usersPageNumber);

            return PartialView("Partials/UsersPartial", models);
        }

        [Authorize(Policy = "Administrator")]
        public int GetUsersTotal()
        {
            return _iUserBLL.GetUsersTotal();
        }

        [Authorize(Policy = "Administrator")]
        [HttpPost]
        public IActionResult Ban(int id)
        {
            var model = _iUserBLL.GetUserById(id);

            if(!model.IsBanned)
            {
                _iUserBLL.Ban(id);
            }

            return RedirectToAction("Items");
        }

        [Authorize(Policy = "Administrator")]
        [HttpPost]
        public IActionResult Unban(int id)
        {
            var model = _iUserBLL.GetUserById(id);

            if (model.IsBanned)
            {
                _iUserBLL.Unban(id);
            }

            return RedirectToAction("Items");
        }
    }
}