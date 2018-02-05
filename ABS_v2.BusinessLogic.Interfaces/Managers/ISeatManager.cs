using ABS_v2.BusinessLogic.Interfaces.Magagers;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Interfaces.Managers
{
    public interface ISeatManager<T> : IManager<T>
    {
        ICollection<string> BookSeat(T entity);
    }
}
