using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace Eonet.UI
{
    public class Program
    {
        public const string EventProcessingFeature = "eonet-event-feature";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            var app = builder.Build();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "ClientApp/dist")),
                RequestPath = ""
            });
            app.UseRouting();
            app.MapControllers();
            app.MapFallbackToFile("ClientApp/dist/index.html");
            app.Run();
        }
    }
}
