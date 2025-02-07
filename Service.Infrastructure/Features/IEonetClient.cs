using System;
using System.Threading;
using System.Threading.Tasks;

namespace Eonet.Service.Infrastructure.Features
{
    public interface IEonetClient
    {
        Task<GeoJSON.Text.Feature.FeatureCollection> GetGeoEvents(DateTime startUtc, DateTime endUtc, CancellationToken cancellationToken = default);
    }
}
