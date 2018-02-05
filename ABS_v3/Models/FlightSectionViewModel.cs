using System.ComponentModel.DataAnnotations;

namespace ABS_v3.Models
{
    public class FlightSectionViewModel
    {
        [Display(Name = "Flight name")]
        [Required]
        public string FlightName { get; set; }

        [Display(Name = "Section class")]
        [Required]
        public string SectionCLass { get; set; }

        [Required]
        public int Rows { get; set; }

        [Required]
        public int Cols { get; set; }
    }
}