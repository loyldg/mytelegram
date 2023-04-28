namespace MyTelegram.EventBus.RabbitMQ;

public class EventBusRabbitMqOptions
{
    public string ExchangeName { get; set; } = "mytelegram_event_bus";
    public string ClientName { get; set; } = string.Empty;
    public int RetryCount { get; set; } = 5;
}

public class RabbitMqOptions
{
    public string HostName { get; set; } = string.Empty;
    public int Port { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}