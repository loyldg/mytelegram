namespace MyTelegram.Messenger.Handlers.LatestLayer.Chatlists;

///<summary>
/// Join channels and supergroups recently added to a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// See <a href="https://corefork.telegram.org/method/chatlists.joinChatlistUpdates" />
///</summary>
internal sealed class JoinChatlistUpdatesHandler : RpcResultObjectHandler<MyTelegram.Schema.Chatlists.RequestJoinChatlistUpdates, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Chatlists.RequestJoinChatlistUpdates obj)
    {
        throw new NotImplementedException();
    }
}
