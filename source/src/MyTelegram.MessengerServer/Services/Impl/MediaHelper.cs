using Google.Protobuf;
using MyTelegram.GrpcService;

namespace MyTelegram.MessengerServer.Services.Impl;

public class MediaHelper : IMediaHelper
{
    private readonly ILogger<MediaHelper> _logger;
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;

    public MediaHelper(IOptions<MyTelegramMessengerServerOptions> options,
        ILogger<MediaHelper> logger)
    {
        _options = options;
        _logger = logger;
    }

    public async Task<IEncryptedFile> SaveEncryptedFileAsync(long reqMsgId,
        IInputEncryptedFile encryptedFile)
    {
        var client = GrpcClientFactory.CreateMediaServiceClient(_options.Value.FileServerGrpcServiceUrl);
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

    public async Task<IPhoto> SavePhotoAsync(long reqMsgId,
        long fileId,
        bool hasVideo,
        double? videoStartTs,
        int parts,
        string? name,
        string? md5)
    {
        var client = GrpcClientFactory.CreateMediaServiceClient(_options.Value.FileServerGrpcServiceUrl);
        var r = await client.SavePhotoAsync(new SavePhotoRequest
        {
            FileId = fileId,
            HasVideo = hasVideo,
            Md5 = md5 ?? string.Empty,
            Name = name ?? string.Empty,
            Parts = parts,
            ReqMsgId = reqMsgId,
            VideoStartTs = videoStartTs ?? 0
        }).ResponseAsync;

        return r.Photo.Span.ToArray().ToTObject<IPhoto>();
    }

    public async Task<IMessageMedia> SaveMediaAsync(IInputMedia media)
    {
        try
        {
            var client = GrpcClientFactory.CreateMediaServiceClient(_options.Value.FileServerGrpcServiceUrl);
            var r = await client.SaveMediaAsync(new SaveMediaRequest
                {
                    Media = ByteString.CopyFrom(media.ToBytes())
                })
                .ResponseAsync;

            return r.Media.Span.ToArray().ToTObject<IMessageMedia>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Save media failed");
            throw new UserFriendlyException("FILE_ID_INVALID");
        }
    }

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
}
