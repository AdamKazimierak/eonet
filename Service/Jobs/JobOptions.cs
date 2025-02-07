namespace Eonet.Service.Jobs
{
    internal class JobOptions
    {
        public const string Job = "Job";
        public Settings FetchEventsJob { get; set; } = new Settings();

        public class Settings
        {
            public string Cron { get; set; } = null!;
        }
    }
}
