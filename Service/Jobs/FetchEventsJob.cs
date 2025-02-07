using Eonet.Service.Contracts.Features;
using Eonet.Service.Infrastructure.Features.Queries;
using Eonet.Service.Mappers;
using MassTransit;
using MediatR;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eonet.Service.Jobs
{
    [DisallowConcurrentExecution]
    internal class FetchEventsJob : IJob
    {
        const int ChunkSize = 50;
        private readonly IMediator _dispatcher;
        private readonly IPublishEndpoint _publishEndpoint;

        public FetchEventsJob(IMediator dispatcher, IPublishEndpoint publishEndpoint)
        {
            _dispatcher = dispatcher;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var collection = await _dispatcher.Send(new GetEventsQuery(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow), context.CancellationToken);

            var chunks = collection.Features
                .ToList()
                .Where(IsPoint)
                .Chunk(ChunkSize);

            foreach (var features in chunks)
            {
                var model = new FeatureCollection
                {
                    Type = collection.Type.ToString(),
                    Features = features.Select(GeoJsonFeatureMapper.ToFeatureModel)
                };
                await _publishEndpoint.Publish(model, context.CancellationToken);
                await Task.Delay(1000, context.CancellationToken);
            }
        }

        private bool IsPoint(GeoJSON.Text.Feature.Feature feature)
            => feature.Geometry.Type == GeoJSON.Text.GeoJSONObjectType.Point;
    }
}
