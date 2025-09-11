namespace MyTelegram.Messenger.Handlers.LatestLayer.Updates;

///<summary>
/// Returns the difference between the current state of updates of a certain channel and transmitted.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 406 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 403 CHANNEL_PUBLIC_GROUP_NA channel/supergroup not available.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 FROM_MESSAGE_BOT_DISABLED Bots can't use fromMessage min constructors.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PERSISTENT_TIMESTAMP_EMPTY Persistent timestamp empty.
/// 400 PERSISTENT_TIMESTAMP_INVALID Persistent timestamp invalid.
/// 500 PERSISTENT_TIMESTAMP_OUTDATED Channel internal replication issues, try again later (treat this like an RPC_CALL_FAIL).
/// 400 PINNED_DIALOGS_TOO_MUCH Too many pinned dialogs.
/// 400 RANGES_INVALID Invalid range provided.
/// 400 USER_BANNED_IN_CHANNEL You're banned from sending messages in supergroups/channels.
/// See <a href="https://corefork.telegram.org/method/updates.getChannelDifference" />
///</summary>
internal sealed class GetChannelDifferenceHandler(
    IMessageAppService messageAppService,
    IQueryProcessor queryProcessor,
    IAckCacheService ackCacheService,
    //ILayeredService<IDifferenceConverter> layeredService,
    IDifferenceConverterService differenceConverterService,
    IAccessHashHelper accessHashHelper,
    ILogger<GetChannelDifferenceHandler> logger)
    : RpcResultObjectHandler<MyTelegram.Schema.Updates.RequestGetChannelDifference,
            MyTelegram.Schema.Updates.IChannelDifference>
{
    //private readonly IRpcResultProcessor _rpcResultProcessor;
    private readonly ILogger<GetChannelDifferenceHandler> _logger = logger;

    protected override async Task<MyTelegram.Schema.Updates.IChannelDifference> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Updates.RequestGetChannelDifference obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Channel);
        if (obj.Channel is TInputChannel inputChannel)
        {
            var isChannelMember = true;
            var channelMemberReadModel = await queryProcessor
                .ProcessAsync(new GetChannelMemberByUserIdQuery(inputChannel.ChannelId, input.UserId));
            isChannelMember = channelMemberReadModel != null;

            if (channelMemberReadModel != null && channelMemberReadModel.Kicked)
            {
                RpcErrors.RpcErrors403.ChannelPublicGroupNa.ThrowRpcError();
            }

            var limit = obj.Limit == 0 ? MyTelegramConsts.DefaultPtsTotalLimit : obj.Limit;
            limit = Math.Min(limit, MyTelegramConsts.DefaultPtsTotalLimit);
            var pts = obj.Pts;
            //if (pts == 1)
            //{
            //    pts = 0;
            //}
            var updatesReadModels = await queryProcessor
                .ProcessAsync(new GetUpdatesQuery(input.UserId, inputChannel.ChannelId, pts, 0, limit));

            var messageIds = updatesReadModels.Where(p => p.UpdatesType == UpdatesType.NewMessages)
                 .Select(p => p.MessageId ?? 0)
                 .ToList()
                 ;
            var users = updatesReadModels.SelectMany(p => p.Users ?? []).ToList();
            var chats = updatesReadModels.SelectMany(p => p.Chats ?? []).ToList();
            chats.Add(inputChannel.ChannelId);

            var dto = await messageAppService
                .GetChannelDifferenceAsync(new GetDifferenceInput(input.UserId, inputChannel.ChannelId,
                    obj.Pts,
                    obj.Limit, messageIds, users, chats));

            var maxPts = 0;

            if (updatesReadModels.Count > 0)
            {
                maxPts = updatesReadModels.Max(p => p.Pts);
                var channelMaxGlobalSeqNo = updatesReadModels.Max(p => p.GlobalSeqNo);

                await ackCacheService.AddRpcPtsToCacheAsync(input.ReqMsgId,
                    0,
                    channelMaxGlobalSeqNo,
                    new Peer(PeerType.Channel, inputChannel.ChannelId),
                    true);
            }

            var allUpdateList = updatesReadModels.Where(p => p.UpdatesType == UpdatesType.Updates)
                .SelectMany(p => p.Updates ?? []).ToList();
            var r = differenceConverterService.ToChannelDifference(input, dto, isChannelMember, allUpdateList, maxPts, layer: input.Layer);

            return r;
        }

        throw new NotImplementedException();
    }
}
