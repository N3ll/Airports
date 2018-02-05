using System.ComponentModel.DataAnnotations;

namespace ABS_v3.Models
{
    public class SeatViewModel
    {
        [Display(Name = "Flight name")]
        [Required]
        public string FlightName { get; set; }

        [Display(Name = "Section class")]
        [Required]
        public string SectionCLass { get; set; }

        [Required]
        public int Row { get; set; }

        [Required]
        public string Col { get; set; }
    }
}