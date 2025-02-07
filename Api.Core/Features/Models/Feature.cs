using System;

namespace Eonet.Api.Core.Features.Models
{
    public class Feature
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string PropertyId { get; set; }
        public string PropertyTitle { get; set; }
        public string PropertyDescription { get; set; }
        public DateTime PropertyDateUtc { get; set; }
        public string CategroyId { get; set; }
        public string CategoryTitle { get; set; }
        public string GeometryType { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
