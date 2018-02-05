using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABS_v2.BusinessLogic.Validators
{
    public class FlightValidator : IValidator<FlightBL>
    {
        public bool IsValid(FlightBL entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public ICollection<string> BrokenRules(FlightBL entity)
        {
            ICollection<string> errors = new List<string>();
            if (string.IsNullOrEmpty(entity.Name))
                errors.Add("The flight must have a name.");

            if (entity.AirlineId == 0)
                errors.Add("Please specify a valid airline id");

            if (entity.DestinationAirportId == 0)
                errors.Add("Please specify a valid destination airport id");

            if (entity.OriginAirportId == 0)
                errors.Add("Please specify a valid origin airport id.");

            if (entity.DestinationAirportId == entity.OriginAirportId)
                errors.Add("Origin and destination airport should be different");

            if (entity.Day == 0)
                errors.Add("The flight must have a day.");

            if (entity.Month == 0)
                errors.Add("The flight must have a month.");

            if (entity.Year == 0)
                errors.Add("The flight must have a year.");

            DateTime departDate;
            if (!DateTime.TryParse(string.Format("{0}/{1}/{2}", entity.Year, entity.Month, entity.Day), out departDate))
            {
                errors.Add("Please enter a valid date.");
            };

            return errors;
        }

    }
}
