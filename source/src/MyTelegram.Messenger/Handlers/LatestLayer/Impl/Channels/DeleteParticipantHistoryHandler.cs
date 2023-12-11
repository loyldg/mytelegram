// ReSharper disable All

namespace MyTelegram.Handlers.Channels;

///<summary>
/// Delete all messages sent by a specific participant of a given supergroup
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PARTICIPANT_ID_INVALID The specified participant ID is invalid.
/// See <a href="https://corefork.telegram.org/method/channels.deleteParticipantHistory" />
///</summary>
internal sealed class DeleteParticipantHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestDeleteParticipantHistory, MyTelegram.Schema.Messages.IAffectedHistory>,
    Channels.IDeleteParticipantHistoryHandler
{
    private readonly IQueryProcessor _queryProcessor;
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IPtsHelper _ptsHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public DeleteParticipantHistoryHandler(IQueryProcessor queryProcessor,
        ICommandBus commandBus,
        IPeerHelper peerHelper,
        IPtsHelper ptsHelper,
        IAccessHashHelper accessHashHelper)
    {
        _queryProcessor = queryProcessor;
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _ptsHelper = ptsHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Channels.RequestDeleteParticipantHistory obj)
    {
        if (obj.Channel is TInputChannel inputChannel)
        {
            await _accessHashHelper.CheckAccessHashAsync(inputChannel.ChannelId, inputChannel.AccessHash);

            var peer = _peerHelper.GetPeer(obj.Participant);
            var messageIds = await _queryProcessor
                .ProcessAsync(new GetMessageIdListByUserIdQuery(inputChannel.ChannelId,
                        peer.PeerId,
                        MyTelegramServerDomainConsts.ClearHistoryDefaultPageSize),
                    default);

            if (messageIds.Count > 0)
            {
                var command = new StartDeleteParticipantHistoryCommand(ChannelId.Create(inputChannel.ChannelId),
                    input.ToRequestInfo(),
                    messageIds.ToList()
                );
                await _commandBus.PublishAsync(command, default);

                return null!;
            }
            else
            {
                return new TAffectedHistory
                {
                    Pts = _ptsHelper.GetCachedPts(inputChannel.ChannelId),
                    PtsCount = 0,
                    Offset = 0
                };
            }
        }

        throw new NotImplementedException();
    }
}
