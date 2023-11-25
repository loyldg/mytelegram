// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Information about a message in a specific position
/// See <a href="https://corefork.telegram.org/constructor/SearchResultsPosition" />
///</summary>
[JsonDerivedType(typeof(TSearchResultPosition), nameof(TSearchResultPosition))]
public interface ISearchResultsPosition : IObject
{
    ///<summary>
    /// Message ID
    ///</summary>
    int MsgId { get; set; }

    ///<summary>
    /// When was the message sent
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// 0-based message position in the full list of suitable messages
    ///</summary>
    int Offset { get; set; }
}
