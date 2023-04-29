using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlDialogConverter
{
    IDialogs ToDialogs(GetDialogOutput output);
    IPeerDialogs ToPeerDialogs(GetDialogOutput output);
}
