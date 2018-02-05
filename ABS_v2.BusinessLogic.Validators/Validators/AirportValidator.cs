using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace ABS_v2.BusinessLogic.Validators
{
    public class AirportValidator : IValidator<AirportBL>
    {
        public bool IsValid(AirportBL entity)
        {
            return BrokenRules(entity).Count() == 0;
        }


        public ICollection<string> BrokenRules(AirportBL entity)
        {
            ICollection<string> errors = new List<string>();
            if (string.IsNullOrEmpty(entity.Name))
                errors.Add("The airport must have a name.");

            if (entity.Name.Length != 3)
                errors.Add("The airport name must contain 3 characters");


            return errors;
        }
    }
}
