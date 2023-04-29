namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public class TlPollConverter : ITlPollConverter
{
    public IPoll ToPoll(IPollReadModel pollReadModel)
    {
        return new TPoll
        {
            Id = pollReadModel.PollId,
            Closed = pollReadModel.Closed,
            MultipleChoice = pollReadModel.MultipleChoice,
            Quiz = pollReadModel.Quiz,
            Question = pollReadModel.Question,
            CloseDate = pollReadModel.CloseDate,
            ClosePeriod = pollReadModel.ClosePeriod,
            Answers = new TVector<IPollAnswer>(pollReadModel.Answers.Select(p => new TPollAnswer
            {
                Option = p.Option,
                Text = p.Text
            }))
        };
    }

    public IPollResults ToPollResults(IPollReadModel pollReadModel,
        IList<string> chosenOptions)
    {
        var pollResults = new TPollResults
        {
            TotalVoters = pollReadModel.TotalVoters,
            Solution = pollReadModel.Solution,
            SolutionEntities = pollReadModel.SolutionEntities.ToTObject<TVector<IMessageEntity>>()
        };

        if (pollReadModel.AnswerVoters != null)
        {
            var voters = pollReadModel.AnswerVoters.Select(p => new TPollAnswerVoters
            {
                Correct = p.Correct,
                Voters = p.Voters,
                Option = Encoding.UTF8.GetBytes(p.Option),
                Chosen = chosenOptions.Contains(p.Option)
            });
            pollResults.Results = new TVector<IPollAnswerVoters>(voters);
        }
        else
        {
            var voters = pollReadModel.Answers.Select(p => new TPollAnswerVoters
            {
                Correct = false,
                Voters = 0,
                Option = Encoding.UTF8.GetBytes(p.Option),
                Chosen = chosenOptions.Contains(p.Option)
            });
            pollResults.Results = new TVector<IPollAnswerVoters>(voters);
        }

        return pollResults;
    }

    public IUpdates ToSelfPollUpdates(IPollReadModel pollReadModel,
        IList<string> chosenOptions)
    {
        var poll = ToPoll(pollReadModel);
        var pollResults = ToPollResults(pollReadModel, chosenOptions);

        var updateMessagePoll = new TUpdateMessagePoll
        {
            Poll = poll,
            PollId = pollReadModel.PollId,
            Results = pollResults
        };

        var updates = new TUpdates
        {
            Updates = new TVector<IUpdate>(updateMessagePoll),
            Chats = new TVector<IChat>(),
            Users = new TVector<IUser>(),
            Date = DateTime.UtcNow.ToTimestamp()
        };

        return updates;
    }

    public IUpdates ToPollUpdates(IPollReadModel pollReadModel,
        IList<string> chosenOptions)
    {
        var pollResults = ToPollResults(pollReadModel, chosenOptions);
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
