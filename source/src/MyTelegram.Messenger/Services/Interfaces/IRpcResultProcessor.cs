namespace MyTelegram.Messenger.Services.Interfaces;

public interface IRpcResultProcessor : ILayeredConverter
{
    IFound ToFound(SearchContactOutput output, int layer);
    IMessages ToMessages(GetMessageOutput output, int layer);
}