namespace MyTelegram.ReadModel;

public interface IContactReadModel : IReadModel
{
    string FirstName { get; }
    string Id { get; }
    string? LastName { get; }
    string Phone { get; }
    long SelfUserId { get; }
    long TargetUserId { get; }
    long? PhotoId { get; }
}