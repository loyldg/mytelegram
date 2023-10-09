namespace MyTelegram.Messenger.TLObjectConverters.Interfaces;

public interface IPollConverter : ILayeredConverter
{
    IPoll ToPoll(IPollReadModel pollReadModel);
    IPollResults ToPollResults(IPollReadModel pollReadModel, IList<string> chosenOptions);

    IUpdates ToSelfPollUpdates(IPollReadModel pollReadModel,
        IList<string> chosenOptions);
    IUpdates ToPollUpdates(IPollReadModel pollReadModel,
        IList<string> chosenOptions);
}