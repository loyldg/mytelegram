namespace MyTelegram.Messenger.Converters.ConverterServices.Contacts;

public interface ISearchConverterService
{
    Task<IFound> ToFoundAsync(IRequestWithAccessHashKeyId request, SearchContactOutput output, int layer);
}