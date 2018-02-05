using ABS_v2.BusinesLogic.PresentationModels;
using ABS_v2.BusinessLogic.Entities;
using ABS_v2.BusinessLogic.Interfaces;
using ABS_v2.BusinessLogic.Interfaces.Managers;
using ABS_v2.BusinessLogic.PresentationModels;
using System;
using System.Collections.Generic;

namespace ABS_v2.BusinessLogic.Managers
{
    public class SystemManager : ISystemManager
    {
        private IAirportManager<AirportBL> AirportManager { get; set; }
        private IAirlineManager<AirlineBL> AirlineManager { get; set; }
        private IFlightManager<FlightBL> FlightManager { get; set; }
        private ISectionClassManager<SectionClassBL> SectionClassManager { get; set; }
        private IFlightSectionManager<FlightSectionBL> FlightSectionManager { get; set; }
        private ISeatManager<SeatBL> SeatManager { get; set; }
        private IFilterManager<FilterBL> FilterManager { get; set; }


        public SystemManager(IAirportManager<AirportBL> airportManager, IAirlineManager<AirlineBL> airlineManager, IFlightManager<FlightBL> flightManager,
            ISectionClassManager<SectionClassBL> sectionClassManager, IFlightSectionManager<FlightSectionBL> flightSectionManager, ISeatManager<SeatBL> seatManager, IFilterManager<FilterBL> filterManager)
        {
            AirportManager = airportManager;
            AirlineManager = airlineManager;
            FlightManager = flightManager;
            SectionClassManager = sectionClassManager;
            FlightSectionManager = flightSectionManager;
            SeatManager = seatManager;
            FilterManager = filterManager;
        }



        public ICollection<string> AddAirport(string name, decimal latitude, decimal longitude, ICollection<string> filters)
        {
            ICollection<FilterBL> filts = new List<FilterBL>();
            foreach (var filt in filters)
            {
                int id = FilterManager.GetEntityId(new FilterBL(filt));
                filts.Add(new FilterBL(id, filt));
            }
            AirportBL airport = new AirportBL(name, latitude, longitude, filts);
            return AirportManager.Add(airport);
        }

        public ICollection<string> AddAirline(string name)
        {
            AirlineBL airline = new AirlineBL(name);
            return AirlineManager.Add(airline);
        }


        public ICollection<string> AddFlight(string flightName, string originAirportName, string destinationAirportName, string airlineName, int day, int month, int year, decimal price)
        {
            AirlineBL airlineTemp = new AirlineBL(airlineName);
            AirportBL originTemp = new AirportBL(originAirportName);
            AirportBL destinationTemp = new AirportBL(destinationAirportName);

            int originAirportId = AirportManager.GetEntityId(originTemp);
            int destinationAirportId = AirportManager.GetEntityId(destinationTemp);
            int airlineId = AirlineManager.GetEntityId(airlineTemp);


            FlightBL flight = new FlightBL(flightName, day, month, year, airlineId, destinationAirportId, originAirportId, price);
            return FlightManager.Add(flight);
        }

        public ICollection<string> AddFlightSection(string flightName, string sectionClass, int rows, int cols)
        {
            ICollection<string> errors = new List<string>();

            if (rows < 1 || rows > 100 || cols < 1 || cols > 10)
            {
                errors.Add("The row should be between 1 and 100 and the columns between 1 and 10");
            }
            else
            {
                FlightBL flightTemp = new FlightBL(flightName);
                int flightId = FlightManager.GetEntityId(flightTemp);

                SectionClassBL sectionClassTemp = new SectionClassBL(sectionClass);
                int sectionClassId = SectionClassManager.GetEntityId(sectionClassTemp);

                ICollection<SeatBL> seats = CreateSeats(rows, cols);

                FlightSectionBL flightSection = new FlightSectionBL(sectionClassId, flightId, seats);
                errors = FlightSectionManager.Add(flightSection);

            }

            return errors;
        }

        private ICollection<SeatBL> CreateSeats(int rows, int cols)
        {
            ICollection<SeatBL> seats = new List<SeatBL>();
            for (int i = 1; i <= rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    char colLetter = (Char)('A' + j);
                    string seatCol = colLetter.ToString();
                    SeatBL seat = new SeatBL(i, seatCol);
                    seats.Add(seat);
                }
            }
            return seats;
        }

