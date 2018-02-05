// <auto-generated>
// ReSharper disable ConvertPropertyToExpressionBody
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable RedundantNameQualifier
// ReSharper disable RedundantOverridenMember
// ReSharper disable UseNameofExpression
// TargetFrameworkVersion = 4.5
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning


namespace ABS_v2.DataAccess.Entities
{

    // Flight
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.29.1.0")]
    public class Flight
    {
        public int Id { get; set; } // Id (Primary key)
        public string Name { get; set; } // Name (length: 50)
        public System.DateTime DepartureDate { get; set; } // DepartureDate
        public int AirlineId { get; set; } // AirlineId
        public int DestinationAirportId { get; set; } // DestinationAirportId
        public int OriginAirportId { get; set; } // OriginAirportId
        public decimal Price { get; set; } // Price

        // Reverse navigation

        /// <summary>
        /// Child FlightSections where [FlightSection].[FlightId] point to this entity (FK_Flight_FlightSection)
        /// </summary>
        public virtual System.Collections.Generic.ICollection<FlightSection> FlightSections { get; set; } // FlightSection.FK_Flight_FlightSection

        // Foreign keys

        /// <summary>
        /// Parent Airline pointed by [Flight].([AirlineId]) (FK_Airline_Flight)
        /// </summary>
        public virtual Airline Airline { get; set; } // FK_Airline_Flight
        /// <summary>
        /// Parent Airport pointed by [Flight].([DestinationAirportId]) (FK_DestinationAirport_Flight)
        /// </summary>
        public virtual Airport DestinationAirport { get; set; } // FK_DestinationAirport_Flight
        /// <summary>
        /// Parent Airport pointed by [Flight].([OriginAirportId]) (FK_OriginAirport_Flight)
        /// </summary>
        public virtual Airport OriginAirport { get; set; } // FK_OriginAirport_Flight

        public Flight()
        {
            FlightSections = new System.Collections.Generic.List<FlightSection>();
        }
    }

}
// </auto-generated>
