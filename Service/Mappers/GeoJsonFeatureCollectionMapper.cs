using Eonet.Service.Contracts.Features;
using System;
using System.Text.Json;

namespace Eonet.Service.Mappers
{
    internal static class GeoJsonFeatureMapper
    {
        public static Feature ToFeatureModel(GeoJSON.Text.Feature.Feature feature)
        {
            var point = (GeoJSON.Text.Geometry.Point)feature.Geometry;

            return new Feature
            {
                Type = feature.Type.ToString(),
                Properties = new Property
                {
                    Id = feature.Properties["id"].ToString(),
                    DateUtc = DateTime.Parse(feature.Properties["date"].ToString()).ToUniversalTime(),
                    Description = feature.Properties.ContainsKey("description")
                             ? feature.Properties["description"]?.ToString()
                             : null,
                    Title = feature.Properties.ContainsKey("title")
                             ? feature.Properties["title"]?.ToString()
                             : null,
                    Categories = feature.Properties.ContainsKey("categories")
                             ? feature.Properties["categories"] is JsonElement element
                                 ? [
                                     new Category
                                        {
                                            Id = element[0].GetProperty("id").GetString(),
                                            Title = element[0].GetProperty("title").GetString()
                                        }
                                   ]
                                 : null
                             : null
                },
                Geometry = new Geometry
                {
                    Type = feature.Geometry.Type.ToString(),
                    Latitude = point.Coordinates.Latitude,
                    Longitude = point.Coordinates.Longitude
                }
            };
        }
    }
}
