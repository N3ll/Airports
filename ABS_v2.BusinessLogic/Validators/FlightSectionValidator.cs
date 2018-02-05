using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace ABS_v2.BusinessLogic.Validators
{
    public class FlightSectionValidator : IValidator<FlightSectionBL>
    {
        public bool IsValid(FlightSectionBL entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public ICollection<string> BrokenRules(FlightSectionBL entity)
        {
            ICollection<string> errors = new List<string>();

            if (entity.SectionClassId == 0)
                errors.Add("Please specify a valid section class id");

            if (entity.FlightId == 0)
                errors.Add("Please specify a valid flight id");

            return errors;
        }

    }
}
