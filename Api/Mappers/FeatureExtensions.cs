using Eonet.Api.Core.Features.Requests;
using Eonet.Contracts.Features;

namespace Eonet.Api.Mappers
{
    public static class FeatureExtensions
    {
        public static CreateFeatureRequest ToCreateFeatureRequest(this Feature feature)
            => new CreateFeatureRequest(
                feature.Type,
                new Core.Features.Requests.Property(
                    feature.Properties.Id,
                    feature.Properties.Title,
                    feature.Properties.Description,
                    feature.Properties.DateUtc,
                    new Core.Features.Requests.Category(feature.Properties.Category.Id, feature.Properties.Category.Title)),
                new Core.Features.Requests.Geometry(feature.Geometry.Type, feature.Geometry.Latitude, feature.Geometry.Longitude));
    }
}
