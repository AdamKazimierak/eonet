using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Eonet.Api.Infrastructure.Features.Queries
{
    public class GetFeaturesQueryHandler : IRequestHandler<GetFeaturesQuery, IEnumerable<GetFeaturesQuery.Result>>
    {
        private readonly ConnectionStringOptions _connectionStringOptions;

        public GetFeaturesQueryHandler(IOptions<ConnectionStringOptions> connectionStringOptions)
        {
            _connectionStringOptions = connectionStringOptions.Value;
        }

        public async Task<IEnumerable<GetFeaturesQuery.Result>> Handle(GetFeaturesQuery request, CancellationToken cancellationToken)
        {
            var command = new CommandDefinition(
                 commandText: "usp_feature_get_all",
                 parameters: new
                 {
                     @start_date_utc = request.StartDateUtc.ToUniversalTime(),
                     @end_date_utc = request.EndDateUtc.ToUniversalTime(),
                     @page_number = request.Page,
                     @page_size = 50,
                 },
                 commandType: System.Data.CommandType.StoredProcedure,
                 cancellationToken: cancellationToken);

            using var connection = new SqlConnection(_connectionStringOptions.Features);

            return await connection.QueryAsync<GetFeaturesQuery.Result>(command);
        }
    }
}
