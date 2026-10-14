namespace MyTelegram.Messenger.Converters.ConverterServices;

public class PollConverterService(
    IPollConverter pollConverter,
    IPollResultsConverter pollResultsConverter
    ) : IPollConverterService, ITransientDependency
{
    public IPoll ToPoll(IPollReadModel pollReadModel)
    {
        return pollConverter.ToPoll(pollReadModel);
    }

    public IPollResults ToPollResults(long userId, IPollReadModel pollReadModel, IList<string> chosenOptions)
    {
        return pollResultsConverter.ToPollResults(userId, pollReadModel, chosenOptions);
    }

    public IUpdates ToPollUpdates(long userId, IPollReadModel pollReadModel, IList<string> chosenOptions)
    {
        var pollResults = ToPollResults(userId, pollReadModel, chosenOptions);
        pollResults.Min = true;

        var updateMessagePoll = new TUpdateMessagePoll
        {
            PollId = pollReadModel.PollId,
            Results = pollResults
        };

        return new TUpdateShort
        {
            Date = DateTime.UtcNow.ToTimestamp(),
            Update = updateMessagePoll
        };
    }
}