using ABS_v2.BusinessLogic.Interfaces;
using ABS_v3.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ABS_v3.Controllers
{
    public class AirportController : Controller
    {
        private readonly ISystemManager SysManager;
        public AirportController(ISystemManager sysManager)
        {
            SysManager = sysManager;
        }
        public ActionResult Create()
        {
            var viewModel = new AirportViewModel()
            {
                FiltersDB = new MultiSelectList(SysManager.GetAllFilters())
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(AirportViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.FiltersDB = new MultiSelectList(SysManager.GetAllFilters());
                return View("Create", viewModel);
            }
            else
            {
                ICollection<string> errors = SysManager.AddAirport(viewModel.Name, viewModel.Latitude, viewModel.Longitude, viewModel.Filters);
                if (errors.Count > 0)
                {
                    ViewBag.ErrorMessages = errors;
                    viewModel.FiltersDB = new MultiSelectList(SysManager.GetAllFilters());
                    return View("Create", viewModel);
                }
            }

            return RedirectToAction("Index", "Home");
        }


        public ActionResult CreateAirportFilter()
        {
            var viewModel = new AirportFilterViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateAirportFilter(AirportFilterViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateAirportFilter", viewModel);
            }
            else
            {
                ICollection<string> errors = SysManager.AddFlightFilter(viewModel.Name);
                if (errors.Count > 0)
                {
                    ViewBag.ErrorMessages = errors;
                    return View("CreateAirportFilter", viewModel);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}