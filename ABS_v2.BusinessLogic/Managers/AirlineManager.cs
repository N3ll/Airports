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
    public class AirlineManager : IAirlineManager<AirlineBL>
    {
        private IValidator<AirlineBL> Validator { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        public AirlineManager(UnitOfWork unitOfWork)
        {
            Validator = new AirlineValidator();
            UnitOfWork = unitOfWork;
        }

        public ICollection<string> Add(AirlineBL airline)
        {
            ICollection<string> errorsValidator;
            bool isValid = airline.Validate(Validator, out errorsValidator);

            ICollection<string> errorsDB = new List<string>();
            if (isValid && !IsExistingInDb(airline, out errorsDB))
            {
                UnitOfWork.AirlineRepo.Add(airline);
                UnitOfWork.Save();
            }

            foreach (var err in errorsDB)
            {
                errorsValidator.Add(err);
            }

            return errorsValidator;
        }

        public bool IsExistingInDb(AirlineBL airline, out ICollection<string> errors)
        {
            errors = new List<string>();
            AirlineBL airlineBL = UnitOfWork.AirlineRepo.GetEntity(a => a.Name.Equals(airline.Name, System.StringComparison.InvariantCultureIgnoreCase));

            if (airlineBL != null)
            {
                errors.Add("There is already an airline with this name");
            }
            return airlineBL != null;
        }

        public int GetEntityId(AirlineBL airline)
        {
            AirlineBL airlineBL = UnitOfWork.AirlineRepo.GetEntity(a => a.Name.Equals(airline.Name, System.StringComparison.InvariantCultureIgnoreCase));
            int id = 0;
            if (airlineBL != null)
            {
                id = airlineBL.Id;
            }
            return id;
        }

        public ICollection<AirlineBL> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