        public ICollection<string> AddSectionClass(string name)
        {
            SectionClassBL sectionClass = new SectionClassBL(name);
            return SectionClassManager.Add(sectionClass);
        }

        public ICollection<string> AddFlightFilter(string name)
        {
            FilterBL filter = new FilterBL(name);
            return FilterManager.Add(filter);
        }

        public ICollection<string> BookSeat(string flightName, string sectionClass, int row, string col)
        {
            ICollection<string> errors = new List<string>();

            FlightBL flightTemp = new FlightBL(flightName);
            int flightId = FlightManager.GetEntityId(flightTemp);
            if (flightId == 0)
            {
                errors.Add("There is no such flight");
            }


            SectionClassBL sectionClassTemp = new SectionClassBL(sectionClass);
            int sectionClassId = SectionClassManager.GetEntityId(sectionClassTemp);

            if (sectionClassId == 0)
            {
                errors.Add("There is no such section class");
            }

            if (flightId != 0 && sectionClassId != 0)
            {
                FlightSectionBL flightSectionTemp = new FlightSectionBL(sectionClassId, flightId);
                int flightSectionId = FlightSectionManager.GetEntityId(flightSectionTemp);

                SeatBL seat = new SeatBL(row, col, flightSectionId);
                errors = SeatManager.BookSeat(seat);
            }

            return errors;
        }

        public ICollection<FlightModel> FindAvailableFlights(string originAirport, string destinationAirport, int year, int month, int day, out ICollection<string> errors)
        {
            errors = new List<string>();
            ICollection<FlightModel> availableFlights = new List<FlightModel>();

            AirportBL originTemp = new AirportBL(originAirport);
            int originId = AirportManager.GetEntityId(originTemp);
            if (originId == 0)
            {
                errors.Add("There is no such origin airport");
            }

            AirportBL destinationTemp = new AirportBL(destinationAirport);
            int destinationId = AirportManager.GetEntityId(destinationTemp);
            if (destinationId == 0)
            {
                errors.Add("There is no such destination airport");
            }

            DateTime departDate;
            if (!DateTime.TryParse(string.Format("{0}/{1}/{2}", year, month, day), out departDate))
            {
                errors.Add("Please enter a valid date");
            }

            if (originId != 0 && destinationId != 0 && departDate != DateTime.MinValue)
            {
                availableFlights = FlightManager.FindAvailableFlights(originAirport, destinationAirport, departDate);
            }

            return availableFlights;
        }



        //public string DisplaySystemDitails()
        //{
        //    ICollection<SystemDetailsViewModel> sysDetails = FlightManager.GetSystemDetails();

        //    StringBuilder systemInfoSB = new StringBuilder();
        //    systemInfoSB.Append(new string('*', 30) + "\n");
        //    systemInfoSB.Append("System information:\n");
        //    systemInfoSB.Append(new string('*', 30) + "\n");

        //    foreach (var sd in sysDetails)
        //    {
        //        systemInfoSB.Append("Departing from airport " + sd.OriginAirportName + ":\n");
        //        systemInfoSB.Append("Flight " + sd.FlightName);
        //        systemInfoSB.Append(" to " + sd.DestinationAirportName);
        //        systemInfoSB.Append(" with section " + sd.SeatClass);
        //        systemInfoSB.Append(" seat " + sd.Row + sd.Col + " is ");
        //        if (sd.Status)
        //        {
        //            systemInfoSB.Append("booked" + "\n");
        //        }
        //        else
        //        {
        //            systemInfoSB.Append("available" + "\n");
        //        }
        //    }
        //    return systemInfoSB.ToString();
        //}

        public ICollection<SystemDetailsModel> DisplaySystemDitails()
        {
            return FlightManager.GetSystemDetails();
        }

        public ICollection<string> GetAllFilters()
        {
            ICollection<string> filtersStings = new List<string>();
            ICollection<FilterModel> filters = FilterManager.GetAllFilters();
            foreach (var filt in filters)
            {
                filtersStings.Add(filt.Name);
            }

            return filtersStings;
        }

        public ICollection<AiportModel> GetAllAirports()
        {
            return AirportManager.GetAllAirports();
        }

        public ICollection<FlightModel> GetAllFlights()
        {
            return FlightManager.GetAllFlights();
        }
    }
}
