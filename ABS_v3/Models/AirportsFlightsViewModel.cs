using ABS_v2.BusinesLogic.PresentationModels;
using ABS_v2.BusinessLogic.PresentationModels;
using System.Collections.Generic;

namespace ABS_v3.Models
{
    public class AirportsFlightsViewModel
    {
        public ICollection<AiportModel> Airports { get; set; }
        public ICollection<FlightModel> Flights { get; set; }
        public AirportsFlightsViewModel(ICollection<AiportModel> airports, ICollection<FlightModel> flights)
        {
            Airports = airports;
            Flights = flights;
        }
    }
}