namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Get full info about a <a href="https://corefork.telegram.org/api/channel#basic-groups">basic group</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getFullChat" />
///</summary>
internal sealed class GetFullChatHandler(
    IQueryProcessor queryProcessor,
    IPeerHelper peerHelper,
    IChatConverterService chatConverterService,
    IPhotoAppService photoAppService,
    IChannelAppService channelAppService)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetFullChat, MyTelegram.Schema.Messages.IChatFull>
{
    protected override async Task<MyTelegram.Schema.Messages.IChatFull> HandleCoreAsync(IRequestInput input,
        RequestGetFullChat obj)
    {
        var peerType = peerHelper.GetPeerType(obj.ChatId);
        switch (peerType)
        {
            case PeerType.Channel:
                {
                    var channel = await channelAppService.GetAsync(obj.ChatId);
                    var channelFull = await queryProcessor.ProcessAsync(new GetChannelFullByIdQuery(obj.ChatId));

                    var channelMember = await queryProcessor
                        .ProcessAsync(new GetChannelMemberByUserIdQuery(obj.ChatId, input.UserId));
                    var peerNotifySettings = await queryProcessor
                        .ProcessAsync(
                            new GetPeerNotifySettingsByIdQuery(PeerNotifySettingsId.Create(input.UserId,
                                PeerType.Channel,
                                obj.ChatId).Value),
                            CancellationToken.None);
                    var photoReadModel = await photoAppService.GetAsync(channel.PhotoId);
                    return chatConverterService.ToChannelFull(
                        input,
                        channel,
                        photoReadModel,
                        channelFull!,
                        channelMember,
                        peerNotifySettings,
                        null,
                        input.Layer
                        );
                }
        }

        throw new NotImplementedException($"Not supported peer type {peerType},chatId={obj.ChatId}");
    }
}
