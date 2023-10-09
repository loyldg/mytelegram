namespace MyTelegram.ReadModel.Impl;

public class PhotoReadModel : IPhotoReadModel,
    IAmReadModelFor<PhotoAggregate, PhotoId, EmptyPhotoEvent>
{
    public long AccessHash { get; private set; }
    public int Date { get; private set; }
    public int DcId { get; private set; }
    public byte[] FileReference { get; private set; } = default!;
    public bool HasStickers { get; private set; }
    public string Id { get; private set; } = default!;
    public long PhotoId { get; private set; }
    public long Size { get; private set; }
    public List<PhotoSize>? Sizes { get; private set; }
    public long UserId { get; private set; }
    public long? Version { get; set; }
    public List<VideoSize>? VideoSizes { get; private set; }

    public Task ApplyAsync(IReadModelContext context,
        IDomainEvent<PhotoAggregate, PhotoId, EmptyPhotoEvent> domainEvent,
        CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}