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

    // Seat
    [System.CodeDom.Compiler.GeneratedCode("EF.Reverse.POCO.Generator", "2.29.1.0")]
    public class Seat
    {
        public int Id { get; set; } // Id (Primary key)
        public int Row { get; set; } // Row
        public string Col { get; set; } // Col (length: 1)
        public int FlightSectionId { get; set; } // FlightSectionId
        public bool Status { get; set; } // Status

        // Foreign keys

        /// <summary>
        /// Parent FlightSection pointed by [Seat].([FlightSectionId]) (FK_FlightSection_Seat)
        /// </summary>
        public virtual FlightSection FlightSection { get; set; } // FK_FlightSection_Seat

        public Seat()
        {
            Status = false;
        }
    }

}
// </auto-generated>
