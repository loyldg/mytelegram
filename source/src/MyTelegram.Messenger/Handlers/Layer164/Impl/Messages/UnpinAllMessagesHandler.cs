// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// <a href="https://corefork.telegram.org/api/pin">Unpin</a> all pinned messages
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// See <a href="https://corefork.telegram.org/method/messages.unpinAllMessages" />
///</summary>
internal sealed class UnpinAllMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestUnpinAllMessages, MyTelegram.Schema.Messages.IAffectedHistory>,
    Messages.IUnpinAllMessagesHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAffectedHistory> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestUnpinAllMessages obj)
    {
        throw new NotImplementedException();
    }
}
