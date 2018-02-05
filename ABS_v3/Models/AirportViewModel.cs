using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ABS_v3.Models
{
    public class AirportViewModel
    {
        [Display(Name = "Airport name")]
        [Required]
        public string Name { get; set; }

        [Required]
        private decimal latitude;

        public decimal Latitude
        {
            get { return Math.Round(latitude, 4); }
            set { latitude = Math.Round(value, 4); }
        }


        [Required]
        private decimal longitude;

        public decimal Longitude
        {
            get { return Math.Round(longitude, 4); }
            set { longitude = Math.Round(value, 4); }
        }

        [Display(Name = "Airport filters")]
        [Required]
        public ICollection<string> Filters { get; set; }
        public MultiSelectList FiltersDB { get; set; }
    }
}