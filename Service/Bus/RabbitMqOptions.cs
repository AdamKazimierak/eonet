namespace Eonet.Service.Bus
{
    internal class RabbitMqOptions
    {
        public const string RabbitMQ = "RabbitMQ";

        public string Host { get; set; } = default!;
        public string User { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
