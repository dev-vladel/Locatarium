using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Locatarium.Web.Models;
using Locatarium.BLL.IBusinessLogic;
using Microsoft.AspNetCore.Authorization;

namespace Locatarium.Web.Controllers
{
    public class HomeController : Controller
    {
        private IResidenceBLL _iResidenceBLL;

        public HomeController(IResidenceBLL iResidenceBLL)
        {
            _iResidenceBLL = iResidenceBLL;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult ResidencesCards(int toSkipNumber)
        {
            var model = _iResidenceBLL.GetResidencesSkip(toSkipNumber);

            return PartialView("Partials/ItemCards", model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
