// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Information about sparse positions of messages
/// See <a href="https://corefork.telegram.org/constructor/messages.SearchResultsPositions" />
///</summary>
public interface ISearchResultsPositions : IObject
{
    ///<summary>
    /// Total number of found messages
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// List of message positions
    /// See <a href="https://corefork.telegram.org/type/SearchResultsPosition" />
    ///</summary>
    TVector<MyTelegram.Schema.ISearchResultsPosition> Positions { get; set; }
}
