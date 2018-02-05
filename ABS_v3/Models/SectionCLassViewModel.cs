using System.ComponentModel.DataAnnotations;

namespace ABS_v3.Models
{
    public class SectionCLassViewModel
    {
        [Display(Name = "Section class")]
        [Required]
        public string Name { get; set; }
    }
}