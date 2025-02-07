using Eonet.Api.Infrastructure.Features.Queries;
using Eonet.Api.Mappers;
using Eonet.Contracts.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Eonet.Api.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/features")]
    [ApiVersion(Version.V1_0)]
    public class FeatureCollectionController : ControllerBase
    {
        private readonly IMediator _dispatcher;

        public FeatureCollectionController(IMediator dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [HttpPost(Name = nameof(AddFeatures))]
        public async Task<ActionResult> AddFeatures(FeatureCollection collection)
        {
            foreach (var feature in collection.Features)
            {
                await _dispatcher.Send(feature.ToCreateFeatureRequest());
            }

            return Ok();
        }

        [HttpGet(Name = nameof(Get))]
        public async Task<ActionResult> Get(DateTime startUtc, DateTime endUtc, int page)
        {
            var features = await _dispatcher.Send(new GetFeaturesQuery(startUtc, endUtc, page));
            return Ok(new Models.FeatureCollection
            {
                Features = features.Select(GetFeaturesQueryResultMapper.ToFeatureModel)
            });
        }
    }
}
