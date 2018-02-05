using ABS_v2.BusinesLogic.PresentationModels;
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
    public class FilterManager : IFilterManager<FilterBL>
    {
        private IValidator<FilterBL> Validator { get; set; }
        private IUnitOfWork UnitOfWork { get; set; }

        public FilterManager(UnitOfWork unitOfWork)
        {
            Validator = new FilterValidator();
            UnitOfWork = unitOfWork;
        }
        public ICollection<string> Add(FilterBL filter)
        {
            ICollection<string> errorsValidator;
            bool isValid = filter.Validate(Validator, out errorsValidator);

            ICollection<string> errorsDB = new List<string>();
            if (isValid && !IsExistingInDb(filter, out errorsDB))
            {
                UnitOfWork.FilterRepo.Add(filter);
                UnitOfWork.Save();
            }

            foreach (var err in errorsDB)
            {
                errorsValidator.Add(err);
            }

            return errorsValidator;
        }

        public bool IsExistingInDb(FilterBL entity, out ICollection<string> errors)
        {
            errors = new List<string>();
            FilterBL filterBL = UnitOfWork.FilterRepo.GetEntity(f => f.Name.Equals(entity.Name, StringComparison.InvariantCultureIgnoreCase));

            if (filterBL != null)
            {
                errors.Add("There is already a flight filter with this name");
            }
            return filterBL != null;
        }

        public int GetEntityId(FilterBL entity)
        {
            FilterBL filterBL = UnitOfWork.FilterRepo.GetEntity(f => f.Name.Equals(entity.Name, System.StringComparison.InvariantCultureIgnoreCase));
            int id = 0;
            if (filterBL != null)
            {
                id = filterBL.Id;
            }
            return id;
        }

        public ICollection<FilterModel> GetAllFilters()
        {
            return UnitOfWork.FilterRepo.GetAllFilters();
        }
    }
}
