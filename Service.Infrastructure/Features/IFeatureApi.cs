using Eonet.Contracts.Features;
using Refit;
using System.Threading;
using System.Threading.Tasks;

namespace Eonet.Service.Infrastructure.Features
{
    public interface IFeatureApi
    {
        [Post("/api/v1/features")]
        Task AddFeatures([Body] FeatureCollection collection, CancellationToken cancellationToken = default);
    }
}