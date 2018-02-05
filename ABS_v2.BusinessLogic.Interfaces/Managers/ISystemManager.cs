using ABS_v2.BusinesLogic.PresentationModels;
using ABS_v2.BusinessLogic.PresentationModels;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Interfaces
{
    public interface ISystemManager
    {
        ICollection<string> AddAirport(string name, decimal latitude, decimal longitude, ICollection<string> filters);
        ICollection<string> AddAirline(string name);

        ICollection<string> AddFlight(string flightName, string originAirportName, string destinationAirportName, string airlineName, int day, int month, int year, decimal price);

        ICollection<string> AddFlightSection(string flightName, string sectionClass, int rows, int cols);
        ICollection<string> AddSectionClass(string name);

        ICollection<string> AddFlightFilter(string name);

        ICollection<string> BookSeat(string flightName, string sectionClass, int row, string col);

        ICollection<FlightModel> FindAvailableFlights(string originAirport, string destinationAirport, int year, int month, int day, out ICollection<string> errors);

        //string DisplaySystemDitails();
        ICollection<SystemDetailsModel> DisplaySystemDitails();
        ICollection<string> GetAllFilters();

        ICollection<AiportModel> GetAllAirports();
        ICollection<FlightModel> GetAllFlights();
    }
}
