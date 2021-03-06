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


namespace ABS_v2.DataAccess.Interfaces
{
    using ABS_v2.DataAccess.Entities;

    public interface IABSDbContext : System.IDisposable
    {
        System.Data.Entity.DbSet<Airline> Airlines { get; set; } // Airline
        System.Data.Entity.DbSet<Airport> Airports { get; set; } // Airport
        System.Data.Entity.DbSet<Audit> Audits { get; set; } // Audit
        System.Data.Entity.DbSet<Filter> Filters { get; set; } // Filter
        System.Data.Entity.DbSet<Flight> Flights { get; set; } // Flight
        System.Data.Entity.DbSet<FlightSection> FlightSections { get; set; } // FlightSection
        System.Data.Entity.DbSet<Seat> Seats { get; set; } // Seat
        System.Data.Entity.DbSet<SectionClass> SectionClasses { get; set; } // SectionClass
        System.Data.Entity.DbSet<VSystemDetail> VSystemDetails { get; set; } // vSystemDetails

        int SaveChanges();
        System.Threading.Tasks.Task<int> SaveChangesAsync();
        System.Threading.Tasks.Task<int> SaveChangesAsync(System.Threading.CancellationToken cancellationToken);
        System.Data.Entity.Infrastructure.DbChangeTracker ChangeTracker { get; }
        System.Data.Entity.Infrastructure.DbContextConfiguration Configuration { get; }
        System.Data.Entity.Database Database { get; }
        System.Data.Entity.Infrastructure.DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
        System.Data.Entity.Infrastructure.DbEntityEntry Entry(object entity);
        System.Collections.Generic.IEnumerable<System.Data.Entity.Validation.DbEntityValidationResult> GetValidationErrors();
        System.Data.Entity.DbSet Set(System.Type entityType);
        System.Data.Entity.DbSet<TEntity> Set<TEntity>() where TEntity : class;
        string ToString();

        // Stored Procedures
        System.Collections.Generic.List<FindAvailableFlightsReturnModel> FindAvailableFlights(string originAirport, string destinationAirport, System.DateTime? departureDate);
        System.Collections.Generic.List<FindAvailableFlightsReturnModel> FindAvailableFlights(string originAirport, string destinationAirport, System.DateTime? departureDate, out int procResult);
        System.Threading.Tasks.Task<System.Collections.Generic.List<FindAvailableFlightsReturnModel>> FindAvailableFlightsAsync(string originAirport, string destinationAirport, System.DateTime? departureDate);

    }

}
// </auto-generated>
