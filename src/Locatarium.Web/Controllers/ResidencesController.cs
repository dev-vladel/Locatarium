using Locatarium.BLL.IBusinessLogic;
using Locatarium.Models;
using Locatarium.Web.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Locatarium.Web.Controllers
{
    public class ResidencesController : Controller
    {
        private IResidenceBLL _iResidenceBLL;
        private IUserBLL _iUserBLL;
        private ICategoryBLL _iCategoryBLL;
        private IHostingEnvironment _iHostingEnvironment;
        private readonly IAuthorizationService _iAuthorizationService;
        private IRatingBLL _iRatingBLL;

        public ResidencesController(IResidenceBLL iResidenceBLL, IUserBLL iUserBLL, ICategoryBLL iCategoryBLL, IHostingEnvironment iHostingEnvironment, IAuthorizationService iAuthorizationService, IRatingBLL iRatingBLL)
        {
            _iResidenceBLL = iResidenceBLL;
            _iUserBLL = iUserBLL;
            _iCategoryBLL = iCategoryBLL;
            _iHostingEnvironment = iHostingEnvironment;
            _iAuthorizationService = iAuthorizationService;
            _iRatingBLL = iRatingBLL;
        }

        [HttpGet]
        [Authorize]
        public IActionResult Items()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public int ItemsTotal()
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber).Value);

            return _iResidenceBLL.GetMyResidencesTotal(userId);
        }

        [HttpGet]
        [Authorize]
        public IActionResult ItemsPartial(int residencesPageNumber)
        {
            var userId = Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber).Value);
            var models = _iResidenceBLL.GetMyResidences(userId, residencesPageNumber);

            return PartialView("Partials/ItemsPartial", models);
        }

        [HttpGet]
        [Authorize]
        public IActionResult Create()
        {
            ResidenceModel model = new ResidenceModel
            {
                ResidenceCategories = _iCategoryBLL.GetAllCategories()
            };

            return View("Create", model);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ResidenceModel model, List<IFormFile> uploadImages)
        {
            model.ResidenceCategories = _iCategoryBLL.GetAllCategories();

            if (ModelState.IsValid)
            {
                model.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber).Value);

                if (uploadImages.Count != 0)
                {
                    model.Images = new List<string>();

                    try
                    {
                        foreach (var image in uploadImages)
                        {
                            await ImageProcesser.UploadeAndResize(_iHostingEnvironment, image);
                            model.Images.Add(ImageProcesser.ReturnFileTarget());
                        }
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("Images", e.Message);

                        return View(model);
                    }
                }

                _iResidenceBLL.CreateResidence(model);

                return RedirectToAction("Items");
            }
            else
            {
                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var model = _iResidenceBLL.GetResidence(id);
            var authorizationResult = await _iAuthorizationService.AuthorizeAsync(User, model, Constants.Update);

            if (authorizationResult.Succeeded)
            {
                model.ResidenceCategories = _iCategoryBLL.GetAllCategories();

                return View("Edit", model);
            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }

        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ResidenceModel model, List<IFormFile> uploadImages)
        {
            model.ResidenceCategories = _iCategoryBLL.GetAllCategories();
            model.UserId = Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber).Value);

            var authorizationResult = await _iAuthorizationService.AuthorizeAsync(User, model, Constants.Update);

            if (authorizationResult.Succeeded)
            {
                if (ModelState.IsValid)
                {
                    if (uploadImages.Count != 0)
                    {
                        _iResidenceBLL.RemoveResidenceImage(_iHostingEnvironment.ContentRootPath + Constants.wwwroot, id);

                        model.Images = new List<string>();

                        try
                        {
                            foreach (var image in uploadImages)
                            {
                                await ImageProcesser.UploadeAndResize(_iHostingEnvironment, image);
                                model.Images.Add(ImageProcesser.ReturnFileTarget());
                            }
                        }
                        catch (Exception e)
                        {
                            ModelState.AddModelError("Images", e.Message);

                            return View(model);
                        }
                    }

                    _iResidenceBLL.UpdateResidence(model);

                    return RedirectToAction("Items");
                }
                else
                {
                    return View(model);
                }
            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var model = _iResidenceBLL.GetResidence(id);
            var authorizationResult = await _iAuthorizationService.AuthorizeAsync(User, model, Constants.Delete);

            if (authorizationResult.Succeeded)
            {
                _iResidenceBLL.RemoveResidenceImage(_iHostingEnvironment.ContentRootPath + Constants.wwwroot, id);
                _iResidenceBLL.RemoveResidence(id);
            }
            else if (User.Identity.IsAuthenticated)
            {
                return new ForbidResult();
            }
            else
            {
                return new ChallengeResult();
            }

            return RedirectToAction("Items");
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var model = _iResidenceBLL.GetResidence(id);
            model.RatingAverage = Math.Round(_iRatingBLL.CalculateAvarageResidence(id), 1);
            model.RatingTotalNumber = _iRatingBLL.CalculateTotalOfRatingsResidence(id);

            return View(model);
        }

        [HttpPost]
        [Route("residences/details/rate")]
        public void Rate(int residenceId, int ratingValue)
        {
            var userLoggedId = Convert.ToInt32(User.FindFirst(ClaimTypes.SerialNumber).Value);

            if (!_iRatingBLL.RateResidence(residenceId, userLoggedId, ratingValue))
            {
                throw new SystemException();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Search(string name, int minPrice, int? maxPrice)
        {
            var models = _iResidenceBLL.GetResidencesFiltered(name, minPrice, maxPrice);

            return PartialView("Partials/SearchPartial", models);
        }
    }
}
