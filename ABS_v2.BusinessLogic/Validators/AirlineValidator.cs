using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace ABS_v2.BusinessLogic.Validators
{
    public class AirlineValidator : IValidator<AirlineBL>
    {
        public bool IsValid(AirlineBL entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public ICollection<string> BrokenRules(AirlineBL entity)
        {
            ICollection<string> errors = new List<string>();
            if (string.IsNullOrEmpty(entity.Name))
                errors.Add("The airline must have a name.");

            if (entity.Name.Length < 1 || entity.Name.Length > 6)
                errors.Add("The airline name must be from between 1 and 6 characters");

            return errors;
        }

    }
}
