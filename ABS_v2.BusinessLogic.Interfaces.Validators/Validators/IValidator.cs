using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Interfaces.Validators
{
    public interface IValidator<T>
    {
        bool IsValid(T entity);
        ICollection<string> BrokenRules(T entity);
    }
}
