// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Save a message <a href="https://corefork.telegram.org/api/drafts">draft</a> associated to a chat.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ENTITY_BOUNDS_INVALID A specified <a href="https://corefork.telegram.org/api/entities#entity-length">entity offset or length</a> is invalid, see <a href="https://corefork.telegram.org/api/entities#entity-length">here »</a> for info on how to properly compute the entity offset/length.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.saveDraft" />
///</summary>
internal sealed class SaveDraftHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSaveDraft, IBool>,
    Messages.ISaveDraftHandler, IProcessedHandler
{
    private readonly ICommandBus _commandBus;
    private readonly IPeerHelper _peerHelper;
    private readonly IAccessHashHelper _accessHashHelper;
    public SaveDraftHandler(ICommandBus commandBus,
        IPeerHelper peerHelper,
        IAccessHashHelper accessHashHelper)
    {
        _commandBus = commandBus;
        _peerHelper = peerHelper;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveDraft obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        int? replyToMsgId = null;
        switch (obj.ReplyTo)
        {
            case null:
                break;
            case TInputReplyToMessage inputReplyToMessage:
                replyToMsgId = inputReplyToMessage.ReplyToMsgId;
                break;
            case TInputReplyToStory inputReplyToStory:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        var peer = _peerHelper.GetPeer(obj.Peer, input.UserId);
        var dialogId = DialogId.Create(input.UserId, peer);
        var saveDraftCommand = new SaveDraftCommand(dialogId,
            input.ToRequestInfo(),
            obj.Message,
            obj.NoWebpage,
            CurrentDate,
            replyToMsgId,
            null);
        await _commandBus.PublishAsync(saveDraftCommand, CancellationToken.None);

        return new TBoolTrue();
    }
}
