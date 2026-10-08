namespace MyTelegram.Messenger.Converters.ConverterServices.Messages;

public interface ISendVoteConverterService
{
    IUpdates ToSelfUpdates(long userId, IPollReadModel pollReadModel, List<string> chosenOptions, int layer);
    IUpdates ToUpdates(long userId, IPollReadModel pollReadModel, List<string> chosenOptions);
}