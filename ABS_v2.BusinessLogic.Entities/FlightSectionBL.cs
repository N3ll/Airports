using ABS_v2.BusinessLogic.Interfaces.Validators;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Entities
{
    public class FlightSectionBL : IValidatable<FlightSectionBL>
    {
        public int Id { get; set; }
        public int SectionClassId { get; set; }
        public int FlightId { get; set; }

        public ICollection<SeatBL> Seats { get; set; }

        public FlightSectionBL(int id, int sectionClassId, int flightId, ICollection<SeatBL> seats)
        {
            Id = id;
            SectionClassId = sectionClassId;
            FlightId = flightId;
            Seats = seats;
        }

        public FlightSectionBL(int sectionClassId, int flightId, ICollection<SeatBL> seats) : this(0, sectionClassId, flightId, seats)
        {
        }

        public FlightSectionBL(int id, int sectionClassId, int flightId) : this(id, sectionClassId, flightId, new List<SeatBL>())
        {
        }

        public FlightSectionBL(int sectionClassId, int flightId) : this(0, sectionClassId, flightId, new List<SeatBL>())
        {
        }


        public bool Validate(IValidator<FlightSectionBL> validator, out ICollection<string> brokenRules)
        {
            brokenRules = validator.BrokenRules(this);
            return validator.IsValid(this);
        }
    }
}
