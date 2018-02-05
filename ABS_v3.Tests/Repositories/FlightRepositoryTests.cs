using ABS_v2.BusinessLogic;
using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Repositories;
using ABS_v2.BusinessLogic.PresentationModels;
using ABS_v2.DataAccess.Context;
using ABS_v2.DataAccess.Entities;
using ABS_v2.DataAccess.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ABS_v3.Tests.Repositories
{
    [TestFixture]
    public class FlightRepositoryTests
    {
        private IFlightRepository<FlightBL, Flight> flightRepository;
        private IABSDbContext context;

        [SetUp]
        public void SetUp()
        {
            context = new FakeABSDbContext();

            var filters = new List<Filter>
            {
                new Filter
                {
                    Id=1,
                    Name = "Sun"
                }
            };

            var airlines = new List<Airline>
            {
                new Airline
                {
                    Id=1,
                    Name = "Color"
                }
            };

            var airports = new List<Airport>
            {
                new Airport
                {
                    Id=1,
                    Name = "AAA",
                    Latitude = 43,
                    Longitude=12,
                    Filters= filters

                },
                 new Airport
                {
                    Id=2,
                    Name = "BBB",
                    Latitude = 44,
                    Longitude=11,
                    Filters= filters

                }
            };

            var flights = new List<Flight>
            {
                new Flight
                {
                    Id=1,
                    Name = "Unicorn",
                    AirlineId = 1,
                    DestinationAirportId = 2,
                    OriginAirportId = 1,
                    DepartureDate = new DateTime(2017,06,1),
                    Price = 20.99m
                },
                 new Flight
                {
                    Id=2,
                    Name = "Rainbow",
                    AirlineId = 1,
                    DestinationAirportId = 1,
                    OriginAirportId = 2,
                    DepartureDate =new DateTime(2017,06,01),
                    Price = 20.99m
                }
            };

            var sectionClasses = new List<SectionClass>
            {
                new SectionClass
                {
                    Id=1,
                    Name ="First"
                }
            };

            var flightSections = new List<FlightSection>
            {
                new FlightSection
                {
                    Id=1,
                    SectionClassId=1,
                    FlightId=1
                }
            };

            var seats = new List<Seat>
            {
                new Seat
                {
                    Id=1,
                    Row=1,
                    Col="A",
                    FlightSectionId=1,
                    Status=false

                }
            };

            context.Flights.AddRange(flights);
            context.Airlines.AddRange(airlines);
            context.Airports.AddRange(airports);
            context.SectionClasses.AddRange(sectionClasses);
            context.FlightSections.AddRange(flightSections);
            context.Seats.AddRange(seats);

            flightRepository = new FlightRepository(context);
        }

        [Test]
        public void GetEntityByName_WrongName_ReturnNull()
        {
            //Arrange
            Expression<Func<Flight, bool>> filter = (x => x.Name.Equals("Rain"));

            //Act
            var flight = flightRepository.GetEntity(filter);

            //Assesrt
            Assert.That(flight, Is.EqualTo(null));
        }

        [Test]
        public void GetEntityByName_ExcistingName_ReturnFlight()
        {
            //Arrange
            Expression<Func<Flight, bool>> filter = (x => x.Name.Equals("Rainbow"));
            var searchedFlight = new FlightBL()
            {
                Name = "Rainbow",
                AirlineId = 1,
                DestinationAirportId = 1,
                OriginAirportId = 2,
                Day = 1,
                Month = 6,
                Year = 2017,
                Price = 20.99m
            };

            //Act
            var flight = flightRepository.GetEntity(filter);

            //Assesrt
            Assert.That(flight.Name, Is.EqualTo(searchedFlight.Name));
            Assert.That(flight.AirlineId, Is.EqualTo(searchedFlight.AirlineId));
            Assert.That(flight.OriginAirportId, Is.EqualTo(searchedFlight.OriginAirportId));
            Assert.That(flight.DestinationAirportId, Is.EqualTo(searchedFlight.DestinationAirportId));
            Assert.That(flight.Day, Is.EqualTo(searchedFlight.Day));
            Assert.That(flight.Month, Is.EqualTo(searchedFlight.Month));
            Assert.That(flight.Year, Is.EqualTo(searchedFlight.Year));
            Assert.That(flight.Price, Is.EqualTo(searchedFlight.Price));
        }

        [Test]
        public void GetAllFlights()
        {
            //Act
            var flights = (flightRepository.GetAllFlights() as List<FlightModel>);

            //Assert
            Assert.That(flights.Count, Is.EqualTo(2));
            Assert.That(flights[0].FlightName, Is.EqualTo("Unicorn"));
            Assert.That(flights[1].FlightName, Is.EqualTo("Rainbow"));
        }

        [Test]
        [TestCase("AAA", "BBB", "01/06/2017")]
        public void FIndAvailableFlights(string originAirport, string destinationAirport, string departureDate)
        {
            DateTime deptDate = DateTime.Parse(departureDate);
            //Act
            var flights = (flightRepository.FindAvailableFlights(originAirport, destinationAirport, deptDate) as List<FlightModel>);

            //Assert
            Assert.That(flights.Count, Is.EqualTo(1));
            Assert.That(flights[0].FlightName, Is.EqualTo("Unicorn"));
        }

    }
}
