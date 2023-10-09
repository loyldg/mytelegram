namespace MyTelegram.Messenger.Services.Interfaces;

public interface IDomainEventMessageFactory
{
    DomainEventMessage CreateDomainEventMessage(IDomainEvent domainEvent);
}