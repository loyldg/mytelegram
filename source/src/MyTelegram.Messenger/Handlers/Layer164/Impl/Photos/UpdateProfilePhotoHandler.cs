// ReSharper disable All

namespace MyTelegram.Handlers.Photos;

///<summary>
/// Installs a previously uploaded photo as a profile photo.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ALBUM_PHOTOS_TOO_MANY You have uploaded too many profile photos, delete some before retrying.
/// 400 FILE_PARTS_INVALID The number of file parts is invalid.
/// 500 FILE_WRITE_EMPTY &nbsp;
/// 400 IMAGE_PROCESS_FAILED Failure while processing image.
/// 400 LOCATION_INVALID The provided location is invalid.
/// 400 PHOTO_CROP_SIZE_SMALL Photo is too small.
/// 400 PHOTO_EXT_INVALID The extension of the photo is invalid.
/// 400 PHOTO_ID_INVALID Photo ID invalid.
/// See <a href="https://corefork.telegram.org/method/photos.updateProfilePhoto" />
///</summary>
internal sealed class UpdateProfilePhotoHandler : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestUpdateProfilePhoto, MyTelegram.Schema.Photos.IPhoto>,
    Photos.IUpdateProfilePhotoHandler
{
    protected override Task<MyTelegram.Schema.Photos.IPhoto> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Photos.RequestUpdateProfilePhoto obj)
    {
        throw new NotImplementedException();
    }
}
