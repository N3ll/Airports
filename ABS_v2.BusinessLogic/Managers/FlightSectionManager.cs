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
    public class FlightSectionManager : IFlightSectionManager<FlightSectionBL>
    {
        private IValidator<FlightSectionBL> Validator { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        public FlightSectionManager(UnitOfWork unitOfWork)
        {
            Validator = new FlightSectionValidator();
            UnitOfWork = unitOfWork;
        }
        public ICollection<string> Add(FlightSectionBL flightSection)
        {
            ICollection<string> errorsValidator;
            bool isValid = flightSection.Validate(Validator, out errorsValidator);

            ICollection<string> errorsDB = new List<string>();
            if (isValid && !IsExistingInDb(flightSection, out errorsDB))
            {
                UnitOfWork.FlightSectionRepo.Add(flightSection);
                UnitOfWork.Save();
            }

            foreach (var err in errorsDB)
            {
                errorsValidator.Add(err);
            }

            return errorsValidator;
        }

        public bool IsExistingInDb(FlightSectionBL flightSection, out ICollection<string> errors)
        {
            errors = new List<string>();
            FlightSectionBL flightSectionBL = UnitOfWork.FlightSectionRepo.GetEntity(fs => fs.SectionClassId == flightSection.SectionClassId && fs.FlightId == flightSection.FlightId);

            if (flightSectionBL != null)
            {
                errors.Add("There is already a flight section with this name");
            }
            return flightSectionBL != null;
        }

        public int GetEntityId(FlightSectionBL entity)
        {
            FlightSectionBL flightSectionBL = UnitOfWork.FlightSectionRepo.GetEntity(fs => fs.SectionClassId == entity.SectionClassId && fs.FlightId == entity.FlightId);
            int id = 0;
            if (flightSectionBL != null)
            {
                id = flightSectionBL.Id;
            }
            return id;
        }

        public ICollection<FlightSectionBL> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
