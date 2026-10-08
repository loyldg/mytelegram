using System.Text;

namespace MyTelegram.Converters.TLObjects.LatestLayer;

internal sealed class PollResultsConverter(IObjectMapper objectMapper) : IPollResultsConverter, ITransientDependency
{

    public int Layer => Layers.LayerLatest;

    public IPollResults ToPollResults(long userId, IPollReadModel pollReadModel, IList<string>? chosenOptions)
    {
        var pollResults = objectMapper.Map<IPollReadModel, TPollResults>(pollReadModel);
        chosenOptions ??= [];
        var hideResults = false;
        var now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();

        var closed = pollReadModel.Closed || (pollReadModel.CloseDate != null && pollReadModel.CloseDate < now);

        if (!closed && pollReadModel.HideResultsUntilClose && pollReadModel.CreatorUserId != userId)
        {
            hideResults = true;
        }
        if (pollReadModel.AnswerVoters?.Count > 0)
        {
            var voters = pollReadModel.AnswerVoters.Select(p => new TPollAnswerVoters
            {
                Correct = pollReadModel.CorrectAnswers?.Contains(p.Option) ?? false,
                Voters = hideResults || (p.RecentVoters == null! || p.Voters == 0) ? null : p.Voters,
                Option = Encoding.UTF8.GetBytes(p.Option),
                Chosen = chosenOptions.Contains(p.Option),
                RecentVoters = (hideResults || (p.RecentVoters == null! || p.Voters == 0)) ? null : [.. p.RecentVoters.Select(x => x.ToPeer().ToPeer())]
            });
            pollResults.Results = new TVector<IPollAnswerVoters>(voters);
        }
        else if (pollReadModel.Answers?.Count > 0)
        {
            var voters = pollReadModel.Answers.Select(p => new TPollAnswerVoters
            {
                Correct = false,
                //Voters = chosenOptions.Contains(p.Option) ? 1 : 0,
                Option = Encoding.UTF8.GetBytes(p.Option),
                Chosen = chosenOptions.Contains(p.Option),
                //RecentVoters = []
            });
            pollResults.Results = new TVector<IPollAnswerVoters>(voters);
        }
        else if (pollReadModel.Answers2?.Count > 0)
        {
            pollResults.Results = [];
            foreach (var pollAnswer in pollReadModel.Answers2 ?? [])
            {
                if (pollAnswer is TPollAnswer pollAnswer1)
                {
                    var voter = new TPollAnswerVoters
                    {
                        Correct = pollReadModel.CorrectAnswers?.Contains(pollAnswer1.Option) ?? false,
                        Option = Encoding.UTF8.GetBytes(pollAnswer1.Option),
                        Chosen = chosenOptions.Contains(pollAnswer1.Option),
                        //Voters = 0,
                        //RecentVoters =null
                    };
                    if (voter.Chosen)
                    {
                        voter.Voters = 1;
                        voter.RecentVoters = [];
                    }

                    pollResults.Results.Add(voter);
                }
            }
        }

        return pollResults;
    }
}