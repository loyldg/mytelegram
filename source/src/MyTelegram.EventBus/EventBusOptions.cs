namespace MyTelegram.EventBus;

public class EventBusOptions
{
    public string ExchangeName { get; set; } = "mytelegram_event_bus";
    public string ClientName { get; set; } = string.Empty;
    public int RetryCount { get; set; } = 5;
}