namespace MyTelegram.Messenger.Handlers.LatestLayer.Phone;
/// <summary>
/// Rate a call, returns info about the rating message sent to the official VoIP bot.
/// Possible errors
/// Code Type Description
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/phone.setCallRating"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SetCallRatingHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSetCallRating, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Phone.RequestSetCallRating obj)
    {
        throw new NotImplementedException();
    }
}