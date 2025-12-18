namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Set maximum Time-To-Live of all messages in the specified chat
/// Possible errors
/// Code Type Description
/// 400 CHAT_NOT_MODIFIED No changes were made to chat information because the new information you passed is identical to the current information.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 TTL_PERIOD_INVALID The specified TTL period is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.setHistoryTTL"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetHistoryTTLHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSetHistoryTTL, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSetHistoryTTL obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates { Updates = [], Chats = [], Users = [], Date = CurrentDate });
    }
}