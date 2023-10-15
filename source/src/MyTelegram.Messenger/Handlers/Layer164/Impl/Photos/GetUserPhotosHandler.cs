// ReSharper disable All

using MyTelegram.Schema.Photos;
using IPhoto = MyTelegram.Schema.IPhoto;


namespace MyTelegram.Handlers.Photos;

///<summary>
/// Returns the list of user photos.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 MAX_ID_INVALID The provided max ID is invalid.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/photos.getUserPhotos" />
///</summary>
internal sealed class GetUserPhotosHandler : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestGetUserPhotos, MyTelegram.Schema.Photos.IPhotos>,
    Photos.IGetUserPhotosHandler
{
    protected override Task<MyTelegram.Schema.Photos.IPhotos> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Photos.RequestGetUserPhotos obj)
    {
        return Task.FromResult<MyTelegram.Schema.Photos.IPhotos>(new TPhotos { Photos = new TVector<IPhoto>(), Users = new TVector<IUser>() });
    }
}
