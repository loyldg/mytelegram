namespace MyTelegram.Messenger.Converters.ConverterServices;

public class DraftConverterService(IDraftMessageConverter draftMessageConverter) : ITransientDependency, IDraftConverterService
{
    public IDraftMessage ToDraftMessage(IDraftReadModel draftReadModel)
    {
        var draftMessage = draftMessageConverter.ToDraftMessage(draftReadModel);
        draftMessage.Media = draftReadModel.Draft.Media2;

        return draftMessage;
    }
}