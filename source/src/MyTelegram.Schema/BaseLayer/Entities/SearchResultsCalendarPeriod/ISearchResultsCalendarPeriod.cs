// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Information about found messages sent on a specific day, used to split the <code>messages</code> in <a href="https://corefork.telegram.org/constructor/messages.searchResultsCalendar">messages.searchResultsCalendar</a> constructors by days.
/// See <a href="https://corefork.telegram.org/constructor/SearchResultsCalendarPeriod" />
///</summary>
[JsonDerivedType(typeof(TSearchResultsCalendarPeriod), nameof(TSearchResultsCalendarPeriod))]
public interface ISearchResultsCalendarPeriod : IObject
{
    ///<summary>
    /// The day this object is referring to.
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// First message ID that was sent on this day.
    ///</summary>
    int MinMsgId { get; set; }

    ///<summary>
    /// Last message ID that was sent on this day.
    ///</summary>
    int MaxMsgId { get; set; }

    ///<summary>
    /// All messages that were sent on this day.
    ///</summary>
    int Count { get; set; }
}
