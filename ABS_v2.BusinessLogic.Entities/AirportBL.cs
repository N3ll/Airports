using ABS_v2.BusinessLogic.Interfaces.Validators;
using System;
using System.Collections.Generic;
namespace ABS_v2.BusinessLogic.Entities
{
    public class AirportBL : IValidatable<AirportBL>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        private decimal latitude;
        public decimal Latitude
        {
            get { return Math.Round(latitude, 4); }
            set { latitude = Math.Round(value, 4); }
        }

        private decimal longitude;
        public decimal Longitude
        {
            get { return Math.Round(longitude, 4); }
            set { longitude = Math.Round(value, 4); }
        }

        public ICollection<FilterBL> Filters;

        public AirportBL(int id, string name, decimal latitude, decimal longitude, ICollection<FilterBL> filters)
        {
            Id = id;
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Filters = filters;
        }

        public AirportBL(string name) : this(0, name, 0, 0, new List<FilterBL>())
        {
        }

        public AirportBL(string name, decimal latitude, decimal longitude, ICollection<FilterBL> filters) : this(0, name, latitude, longitude, filters)
        {
        }

        public AirportBL() : this(0, "", 0, 0, new List<FilterBL>())
        {
        }

        public bool Validate(IValidator<AirportBL> validator, out ICollection<string> brokenRules)
        {
            brokenRules = validator.BrokenRules(this);
            return validator.IsValid(this);
        }
    }
}
