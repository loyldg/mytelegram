// ReSharper disable All

namespace MyTelegram.Handlers.Chatlists;

///<summary>
/// Fetch new chats associated with an imported <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>. Must be invoked at most every <code>chatlist_update_period</code> seconds (as per the related <a href="https://corefork.telegram.org/api/config#chatlist-update-period">client configuration parameter »</a>).
/// <para>Possible errors</para>
/// Code Type Description
/// 400 INPUT_CHATLIST_INVALID &nbsp;
/// See <a href="https://corefork.telegram.org/method/chatlists.getChatlistUpdates" />
///</summary>
internal sealed class GetChatlistUpdatesHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestGetChatlistUpdates, MyTelegram.Schema.Chatlists.IChatlistUpdates>,
    Chatlists.IGetChatlistUpdatesHandler
{
    protected override Task<MyTelegram.Schema.Chatlists.IChatlistUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestGetChatlistUpdates obj)
    {
        throw new NotImplementedException();
    }
}
