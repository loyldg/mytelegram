// ReSharper disable All

namespace MyTelegram.Handlers.Contacts;

///<summary>
/// Get contacts near you
/// <para>Possible errors</para>
/// Code Type Description
/// 400 GEO_POINT_INVALID Invalid geoposition provided.
/// 406 USERPIC_PRIVACY_REQUIRED You need to disable privacy settings for your profile picture in order to make your geolocation public.
/// 406 USERPIC_UPLOAD_REQUIRED You must have a profile picture to publish your geolocation.
/// See <a href="https://corefork.telegram.org/method/contacts.getLocated" />
///</summary>
internal sealed class GetLocatedHandler : RpcResultObjectHandler<MyTelegram.Schema.Contacts.RequestGetLocated, MyTelegram.Schema.IUpdates>,
    Contacts.IGetLocatedHandler
{
    protected override Task<MyTelegram.Schema.IUpdates> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Contacts.RequestGetLocated obj)
    {
        throw new NotImplementedException();
    }
}
