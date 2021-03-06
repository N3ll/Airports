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


namespace ABS_v2.DataAccess.Configuration
{
    using ABS_v2.DataAccess.Entities;

    // Flight
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.29.1.0")]
    public class FlightConfiguration : System.Data.Entity.ModelConfiguration.EntityTypeConfiguration<Flight>
    {
        public FlightConfiguration()
            : this("dbo")
        {
        }

        public FlightConfiguration(string schema)
        {
            ToTable("Flight", schema);
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName(@"Id").HasColumnType("int").IsRequired().HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity);
            Property(x => x.Name).HasColumnName(@"Name").HasColumnType("nvarchar").IsRequired().HasMaxLength(50);
            Property(x => x.DepartureDate).HasColumnName(@"DepartureDate").HasColumnType("datetime").IsRequired();
            Property(x => x.AirlineId).HasColumnName(@"AirlineId").HasColumnType("int").IsRequired();
            Property(x => x.DestinationAirportId).HasColumnName(@"DestinationAirportId").HasColumnType("int").IsRequired();
            Property(x => x.OriginAirportId).HasColumnName(@"OriginAirportId").HasColumnType("int").IsRequired();
            Property(x => x.Price).HasColumnName(@"Price").HasColumnType("decimal").IsRequired().HasPrecision(10,6);

            // Foreign keys
            HasRequired(a => a.Airline).WithMany(b => b.Flights).HasForeignKey(c => c.AirlineId).WillCascadeOnDelete(false); // FK_Airline_Flight
            HasRequired(a => a.DestinationAirport).WithMany(b => b.DestinationAirport).HasForeignKey(c => c.DestinationAirportId).WillCascadeOnDelete(false); // FK_DestinationAirport_Flight
            HasRequired(a => a.OriginAirport).WithMany(b => b.OriginAirport).HasForeignKey(c => c.OriginAirportId).WillCascadeOnDelete(false); // FK_OriginAirport_Flight
        }
    }

}
// </auto-generated>
