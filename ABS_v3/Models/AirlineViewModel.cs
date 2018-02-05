using System.ComponentModel.DataAnnotations;

namespace ABS_v3.Models
{
    public class AirlineViewModel
    {
        [Display(Name = "Airline name")]
        [Required]
        public string Name { get; set; }
    }
}