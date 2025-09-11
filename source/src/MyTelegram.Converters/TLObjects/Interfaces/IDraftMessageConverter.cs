namespace MyTelegram.Converters.TLObjects.Interfaces;

public interface IDraftMessageConverter : ILayeredConverter
{
    ILayeredDraftMessage ToDraftMessage(IDraftReadModel draftReadModel);
    ILayeredDraftMessage ToDraftMessage(Draft draft);
}