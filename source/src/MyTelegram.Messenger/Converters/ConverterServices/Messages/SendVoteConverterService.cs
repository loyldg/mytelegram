namespace MyTelegram.Messenger.Converters.ConverterServices.Messages;

internal sealed class SendVoteConverterService(IPollConverterService pollConverterService) : ISendVoteConverterService, ITransientDependency
{
    public IUpdates ToSelfUpdates(long userId, IPollReadModel pollReadModel, List<string> chosenOptions, int layer)
    {
        var poll = pollConverterService.ToPoll(pollReadModel, layer);
        var pollResults = pollConverterService.ToPollResults(userId, pollReadModel, chosenOptions, layer);

        var updateMessagePoll = new TUpdateMessagePoll
        {
            Poll = poll,
            PollId = pollReadModel.PollId,
            Results = pollResults
        };

        return new TUpdates
        {
            Updates = [updateMessagePoll],
            Chats = [],
            Users = [],
            Date = DateTime.UtcNow.ToTimestamp()
        };
    }

    public IUpdates ToUpdates(long userId, IPollReadModel pollReadModel, List<string> chosenOptions)
    {
        //var poll = pollConverterService.ToPoll(pollReadModel);
        var pollResults = pollConverterService.ToPollResults(userId, pollReadModel, chosenOptions);
        pollResults.Min = true;

        var updateMessagePoll = new TUpdateMessagePoll
        {
            PollId = pollReadModel.PollId,
            Results = pollResults
        };

        return new TUpdates
        {
            Updates = [updateMessagePoll],
            Chats = [],
            Users = [],
            Date = DateTime.UtcNow.ToTimestamp()
        };
    }
}
