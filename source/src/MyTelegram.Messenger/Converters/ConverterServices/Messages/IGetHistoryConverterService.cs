namespace MyTelegram.Messenger.Converters.ConverterServices.Messages;

public interface IGetHistoryConverterService
{
    Task<IMessages> ToMessagesAsync(IRequestWithAccessHashKeyId request, GetMessageOutput output, int layer);
}