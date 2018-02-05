using System;

namespace ABS_v2.BusinessLogic.PresentationModels
{
    public class FlightModel
    {
        public string FlightName { get; set; }
        public string AirlineName { get; set; }
        public string OriginAirport { get; set; }
        public string DestinationAirport { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal Price { get; set; }



        public FlightModel(string flightName, string airlineName, string originAirport, string destinationAirport, DateTime departureDate, decimal price)
        {
            FlightName = flightName;
            AirlineName = airlineName;
            OriginAirport = originAirport;
            DestinationAirport = destinationAirport;
            DepartureDate = departureDate;
            Price = price;
        }
    }
}
