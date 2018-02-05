using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Interfaces.Magagers
{
    public interface IManager<T>
    {
        ICollection<string> Add(T entity);
        bool IsExistingInDb(T entity, out ICollection<string> errors);
        int GetEntityId(T entity);
    }
}
