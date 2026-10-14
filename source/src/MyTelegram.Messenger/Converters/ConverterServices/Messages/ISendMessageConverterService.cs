namespace MyTelegram.Messenger.Converters.ConverterServices.Messages;

public interface ISendMessageConverterService
{
    IUpdates ToSelfOtherDeviceUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent);

    IUpdates ToUpdates(SendOutboxMessageCompletedSagaEvent aggregateEvent);
    IUpdates ToUpdates(ReceiveInboxMessageCompletedSagaEvent data);
}