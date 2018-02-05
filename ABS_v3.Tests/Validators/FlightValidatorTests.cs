using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Validators;
using ABS_v2.BusinessLogic.Validators;
using NUnit.Framework;
using System.Collections.Generic;

namespace ABS_v3.Tests.Validators
{
    [TestFixture]
    public class FlightValidatorTests
    {
        private IValidator<FlightBL> validator;

        [SetUp]
        public void SetUp()
        {
            validator = new FlightValidator();
        }

        [Test]
        [TestCase("", 1, 1, 2017, 1, 1, 1, 20.3, false)]
        [TestCase("Flight", 100, 1, 2017, 1, 1, 1, 20.3, false)]
        [TestCase("Flight", 1, 1, 2017, 0, 1, 1, 20.3, false)]
        [TestCase("Flight", 1, 1, 2017, 1, 1, 1, 0, false)]
        [TestCase("Flight", 1, 1, 2017, 1, 1, 1, 20.3, false)]
        [TestCase("Flight", 1, 1, 2017, 1, 1, 1, 20.3, false)]
        [TestCase("Flight", 1, 1, 2017, 1, 1, 2, 20.3, true)]


        public void ValidateFlight(string name, int day, int month, int year, int airlineId, int destinationAirportId, int originAirportId, decimal price, bool expected)
        {
            //Arramge
            var flight = new FlightBL(name, day, month, year, airlineId, destinationAirportId, originAirportId, price);

            ICollection<string> errors = new List<string>();

            //Act
            var result = flight.Validate(validator, out errors);

            //Assert
            Assert.That(result, Is.EqualTo(expected));


        }
    }
}
