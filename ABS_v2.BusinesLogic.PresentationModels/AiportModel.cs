using System.Collections.Generic;

namespace ABS_v2.BusinesLogic.PresentationModels
{
    public class AiportModel
    {
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public ICollection<string> Filters { get; set; }
        public AiportModel(string name, decimal latitude, decimal longitude, ICollection<string> filters)
        {
            Name = name;
            Latitude = latitude;
            Longitude = longitude;
            Filters = filters;
        }
    }
}
