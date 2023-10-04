// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Messages found and affected by changes
/// See <a href="https://corefork.telegram.org/constructor/messages.AffectedFoundMessages" />
///</summary>
public interface IAffectedFoundMessages : IObject
{
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">Event count after generation</a>
    ///</summary>
    int Pts { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">Number of events that were generated</a>
    ///</summary>
    int PtsCount { get; set; }

    ///<summary>
    /// If bigger than zero, the request must be repeated to remove more messages
    ///</summary>
    int Offset { get; set; }

    ///<summary>
    /// Affected message IDs
    ///</summary>
    TVector<int> Messages { get; set; }
}
