namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

///<summary>
/// Save a message <a href="https://corefork.telegram.org/api/drafts">draft</a> associated to a chat.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ENTITY_BOUNDS_INVALID A specified <a href="https://corefork.telegram.org/api/entities#entity-length">entity offset or length</a> is invalid, see <a href="https://corefork.telegram.org/api/entities#entity-length">here»</a> for info on how to properly compute the entity offset/length.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.saveDraft" />
///</summary>
internal sealed class SaveDraftHandler(
    ICommandBus commandBus,
    IPeerHelper peerHelper,
    IMediaHelper mediaHelper,
    IAccessHashHelper accessHashHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSaveDraft, IBool>
{
    protected override async Task<IBool> HandleCoreAsync(IRequestInput input,
        RequestSaveDraft obj)
    {
        await accessHashHelper.CheckAccessHashAsync(input, obj.Peer);
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
        var peer = peerHelper.GetPeer(obj.Peer, input.UserId);
        var dialogId = DialogId.Create(input.UserId, peer);
        IMessageMedia? media = null;
        if (obj.Media != null)
        {
            media = await mediaHelper.SaveMediaAsync(obj.Media);
        }
        var saveDraftCommand = new SaveDraftCommand(dialogId,
            input.ToRequestInfo(),
            new Draft(obj.NoWebpage, obj.InvertMedia, replyToMsgId, obj.Message, CurrentDate,
                entities2: obj.Entities, media: media, effect: obj.Effect, media2: obj.Media, replyTo: obj.ReplyTo));
        await commandBus.PublishAsync(saveDraftCommand);

        return new TBoolTrue();
    }
}
