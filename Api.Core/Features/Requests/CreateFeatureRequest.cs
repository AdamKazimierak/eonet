using MediatR;
using System.Collections.Generic;
using System;

namespace Eonet.Api.Core.Features.Requests
{
    public class CreateFeatureRequest : IRequest
    {
        public CreateFeatureRequest(
            string type,
            Property properties,
            Geometry geometry)
        {
            Type = type;
            Properties = properties;
            Geometry = geometry;
        }

        public string Type { get; }
        public Property Properties { get; }
        public Geometry Geometry { get; }
    }

    public class Property
    {
        public Property(
            string id,
            string title,
            string description,
            DateTime dateUtc,
            Category categories)
        {
            Id = id;
            Title = title;
            Description = description;
            DateUtc = dateUtc;
            Categories = categories;
        }

        public string Id { get; }
        public string Title { get; }
        public string Description { get; }
        public string Link { get; }
        public DateTime DateUtc { get; }
        public Category Categories { get; }
    }

    public class Category
    {
        public Category(
            string id,
            string title)
        {
            Id = id;
            Title = title;
        }

        public string Id { get; }
        public string Title { get; }
    }

    public class Geometry
    {
        public Geometry(
            string type,
            double latitude,
            double longitude)
        {
            Type = type;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string Type { get; }
        public double Latitude { get; }
        public double Longitude { get; }
    }
}
