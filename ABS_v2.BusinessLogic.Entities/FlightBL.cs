using ABS_v2.BusinessLogic.Interfaces.Validators;
using System;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Entities
{
    public class FlightBL : IValidatable<FlightBL>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public int AirlineId { get; set; }
        public int DestinationAirportId { get; set; }
        public int OriginAirportId { get; set; }

        private decimal price;
        public decimal Price
        {
            get { return Math.Round(price, 4); }
            set { price = Math.Round(value, 4); }
        }


        private DateTime departureDate;

        public DateTime DepartureDate
        {
            get
            {
                return new DateTime(Year, Month, Day);
            }
        }

        public FlightBL(int id, string name, int day, int month, int year, int airlineId, int destinationAirportId, int originAirportId, decimal price
            )
        {
            Id = id;
            Name = name;
            Day = day;
            Month = month;
            Year = year;
            AirlineId = airlineId;
            DestinationAirportId = destinationAirportId;
            OriginAirportId = originAirportId;
            Price = price;
        }

        public FlightBL(string name, int day, int month, int year, int airlineId, int destinationAirportId, int originAirportId, decimal price)
            : this(0, name, day, month, year, airlineId, destinationAirportId, originAirportId, price)
        {
        }

        public FlightBL(string name) : this(0, name, 0, 0, 0, 0, 0, 0, 0)
        {
        }

        public FlightBL() : this(0, "", 0, 0, 0, 0, 0, 0, 0)
        {
        }

        public bool Validate(IValidator<FlightBL> validator, out ICollection<string> brokenRules)
        {
            brokenRules = validator.BrokenRules(this);
            return validator.IsValid(this);
        }
    }
}
