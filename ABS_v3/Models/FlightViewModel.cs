using System;
using System.ComponentModel.DataAnnotations;

namespace ABS_v3.Models
{
    public class FlightViewModel
    {
        [Display(Name = "Flight name")]
        [Required]
        public string FlightName { get; set; }

        [Display(Name = "Origin airport name")]
        [Required]
        public string OriginAirportName { get; set; }

        [Display(Name = "Destination airport name")]
        [Required]
        public string DestinationAirportName { get; set; }

        [Display(Name = "Airline name")]
        [Required]
        public string AirlineName { get; set; }

        [Required]
        private decimal price;

        public decimal Price
        {
            get { return Math.Round(price, 4); ; }
            set { price = Math.Round(value, 4); }
        }


        [Required]
        public int Day { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        public DateTime DepartureDate { get; set; }



        public FlightViewModel(string flightName, string airlineName, string originAirport, string destinationAirport, int day, int month, int year, decimal price)
        {
            FlightName = flightName;
            AirlineName = airlineName;
            OriginAirportName = originAirport;
            DestinationAirportName = destinationAirport;
            Day = day;
            Month = month;
            Year = year;
            Price = price;
        }
        public FlightViewModel(string flightName, string airlineName, string originAirport, string destinationAirport, DateTime departureDate, decimal price)
        {
            FlightName = flightName;
            AirlineName = airlineName;
            OriginAirportName = originAirport;
            DestinationAirportName = destinationAirport;
            DepartureDate = departureDate;
            Price = price;
        }
        public FlightViewModel() : this("", "", "", "", 0, 0, 0, 0)
        {
        }
    }
}