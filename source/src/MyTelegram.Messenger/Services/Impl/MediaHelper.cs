using Google.Protobuf;
using MyTelegram.Domain.Aggregates.Photo;

namespace MyTelegram.Messenger.Services.Impl;

public class MediaHelper(
    IOptionsMonitor<MyTelegramMessengerServerOptions> options,
    ICacheManager<UserCacheItem> cacheManager,
    IQueryProcessor queryProcessor,
    IPeerHelper peerHelper,
    IObjectMapper objectMapper,
    ICommandBus commandBus,
    ILogger<MediaHelper> logger)
    : IMediaHelper, ITransientDependency
{
    public MessageType GeMessageType(IMessageMedia? media)
    {
        if (media == null)
        {
            return MessageType.Text;
        }

        return media switch
        {
            TMessageMediaContact => MessageType.Contacts,
            TMessageMediaDice => MessageType.Game,
            TMessageMediaDocument => MessageType.Document,
            TMessageMediaEmpty => MessageType.Text,
            TMessageMediaGame => MessageType.Game,
            TMessageMediaGeo => MessageType.Geo,
            TMessageMediaGeoLive => MessageType.Geo,
            TMessageMediaInvoice => MessageType.Voice,
            TMessageMediaPhoto => MessageType.Photo,
            TMessageMediaPoll => MessageType.Poll,
            TMessageMediaUnsupported => MessageType.Text,
            TMessageMediaVenue => MessageType.Geo,
            TMessageMediaWebPage => MessageType.Url,
            _ => throw new ArgumentOutOfRangeException(nameof(media))
        };
    }

    public async Task<IEncryptedFile> SaveEncryptedFileAsync(long reqMsgId,
            IInputEncryptedFile encryptedFile)
    {
        var client = GrpcClientFactory.CreateMediaServiceClient(options.CurrentValue.FileServerGrpcServiceUrl);
        var r = await client
            .SaveEncryptedFileAsync(new SaveEncryptedFileRequest
            {
                EncryptedFile = ByteString.CopyFrom(encryptedFile.ToBytes()),
                ReqMsgId = reqMsgId
            }).ResponseAsync;

        return new TEncryptedFile
        {
            AccessHash = r.AccessHash,
            DcId = r.DcId,
            Id = r.Id,
            KeyFingerprint = r.KeyFingerprint,
            Size = r.Size
        };
    }

    public Task<IMessageMedia?> SaveMediaAsync(IInputMedia? media)
    {
        return SaveMediaCoreAsync(media);
    }

    private async Task CreatePhotoAsync(long userId, IPhoto photo, bool hasVideo, int size, bool isProfilePhoto)
    {

        switch (photo)
        {
            case TPhoto photo1:
                var command = new CreatePhotoCommand(PhotoId.Create(photo1.Id),
                    userId,
                    new PhotoItem(photo1.Id, photo1.AccessHash, photo1.FileReference, photo1.Date,
                        photo1.DcId,
                        size,
                        false,
                        hasVideo,
                        IsProfilePhoto: isProfilePhoto,
                        Sizes2: [.. photo1.Sizes],
                        VideoSizes2: photo1.VideoSizes?.ToList()
                    )
                );
                await commandBus.PublishAsync(command);
                break;
        }
    }

    private List<PhotoSize>? ToPhotoSize(TVector<IPhotoSize> photoSizes)
    {
        List<PhotoSize>? sizes = null;
        foreach (var photoSize in photoSizes)
        {
            switch (photoSize)
            {
                case TPhotoSize photoSize1:
                    sizes ??= [];
                    sizes.Add(new PhotoSize(photoSize1.W, photoSize1.H, photoSize1.Size, photoSize1.Type));
                    break;
            }
        }

        return sizes;
    }

    private List<VideoSize>? ToVideoSizes(TVector<IVideoSize>? videoSizes)
    {
        List<VideoSize>? sizes = null;
        foreach (var videoSize in videoSizes ?? [])
        {
            switch (videoSize)
            {
                case TVideoSize videoSize1:
                    sizes ??= [];
                    sizes.Add(new VideoSize(videoSize1.W, videoSize1.H, videoSize1.Size, videoSize1.Type, videoSize1.VideoStartTs ?? 0));
                    break;
            }
        }

        return sizes;
    }

    public async Task<SavePhotoResult> SavePhotoAsync(long reqMsgId,
            long userId,
        long fileId,
        bool hasVideo,
        double? videoStartTs,
        int parts,
        string? name,
        string? md5,
        IVideoSize? videoEmojiMarkup = null,
        bool isProfilePhoto = false
        )
    {
        var client = GrpcClientFactory.CreateMediaServiceClient(options.CurrentValue.FileServerGrpcServiceUrl);

        var r = await client.SavePhotoAsync(new SavePhotoRequest
        {
            UserId = userId,
            FileId = fileId,
            HasVideo = hasVideo,
            Md5 = md5 ?? string.Empty,
            Name = name ?? string.Empty,
            Parts = parts,
            ReqMsgId = reqMsgId,
            VideoStartTs = videoStartTs ?? 0,
            VideoEmojiMarkup = videoEmojiMarkup == null ? ByteString.Empty : ByteString.CopyFrom(videoEmojiMarkup.ToBytes()),
            IsProfilePhoto = isProfilePhoto

        }).ResponseAsync;

        var photo = r.Photo.Memory.ToTObject<IPhoto>();
        await CreatePhotoAsync(userId, photo, hasVideo, (int)r.Size, isProfilePhoto);

        return new SavePhotoResult(r.PhotoId, r.Photo.Memory.ToTObject<IPhoto>());
    }

    private async Task<TMessageMediaContact> CreateMediaContactAsync(TInputMediaContact inputMediaContact)
    {
        var cachedUserItem = await cacheManager
                .GetAsync(UserCacheItem.GetCacheKey(inputMediaContact.PhoneNumber))
            ;
        return new TMessageMediaContact
        {
            FirstName = inputMediaContact.FirstName,
            LastName = inputMediaContact.LastName ?? string.Empty,
            PhoneNumber = inputMediaContact.PhoneNumber?.Replace(" ", string.Empty) ?? string.Empty,
            Vcard = inputMediaContact.Vcard ?? string.Empty,
            UserId = cachedUserItem?.UserId ?? 0
        };
    }

    private IMessageMedia CreateMediaDice(TInputMediaDice inputMediaDice)
    {
        int value;// dice value: 1-6
        switch (inputMediaDice.Emoticon)
        {
            case "🎰": // Slot machine, value: 0-65
                value = Random.Shared.Next(0, 65);
                break;
            case "⚽️":
            case "🏀":
                value = Random.Shared.Next(1, 6);
                break;
            default:// dice value: 1-6
                value = Random.Shared.Next(1, 7);
                break;
        }

        return new TMessageMediaDice
        {
            Emoticon = inputMediaDice.Emoticon,
            Value = value
        };
    }

    private IMessageMedia CreateMediaGeoLive(TInputMediaGeoLive inputMediaGeoLive)
    {
        IGeoPoint geo = new TGeoPointEmpty();
        if (inputMediaGeoLive.GeoPoint is TInputGeoPoint inputGeoPoint1)
        {
            geo = new TGeoPoint
            {
                AccuracyRadius = inputGeoPoint1.AccuracyRadius,
                Lat = inputGeoPoint1.Lat,
                Long = inputGeoPoint1.Long,
                AccessHash = Random.Shared.NextInt64()
            };
        }

        return new TMessageMediaGeoLive
        {
            Heading = inputMediaGeoLive.Heading,
            Period = inputMediaGeoLive.Period ?? 0,
            ProximityNotificationRadius = inputMediaGeoLive.ProximityNotificationRadius,
            Geo = geo
        };
    }

    private IMessageMedia CreateMediaGeoPoint(TInputMediaGeoPoint inputMediaGeoPoint)
    {
        switch (inputMediaGeoPoint.GeoPoint)
        {
            case TInputGeoPoint inputGeoPoint1:
                return new TMessageMediaGeo
                {
                    Geo = new TGeoPoint
                    {
                        AccuracyRadius = inputGeoPoint1.AccuracyRadius,
                        Lat = inputGeoPoint1.Lat,
                        Long = inputGeoPoint1.Long,
                        AccessHash = Random.Shared.NextInt64()
                    }
                };
        }

        return new TMessageMediaGeo
        {
            Geo = new TGeoPointEmpty()
        };
    }

    private async Task<IMessageMedia> CreateMediaOnFileServerAsync(IInputMedia media)
    {
        try
        {
            var client = GrpcClientFactory.CreateMediaServiceClient(options.CurrentValue.FileServerGrpcServiceUrl);
            var r = await client.SaveMediaAsync(new SaveMediaRequest
            {
                Media = ByteString.CopyFrom(media.ToBytes())
            })
                .ResponseAsync;
            return r.Media.Memory.ToTObject<IMessageMedia>();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Save media failed");
            RpcErrors.RpcErrors400.FileIdInvalid.ThrowRpcError();
        }

        throw new InvalidOperationException();
    }

    private IMessageMedia CreateMediaPoll(TInputMediaPoll inputMediaPoll)
    {
        return new TMessageMediaPoll
        {
            Poll = inputMediaPoll.Poll,
            Results = new TPollResults()
        };
    }

    private Task<IMessageMedia> CreateMediaStoryAsync(TInputMediaStory inputMediaStory)
    {
        throw new NotImplementedException();
    }

    private IMessageMedia CreateMediaVenue(TInputMediaVenue inputMediaVenue)
    {
        TInputGeoPoint? inputGeoPoint = null;
        if (inputMediaVenue.GeoPoint is TInputGeoPoint geoPoint)
        {
            inputGeoPoint = geoPoint;
        }

        return new TMessageMediaVenue
        {
            Title = inputMediaVenue.Title,
            Address = inputMediaVenue.Address,
            Provider = inputMediaVenue.Provider,
            VenueId = inputMediaVenue.VenueId,
            VenueType = inputMediaVenue.VenueType,
            Geo = inputGeoPoint == null
                ? new TGeoPointEmpty()
                : new TGeoPoint
                {
                    AccuracyRadius = inputGeoPoint.AccuracyRadius,
                    Lat = inputGeoPoint.Lat,
                    Long = inputGeoPoint.Long
                }
        };
    }

    private Task<IMessageMedia> CreateMediaWebPageAsync(TInputMediaWebPage inputMediaWebPage)
    {
        throw new NotImplementedException();
    }

    private async Task<IMessageMedia?> SaveMediaCoreAsync(IInputMedia? media)
    {
        switch (media)
        {
            case TInputMediaContact inputMediaContact:
                return await CreateMediaContactAsync(inputMediaContact);
            case TInputMediaDice inputMediaDice:
                return CreateMediaDice(inputMediaDice);
            case TInputMediaDocument:
            case TInputMediaDocumentExternal:
            case TInputMediaPhoto:
            case TInputMediaPhotoExternal:
            case TInputMediaUploadedDocument:
            case TInputMediaUploadedPhoto:
                return await CreateMediaOnFileServerAsync(media);
            case TInputMediaPaidMedia:
            case TInputMediaGame:
            case TInputMediaInvoice:
                throw new NotImplementedException();
            case TInputMediaEmpty:
                return new TMessageMediaEmpty();
            case TInputMediaGeoLive inputMediaGeoLive:
                return CreateMediaGeoLive(inputMediaGeoLive);
            case TInputMediaGeoPoint inputMediaGeoPoint:
                return CreateMediaGeoPoint(inputMediaGeoPoint);
            case TInputMediaPoll inputMediaPoll:
                return CreateMediaPoll(inputMediaPoll);
            case TInputMediaStory inputMediaStory:
                return await CreateMediaStoryAsync(inputMediaStory);
            case TInputMediaVenue inputMediaVenue:
                return CreateMediaVenue(inputMediaVenue);
            case TInputMediaWebPage inputMediaWebPage:
                return await CreateMediaWebPageAsync(inputMediaWebPage);
            default:
                return null;
        }
    }
}
