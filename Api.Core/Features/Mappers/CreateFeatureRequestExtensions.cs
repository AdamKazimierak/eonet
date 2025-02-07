using Eonet.Api.Core.Features.Models;
using Eonet.Api.Core.Features.Requests;

namespace Eonet.Api.Core.Features.Mappers
{
    internal static class CreateFeatureRequestExtensions
    {
        public static Feature ToFeature(this CreateFeatureRequest request)
        {
            return new Feature
            {
                Type = request.Type,
                CategoryTitle = request.Properties.Categories.Title,
                CategroyId = request.Properties.Categories.Id,
                PropertyId = request.Properties.Id,
                PropertyTitle = request.Properties.Title,
                PropertyDescription = request.Properties.Description,
                PropertyDateUtc = request.Properties.DateUtc,
                GeometryType = request.Geometry.Type,
                Latitude = request.Geometry.Latitude,
                Longitude = request.Geometry.Longitude
            };
        }
    }
}
