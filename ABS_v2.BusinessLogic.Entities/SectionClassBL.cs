using ABS_v2.BusinessLogic.Interfaces.Validators;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Entities
{
    public class SectionClassBL : IValidatable<SectionClassBL>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public SectionClassBL(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public SectionClassBL(string name) : this(0, name)
        {
        }

        public SectionClassBL() : this(0, "")
        {
        }


        public bool Validate(IValidator<SectionClassBL> validator, out ICollection<string> brokenRules)
        {
            brokenRules = validator.BrokenRules(this);
            return validator.IsValid(this);
        }
    }
}
