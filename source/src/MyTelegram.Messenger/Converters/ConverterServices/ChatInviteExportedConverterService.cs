namespace MyTelegram.Messenger.Converters.ConverterServices;

public class ChatInviteExportedConverterService(
    IChatInviteExportedConverter chatInviteExportedConverter,
    IChatInviteLinkHelper chatInviteLinkHelper,
    IOptionsMonitor<MyTelegramMessengerServerOptions> options) : IChatInviteExportedConverterService, ITransientDependency
{
    public ILayeredExportedChatInvite ToExportedChatInvite(IChatInviteReadModel readModel)
    {
        var exportedChatInvite = chatInviteExportedConverter.ToExportedChatInvite(readModel);
        exportedChatInvite.Link = chatInviteLinkHelper.GetFullLink(options.CurrentValue.JoinChatDomain, readModel.Link);

        return exportedChatInvite;
    }

    public ILayeredExportedChatInvite ToExportedChatInvite(ChatInviteCreatedEvent source)
    {
        var exportedChatInvite = chatInviteExportedConverter.ToExportedChatInvite(source);
        exportedChatInvite.Link = chatInviteLinkHelper.GetFullLink(options.CurrentValue.JoinChatDomain, source.Hash);

        return exportedChatInvite;
    }

    public ILayeredExportedChatInvite ToExportedChatInvite(ChatInviteEditedEvent source, string link)
    {
        var exportedChatInvite = chatInviteExportedConverter.ToExportedChatInvite(source);
        exportedChatInvite.Link = chatInviteLinkHelper.GetFullLink(options.CurrentValue.JoinChatDomain, link);

        return exportedChatInvite;
    }
}