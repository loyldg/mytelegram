namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Returns chat basic info on their IDs.
/// Possible errors
/// Code Type Description
/// 400 CHAT_ID_INVALID The provided chat id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getChats"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class GetChatsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetChats, MyTelegram.Schema.Messages.IChats>
{
    protected override Task<MyTelegram.Schema.Messages.IChats> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetChats obj)
    {
        throw new NotImplementedException();
    }
}