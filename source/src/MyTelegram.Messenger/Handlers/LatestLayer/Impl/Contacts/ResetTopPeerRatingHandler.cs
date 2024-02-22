// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Reset <a href="https://corefork.telegram.org/api/top-rating">rating</a> of top peer
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/contacts.resetTopPeerRating" />
///</summary>
internal sealed class ResetTopPeerRatingHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestResetTopPeerRating, IBool>,
    Contacts.IResetTopPeerRatingHandler
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestResetTopPeerRating obj)
    {
        throw new NotImplementedException();
    }
}
