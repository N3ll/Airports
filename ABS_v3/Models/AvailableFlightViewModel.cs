using System.ComponentModel.DataAnnotations;

namespace ABS_v3.Models
{
    public class AvailableFlightViewModel
    {

        [Display(Name = "Origin airport name")]
        [Required]
        public string OriginAirportName { get; set; }

        [Display(Name = "Destination airport name")]
        [Required]
        public string DestinationAirportName { get; set; }


        [Required]
        public int Day { get; set; }

        [Required]
        public int Month { get; set; }

        [Required]
        public int Year { get; set; }

        public AvailableFlightViewModel(string originAirport, string destinationAirport, int day, int month, int year)
        {
            OriginAirportName = originAirport;
            DestinationAirportName = destinationAirport;
            Day = day;
            Month = month;
            Year = year;
        }

        public AvailableFlightViewModel() : this("", "", 0, 0, 0)
        {
        }
    }
}
