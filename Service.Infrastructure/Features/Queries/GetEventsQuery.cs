using MediatR;
using System;

namespace Eonet.Service.Infrastructure.Features.Queries
{
    public class GetEventsQuery : IRequest<GeoJSON.Text.Feature.FeatureCollection>
    {
        public GetEventsQuery(DateTime start, DateTime end)
        {
            StartUtc = start;
            EndUtc = end;
        }

        public DateTime StartUtc { get; }
        public DateTime EndUtc { get; }
    }
}
