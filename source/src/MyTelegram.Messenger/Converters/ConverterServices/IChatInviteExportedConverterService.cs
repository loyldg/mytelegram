namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IChatInviteExportedConverterService
{
    ILayeredExportedChatInvite ToExportedChatInvite(IChatInviteReadModel readModel);
    ILayeredExportedChatInvite ToExportedChatInvite(ChatInviteCreatedEvent source);
    ILayeredExportedChatInvite ToExportedChatInvite(ChatInviteEditedEvent source, string link);
}