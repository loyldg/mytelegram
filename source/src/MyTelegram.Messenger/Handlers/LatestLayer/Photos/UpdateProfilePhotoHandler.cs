namespace MyTelegram.Messenger.Handlers.LatestLayer.Photos;
/// <summary>
/// Installs a previously uploaded photo as a profile photo.
/// Possible errors
/// Code Type Description
/// 400 ALBUM_PHOTOS_TOO_MANY You have uploaded too many profile photos, delete some before retrying.
/// 400 BOT_FALLBACK_UNSUPPORTED The fallback flag can't be set for bots.
/// 400 FILE_PARTS_INVALID The number of file parts is invalid.
/// 400 IMAGE_PROCESS_FAILED Failure while processing image.
/// 400 LOCATION_INVALID The provided location is invalid.
/// 400 PHOTO_CROP_SIZE_SMALL Photo is too small.
/// 400 PHOTO_EXT_INVALID The extension of the photo is invalid.
/// 400 PHOTO_ID_INVALID Photo ID invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/photos.updateProfilePhoto"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✔] [Anonymous ✖]
/// </remarks>
internal sealed class UpdateProfilePhotoHandler(ICommandBus commandBus) : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestUpdateProfilePhoto, MyTelegram.Schema.Photos.IPhoto>
{
    protected override async Task<MyTelegram.Schema.Photos.IPhoto> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Photos.RequestUpdateProfilePhoto obj)
    {
        var photoId = 0L;
        switch (obj.Id)
        {
            case TInputPhoto inputPhoto:
                photoId = inputPhoto.Id;
                break;
        }

        var command = new UpdateProfilePhotoCommand(UserId.Create(input.UserId), input.ToRequestInfo(), photoId, obj.Fallback);
        await commandBus.PublishAsync(command);
        return null!;
    }
}