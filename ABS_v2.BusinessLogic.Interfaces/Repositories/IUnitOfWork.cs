using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.PresentationModels;
using ABS_v2.DataAccess.Entities;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IAirportRepository<AirportBL, Airport> AirportRepo { get; }
        IEntityRepository<AirlineBL, Airline> AirlineRepo { get; }
        IFlightRepository<FlightBL, Flight> FlightRepo { get; }
        IEntityRepository<FlightSectionBL, FlightSection> FlightSectionRepo { get; }
        IEntityRepository<SeatBL, Seat> SeatRepo { get; }
        IEntityRepository<SectionClassBL, SectionClass> SectionClassRepo { get; }
        IFilterRepository<FilterBL, Filter> FilterRepo { get; }

        void Save();
        ICollection<SystemDetailsModel> GetSystemDetails();
    }
}
