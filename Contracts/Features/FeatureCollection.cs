using System;
using System.Collections.Generic;

namespace Eonet.Contracts.Features
{
    public class FeatureCollection
    {
        public string Type { get; set; }
        public IEnumerable<Feature> Features { get; set; }
    }

    public class Feature
    {
        public string Type { get; set; }
        public Property Properties { get; set; }
        public Geometry Geometry { get; set; }
    }

    public class Property
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateUtc { get; set; }
        public Category Category { get; set; }
    }

    public class Category
    {
        public string Id { get; set; }
        public string Title { get; set; }
    }

    public class Geometry
    {
        public string Type { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
