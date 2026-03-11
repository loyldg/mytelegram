namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getEmojiGameInfo"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetEmojiGameInfoHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetEmojiGameInfo, MyTelegram.Schema.Messages.IEmojiGameInfo>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.Messages.IEmojiGameInfo> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetEmojiGameInfo obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IEmojiGameInfo>(new TEmojiGameUnavailable());
    }
}