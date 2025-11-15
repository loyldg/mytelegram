namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Adds a peer to a blocklist, see <a href="https://corefork.telegram.org/api/block">here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CONTACT_ID_INVALID The provided contact ID is invalid.
/// 400 INPUT_USER_DEACTIVATED The specified user was deleted.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.block"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class BlockHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestBlock, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, RequestBlock obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}