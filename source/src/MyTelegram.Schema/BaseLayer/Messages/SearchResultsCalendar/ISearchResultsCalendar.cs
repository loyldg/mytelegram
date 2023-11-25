// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Information about found messages sent on a specific day
/// See <a href="https://corefork.telegram.org/constructor/messages.SearchResultsCalendar" />
///</summary>
[JsonDerivedType(typeof(TSearchResultsCalendar), nameof(TSearchResultsCalendar))]
public interface ISearchResultsCalendar : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// If set, indicates that the results may be inexact
    ///</summary>
    bool Inexact { get; set; }

    ///<summary>
    /// Total number of results matching query
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// Starting timestamp of attached messages
    ///</summary>
    int MinDate { get; set; }

    ///<summary>
    /// Ending timestamp of attached messages
    ///</summary>
    int MinMsgId { get; set; }

    ///<summary>
    /// Indicates the absolute position of <code>messages[0]</code> within the total result set with count <code>count</code>. <br>This is useful, for example, if we need to display a <code>progress/total</code> counter (like <code>photo 134 of 200</code>, for all media in a chat, we could simply use <code>photo ${offset_id_offset} of ${count}</code>.
    ///</summary>
    int? OffsetIdOffset { get; set; }

    ///<summary>
    /// Used to split the <code>messages</code> by days: multiple <a href="https://corefork.telegram.org/type/SearchResultsCalendarPeriod">SearchResultsCalendarPeriod</a> constructors are returned, each containing information about the first, last and total number of messages matching the filter that were sent on a specific day.  <br>This information can be easily used to split the returned <code>messages</code> by day.
    /// See <a href="https://corefork.telegram.org/type/SearchResultsCalendarPeriod" />
    ///</summary>
    TVector<MyTelegram.Schema.ISearchResultsCalendarPeriod> Periods { get; set; }

    ///<summary>
    /// Messages
    /// See <a href="https://corefork.telegram.org/type/Message" />
    ///</summary>
    TVector<MyTelegram.Schema.IMessage> Messages { get; set; }

    ///<summary>
    /// Mentioned chats
    /// See <a href="https://corefork.telegram.org/type/Chat" />
    ///</summary>
    TVector<MyTelegram.Schema.IChat> Chats { get; set; }

    ///<summary>
    /// Mentioned users
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
