using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Repositories;
using ABS_v2.DataAccess.Entities;
using ABS_v2.DataAccess.Interfaces;
using System;
using System.Linq;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic
{
    public class SectionClassRepository : IEntityRepository<SectionClassBL, SectionClass>
    {
        private IABSDbContext db { get; set; }

        public SectionClassRepository(IABSDbContext context)
        {
            db = context;
        }

        public void Add(SectionClassBL sectionClass)
        {
            SectionClass sectionClassDB = new SectionClass { Name = sectionClass.Name };
            db.SectionClasses.Add(sectionClassDB);
        }

        public SectionClassBL GetEntityByName(string name)
        {
            SectionClassBL sectionClassBL = null;
            SectionClass sectionClass = db.SectionClasses.Where(a => a.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).SingleOrDefault();
            if (sectionClass != null)
            {
                sectionClassBL = new SectionClassBL(sectionClass.Id, sectionClass.Name);
            }
            return sectionClassBL;
        }

        public SectionClassBL GetEntity(System.Linq.Expressions.Expression<Func<SectionClass, bool>> filter)
        {
            SectionClassBL sectionClassBL = null;
            SectionClass sectionClass = db.SectionClasses.Where(filter).SingleOrDefault();
            if (sectionClass != null)
            {
                sectionClassBL = new SectionClassBL(sectionClass.Id, sectionClass.Name);
            }
            return sectionClassBL;
        }

        public void UpdateEntity(SectionClassBL entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<SectionClassBL> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}


