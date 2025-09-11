namespace MyTelegram.Converters.TLObjects.LatestLayer;

internal sealed class DraftMessageConverter(IObjectMapper objectMapper) : IDraftMessageConverter, ITransientDependency
{
    public int Layer => Layers.LayerLatest;

    public ILayeredDraftMessage ToDraftMessage(IDraftReadModel draftReadModel)
    {
        return objectMapper.Map<IDraftReadModel, TDraftMessage>(draftReadModel);
    }

    public ILayeredDraftMessage ToDraftMessage(Draft draft)
    {
        return objectMapper.Map<Draft, TDraftMessage>(draft);

    }
}