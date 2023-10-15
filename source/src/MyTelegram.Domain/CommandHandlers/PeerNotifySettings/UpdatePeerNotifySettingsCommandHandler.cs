using MyTelegram.Domain.Commands.PeerNotifySettings;

namespace MyTelegram.Domain.CommandHandlers.PeerNotifySettings;

public class UpdatePeerNotifySettingsCommandHandler : CommandHandler<PeerNotifySettingsAggregate, PeerNotifySettingsId,
    UpdatePeerNotifySettingsCommand>
{
    public override Task ExecuteAsync(PeerNotifySettingsAggregate aggregate,
        UpdatePeerNotifySettingsCommand command,
        CancellationToken cancellationToken)
    {
        aggregate.UpdatePeerNotifySettings(command.RequestInfo,
            command.OwnerPeerId,
            command.PeerType,
            command.PeerId,
            command.ShowPreviews,
            command.Silent,
            command.MuteUntil,
            command.Sound);
        return Task.CompletedTask;
    }
}
