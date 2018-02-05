using ABS_v2.BusinessLogic.PresentationModels;
using System;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Interfaces.Repositories
{
    public interface IFlightRepository<T, E> : IEntityRepository<T, E>
    {
        ICollection<FlightModel> FindAvailableFlights(string originAirport, string destinationAirport, DateTime departureDate);

        ICollection<FlightModel> GetAllFlights();

    }
}
