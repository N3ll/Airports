using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Repositories;
using ABS_v2.DataAccess.Entities;
using ABS_v2.DataAccess.Interfaces;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic
{
    public class AirlineRepository : IEntityRepository<AirlineBL, Airline>
    {
        private IABSDbContext db { get; set; }

        public AirlineRepository(IABSDbContext context)
        {
            db = context;
        }

        public void Add(AirlineBL airline)
        {
            Airline airlineDB = new Airline { Name = airline.Name };
            db.Airlines.Add(airlineDB);
        }

        public AirlineBL GetEntity(Expression<Func<Airline, bool>> filter)
        {
            AirlineBL airlineBL = null;
            Airline airline = db.Airlines.Where(filter).SingleOrDefault();
            if (airline != null)
            {
                airlineBL = new AirlineBL(airline.Id, airline.Name);
            }
            return airlineBL;
        }

        public void UpdateEntity(AirlineBL entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<AirlineBL> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
