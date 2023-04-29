// ReSharper disable All

namespace MyTelegram.Schema.Messages;

public interface ISearchResultsPositions : IObject
{
    int Count { get; set; }
    TVector<Schema.ISearchResultsPosition> Positions { get; set; }
}
