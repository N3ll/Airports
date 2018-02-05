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
    public class SeatRepository : IEntityRepository<SeatBL, Seat>
    {
        private IABSDbContext db { get; set; }

        public SeatRepository(IABSDbContext context)
        {
            db = context;
        }

        public void Add(SeatBL seat)
        {
            Seat seatDB = new Seat { Row = seat.Row, Col = seat.Col, FlightSectionId = seat.FlightSectionId, Status = seat.Status };
            db.Seats.Add(seatDB);

        }

        public SeatBL GetEntity(Expression<Func<Seat, bool>> filter)
        {
            SeatBL seatBL = null;
            Seat seat = db.Seats.Where(filter).SingleOrDefault();
            if (seat != null)
            {
                seatBL = new SeatBL(seat.Id, seat.Row, seat.Col, seat.FlightSectionId, seat.Status);
            }
            return seatBL;
        }

        public void UpdateEntity(SeatBL seat)
        {
            Seat seatToBook = db.Seats.Where(s => s.Id == seat.Id).SingleOrDefault();
            seatToBook.Status = seat.Status;
        }

        public ICollection<SeatBL> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
