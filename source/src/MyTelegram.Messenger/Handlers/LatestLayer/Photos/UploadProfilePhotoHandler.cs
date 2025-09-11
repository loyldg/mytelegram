namespace MyTelegram.Messenger.Handlers.LatestLayer.Photos;

///<summary>
/// Updates current user profile photo.The <code>file</code>, <code>video</code> and <code>video_emoji_markup</code> flags are mutually exclusive.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 ALBUM_PHOTOS_TOO_MANY You have uploaded too many profile photos, delete some before retrying.
/// 400 BOT_INVALID This is not a valid bot.
/// 400 EMOJI_MARKUP_INVALID The specified <code>video_emoji_markup</code> was invalid.
/// 400 FILE_PARTS_INVALID The number of file parts is invalid.
/// 400 IMAGE_PROCESS_FAILED Failure while processing image.
/// 400 PHOTO_CROP_FILE_MISSING Photo crop file missing.
/// 400 PHOTO_CROP_SIZE_SMALL Photo is too small.
/// 400 PHOTO_EXT_INVALID The extension of the photo is invalid.
/// 400 PHOTO_FILE_MISSING Profile photo file missing.
/// 400 PHOTO_INVALID Photo invalid.
/// 400 STICKER_MIME_INVALID The specified sticker MIME type is invalid.
/// 400 VIDEO_FILE_INVALID The specified video file is invalid.
/// See <a href="https://corefork.telegram.org/method/photos.uploadProfilePhoto" />
///</summary>
internal sealed class UploadProfilePhotoHandler(
    IMediaHelper mediaHelper,
    ICommandBus commandBus)
    : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestUploadProfilePhoto, MyTelegram.Schema.Photos.IPhoto>
{
    protected override async Task<MyTelegram.Schema.Photos.IPhoto> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Photos.RequestUploadProfilePhoto obj)
    {
        var file = obj.File ?? obj.Video;
        var md5 = string.Empty;
        var parts = 0;
        var name = string.Empty;
        switch (file)
        {
            case TInputFile inputFile:
                md5 = inputFile.Md5Checksum;
                break;
            case TInputFileBig inputFileBig:
                break;
        }

        var r = await mediaHelper.SavePhotoAsync(input.ReqMsgId,
            input.UserId,
            file.GetFileId(),
            obj.Video != null,
            obj.VideoStartTs,
            parts,
            name,
            md5 ?? string.Empty,
            obj.VideoEmojiMarkup
            );
        var command = new UploadProfilePhotoCommand(UserId.Create(input.UserId),
            input.ToRequestInfo(),
            r.PhotoId,
            obj.Fallback,
            obj.VideoEmojiMarkup
        );
        await commandBus.PublishAsync(command);

        return null!;
    }
}
