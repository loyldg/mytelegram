// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Change the set of <a href="https://corefork.telegram.org/api/reactions">message reactions »</a> that can be used in a certain group, supergroup or channel
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.setChatAvailableReactions" />
///</summary>
internal sealed class SetChatAvailableReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetChatAvailableReactions, MyTelegram.Schema.IUpdates>,
    Messages.ISetChatAvailableReactionsHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestSetChatAvailableReactions obj)
    {
        throw new NotImplementedException();
    }
}
