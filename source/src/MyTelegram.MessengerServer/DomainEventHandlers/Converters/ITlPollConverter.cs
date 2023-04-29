namespace MyTelegram.MessengerServer.DomainEventHandlers.Converters;

public interface ITlPollConverter
{
    IPoll ToPoll(IPollReadModel pollReadModel);

    IPollResults ToPollResults(IPollReadModel pollReadModel,
        IList<string> chosenOptions);

    IUpdates ToPollUpdates(IPollReadModel pollReadModel,
        IList<string> chosenOptions);

    IUpdates ToSelfPollUpdates(IPollReadModel pollReadModel,
        IList<string> chosenOptions);
}
