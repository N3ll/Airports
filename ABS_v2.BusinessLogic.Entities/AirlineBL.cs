using ABS_v2.BusinessLogic.Interfaces.Validators;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Entities
{
    public class AirlineBL : IValidatable<AirlineBL>
    {
        public int Id { get; set; }
        public string Name { get; set; }


        public AirlineBL(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public AirlineBL(string name) : this(0, name)
        {
        }

        public AirlineBL() : this(0, "")
        {
        }

        public bool Validate(IValidator<AirlineBL> validator, out ICollection<string> brokenRules)
        {
            brokenRules = validator.BrokenRules(this);
            return validator.IsValid(this);
        }
    }
}
