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
    Messages.ISaveDraftHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSaveDraft obj)
    {
        throw new NotImplementedException();
    }
}
