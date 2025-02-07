using Eonet.Api.Core.Features.Models;
using System.Threading.Tasks;

namespace Eonet.Api.Core.Features
{
    public interface IFeatureStore
    {
        Task Create(Feature feature);
        Task<bool> DoesFeatureExistByPropertyId(string propertyId);
    }
}
