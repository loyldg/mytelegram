// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Messages affected by changes
/// See <a href="https://corefork.telegram.org/constructor/messages.AffectedMessages" />
///</summary>
public interface IAffectedMessages : IObject
{
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">Event count after generation</a>
    ///</summary>
    int Pts { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/updates">Number of events that were generated</a>
    ///</summary>
    int PtsCount { get; set; }
}
