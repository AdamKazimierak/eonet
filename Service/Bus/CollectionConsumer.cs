using Eonet.Service.Contracts.Features;
using Eonet.Service.Infrastructure.Features;
using Eonet.Service.Mappers;
using MassTransit;
using System.Threading.Tasks;

namespace Eonet.Service.Bus
{
    internal class CollectionConsumer : IConsumer<FeatureCollection>
    {
        private readonly IFeatureApi _featureApi;

        public CollectionConsumer(IFeatureApi featureApi)
        {
            _featureApi = featureApi;
        }

        public async Task Consume(ConsumeContext<FeatureCollection> context)
        {
            await _featureApi.AddFeatures(context.Message.ToEonetContractFeatureCollection(), context.CancellationToken);
        }
    }
}
