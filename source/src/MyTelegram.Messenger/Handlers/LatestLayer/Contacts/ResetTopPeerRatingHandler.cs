namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Reset <a href="https://corefork.telegram.org/api/top-rating">rating</a> of top peer
/// Possible errors
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.resetTopPeerRating"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ResetTopPeerRatingHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResetTopPeerRating, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Contacts.RequestResetTopPeerRating obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}