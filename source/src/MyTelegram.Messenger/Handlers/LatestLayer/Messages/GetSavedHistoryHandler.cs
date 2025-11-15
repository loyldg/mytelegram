namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Fetch <a href="https://corefork.telegram.org/api/saved-messages">saved messages »</a> forwarded from a specific peer, or fetch messages from a <a href="https://corefork.telegram.org/api/monoforum">monoforum topic »</a>.
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getSavedHistory"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedHistoryHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSavedHistory, MyTelegram.Schema.Messages.IMessages>
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetSavedHistory obj)
    {
        return Task.FromResult<MyTelegram.Schema.Messages.IMessages>(new TMessages { Chats = [], Messages = [], Users = [], Topics = [] });
    }
}