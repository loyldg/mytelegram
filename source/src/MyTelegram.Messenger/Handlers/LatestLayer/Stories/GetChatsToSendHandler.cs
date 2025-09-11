namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;

///<summary>
/// See <a href="https://corefork.telegram.org/method/stories.getChatsToSend" />
///</summary>
internal sealed class GetChatsToSendHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetChatsToSend, MyTelegram.Schema.Messages.IChats>
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Stories.RequestGetChatsToSend obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IChats>(new TChats
        {
            Chats = []
        });
    }
}
