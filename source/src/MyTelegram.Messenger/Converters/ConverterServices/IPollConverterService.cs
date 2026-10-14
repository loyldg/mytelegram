namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IPollConverterService
{
    IPoll ToPoll(IPollReadModel pollReadModel);
    IPollResults ToPollResults(long userId, IPollReadModel pollReadModel, IList<string> chosenOptions);
    IUpdates ToPollUpdates(long userId, IPollReadModel pollReadModel, IList<string> chosenOptions);
}