using ABS_v2.BusinessLogic.Interfaces;
using ABS_v3.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ABS_v3.Controllers
{
    public class FlightController : Controller
    {
        private readonly ISystemManager SysManager;
        public FlightController(ISystemManager sysManager)
        {
            SysManager = sysManager;
        }


        public ActionResult CreateSectionClass()
        {
            var viewModel = new SectionCLassViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateSectionClass(SectionCLassViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateSectionClass", viewModel);
            }
            else
            {
                ICollection<string> errors = SysManager.AddSectionClass(viewModel.Name);
                if (errors.Count > 0)
                {
                    ViewBag.ErrorMessages = errors;
                    return View("CreateSectionClass", viewModel);

                }
            }

            return RedirectToAction("Index", "Home");
        }



        public ActionResult CreateFlight()
        {
            var viewModel = new FlightViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateFlight(FlightViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {

                return View("CreateFlight", viewModel);
            }
            else
            {
                ICollection<string> errors = SysManager.AddFlight(viewModel.FlightName, viewModel.OriginAirportName, viewModel.DestinationAirportName, viewModel.AirlineName, viewModel.Day, viewModel.Month, viewModel.Year, viewModel.Price);
                if (errors.Count > 0)
                {
                    ViewBag.ErrorMessages = errors;
                    return View("CreateFlight", viewModel);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult CreateFlightSection()
        {
            var viewModel = new FlightSectionViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CreateFlightSection(FlightSectionViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateFlightSection", viewModel);
            }
            else
            {
                ICollection<string> errors = SysManager.AddFlightSection(viewModel.FlightName, viewModel.SectionCLass, viewModel.Rows, viewModel.Cols);
                if (errors.Count > 0)
                {
                    ViewBag.ErrorMessages = errors;
                    return View("CreateFlightSection", viewModel);
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult BookSeat()
        {
            var viewModel = new SeatViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult BookSeat(SeatViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                return View("BookSeat", viewModel);
            }
            else
            {
                ICollection<string> errors = SysManager.BookSeat(viewModel.FlightName, viewModel.SectionCLass, viewModel.Row, viewModel.Col);
                if (errors.Count > 0)
                {
                    ViewBag.ErrorMessages = errors;
                    return View("BookSeat", viewModel);
                }
            }
            return RedirectToAction("Index", "Home");
        }
    }
}