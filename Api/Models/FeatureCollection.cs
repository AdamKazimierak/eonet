using System.Collections.Generic;
using System;

namespace Eonet.Api.Models
{
    public class FeatureCollection
    {
        public string Type => "FeatureCollection";
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
        public DateTime Date { get; set; }
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
        public IEnumerable<double> Coordinates { get; set; }
    }
}
