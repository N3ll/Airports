using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using System.Collections.Generic;
using System.Linq;

namespace ABS_v2.BusinessLogic.Validators
{
    public class SectionClassValidator : IValidator<SectionClassBL>
    {
        public bool IsValid(SectionClassBL entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public ICollection<string> BrokenRules(SectionClassBL entity)
        {
            ICollection<string> errors = new List<string>();
            if (string.IsNullOrEmpty(entity.Name))
                errors.Add("The class section must have a name.");

            return errors;
        }
    }
}
