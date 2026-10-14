namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IDialogConverterService
{
    Task<IDialogs> ToDialogsAsync(IRequestWithAccessHashKeyId request, GetDialogOutput output);
    Task<IPeerDialogs> ToPeerDialogsAsync(IRequestWithAccessHashKeyId request, GetDialogOutput output);
}