namespace MyTelegram.MessengerServer.Abp.EventHandlers;

public class UserIsOnlineEventHandler : IDistributedEventHandler<UserIsOnlineEvent>, ITransientDependency
{
    private readonly ILogger<UserIsOnlineEventHandler> _logger;
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly IScheduleAppService _scheduleAppService;

    public UserIsOnlineEventHandler(ILogger<UserIsOnlineEventHandler> logger,
        IObjectMessageSender objectMessageSender,
        IScheduleAppService scheduleAppService)
    {
        _logger = logger;
        _objectMessageSender = objectMessageSender;
        _scheduleAppService = scheduleAppService;
    }

    public async Task HandleEventAsync(UserIsOnlineEvent eventData)
    {
        _logger.LogDebug("User {UserId} is online,tempAuthKeyId={TempAuthKeyId:x2},permAuthKeyId={PermAuthKeyId:x2}",
            eventData.UserId,
            eventData.TempAuthKeyId,
            eventData.PermAuthKeyId);
        await _scheduleAppService.ExecuteAsync(() =>
        {
            var updatesTooLong = new TUpdatesTooLong();
            _objectMessageSender.PushSessionMessageToAuthKeyIdAsync(eventData.TempAuthKeyId, updatesTooLong);
        },
            TimeSpan.FromMilliseconds(1500)).ConfigureAwait(false);
    }
}
