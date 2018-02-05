using ABS_v2.BusinessLogic.Interfaces.Magagers;
using ABS_v2.BusinessLogic.PresentationModels;
using System;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Interfaces.Managers
{
    public interface IFlightManager<T> : IManager<T>
    {
        ICollection<FlightModel> FindAvailableFlights(string originAirport, string destinationAirport, DateTime departureDate);
        ICollection<SystemDetailsModel> GetSystemDetails();

        ICollection<FlightModel> GetAllFlights();
    }
}
