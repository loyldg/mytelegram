namespace MyTelegram.Converters.Mappers.LatestLayer;

internal sealed class PollResultsMapper
    : IObjectMapper<IPollReadModel, TPollResults>,
        ILayeredMapper,
        ITransientDependency
{
    public int Layer => Layers.LayerLatest;


    public TPollResults Map(IPollReadModel source)
    {
        return Map(source, new TPollResults());
    }

    public TPollResults Map(
        IPollReadModel source,
        TPollResults destination
    )
    {
        //destination.Min = source.Min;
        //destination.Results = source.Results;

        destination.TotalVoters = source.TotalVoters;
        //destination.RecentVoters = source.RecentVoters;
        destination.Solution = source.Solution;
        destination.SolutionEntities = source.SolutionEntities2 != null
            ? [.. source.SolutionEntities2]
            : source.SolutionEntities.ToTObject<TVector<IMessageEntity>>();
        destination.SolutionMedia = source.SolutionMedia;

        if (source.AnswerVoters?.Count > 0)
        {
            destination.RecentVoters =
                [.. source.AnswerVoters.SelectMany(p => p.RecentVoters ?? []).Select(p => p.ToPeer().ToPeer())];
        }

        return destination;
    }
}