namespace MyTelegram.Messenger.Handlers.LatestLayer.Channels;
/// <summary>
/// Can only be invoked by non-bot admins of a <a href="https://corefork.telegram.org/api/monoforum">monoforum »</a>, obtains the original sender of a message sent by other monoforum admins to the monoforum, on behalf of the channel associated to the monoforum.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/channels.getMessageAuthor"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetMessageAuthorHandler : RpcResultObjectHandler<MyTelegram.Schema.Channels.RequestGetMessageAuthor, MyTelegram.Schema.IUser>
{
    protected override Task<MyTelegram.Schema.IUser> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Channels.RequestGetMessageAuthor obj)
    {
        throw new NotImplementedException();
    }
}