namespace MyTelegram.ReadModel;

public interface IContactReadModel : IReadModel
{
    long? PhotoId { get; }
}