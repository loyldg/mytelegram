namespace MyTelegram.Domain.Aggregates.Photo;

public class PhotoAggregate : AggregateRoot<PhotoAggregate, PhotoId>
{
    public PhotoAggregate(PhotoId id) : base(id)
    {
    }
}