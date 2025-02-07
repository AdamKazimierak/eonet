using System;
using System.Net.Http;
using System.Net.Mime;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Eonet.Service.Infrastructure.Features
{
    public class EonetClient : IEonetClient
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public EonetClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<GeoJSON.Text.Feature.FeatureCollection> GetGeoEvents(DateTime startUtc, DateTime endUtc, CancellationToken cancellationToken = default)
        {
            var response = await _httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get, $"/api/v3/events/geojson?start={startUtc:yyyy-MM-dd}&end={endUtc:yyyy-MM-dd}")
            {
                Headers =
                {
                    {"Accept",  MediaTypeNames.Application.Json }
                }
            }, cancellationToken);

            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync(cancellationToken);

            return JsonSerializer.Deserialize<GeoJSON.Text.Feature.FeatureCollection>(content, _jsonSerializerOptions);
        }
    }
}
