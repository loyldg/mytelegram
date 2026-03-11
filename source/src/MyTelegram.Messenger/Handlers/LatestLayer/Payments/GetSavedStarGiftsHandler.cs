using MyTelegram.Schema.Payments;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Payments;
/// <summary>
/// Fetch the full list of <a href="https://corefork.telegram.org/api/gifts">gifts</a> owned by a peer.Note that unlike what the name suggests, the method can be used to fetch both "saved" and "unsaved" gifts (aka gifts both pinned and not pinned) to the profile, depending on the passed flags.
/// Possible errors
/// Code Type Description
/// 400 BUSINESS_CONNECTION_INVALID The <code>connection_id</code> passed to the wrapping <a href="https://corefork.telegram.org/api/business">invokeWithBusinessConnection</a> call is invalid.
/// 400 COLLECTION_ID_INVALID  
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/payments.getSavedStarGifts"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class GetSavedStarGiftsHandler : RpcResultObjectHandler<MyTelegram.Schema.Payments.RequestGetSavedStarGifts, MyTelegram.Schema.Payments.ISavedStarGifts>
{
    protected override Task<MyTelegram.Schema.Payments.ISavedStarGifts> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Payments.RequestGetSavedStarGifts obj)
    {
        return Task.FromResult<MyTelegram.Schema.Payments.ISavedStarGifts>(new TSavedStarGifts { Chats = [], Gifts = [], Users = [] });
    }
}