// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Mark a <a href="https://corefork.telegram.org/api/threads">thread</a> as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.readDiscussion" />
///</summary>
internal sealed class ReadDiscussionHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestReadDiscussion, IBool>,
    Messages.IReadDiscussionHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    private readonly IQueryProcessor _queryProcessor;
    public ReadDiscussionHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IAccessHashHelper accessHashHelper, IQueryProcessor queryProcessor)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
        _queryProcessor = queryProcessor;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestReadDiscussion obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var selfDialogId = DialogId.Create(input.UserId, peer);

        var messageReadModel =
            await _queryProcessor.ProcessAsync(new GetMessageByIdQuery(MessageId.Create(peer.PeerId, obj.MsgId).Value), default);

        if (messageReadModel == null)
        {
            RpcErrors.RpcErrors400.MessageIdInvalid.ThrowRpcError();
        }

        //Console.WriteLine($"ReqMsgId={input.ReqMsgId} {input.UserId} ReadDiscussion:{obj.MsgId} {obj.ReadMaxId}");
        var command = new ReadChannelInboxMessageCommand(
            selfDialogId,
            input.ToRequestInfo(),
            input.UserId,
            peer.PeerId,
            obj.ReadMaxId,
            messageReadModel!.SenderPeerId,
            obj.MsgId);
        await _commandBus.PublishAsync(command, CancellationToken.None);

        return new TBoolTrue();
    }
}
