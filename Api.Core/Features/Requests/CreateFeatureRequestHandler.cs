using Eonet.Api.Core.Features.Mappers;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Eonet.Api.Core.Features.Requests
{
    public class CreateFeatureRequestHandler : IRequestHandler<CreateFeatureRequest>
    {
        private readonly IFeatureStore _featureStore;

        public CreateFeatureRequestHandler(IFeatureStore featureStore)
        {
            _featureStore = featureStore;
        }

        public async Task Handle(CreateFeatureRequest request, CancellationToken cancellationToken)
        {
            if (await _featureStore.DoesFeatureExistByPropertyId(request.Properties.Id))
            {
                return;
            };

            await _featureStore.Create(request.ToFeature());
        }
    }
}
