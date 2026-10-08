namespace MyTelegram.Messenger.Converters.ConverterServices;

public class PollConverterService(
    ILayeredService<IPollConverter> pollLayeredService,
    ILayeredService<IPollResultsConverter> pollResultsLayeredService) : IPollConverterService, ITransientDependency
{
    public IPoll ToPoll(IPollReadModel pollReadModel, int layer = 0)
    {
        return pollLayeredService.GetConverter(layer).ToPoll(pollReadModel);
    }

    public IPollResults ToPollResults(long userId, IPollReadModel pollReadModel, IList<string> chosenOptions, int layer = 0)
    {
        return pollResultsLayeredService.GetConverter(layer).ToPollResults(userId, pollReadModel, chosenOptions);
    }

    public IUpdates ToPollUpdates(long userId, IPollReadModel pollReadModel, IList<string> chosenOptions, int layer = 0)
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