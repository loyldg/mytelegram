using System.Text;

namespace MyTelegram.Converters.TLObjects.LatestLayer;

internal sealed class PollConverter(IObjectMapper objectMapper) : IPollConverter, ITransientDependency
{

    public int Layer => Layers.LayerLatest;

    public IPoll ToPoll(IPollReadModel readModel)
    {
        return objectMapper.Map<IPollReadModel, TPoll>(readModel);
    }

    public IPollResults ToPollResults(IPollReadModel pollReadModel, IList<string>? chosenOptions)
    {
        var pollResults = objectMapper.Map<IPollReadModel, TPollResults>(pollReadModel);
        chosenOptions ??= [];

        if (pollReadModel.AnswerVoters != null)
        {
            var voters = pollReadModel.AnswerVoters.Select(p => new TPollAnswerVoters
            {
                Correct = p.Correct,
                Voters = p.Voters,
                Option = Encoding.UTF8.GetBytes(p.Option),
                Chosen = chosenOptions.Contains(p.Option),
                RecentVoters = p.RecentVoters?.Count > 0 ? [.. p.RecentVoters.Select(p => p.ToPeer().ToPeer())] : []
            });
            pollResults.Results = new TVector<IPollAnswerVoters>(voters);
        }
        else
        {
            var voters = pollReadModel.Answers?.Select(p => new TPollAnswerVoters
            {
                Correct = false,
                Voters = 0,
                Option = Encoding.UTF8.GetBytes(p.Option),
                Chosen = chosenOptions.Contains(p.Option),
                RecentVoters = []
            }) ?? [];
            pollResults.Results = new TVector<IPollAnswerVoters>(voters);
        }

        return pollResults;
    }
}