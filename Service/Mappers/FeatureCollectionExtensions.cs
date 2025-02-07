using System.Linq;

namespace Eonet.Service.Mappers
{
    internal static class FeatureCollectionExtensions
    {
        public static Eonet.Contracts.Features.FeatureCollection ToEonetContractFeatureCollection(this Service.Contracts.Features.FeatureCollection collection)
        {
            return new Eonet.Contracts.Features.FeatureCollection
            {
                Type = collection.Type,
                Features = collection.Features.Select(f => new Eonet.Contracts.Features.Feature
                {
                    Type = f.Type,
                    Geometry = new Eonet.Contracts.Features.Geometry
                    {
                        Type = f.Geometry.Type,
                        Longitude = f.Geometry.Longitude,
                        Latitude = f.Geometry.Latitude,
                    },
                    Properties = new Eonet.Contracts.Features.Property
                    {
                        Id = f.Properties.Id,
                        Title = f.Properties.Title,
                        Description = f.Properties.Description,
                        DateUtc = f.Properties.DateUtc,
                        Category = new Eonet.Contracts.Features.Category
                        {
                            Id = f.Properties.Categories[0].Id,
                            Title = f.Properties.Categories[0].Title
                        }
                    }
                })
            };
        }
    }
}
