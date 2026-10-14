namespace MyTelegram.Messenger.Converters.ConverterServices.Channels;

public interface IInviteToChannelConverterService
{
    IInvitedUsers ToInvitedUsers(SendOutboxMessageCompletedSagaEvent data);

    IUpdates ToInviteToChannelUpdates(
        SendOutboxMessageCompletedSagaEvent data
    );
}