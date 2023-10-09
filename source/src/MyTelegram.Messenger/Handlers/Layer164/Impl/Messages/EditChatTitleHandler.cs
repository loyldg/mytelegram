// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Changes chat name and sends a service message on it.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 CHAT_TITLE_EMPTY No chat title provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.editChatTitle" />
///</summary>
internal sealed class EditChatTitleHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestEditChatTitle, MyTelegram.Schema.IUpdates>,
    Messages.IEditChatTitleHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestEditChatTitle obj)
    {
        throw new NotImplementedException();
    }
}
