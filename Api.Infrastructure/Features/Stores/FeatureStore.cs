using Dapper;
using Eonet.Api.Core.Features;
using Eonet.Api.Core.Features.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Eonet.Api.Infrastructure.Features.Stores
{
    public class FeatureStore : IFeatureStore
    {
        private const int UniqueIndexViolationCode = 2601;
        private const int UniqueConstraintViolationCode = 2627;

        private readonly ConnectionStringOptions _connectionStringOptions;
        private readonly ILogger<FeatureStore> _logger;

        public FeatureStore(IOptions<ConnectionStringOptions> connectionStringOptions, ILogger<FeatureStore> logger)
        {
            _connectionStringOptions = connectionStringOptions.Value;
            _logger = logger;
        }

        private void LogConflict(string propertyId, Exception ex = null)
        {
            _logger.LogWarning(ex, "Failed to create feature due to unique constraint violation. Feature property id: {id}", propertyId);
        }

        public async Task Create(Feature feature)
        {
            var command = new CommandDefinition(
                commandText: "usp_feature_create",
                parameters: new
                {
                    feature_type = feature.Type,
                    property_id = feature.PropertyId,
                    property_title = feature.PropertyTitle,
                    property_description = feature.PropertyDescription,
                    property_date_utc = feature.PropertyDateUtc.ToUniversalTime(),
                    category_id = feature.CategroyId,
                    category_title = feature.CategoryTitle,
                    geometry_type = feature.GeometryType,
                    latitude = feature.Latitude,
                    longitude = feature.Longitude
                },
                commandType: System.Data.CommandType.StoredProcedure);

            using var connection = new SqlConnection(_connectionStringOptions.Features);

            try
            {
                var id = await connection.ExecuteScalarAsync<int?>(command);

                if (!id.HasValue)
                {
                    LogConflict(feature.PropertyId);
                }
            }
            catch (SqlException ex) when (ex.Number == UniqueConstraintViolationCode || ex.Number == UniqueIndexViolationCode)
            {
                LogConflict(feature.PropertyId, ex);
            }
        }

        public async Task<bool> DoesFeatureExistByPropertyId(string propertyId)
        {
            var command = new CommandDefinition(
                  commandText: "usp_feature_propertyid_exists",
                  parameters: new
                  {
                      property_id = propertyId,
                  },
                  commandType: System.Data.CommandType.StoredProcedure);

            using var connection = new SqlConnection(_connectionStringOptions.Features);

            return await connection.ExecuteScalarAsync<bool>(command);
        }
    }
}
