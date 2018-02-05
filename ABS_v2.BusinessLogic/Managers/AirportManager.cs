using ABS_v2.BusinesLogic.PresentationModels;
using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Managers;
using ABS_v2.BusinessLogic.Interfaces.Repositories;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using ABS_v2.BusinessLogic.Repositories;
using ABS_v2.BusinessLogic.Validators;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Managers
{
    public class AirportManager : IAirportManager<AirportBL>
    {
        private IValidator<AirportBL> Validator { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        public AirportManager(UnitOfWork unitOfWork)
        {
            Validator = new AirportValidator();
            UnitOfWork = unitOfWork;
        }
        public ICollection<string> Add(AirportBL airport)
        {
            ICollection<string> errorsValidator;
            bool isValid = airport.Validate(Validator, out errorsValidator);

            ICollection<string> errorsDB = new List<string>();
            if (isValid && !IsExistingInDb(airport, out errorsDB))
            {
                UnitOfWork.AirportRepo.Add(airport);
                UnitOfWork.Save();
            }

            foreach (var err in errorsDB)
            {
                errorsValidator.Add(err);
            }

            return errorsValidator;
        }


        public bool IsExistingInDb(AirportBL airport, out ICollection<string> errors)
        {
            errors = new List<string>();
            AirportBL airportBL = UnitOfWork.AirportRepo.GetEntity(a => a.Name.Equals(airport.Name, System.StringComparison.InvariantCultureIgnoreCase));
            if (airportBL != null)
            {
                errors.Add("There is already an airport with this name");
            }
            return airportBL != null;
        }

        public int GetEntityId(AirportBL airport)
        {
            AirportBL airportBL = UnitOfWork.AirportRepo.GetEntity(a => a.Name.Equals(airport.Name, System.StringComparison.InvariantCultureIgnoreCase));
            int id = 0;
            if (airportBL != null)
            {
                id = airportBL.Id;
            }
            return id;
        }


        public ICollection<AiportModel> GetAllAirports()
        {
            return UnitOfWork.AirportRepo.GetAllAirports();
        }


    }
}
