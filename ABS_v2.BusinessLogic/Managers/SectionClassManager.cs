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
    public class SectionClassManager : ISectionClassManager<SectionClassBL>
    {
        private IValidator<SectionClassBL> Validator { get; set; }
        private readonly IUnitOfWork UnitOfWork;

        public SectionClassManager(UnitOfWork unitOfWork)
        {
            Validator = new SectionClassValidator();
            UnitOfWork = unitOfWork;
        }
        public ICollection<string> Add(SectionClassBL sectionClass)
        {
            ICollection<string> errorsValidator;
            bool isValid = sectionClass.Validate(Validator, out errorsValidator);

            ICollection<string> errorsDB = new List<string>();
            if (isValid && !IsExistingInDb(sectionClass, out errorsDB))
            {
                UnitOfWork.SectionClassRepo.Add(sectionClass);
                UnitOfWork.Save();
            }

            foreach (var err in errorsDB)
            {
                errorsValidator.Add(err);
            }

            return errorsValidator;
        }

        public bool IsExistingInDb(SectionClassBL sectionClass, out ICollection<string> errors)
        {
            errors = new List<string>();
            SectionClassBL sectionClassBL = UnitOfWork.SectionClassRepo.GetEntity(s => s.Name.Equals(sectionClass.Name, System.StringComparison.InvariantCultureIgnoreCase));
            if (sectionClassBL != null)
            {
                errors.Add("There is already a section class with this name");
            }
            return sectionClassBL != null;
        }

        public int GetEntityId(SectionClassBL sectionClass)
        {
            SectionClassBL sectionCLassBL = UnitOfWork.SectionClassRepo.GetEntity(s => s.Name.Equals(sectionClass.Name, System.StringComparison.InvariantCultureIgnoreCase));
            int id = 0;
            if (sectionCLassBL != null)
            {
                id = sectionCLassBL.Id;
            }
            return id;
        }

        public ICollection<SectionClassBL> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
