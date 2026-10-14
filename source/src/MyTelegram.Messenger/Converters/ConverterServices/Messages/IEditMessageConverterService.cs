namespace MyTelegram.Messenger.Converters.ConverterServices.Messages;

public interface IEditMessageConverterService
{
    IUpdates ToEditQuickReplyMessageUpdates(OutboxMessageEditCompletedSagaEvent data);
    IUpdates ToEditMessageUpdates(InboxMessageEditCompletedSagaEvent data);
    IUpdates ToEditMessageUpdates(long selfUserId, OutboxMessageEditCompletedSagaEvent data);
}