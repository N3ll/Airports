namespace ABS_v3.Models
{
    public class SysDetailsViewModel
    {
        public string FlightName { get; set; }
        public string AirlineName { get; set; }
        public string OriginAirportName { get; set; }
        public string DestinationAirportName { get; set; }
        public string SeatClass { get; set; }
        public int Row { get; set; }
        public string Col { get; set; }
        public bool Status { get; set; }


        public SysDetailsViewModel(string flightName, string airlineName, string originAirport, string destinationAirport, string seatClass, int row, string col, bool status)
        {
            FlightName = flightName;
            AirlineName = airlineName;
            OriginAirportName = originAirport;
            DestinationAirportName = destinationAirport;
            SeatClass = seatClass;
            Row = row;
            Col = col;
            Status = status;
        }
    }
}