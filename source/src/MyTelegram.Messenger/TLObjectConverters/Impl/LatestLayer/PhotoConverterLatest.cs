namespace MyTelegram.Messenger.TLObjectConverters.Impl.LatestLayer;

public class PhotoConverterLatest : IPhotoConverterLatest
{
    public virtual int Layer => Layers.LayerLatest;

    public int RequestLayer { get; set; }

    public virtual IChatPhoto ToChatPhoto(IPhotoReadModel? photoReadModel)
    {
        if (photoReadModel == null)
        {
            return new TChatPhotoEmpty();
        }

        return new TChatPhoto
        {
            DcId = photoReadModel.DcId,
            PhotoId = photoReadModel.PhotoId,
            HasVideo = photoReadModel.VideoSizes?.Count > 0
        };
    }

    public virtual IPhoto ToPhoto(IPhotoReadModel? photoReadModel)
    {
        if (photoReadModel == null)
        {
            return new TPhotoEmpty();
        }

        var photo = new TPhoto
        {
            HasStickers = photoReadModel.HasStickers,
            Id = photoReadModel.PhotoId,
            AccessHash = photoReadModel.AccessHash,
            Date = photoReadModel.Date,
            DcId = photoReadModel.DcId,
            FileReference = photoReadModel.FileReference
        };

        if (photoReadModel.Sizes?.Count > 0)
        {
            photo.Sizes = new TVector<IPhotoSize>();
            foreach (var s in photoReadModel.Sizes)
            {
                photo.Sizes.Add(new TPhotoSize
                {
                    H = s.H,
                    W = s.W,
                    Size = (int)s.Size,
                    Type = s.Type
                });
            }
        }

        if (photoReadModel.VideoSizes?.Count > 0)
        {
            photo.VideoSizes = new TVector<IVideoSize>();
            foreach (var s in photoReadModel.VideoSizes)
            {
                photo.VideoSizes.Add(new TVideoSize
                {
                    H = s.H,
                    W = s.W,
                    Size = (int)s.Size,
                    Type = s.Type,
                    VideoStartTs = s.VideoStartTs
                });
            }
        }

        return photo;
    }

    public virtual IUserProfilePhoto ToProfilePhoto(IPhotoReadModel? photoReadModel)
    {
        if (photoReadModel == null)
        {
            return new TUserProfilePhotoEmpty();
        }

        return new TUserProfilePhoto
        {
            DcId = photoReadModel.DcId,
            PhotoId = photoReadModel.PhotoId,
            HasVideo = photoReadModel.VideoSizes?.Count > 0
        };
    }
}