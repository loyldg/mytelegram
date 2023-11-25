// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains info about an event that occurred in the application.
/// See <a href="https://corefork.telegram.org/constructor/InputAppEvent" />
///</summary>
[JsonDerivedType(typeof(TInputAppEvent), nameof(TInputAppEvent))]
public interface IInputAppEvent : IObject
{
    ///<summary>
    /// Client's exact timestamp for the event
    ///</summary>
    double Time { get; set; }

    ///<summary>
    /// Type of event
    ///</summary>
    string Type { get; set; }

    ///<summary>
    /// Arbitrary numeric value for more convenient selection of certain event types, or events referring to a certain object
    ///</summary>
    long Peer { get; set; }

    ///<summary>
    /// Details of the event
    /// See <a href="https://corefork.telegram.org/type/JSONValue" />
    ///</summary>
    MyTelegram.Schema.IJSONValue Data { get; set; }
}
