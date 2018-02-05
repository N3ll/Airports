using ABS_v2.BusinesLogic.PresentationModels;
using ABS_v2.BusinessLogic.Interfaces;
using ABS_v2.BusinessLogic.PresentationModels;
using ABS_v3.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ABS_v3.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISystemManager SysManager;
        public HomeController(ISystemManager sysManager)
        {
            SysManager = sysManager;
        }
        public ActionResult Index()
        {
            ICollection<SysDetailsViewModel> details = new List<SysDetailsViewModel>();
            var detailsDB = SysManager.DisplaySystemDitails();

            foreach (var detail in detailsDB)
            {
                details.Add(new SysDetailsViewModel(detail.FlightName, detail.AirlineName, detail.OriginAirportName, detail.DestinationAirportName, detail.SeatClass, detail.Row, detail.Col, detail.Status));
            }
            return View(details);
        }

        public ActionResult FindAvailableFlights()
        {
            var viewModel = new AvailableFlightViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult FindAvailableFlights(AvailableFlightViewModel viewModel)
        {
            ICollection<FlightViewModel> availableFlights = new List<FlightViewModel>();

            if (!ModelState.IsValid)
            {
                return View("FindAvailableFlights", viewModel);
            }
            else
            {
                ICollection<string> errors;
                ICollection<FlightModel> flights = SysManager.FindAvailableFlights(viewModel.OriginAirportName, viewModel.DestinationAirportName, viewModel.Year, viewModel.Month, viewModel.Day, out errors);
                if (errors.Count > 0)
                {
                    ViewBag.ErrorMessages = errors;
                    return View("FindAvailableFlights", viewModel);
                }
                else
                {
                    availableFlights = new List<FlightViewModel>();
                    foreach (var flight in flights)
                    {
                        availableFlights.Add(new FlightViewModel(flight.FlightName, flight.AirlineName, flight.OriginAirport, flight.DestinationAirport, flight.DepartureDate, flight.Price));
                    }
                }
            }

            return View("DisplayAvailableFlights", availableFlights);
        }

        public ActionResult DisplayAirportsFlights()
        {
            ICollection<AiportModel> airports = SysManager.GetAllAirports();
            ICollection<FlightModel> flights = SysManager.GetAllFlights();

            var viewModel = new AirportsFlightsViewModel(airports, flights);


            return View(viewModel);
        }

        public JsonResult GetModel()
        {
            ICollection<AiportModel> airports = SysManager.GetAllAirports();
            ICollection<FlightModel> flights = SysManager.GetAllFlights();
            var viewModel = new AirportsFlightsViewModel(airports, flights);
            return Json(viewModel, JsonRequestBehavior.AllowGet);
        }
    }
}