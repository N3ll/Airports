using ABS_v2.BusinessLogic.Interfaces.Validators;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Entities
{
    public class SeatBL : IValidatable<SeatBL>
    {
        public int Id { get; set; }
        public int Row { get; set; }
        public string Col { get; set; }
        public int FlightSectionId { get; set; }
        public bool Status { get; set; }

        public SeatBL(int id, int row, string col, int flightSectionId, bool status)
        {
            Id = id;
            Row = row;
            Col = col;
            FlightSectionId = flightSectionId;
            Status = status;
        }

        public SeatBL(int row, string col) : this(0, row, col, 0, false)
        {
        }

        public SeatBL(int row, string col, int flightSectionId) : this(0, row, col, flightSectionId, false)
        {
        }

        public SeatBL(int row, string col, int flightSectionId, bool status) : this(0, row, col, flightSectionId, status)
        {
        }


        public bool Validate(IValidator<SeatBL> validator, out ICollection<string> brokenRules)
        {
            brokenRules = validator.BrokenRules(this);
            return validator.IsValid(this);
        }
    }
}
