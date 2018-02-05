using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Managers;
using ABS_v2.BusinessLogic.Interfaces.Repositories;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using ABS_v2.BusinessLogic.PresentationModels;
using ABS_v2.BusinessLogic.Repositories;
using ABS_v2.BusinessLogic.Validators;
using System;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Managers
{
    public class FlightManager : IFlightManager<FlightBL>
    {
        private IValidator<FlightBL> Validator { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        public FlightManager(UnitOfWork unitOfWork)
        {
            Validator = new FlightValidator();
            UnitOfWork = unitOfWork;
        }

        public ICollection<string> Add(FlightBL flight)
        {
            ICollection<string> errorsValidator;
            bool isValid = flight.Validate(Validator, out errorsValidator);

            ICollection<string> errorsDB = new List<string>();
            if (isValid && !IsExistingInDb(flight, out errorsDB))
            {
                UnitOfWork.FlightRepo.Add(flight);
                UnitOfWork.Save();
            }

            foreach (var err in errorsDB)
            {
                errorsValidator.Add(err);
            }

            return errorsValidator;
        }

        public bool IsExistingInDb(FlightBL flight, out ICollection<string> errors)
        {
            errors = new List<string>();
            FlightBL flightBL = UnitOfWork.FlightRepo.GetEntity(f => f.Name.Equals(flight.Name, StringComparison.InvariantCultureIgnoreCase));
            if (flightBL != null)
            {
                errors.Add("There is already a flight with this name");
            }
            return flightBL != null;
        }

        public int GetEntityId(FlightBL flight)
        {
            FlightBL flightBL = UnitOfWork.FlightRepo.GetEntity(f => f.Name.Equals(flight.Name, StringComparison.InvariantCultureIgnoreCase));
            int id = 0;
            if (flightBL != null)
            {
                id = flightBL.Id;
            }
            return id;
        }

        public ICollection<FlightModel> FindAvailableFlights(string originAirport, string destinationAirport, DateTime departureDate)
        {

            ICollection<FlightModel> flights = new List<FlightModel>();
            flights = UnitOfWork.FlightRepo.FindAvailableFlights(originAirport, destinationAirport, departureDate);

            return flights;
        }

        public ICollection<SystemDetailsModel> GetSystemDetails()
        {
            ICollection<SystemDetailsModel> sysDetails = new List<SystemDetailsModel>();
            sysDetails = UnitOfWork.GetSystemDetails();
            return sysDetails;
        }

        public ICollection<FlightModel> GetAllFlights()
        {
            return UnitOfWork.FlightRepo.GetAllFlights();
        }
    }
}
