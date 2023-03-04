// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISearchResultsCalendarPeriod : IObject
{
    int Date { get; set; }
    int MinMsgId { get; set; }
    int MaxMsgId { get; set; }
    int Count { get; set; }
}
