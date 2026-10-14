namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IDraftConverterService
{
    IDraftMessage ToDraftMessage(IDraftReadModel draftReadModel);
}