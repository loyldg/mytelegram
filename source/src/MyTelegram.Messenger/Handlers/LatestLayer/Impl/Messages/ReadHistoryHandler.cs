// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Marks message history as read.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.readHistory" />
///</summary>
internal sealed class ReadHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadHistory, MyTelegram.Schema.Messages.IAffectedMessages>,
    Messages.IReadHistoryHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IQueryProcessor _queryProcessor;
    public ReadHistoryHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IAccessHashHelper accessHashHelper, IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IAffectedMessages> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestReadHistory obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var selfDialogId = DialogId.Create(input.UserId, peer);

        var unreadCount =
            await _queryProcessor.ProcessAsync(new GetUnreadCountQuery(input.UserId, peer.PeerId, obj.MaxId), default);

        var readInboxMessageCommand = new ReadInboxMessageCommand2(selfDialogId,
            input.ToRequestInfo(),
            input.UserId,
            input.UserId,
            obj.MaxId,
            unreadCount,
            peer,
            CurrentDate
            );
        // Console.WriteLine("SourceId:{0}",readInboxMessageCommand.GetSourceId().Value);
        await _commandBus.PublishAsync(readInboxMessageCommand, default);

        return null!;
    }
}
