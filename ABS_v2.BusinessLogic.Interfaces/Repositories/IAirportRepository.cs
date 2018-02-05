using ABS_v2.BusinesLogic.PresentationModels;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Interfaces.Repositories
{
    public interface IAirportRepository<T, E> : IEntityRepository<T, E>
    {
        ICollection<AiportModel> GetAllAirports();
    }
}
