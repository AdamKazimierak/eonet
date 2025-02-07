using GeoJSON.Text.Feature;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Eonet.Service.Infrastructure.Features.Queries
{
    public class GetEventsQueryHandler : IRequestHandler<GetEventsQuery, FeatureCollection>
    {
        private readonly IEonetClient _eonetClient;

        public GetEventsQueryHandler(IEonetClient eonetClient)
        {
            _eonetClient = eonetClient;
        }

        public Task<FeatureCollection> Handle(GetEventsQuery request, CancellationToken cancellationToken)
        {
            return _eonetClient.GetGeoEvents(request.StartUtc, request.EndUtc, cancellationToken);
        }
    }
}
