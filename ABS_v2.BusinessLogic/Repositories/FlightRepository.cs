using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Repositories;
using ABS_v2.BusinessLogic.PresentationModels;
using ABS_v2.DataAccess.Entities;
using ABS_v2.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ABS_v2.BusinessLogic
{
    public class FlightRepository : IFlightRepository<FlightBL, Flight>

    {
        private IABSDbContext db { get; set; }

        public FlightRepository(IABSDbContext context)
        {
            this.db = context;
        }

        public void Add(FlightBL flight)
        {
            Flight flightDB = new Flight
            {
                Name = flight.Name,
                AirlineId = flight.AirlineId,
                DestinationAirportId = flight.DestinationAirportId,
                OriginAirportId = flight.OriginAirportId,
                DepartureDate = flight.DepartureDate,
                Price = flight.Price
            };

            db.Flights.Add(flightDB);
        }

        public FlightBL GetEntity(Expression<Func<Flight, bool>> filter)
        {
            FlightBL flightBL = null;
            Flight flight = db.Flights.Where(filter).SingleOrDefault();
            if (flight != null)
            {
                flightBL = new FlightBL(flight.Id, flight.Name, flight.DepartureDate.Day, flight.DepartureDate.Month, flight.DepartureDate.Year, flight.AirlineId, flight.DestinationAirportId, flight.OriginAirportId, flight.Price);
            }
            return flightBL;
        }


        public ICollection<FlightModel> FindAvailableFlights(string originAirport, string destinationAirport, DateTime departureDate)
        {
            ICollection<FindAvailableFlightsReturnModel> flightsDB = db.FindAvailableFlights(originAirport, destinationAirport, departureDate);
            ICollection<FlightModel> flights = new List<FlightModel>();

            foreach (var flight in flightsDB)
            {
                flights.Add(new FlightModel(flight.FlightName, flight.AirlineName, flight.OriginAirport, flight.DestinationAIrport, flight.DepartureDate, flight.Price));
            }

            return flights;
        }

        public ICollection<FlightModel> GetAllFlights()
        {
            ICollection<FlightModel> flights = new List<FlightModel>();
            var flightsDB = db.Flights.ToList();

            foreach (var flight in flightsDB)
            {
                string airlineName = db.Airlines.Where(a => a.Id == flight.AirlineId).SingleOrDefault().Name;
                string originAirportName = db.Airports.Where(ao => ao.Id == flight.OriginAirportId).SingleOrDefault().Name;
                string destinationAirportName = db.Airports.Where(ad => ad.Id == flight.DestinationAirportId).SingleOrDefault().Name;
                flights.Add(new FlightModel(flight.Name, airlineName, originAirportName, destinationAirportName, flight.DepartureDate, flight.Price));
            }

            return flights;
        }

        public void UpdateEntity(FlightBL entity)
        {
            throw new NotImplementedException();
        }

    }
}
