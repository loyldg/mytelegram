using MyTelegram.Domain.Aggregates.PeerNotifySettings;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;
using IChatFull = MyTelegram.Schema.Messages.IChatFull;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class GetFullChatHandler : RpcResultObjectHandler<RequestGetFullChat, IChatFull>,
    IGetFullChatHandler, IProcessedHandler
{
    private readonly ITlChatConverter _chatConverter;
    private readonly IPeerHelper _peerHelper;
    private readonly IQueryProcessor _queryProcessor;

    public GetFullChatHandler(IQueryProcessor queryProcessor,
        IPeerHelper peerHelper,
        ITlChatConverter chatConverter)
    {
        _queryProcessor = queryProcessor;
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
                    CancellationToken.None);
                var channelFull = await _queryProcessor.ProcessAsync(new GetChannelFullByIdQuery(obj.ChatId),
                    CancellationToken.None);

                var channelMember = await _queryProcessor
                        .ProcessAsync(new GetChannelMemberByUidQuery(obj.ChatId, input.UserId), default)
                    ;
                var peerNotifySettings = await _queryProcessor
                    .ProcessAsync(
                        new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                            PeerType.Channel,
                            obj.ChatId)),
                        CancellationToken.None);

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
                    ;

                if (chat == null) ThrowHelper.ThrowUserFriendlyException("CHAT_ID_INVALID");

                var userList = await _queryProcessor
                    .ProcessAsync(new GetUsersByUidListQuery(chat!.ChatMembers.Select(p => p.UserId).ToList()),
                        CancellationToken.None);

                var peerNotifySettings = await _queryProcessor
                    .ProcessAsync(
                        new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                            PeerType.Chat,
                            obj.ChatId)),
                        CancellationToken.None);

                return _chatConverter.ToChatFull(chat,
                    userList,
                    peerNotifySettings,
                    input.UserId);
            }
        }

        throw new NotImplementedException($"Not supported peer type {peerType},chatId={obj.ChatId}");
    }
}