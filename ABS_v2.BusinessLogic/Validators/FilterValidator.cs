using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABS_v2.BusinessLogic.Validators
{
    public class FilterValidator : IValidator<FilterBL>
    {
        public bool IsValid(FilterBL entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public ICollection<string> BrokenRules(FilterBL entity)
        {
            ICollection<string> errors = new List<string>();
            if (string.IsNullOrEmpty(entity.Name))
                errors.Add("The filter section must have a name.");

            return errors;
        }
    }
}
