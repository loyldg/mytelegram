// ReSharper disable All

namespace MyTelegram.Handlers.Photos;

///<summary>
/// Upload a custom profile picture for a contact, or suggest a new profile picture to a contact.The <code>file</code>, <code>video</code> and <code>video_emoji_markup</code> flags are mutually exclusive.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/photos.uploadContactProfilePhoto" />
///</summary>
internal sealed class UploadContactProfilePhotoHandler : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestUploadContactProfilePhoto, MyTelegram.Schema.Photos.IPhoto>,
    Photos.IUploadContactProfilePhotoHandler
{
    protected override Task<MyTelegram.Schema.Photos.IPhoto> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Photos.RequestUploadContactProfilePhoto obj)
    {
        throw new NotImplementedException();
    }
}
