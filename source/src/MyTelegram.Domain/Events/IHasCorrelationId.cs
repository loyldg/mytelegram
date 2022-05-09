namespace MyTelegram.Domain.Events;

public interface IHasCorrelationId
{
    Guid CorrelationId { get; }
}
