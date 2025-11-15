namespace MyTelegram.Messenger.Handlers.LatestLayer.Contacts;
/// <summary>
/// Get users and geochats near you, see <a href="https://corefork.telegram.org/api/nearby">here »</a> for more info.
/// Possible errors
/// Code Type Description
/// 406 BUSINESS_ADDRESS_ACTIVE The user is currently advertising a <a href="https://corefork.telegram.org/api/business#location">Business Location</a>, the location may only be changed (or removed) using <a href="https://corefork.telegram.org/method/account.updateBusinessLocation">account.updateBusinessLocation »</a>.  .
/// 400 GEO_POINT_INVALID Invalid geoposition provided.
/// 406 USERPIC_PRIVACY_REQUIRED You need to disable privacy settings for your profile picture in order to make your geolocation public.
/// 406 USERPIC_UPLOAD_REQUIRED You must have a profile picture to publish your geolocation.
/// <para><c>See <a href="https://corefork.telegram.org/method/contacts.getLocated"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class GetLocatedHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetLocated, MyTelegram.Schema.IUpdates>
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Contacts.RequestGetLocated obj)
    {
        return Task.FromResult<IUpdates>(new TUpdates { Chats = [], Users = [], Date = CurrentDate, Updates = [] });
    }
}