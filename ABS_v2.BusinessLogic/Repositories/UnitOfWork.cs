using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces.Repositories;
using ABS_v2.BusinessLogic.PresentationModels;
using ABS_v2.DataAccess.Context;
using ABS_v2.DataAccess.Entities;
using ABS_v2.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace ABS_v2.BusinessLogic.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IABSDbContext context = new ABSDbContext();
        private IAirportRepository<AirportBL, Airport> airportRepo;
        private IEntityRepository<AirlineBL, Airline> airlineRepo;
        private IFlightRepository<FlightBL, Flight> flightRepo;
        private IEntityRepository<FlightSectionBL, FlightSection> flightSectionRepo;
        private IEntityRepository<SeatBL, Seat> seatRepo;
        private IEntityRepository<SectionClassBL, SectionClass> sectionClassRepo;
        private IFilterRepository<FilterBL, Filter> filterRepo;


        public IAirportRepository<AirportBL, Airport> AirportRepo
        {
            get
            {

                if (this.airportRepo == null)
                {
                    this.airportRepo = new AirportRepository(context);
                }
                return airportRepo;
            }
        }

        public IEntityRepository<AirlineBL, Airline> AirlineRepo
        {
            get
            {

                if (this.airlineRepo == null)
                {
                    this.airlineRepo = new AirlineRepository(context);
                }
                return airlineRepo;
            }
        }

        public IFlightRepository<FlightBL, Flight> FlightRepo
        {
            get
            {

                if (this.flightRepo == null)
                {
                    this.flightRepo = new FlightRepository(context);
                }
                return flightRepo;
            }
        }

        public IEntityRepository<FlightSectionBL, FlightSection> FlightSectionRepo
        {
            get
            {

                if (this.flightSectionRepo == null)
                {
                    this.flightSectionRepo = new FlightSectionRepository(context);
                }
                return flightSectionRepo;
            }
        }

        public IEntityRepository<SeatBL, Seat> SeatRepo
        {
            get
            {

                if (this.seatRepo == null)
                {
                    this.seatRepo = new SeatRepository(context);
                }
                return seatRepo;
            }
        }

        public IEntityRepository<SectionClassBL, SectionClass> SectionClassRepo
        {
            get
            {

                if (this.sectionClassRepo == null)
                {
                    this.sectionClassRepo = new SectionClassRepository(context);
                }
                return sectionClassRepo;
            }
        }

        public IFilterRepository<FilterBL, Filter> FilterRepo
        {
            get
            {

                if (this.filterRepo == null)
                {
                    this.filterRepo = new FilterRepository(context);
                }
                return filterRepo;
            }
        }


        public void Save()
        {
            context.SaveChanges();
        }

        public ICollection<SystemDetailsModel> GetSystemDetails()
        {
            ICollection<VSystemDetail> sysDetailsDB = context.VSystemDetails.ToList();
            ICollection<SystemDetailsModel> sysDetails = new List<SystemDetailsModel>();
            foreach (var item in sysDetailsDB)
            {
                sysDetails.Add(new SystemDetailsModel(item.FlightName, item.AirlineName, item.OriginAirportName, item.DestinationAirportName, item.SeatClass, item.Row, item.Col, item.Status));
            }
            return sysDetails;
        }
    }
}
