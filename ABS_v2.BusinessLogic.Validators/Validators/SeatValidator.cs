using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace ABS_v2.BusinessLogic.Validators
{
    public class SeatValidator : IValidator<SeatBL>
    {
        public bool IsValid(SeatBL entity)
        {
            return BrokenRules(entity).Count() == 0;
        }

        public ICollection<string> BrokenRules(SeatBL entity)
        {
            ICollection<string> errors = new List<string>();
            if (!(entity.Row >= 1 && entity.Row <= 100))
                errors.Add("The row number should be between 1 and 100");

            if (!IsEqualToChars(entity.Col))
                errors.Add("The column must be equal to one of the following letter ABCDEFGHIJ");

            return errors;
        }

        private bool IsEqualToChars(string seat)
        {
            const string PATTERN = @"[a-jA-J]";
            Console.WriteLine(Regex.IsMatch(seat, PATTERN));
            return Regex.IsMatch(seat, PATTERN);
        }
    }
}
