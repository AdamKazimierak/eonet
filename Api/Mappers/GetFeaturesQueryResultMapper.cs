using Eonet.Api.Infrastructure.Features.Queries;

namespace Eonet.Api.Mappers
{
    public static class GetFeaturesQueryResultMapper
    {
        public static Models.Feature ToFeatureModel(GetFeaturesQuery.Result feature) => new()
        {
            Type = feature.Type,
            Geometry = new Models.Geometry
            {
                Type = feature.GeometryType,
                Coordinates =
                [
                    feature.Longitude,
                    feature.Latitude
                ]
            },
            Properties = new Models.Property
            {
                Category = new Models.Category
                {
                    Id = feature.CategroyId,
                    Title = feature.CategoryTitle
                },
                Date = feature.PropertyDateUtc,
                Description = feature.PropertyDescription,
                Id = feature.PropertyId,
                Title = feature.PropertyTitle
            }
        };
    }
}
