using System.ComponentModel.DataAnnotations;

namespace ABS_v3.Models
{
    public class AirportFilterViewModel
    {
        [Display(Name = "Flight filter name")]
        [Required]
        public string Name { get; set; }
    }
}