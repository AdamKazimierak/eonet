using Eonet.Service.Bus;
using Eonet.Service.Infrastructure.Features;
using Eonet.Service.Jobs;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Refit;
using System;

namespace Eonet.Service
{
    public class Program
    {
        public const string EventProcessingFeature = "eonet-event-feature";

        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(Infrastructure.Reference.Assembly);
            });

            builder.Services.AddRefitClient<IFeatureApi>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["Api:Features:BaseUrl"]));

            builder.Services.AddHttpClient<IEonetClient, EonetClient>(c =>
            {
                c.BaseAddress = new Uri("https://eonet.gsfc.nasa.gov");
            });

            builder.Services.AddQuartz(quartz =>
            {
                var configuration = builder.Configuration
                    .GetSection(JobOptions.Job)
                    .Get<JobOptions>()!;

                var job = new JobKey(nameof(FetchEventsJob));
                quartz.AddJob<FetchEventsJob>(opts => opts.WithIdentity(job));
                quartz.AddTrigger(opts => opts
                    .ForJob(job)
                    .WithIdentity($"{nameof(FetchEventsJob)}-trigger")
                    .WithCronSchedule(configuration.FetchEventsJob.Cron)
                    .StartNow());
            });

            builder.Services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });

            builder.Services.AddMassTransit(mt =>
            {
                mt.AddConsumer<CollectionConsumer>();
                mt.UsingRabbitMq((context, cfg) =>
                {
                    var configuration = builder.Configuration
                        .GetSection(RabbitMqOptions.RabbitMQ)
                        .Get<RabbitMqOptions>()!;

                    cfg.Host(configuration.Host, "/", h =>
                    {
                        h.Username(configuration.User);
                        h.Password(configuration.Password);
                    });

                    cfg.ReceiveEndpoint(AppDomain.CurrentDomain.FriendlyName, e =>
                    {
                        e.UseKillSwitch(k =>
                        {
                            k.SetActivationThreshold(10);
                            k.SetTripThreshold(0.15);
                            //k.SetExceptionFilter
                            k.SetRestartTimeout(TimeSpan.FromMinutes(1));
                        });

                        e.UseMessageRetry(r =>
                        {
                            r.Incremental(3, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
                        });

                        e.UseInMemoryOutbox(context);
                        e.Consumer<CollectionConsumer>(context);
                    });
                });
            });

            var host = builder.Build();
            host.Run();
        }
    }
}