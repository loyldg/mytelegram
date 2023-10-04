// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Object contains info on affected part of communication history with the user or in a chat.
/// See <a href="https://corefork.telegram.org/constructor/messages.AffectedHistory" />
///</summary>
public interface IAffectedHistory : IObject
{
    ///<summary>
    /// Number of events occurred in a text box
    ///</summary>
    int Pts { get; set; }

    ///<summary>
    /// Number of affected events
    ///</summary>
    int PtsCount { get; set; }

    ///<summary>
    /// If a parameter contains positive value, it is necessary to repeat the method call using the given value; during the proceeding of all the history the value itself shall gradually decrease
    ///</summary>
    int Offset { get; set; }
}
