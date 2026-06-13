namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// Get a list of <a href="https://corefork.telegram.org/api/sponsored-messages">sponsored messages for a peer, see here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.getSponsoredMessages"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetSponsoredMessagesHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetSponsoredMessages, MyTelegram.Schema.Messages.ISponsoredMessages>
{
    protected override Task<MyTelegram.Schema.Messages.ISponsoredMessages> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestGetSponsoredMessages obj)
    {
        return Task.FromResult<ISponsoredMessages>(new TSponsoredMessagesEmpty());
    }
}