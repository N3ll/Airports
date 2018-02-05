using ABS_v2.BusinesLogic.PresentationModels;
using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Repositories;
using ABS_v2.DataAccess.Entities;
using ABS_v2.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ABS_v2.BusinessLogic
{
    public class AirportRepository : IAirportRepository<AirportBL, Airport>

    {

        private IABSDbContext db { get; set; }

        public AirportRepository(IABSDbContext context)
        {
            this.db = context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="airport"></param>
        public void Add(AirportBL airport)
        {
            ICollection<Filter> filters = new List<Filter>();
            foreach (var filt in airport.Filters)
            {
                Filter flt = new Filter { Id = filt.Id, Name = filt.Name };
                db.Filters.Attach(flt);
                filters.Add(flt);
            }

            Airport airportDB = new Airport
            {
                Name = airport.Name,
                Latitude = airport.Latitude,
                Longitude = airport.Longitude,
                Filters = filters
            };

            db.Airports.Add(airportDB);
        }


        public AirportBL GetEntity(Expression<Func<Airport, bool>> filter)
        {
            AirportBL airportBL = null;
            Airport airport = db.Airports.Where(filter).SingleOrDefault();
            if (airport != null)
            {
                ICollection<FilterBL> filters = new List<FilterBL>();
                foreach (var filt in airport.Filters)
                {
                    filters.Add(new FilterBL(filt.Name));
                }

                airportBL = new AirportBL(airport.Id, airport.Name, airport.Latitude, airport.Longitude, filters);
            }
            return airportBL;
        }

        public void UpdateEntity(AirportBL entity)
        {
            throw new NotImplementedException();
        }


        public ICollection<AiportModel> GetAllAirports()
        {
            ICollection<AiportModel> airports = new List<AiportModel>();
            if (db.Airports != null)
            {
                ICollection<Airport> airprotsDB = db.Airports.ToList();

                foreach (var airport in airprotsDB)
                {
                    ICollection<string> filters = new List<string>();

                    ICollection<Filter> filtersDB = (from filter in db.Filters
                                                     from fl in filter.Airports
                                                     where fl.Name.Equals(airport.Name, StringComparison.InvariantCultureIgnoreCase)
                                                     select filter).ToList();
                    foreach (var filter in filtersDB)
                    {
                        filters.Add(filter.Name);
                    }

                    airports.Add(new AiportModel(airport.Name, airport.Latitude, airport.Longitude, filters));
                }
            }
            return airports;
        }


    }
}
