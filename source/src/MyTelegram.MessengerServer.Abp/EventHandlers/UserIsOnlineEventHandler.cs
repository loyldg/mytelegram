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

        //var needPushNewUpdatesToClient = false;
        //var ptsReadModel = await _queryProcessor.ProcessAsync(new GetPtsByPeerIdQuery(eventData.UserId), default)
        //    .ConfigureAwait(false);
        //if (ptsReadModel != null)
        //{
        //    var ptsForAuthKeyIdReadModel = await _queryProcessor
        //        .ProcessAsync(new GetPtsByPermAuthKeyIdQuery(eventData.UserId, eventData.PermAuthKeyId), default)
        //        .ConfigureAwait(false);

        //    if (ptsForAuthKeyIdReadModel != null)
        //    {
        //        var diff = ptsReadModel.Pts - ptsForAuthKeyIdReadModel?.Pts ?? 0;
        //        var channelDiff = ptsReadModel.GlobalSeqNo - ptsForAuthKeyIdReadModel?.GlobalSeqNo ?? 0;
        //        _logger.LogDebug(
        //            "User {UserId} pts diff={Diff},channelDiff={ChannelDiff},tempAuthKeyId={TempAuthKeyId:x2}",
        //            eventData.UserId,
        //            diff,
        //            channelDiff,
        //            eventData.TempAuthKeyId);
        //        if (diff > 0 || channelDiff > 0)
        //        {
        //            needPushNewUpdatesToClient = true;
        //        }
        //    } else
        //    {
        //        _logger.LogDebug("Pts for authKeyId not found,userId={UserId},tempAuthKeyId={TempAuthKeyId:x2} permAuthKeyId={PermAuthKeyId:x2}", eventData.UserId, eventData.TempAuthKeyId, eventData.PermAuthKeyId);
        //    }
        //} else
        //{
        //    _logger.LogWarning("Pts for userId={UserId} not found", eventData.UserId);
        //}

        //if (needPushNewUpdatesToClient)
        //{

        await _scheduleAppService.ExecuteAsync(() => {
                var updatesTooLong = new TUpdatesTooLong();
                _objectMessageSender.PushSessionMessageToAuthKeyIdAsync(eventData.TempAuthKeyId, updatesTooLong);
            },
            TimeSpan.FromMilliseconds(1500)).ConfigureAwait(false);
        //}
    }
}
