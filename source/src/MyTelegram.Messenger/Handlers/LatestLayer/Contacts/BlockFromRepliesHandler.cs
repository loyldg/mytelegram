namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Stop getting notifications about <a href="https://corefork.telegram.org/api/discussion">discussion replies</a> of a certain user in <code>@replies</code>
/// Possible errors
/// Code Type Description
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.blockFromReplies"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class BlockFromRepliesHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestBlockFromReplies, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Contacts.RequestBlockFromReplies obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates { Updates = [], Chats = [], Users = [], Date = CurrentDate });
    }
}