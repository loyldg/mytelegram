namespace MyTelegram.Messenger.Handlers.Stories;
/// <summary>
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/stories.startLive"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class StartLiveHandler : RpcResultObjectHandler<MyTelegram.Schema.Stories.RequestStartLive, MyTelegram.Schema.IUpdates>, IObjectHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Stories.RequestStartLive obj)
    {
        throw new NotImplementedException();
    }
}