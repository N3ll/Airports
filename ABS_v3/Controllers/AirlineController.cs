using ABS_v2.BusinessLogic.Interfaces;
using ABS_v3.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ABS_v3.Controllers
{
    public class AirlineController : Controller
    {
        private readonly ISystemManager SysManager;
        public AirlineController(ISystemManager sysManager)
        {
            SysManager = sysManager;
        }
        public ActionResult Create()
        {
            var viewModel = new AirlineViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Create(AirlineViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", viewModel);
            }
            else
            {
                ICollection<string> errors = SysManager.AddAirline(viewModel.Name);
                if (errors.Count > 0)
                {
                    ViewBag.ErrorMessages = errors;
                    return View("Create", viewModel);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}