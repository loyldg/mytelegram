namespace MyTelegram.Messenger.Converters.ConverterServices.Channels;

public interface IJoinChannelConverterService
{
    MyTelegram.Schema.IUpdates ToJoinChannelUpdates(long selfUserId, SendOutboxMessageCompletedSagaEvent data);
    MyTelegram.Schema.IUpdates ToJoinChannelUpdates(SendOutboxMessageCompletedSagaEvent data);
}