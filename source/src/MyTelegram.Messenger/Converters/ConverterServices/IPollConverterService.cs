namespace MyTelegram.Messenger.Converters.ConverterServices;

public interface IPollConverterService
{
    IPoll ToPoll(IPollReadModel pollReadModel, int layer = 0);
    IPollResults ToPollResults(long userId, IPollReadModel pollReadModel, IList<string> chosenOptions, int layer = 0);
    IUpdates ToPollUpdates(long userId, IPollReadModel pollReadModel, IList<string> chosenOptions, int layer = 0);
}