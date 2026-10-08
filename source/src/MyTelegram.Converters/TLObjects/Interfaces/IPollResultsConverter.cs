namespace MyTelegram.Converters.TLObjects.Interfaces;

public interface IPollResultsConverter : ILayeredConverter
{
    IPollResults ToPollResults(long userId, IPollReadModel pollReadModel, IList<string>? chosenOptions);
}