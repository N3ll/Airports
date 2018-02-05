using ABS_v2.BusinessLogic.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABS_v2.BusinessLogic.Entities
{
    public class FilterBL
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public FilterBL(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public FilterBL(string name) : this(0, name)
        {
        }

        public FilterBL() : this(0, "")
        {
        }


        public bool Validate(IValidator<FilterBL> validator, out ICollection<string> brokenRules)
        {
            brokenRules = validator.BrokenRules(this);
            return validator.IsValid(this);
        }
    }
}

