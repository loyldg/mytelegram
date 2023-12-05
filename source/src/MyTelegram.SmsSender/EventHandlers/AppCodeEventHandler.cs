namespace MyTelegram.SmsSender.EventHandlers;

public class AppCodeEventHandler : IEventHandler<AppCodeCreatedIntegrationEvent>
{
    private readonly ISmsSender _smsSender;
    private readonly ILogger<AppCodeEventHandler> _logger;

    public AppCodeEventHandler(ISmsSender smsSender, ILogger<AppCodeEventHandler> logger)
    {
        _smsSender = smsSender;
        _logger = logger;
    }

    public async Task HandleEventAsync(AppCodeCreatedIntegrationEvent eventData)
    {
        var phoneNumber = eventData.PhoneNumber;
        if (!phoneNumber.StartsWith("+"))
        {
            phoneNumber = $"+{phoneNumber}";
        }

        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
        if (eventData.Expire < now)
        {
            _logger.LogWarning("App code expired,data={@Data}", eventData);
            return;
        }

        try
        {
            await _smsSender.SendAsync(phoneNumber, $"MyTelegram code:{eventData.Code}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Send sms failed,data={@Data}", eventData);
        }
    }
}
