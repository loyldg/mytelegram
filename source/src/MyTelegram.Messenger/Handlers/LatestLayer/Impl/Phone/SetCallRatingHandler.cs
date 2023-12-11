// ReSharper disable All

namespace MyTelegram.Handlers.Phone;

///<summary>
/// Rate a call, returns info about the rating message sent to the official VoIP bot.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CALL_PEER_INVALID The provided call peer object is invalid.
/// See <a href="https://corefork.telegram.org/method/phone.setCallRating" />
///</summary>
internal sealed class SetCallRatingHandler : RpcResultObjectHandler<MyTelegram.Schema.Phone.RequestSetCallRating, MyTelegram.Schema.IUpdates>,
    Phone.ISetCallRatingHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Phone.RequestSetCallRating obj)
    {
        throw new NotImplementedException();
    }
}
