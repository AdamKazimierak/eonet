using Eonet.Api.Core.Features;
using Eonet.Api.Infrastructure;
using Eonet.Api.Infrastructure.Features.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Eonet.Api
{
    public class Program
    {
        public const string EventProcessingFeature = "eonet-event-feature";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseSerilog((a, b) => b.ReadFrom.Configuration(builder.Configuration));
            builder.Services.AddCors();
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddApiVersioning();
            builder.Services.AddSwaggerGen();
            builder.Services.AddHealthChecks()
                .AddSqlServer(builder.Configuration.GetConnectionString(nameof(ConnectionStringOptions.Features)), tags: [EventProcessingFeature]);
            builder.Services.Configure<ConnectionStringOptions>(builder.Configuration.GetSection(ConnectionStringOptions.ConnectionStrings));
            builder.Services.AddTransient<IFeatureStore, FeatureStore>();
            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(
                    Infrastructure.Reference.Assembly,
                    Core.Reference.Assembly);
            });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseCors(x => x
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => true)
                    .AllowCredentials());
            }
            app.MapControllers();
            app.MapHealthChecks("/health");
            app.Run();
        }
    }
}
