using ABS_v2.BusinesLogic.PresentationModels;
using ABS_v2.BusinessLogic.Interfaces.Magagers;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Interfaces.Managers
{
    public interface IFilterManager<T> : IManager<T>
    {
        ICollection<FilterModel> GetAllFilters();
    }
}
