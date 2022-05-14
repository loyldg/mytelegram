// ReSharper disable All

namespace MyTelegram.Schema;

public interface ISearchResultsPosition : IObject
{
    int MsgId { get; set; }
    int Date { get; set; }
    int Offset { get; set; }

}
