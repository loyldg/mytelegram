namespace MyTelegram.Messenger.Handlers.LatestLayer.Photos;

///<summary>
/// Upload a custom profile picture for a contact, or suggest a new profile picture to a contact.The <code>file</code>, <code>video</code> and <code>video_emoji_markup</code> flags are mutually exclusive.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CONTACT_MISSING The specified user is not a contact.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// See <a href="https://corefork.telegram.org/method/photos.uploadContactProfilePhoto" />
///</summary>
internal sealed class UploadContactProfilePhotoHandler(
    ICommandBus commandBus,
    IMediaHelper mediaHelper,
    IPeerHelper peerHelper)
    : RpcResultObjectHandler<MyTelegram.Schema.Photos.RequestUploadContactProfilePhoto,
            MyTelegram.Schema.Photos.IPhoto>
{
    protected override async Task<MyTelegram.Schema.Photos.IPhoto> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Photos.RequestUploadContactProfilePhoto obj)
    {
        var file = obj.File ?? obj.Video;
        var md5 = string.Empty;
        var parts = 0;
        var name = string.Empty;
        switch (file)
        {
            case TInputFile inputFile:
                md5 = inputFile.Md5Checksum;
                name = inputFile.Name;
                break;
            case TInputFileBig inputFileBig:
                name = inputFileBig.Name;
                parts = inputFileBig.Parts;
                break;
        }

        VideoSizeEmojiMarkup? videoSizeEmojiMarkup = null;

        var photoId = 0L;
        IPhoto? photo = null;
        if (file != null)
        {
            var r = file == null
                ? null
                : await mediaHelper.SavePhotoAsync(input.ReqMsgId,
                    input.UserId,
                    file.GetFileId(),
                    obj.Video != null,
                    obj.VideoStartTs,
                    parts,
                    name,
                    md5 ?? string.Empty);
            if (r != null)
            {
                photoId = r.PhotoId;
                photo = r.Photo;
            }
        }

        var peer = peerHelper.GetPeer(obj.UserId);
        var command = new UpdateContactProfilePhotoCommand(
            ContactId.Create(input.UserId, peer.PeerId),
            input.ToRequestInfo(),
            input.UserId,
            peer.PeerId,
            photoId,
            obj.Suggest,
            obj.Suggest ? photo : null
        );

        await commandBus.PublishAsync(command);

        return null!;
    }
}
