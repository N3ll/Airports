using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Interfaces.Validators
{
    public interface IValidatable<T>
    {
        bool Validate(IValidator<T> validator, out ICollection<string> brokenRules);
    }
}
