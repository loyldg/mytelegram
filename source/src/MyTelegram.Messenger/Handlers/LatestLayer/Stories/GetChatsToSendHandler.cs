namespace MyTelegram.Messenger.Handlers.LatestLayer.Stories;
/// <summary>
/// Obtain a list of channels where the user can post <a href="https://corefork.telegram.org/api/stories">stories</a>
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.getChatsToSend"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetChatsToSendHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestGetChatsToSend, MyTelegram.Schema.Messages.IChats>
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestGetChatsToSend obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IChats>(new TChats { Chats = [] });
    }
}