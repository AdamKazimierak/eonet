using MediatR;
using System;
using System.Collections.Generic;

namespace Eonet.Api.Infrastructure.Features.Queries
{
    public class GetFeaturesQuery : IRequest<IEnumerable<GetFeaturesQuery.Result>>
    {
        public GetFeaturesQuery(DateTime start, DateTime end, int page = 1)
        {
            StartDateUtc = start;
            EndDateUtc = end;
            Page = page;
        }

        public DateTime StartDateUtc { get; }
        public DateTime EndDateUtc { get; }
        public int Page { get; }

        public class Result
        {
            public int Id { get; set; }
            public string Type { get; set; }
            public string PropertyId { get; set; }
            public string PropertyTitle { get; set; }
            public string PropertyDescription { get; set; }
            public DateTime PropertyDateUtc { get; set; }
            public string CategroyId { get; set; }
            public string CategoryTitle { get; set; }
            public string GeometryType { get; set; }
            public double Latitude { get; set; }
            public double Longitude { get; set; }
        }
    }
}
