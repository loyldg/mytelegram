namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public class TlPhotoConverter : ITlPhotoConverter
{
    private readonly IDataCenterHelper _dataCenterHelper;

    public TlPhotoConverter(IDataCenterHelper dataCenterHelper)
    {
        _dataCenterHelper = dataCenterHelper;
    }

    public IChatPhoto GetChatPhoto(byte[]? photo)
    {
        if (photo?.Length > 0)
        {
            var tlObject = photo.ToTObject<IObject>();
            if (tlObject is TPhoto tPhoto)
            {
                return new TChatPhoto
                {
                    DcId = tPhoto.DcId,
                    HasVideo = tPhoto.VideoSizes?.Count > 0,
                    PhotoId = tPhoto.Id
                    //PhotoSmall = new TFileLocationToBeDeprecated
                    //{
                    //    VolumeId = tPhoto.Id,
                    //    LocalId = 1
                    //},
                    //PhotoBig = new TFileLocationToBeDeprecated
                    //{
                    //    VolumeId = tPhoto.Id,
                    //    LocalId = 1
                    //}
                };
            }

            var chatPhoto = photo.ToTObject<IChatPhoto>();
            return chatPhoto;
            //var hasVideo = false;
            //var id = 0L;
            //switch (chatPhoto)
            //{
            //    case TChatPhoto chatPhoto1:
            //        return chatPhoto1;
            //    case Schema.TChatPhoto chatPhoto2:
            //        hasVideo = chatPhoto2.HasVideo;
            //        id = chatPhoto2.PhotoId;
            //        break;
            //    default:
            //        throw new ArgumentOutOfRangeException(nameof(chatPhoto));
            //}

            //return new TChatPhoto()
            //{
            //    DcId = MyTelegramServerDomainConsts.MediaDcId,
            //    PhotoSmall = new TFileLocationToBeDeprecated
            //    {
            //        VolumeId = id,
            //        LocalId = 1,
            //    },
            //    PhotoBig = new TFileLocationToBeDeprecated
            //    {
            //        VolumeId = id,
            //        LocalId = 1,
            //    },
            //    HasVideo = hasVideo

            //    //PhotoId = chatPhoto.Id,
            //    //StrippedThumb = s1?.Bytes
            //};
        }

        return new TChatPhotoEmpty();
    }

    public IUserProfilePhoto GetProfilePhoto(byte[]? profilePhoto)
    {
        if (profilePhoto?.Length > 0)
        {
            var photo = profilePhoto.ToTObject<IPhoto>();
            var hasVideo = false;
            if (photo is TPhoto tPhoto)
            {
                hasVideo = tPhoto.VideoSizes?.Count > 0;
            }

            return new TUserProfilePhoto
            {
                DcId = _dataCenterHelper.GetMediaDcId(),
                PhotoId = photo.Id,

                //PhotoBig = new TFileLocationToBeDeprecated
                //{
                //    VolumeId = photo.Id,
                //    LocalId = 1,
                //},
                //PhotoSmall = new TFileLocationToBeDeprecated
                //{
                //    VolumeId = photo.Id,
                //    LocalId = 1
                //},
                HasVideo = hasVideo
            };
        }

        return new TUserProfilePhotoEmpty();
    }
}