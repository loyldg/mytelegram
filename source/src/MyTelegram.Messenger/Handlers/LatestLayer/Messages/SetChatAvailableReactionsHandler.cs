namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Change the set of <a href="https://corefork.telegram.org/api/reactions">message reactions »</a> that can be used in a certain group, supergroup or channel
/// Possible errors
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 DOCUMENT_INVALID The specified document is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 REACTION_INVALID The specified reaction is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.setChatAvailableReactions"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetChatAvailableReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetChatAvailableReactions, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSetChatAvailableReactions obj)
    {
        throw new NotImplementedException();
    }
}