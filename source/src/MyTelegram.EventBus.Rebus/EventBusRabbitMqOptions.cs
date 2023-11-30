namespace MyTelegram.EventBus.Rebus;

public class EventBusRabbitMqOptions
{
    public string ExchangeName { get; set; } = "mytelegram_event_bus";
    public string ClientName { get; set; } = string.Empty;
    public int RetryCount { get; set; } = 5;
}