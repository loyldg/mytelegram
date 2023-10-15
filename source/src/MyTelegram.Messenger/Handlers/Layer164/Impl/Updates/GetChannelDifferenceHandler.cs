// ReSharper disable All

using Microsoft.Extensions.Logging;

namespace MyTelegram.Handlers.Updates;

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
internal sealed class GetChannelDifferenceHandler : RpcResultObjectHandler<MyTelegram.Schema.Updates.RequestGetChannelDifference, MyTelegram.Schema.Updates.IChannelDifference>,
    Updates.IGetChannelDifferenceHandler
{
    private readonly IAckCacheService _ackCacheService;
    private readonly IMessageAppService _messageAppService;
    private readonly IQueryProcessor _queryProcessor;
    //private readonly IRpcResultProcessor _rpcResultProcessor;
    private readonly ILayeredService<IDifferenceConverter> _layeredService;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly ILogger<GetChannelDifferenceHandler> _logger;
    public GetChannelDifferenceHandler(IMessageAppService messageAppService,
        IQueryProcessor queryProcessor,
        IAckCacheService ackCacheService,
        ILayeredService<IDifferenceConverter> layeredService,
        IAccessHashHelper accessHashHelper, ILogger<GetChannelDifferenceHandler> logger)
    {
        _messageAppService = messageAppService;
        _queryProcessor = queryProcessor;
        _ackCacheService = ackCacheService;
        _layeredService = layeredService;
        _accessHashHelper = accessHashHelper;
        _logger = logger;
    }

    protected override async Task<MyTelegram.Schema.Updates.IChannelDifference> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Updates.RequestGetChannelDifference obj)
    {

        await _accessHashHelper.CheckAccessHashAsync(obj.Channel);
        if (obj.Channel is TInputChannel inputChannel)
        {
            Console.WriteLine($"[{input.UserId}]get channel difference:{inputChannel.ChannelId} pts:{obj.Pts}");

            var isChannelMember = true;
            var channelMemberReadModel = await _queryProcessor
                .ProcessAsync(new GetChannelMemberByUidQuery(inputChannel.ChannelId, input.UserId), default)
         ;
            isChannelMember = channelMemberReadModel != null;

            if (channelMemberReadModel != null && channelMemberReadModel.Kicked)
            {
                //ThrowHelper.ThrowUserFriendlyException("CHANNEL_PUBLIC_GROUP_NA");
                RpcErrors.RpcErrors403.ChannelPublicGroupNa.ThrowRpcError();
            }

            var limit = obj.Limit == 0 ? MyTelegramServerDomainConsts.DefaultPtsTotalLimit : obj.Limit;
            limit = Math.Min(limit, MyTelegramServerDomainConsts.DefaultPtsTotalLimit);
            var pts = obj.Pts;
            //if (pts == 1)
            //{
            //    pts = 0;
            //}
            var updatesReadModels = await _queryProcessor
                .ProcessAsync(new GetUpdatesQuery(input.UserId, inputChannel.ChannelId, pts, 0, limit), default);

            //Console.WriteLine($"=============== {input.UserId} {inputChannel.ChannelId}   updates:{updatesReadModels.Count}  pts:{obj.Pts}");
            _logger.LogWarning("##### GetChannelDifferenceHandler:{UserId}  channelId={ChannelId} pts={Pts}  updates count={Count}", input.UserId, inputChannel.ChannelId, obj.Pts, updatesReadModels.Count);

            var messageIds = updatesReadModels.Where(p => p.UpdatesType == UpdatesType.NewMessages)
                 .Select(p => p.MessageId ?? 0)
                 .ToList()
                 ;
            var users = updatesReadModels.SelectMany(p => p.Users ?? new List<long>(0)).ToList();
            var chats = updatesReadModels.SelectMany(p => p.Chats ?? new List<long>(0)).ToList();
            chats.Add(inputChannel.ChannelId);

            var dto = await _messageAppService
                .GetChannelDifferenceAsync(new GetDifferenceInput(input.UserId, inputChannel.ChannelId,
                    obj.Pts,
                    obj.Limit, messageIds, users, chats));

            var maxPts = 0;

            if (updatesReadModels.Any())
            {
                maxPts = updatesReadModels.Max(p => p.Pts);
                var channelMaxGlobalSeqNo = updatesReadModels.Max(p => p.GlobalSeqNo);

                await _ackCacheService.AddRpcPtsToCacheAsync(input.ReqMsgId,
                    0,
                    channelMaxGlobalSeqNo,
                    new Peer(PeerType.Channel, inputChannel.ChannelId));
            }

            var allUpdateList = updatesReadModels.Where(p => p.UpdatesType == UpdatesType.Updates)
                .SelectMany(p => p.Updates.ToTObject<TVector<IUpdate>>()).ToList();
            _logger.LogInformation("Get channelDifference:updatesCount={Count} {@Input} {@Data},fromPts={Pts} channel updates count={Count}", updatesReadModels.Count, input, new { }, obj.Pts, updatesReadModels.Count);
            return _layeredService.GetConverter(input.Layer).ToChannelDifference(dto, isChannelMember, allUpdateList, maxPts);
        }

        throw new NotImplementedException();
    }
}
