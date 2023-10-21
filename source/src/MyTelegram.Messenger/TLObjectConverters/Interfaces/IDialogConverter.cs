namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IDialogConverter : ILayeredConverter, IHasRequestLayer
{
    IDialogs ToDialogs(GetDialogOutput output);
    IPeerDialogs ToPeerDialogs(GetDialogOutput output);
}