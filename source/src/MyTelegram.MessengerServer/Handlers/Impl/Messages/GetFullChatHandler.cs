using MyTelegram.Domain.Aggregates.PeerNotifySettings;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using IChatFull = MyTelegram.Schema.Messages.IChatFull;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetFullChatHandler : RpcResultObjectHandler<RequestGetFullChat, IChatFull>,
    IGetFullChatHandler, IProcessedHandler
{
    private readonly ILogger<GetFullChatHandler> _logger;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IRpcResultProcessor _rpcResultProcessor;
    private readonly ITlChatConverter _chatConverter;
    public GetFullChatHandler(IQueryProcessor queryProcessor,
        IRpcResultProcessor rpcResultProcessor,
        ILogger<GetFullChatHandler> logger,
        IPeerHelper peerHelper,
        ITlChatConverter chatConverter)
    {
        _queryProcessor = queryProcessor;
        _rpcResultProcessor = rpcResultProcessor;
        _logger = logger;
        _peerHelper = peerHelper;
        _chatConverter = chatConverter;
    }

    protected override async Task<IChatFull> HandleCoreAsync(IRequestInput input,
        RequestGetFullChat obj)
    {
        var peerType = _peerHelper.GetPeerType(obj.ChatId);
        switch (peerType)
        {
            case PeerType.Channel:
                {
                    var channel = await _queryProcessor.ProcessAsync(new GetChannelByIdQuery(obj.ChatId),
                        CancellationToken.None).ConfigureAwait(false);
                    var channelFull = await _queryProcessor.ProcessAsync(new GetChannelFullByIdQuery(obj.ChatId),
                        CancellationToken.None).ConfigureAwait(false);

                    var channelMember = await _queryProcessor
                        .ProcessAsync(new GetChannelMemberByUidQuery(obj.ChatId, input.UserId), default)
                        .ConfigureAwait(false);
                    var peerNotifySettings = await _queryProcessor
                        .ProcessAsync(
                            new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                                PeerType.Channel,
                                obj.ChatId)),
                            CancellationToken.None).ConfigureAwait(false);

                    return _chatConverter.ToChatFull(channel,
                        channelFull!,
                        channelMember,
                        peerNotifySettings,
                        input.UserId);
                }
            case PeerType.Chat:
                {
                    var chat = await _queryProcessor
                        .ProcessAsync(new GetChatByChatIdQuery(obj.ChatId), CancellationToken.None)
                        .ConfigureAwait(false);

                    if (chat == null)
                    {
                        ThrowHelper.ThrowUserFriendlyException("CHAT_ID_INVALID");
                    }

                    var userList = await _queryProcessor
                        .ProcessAsync(new GetUsersByUidListQuery(chat!.ChatMembers.Select(p => p.UserId).ToList()),
                            CancellationToken.None).ConfigureAwait(false);


                    var peerNotifySettings = await _queryProcessor
                        .ProcessAsync(
                            new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                                PeerType.Chat,
                                obj.ChatId)),
                            CancellationToken.None).ConfigureAwait(false);



                    return _rpcResultProcessor.ToChatFull(chat,
                        userList,
                        peerNotifySettings,
                        input.UserId);
                }
        }

        throw new NotImplementedException($"Not supported peer type {peerType},chatId={obj.ChatId}");
    }
}
