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
    public class FlightSectionRepository : IEntityRepository<FlightSectionBL, FlightSection>

    {
        private IABSDbContext db { get; set; }

        public FlightSectionRepository(IABSDbContext context)
        {
            this.db = context;
        }

        public void Add(FlightSectionBL flightSection)
        {
            ICollection<Seat> seats = new List<Seat>();

            foreach (var seat in flightSection.Seats)
            {
                seats.Add(new Seat { Row = seat.Row, Col = seat.Col, FlightSectionId = seat.FlightSectionId, Status = seat.Status });
            }

            FlightSection flightSectionDB = new FlightSection { SectionClassId = flightSection.SectionClassId, FlightId = flightSection.FlightId, Seats = seats };
            db.FlightSections.Add(flightSectionDB);
        }


        public FlightSectionBL GetEntity(Expression<Func<FlightSection, bool>> filter)
        {
            FlightSectionBL flightSectionBL = null;
            FlightSection flightSection = db.FlightSections.Where(filter).SingleOrDefault();
            if (flightSection != null)
            {
                flightSectionBL = new FlightSectionBL(flightSection.Id, flightSection.SectionClassId, flightSection.FlightId);
            }
            return flightSectionBL;
        }

        public void UpdateEntity(FlightSectionBL entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<FlightSectionBL> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}

