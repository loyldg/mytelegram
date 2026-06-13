namespace MyTelegram.ReadModel.Impl;

public partial class PhotoReadModel : ReadModelBase, IPhotoReadModel,
    IAmReadModelFor<PhotoAggregate, PhotoId, PhotoCreatedEvent>,
    IAmReadModelFor<PhotoAggregate, PhotoId, PhotoDeletedEvent>,
    IAmReadModelFor<PhotoAggregate, PhotoId, SetAsProfilePhotoCompletedEvent>
{
    public long AccessHash { get; private set; }
    public int Date { get; private set; }
    public int DcId { get; private set; }
    public byte[] FileReference { get; private set; } = null!;
    public bool HasStickers { get; private set; }
    public bool HasVideo { get; private set; }
    public string Id { get; private set; } = null!;
    public long PhotoId { get; private set; }
    public long Size { get; private set; }
    public List<PhotoSize>? Sizes { get; private set; }
    public long UserId { get; private set; }
    public long? Version { get; set; }

    public List<VideoSize>? VideoSizes { get; private set; }
    public bool IsProfilePhoto { get; private set; }
    public List<IPhotoSize>? Sizes2 { get; private set; }
    public List<IVideoSize>? VideoSizes2 { get; private set; }

    public Task ApplyAsync(IReadModelContext context,
            IDomainEvent<PhotoAggregate, PhotoId, PhotoCreatedEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        var photo = domainEvent.AggregateEvent.Photo;
        Id = domainEvent.AggregateIdentity.Value;
        UserId = domainEvent.AggregateEvent.UserId;

        PhotoId = photo.Id;
        AccessHash = photo.AccessHash;
        FileReference = photo.FileReference;
        Date = photo.Date;
        DcId = photo.DcId;
        Size = photo.Size;
        HasVideo = photo.HasVideo;
        HasStickers = photo.HasStickers;
        Sizes = photo.Sizes;
        VideoSizes = photo.VideoSizes;
        IsProfilePhoto = photo.IsProfilePhoto;
        Sizes2 = photo.Sizes2;
        VideoSizes2 = photo.VideoSizes2;

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PhotoAggregate, PhotoId, PhotoDeletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        context.MarkForDeletion();

        return Task.CompletedTask;
    }

    public Task ApplyAsync(IReadModelContext context, IDomainEvent<PhotoAggregate, PhotoId, SetAsProfilePhotoCompletedEvent> domainEvent, CancellationToken cancellationToken)
    {
        IsProfilePhoto = true;

        return Task.CompletedTask;
    }
}