using MyTelegram.Messenger.Services.Caching;

namespace MyTelegram.Messenger.QueryServer.EventHandlers;

public class UserIsOnlineEventHandler : IEventHandler<UserIsOnlineEvent>
{
    private readonly ILogger<UserIsOnlineEventHandler> _logger;
    private readonly IObjectMessageSender _objectMessageSender;
    private readonly IScheduleAppService _scheduleAppService;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IPtsHelper _ptsHelper;

    public UserIsOnlineEventHandler(ILogger<UserIsOnlineEventHandler> logger,
        IObjectMessageSender objectMessageSender,
        IScheduleAppService scheduleAppService, IQueryProcessor queryProcessor, IPtsHelper ptsHelper)
    {
        _logger = logger;
        _objectMessageSender = objectMessageSender;
        _scheduleAppService = scheduleAppService;
        _queryProcessor = queryProcessor;
        _ptsHelper = ptsHelper;
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
            TimeSpan.FromMilliseconds(1500));
    }
}
