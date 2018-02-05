using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Managers;
using ABS_v2.BusinessLogic.Interfaces.Repositories;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using ABS_v2.BusinessLogic.Repositories;
using ABS_v2.BusinessLogic.Validators;
using System;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Managers
{
    public class SeatManager : ISeatManager<SeatBL>
    {
        private IValidator<SeatBL> Validator { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        public SeatManager(UnitOfWork unitOfWork)
        {
            Validator = new SeatValidator();
            UnitOfWork = unitOfWork;
        }
        public ICollection<string> Add(SeatBL seat)
        {
            ICollection<string> errorsValidator;
            bool isValid = seat.Validate(Validator, out errorsValidator);

            ICollection<string> errorsDB = new List<string>();
            if (isValid && !IsExistingInDb(seat, out errorsDB))
            {
                UnitOfWork.SeatRepo.Add(seat);
                UnitOfWork.Save();
            }

            foreach (var err in errorsDB)
            {
                errorsValidator.Add(err);
            }

            return errorsValidator;
        }

        public bool IsExistingInDb(SeatBL seat, out ICollection<string> errors)
        {
            errors = new List<string>();
            SeatBL seatBL = UnitOfWork.SeatRepo.GetEntity(s => s.Row == seat.Row && s.Col.Equals(seat.Col, StringComparison.InvariantCultureIgnoreCase) && s.FlightSectionId == seat.FlightSectionId);

            if (seatBL != null)
            {
                errors.Add("There is already a seat with this name");
            }
            return seatBL != null;
        }

        public int GetEntityId(SeatBL entity)
        {
            SeatBL seatBL = UnitOfWork.SeatRepo.GetEntity(s => s.FlightSectionId == entity.FlightSectionId && s.Row == entity.Row && s.Col == entity.Col);
            int id = 0;
            if (seatBL != null)
            {
                id = seatBL.Id;
            }
            return id;
        }

        public ICollection<string> BookSeat(SeatBL seatToBook)
        {
            ICollection<string> errors = new List<string>();
            SeatBL seatBL = UnitOfWork.SeatRepo.GetEntity(s => s.Row == seatToBook.Row && s.Col == seatToBook.Col && s.FlightSectionId == seatToBook.FlightSectionId);
            if (seatBL == null)
            {
                errors.Add("There is no such seat");
            }
            else
            {
                if (seatBL.Status)
                {
                    errors.Add("Seat is already booked");
                }
                else
                {
                    seatBL.Status = true;
                    UnitOfWork.SeatRepo.UpdateEntity(seatBL);
                    UnitOfWork.Save();
                }
            }
            return errors;
        }

        public ICollection<SeatBL> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
