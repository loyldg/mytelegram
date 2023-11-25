// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates a range of chat messages
/// See <a href="https://corefork.telegram.org/constructor/MessageRange" />
///</summary>
[JsonDerivedType(typeof(TMessageRange), nameof(TMessageRange))]
public interface IMessageRange : IObject
{
    ///<summary>
    /// Start of range (message ID)
    ///</summary>
    int MinId { get; set; }

    ///<summary>
    /// End of range (message ID)
    ///</summary>
    int MaxId { get; set; }
}
