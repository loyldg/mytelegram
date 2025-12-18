namespace MyTelegram.Messenger.Handlers.LatestLayer.Chatlists;
/// <summary>
/// Fetch new chats associated with an imported <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>. Must be invoked at most every <code>chatlist_update_period</code> seconds (as per the related <a href="https://corefork.telegram.org/api/config#chatlist-update-period">client configuration parameter »</a>).
/// Possible errors
/// Code Type Description
/// 400 FILTER_ID_INVALID The specified filter ID is invalid.
/// 400 FILTER_NOT_SUPPORTED The specified filter cannot be used in this context.
/// 400 INPUT_CHATLIST_INVALID The specified folder is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/chatlists.getChatlistUpdates"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetChatlistUpdatesHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestGetChatlistUpdates, MyTelegram.Schema.Chatlists.IChatlistUpdates>
{
    protected override Task<MyTelegram.Schema.Chatlists.IChatlistUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Chatlists.RequestGetChatlistUpdates obj)
    {
        throw new NotImplementedException();
    }
}