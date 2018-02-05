using ABS_v2.BusinesLogic.PresentationModels;
using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Repositories;
using ABS_v2.DataAccess.Entities;
using ABS_v2.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ABS_v2.BusinessLogic.Repositories
{
    class FilterRepository : IFilterRepository<FilterBL, Filter>
    {
        private IABSDbContext db { get; set; }

        public FilterRepository(IABSDbContext context)
        {
            db = context;
        }

        public void Add(FilterBL filter)
        {
            Filter filterDB = new Filter { Name = filter.Name };
            db.Filters.Add(filterDB);
        }

        public FilterBL GetEntityByName(string name)
        {
            FilterBL filterBL = null;
            Filter filter = db.Filters.Where(a => a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
            if (filter != null)
            {
                filterBL = new FilterBL(filter.Id, filter.Name);
            }
            return filterBL;
        }

        public FilterBL GetEntity(System.Linq.Expressions.Expression<Func<Filter, bool>> filter)
        {
            FilterBL sectionClassBL = null;
            Filter filt = db.Filters.Where(filter).SingleOrDefault();
            if (filt != null)
            {
                sectionClassBL = new FilterBL(filt.Id, filt.Name);
            }
            return sectionClassBL;
        }


        public void UpdateEntity(FilterBL entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<FilterModel> GetAllFilters()
        {
            ICollection<FilterModel> filters = new List<FilterModel>();
            ICollection<Filter> filtersDB = db.Filters.ToList();

            foreach (var filt in filtersDB)
            {
                filters.Add(new FilterModel(filt.Name));
            }

            return filters;
        }
    }
}
