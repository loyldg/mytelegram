// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Message entities, representing styled text in a message
/// See <a href="https://corefork.telegram.org/constructor/MessageEntity" />
///</summary>
public interface IMessageEntity : IObject
{
    ///<summary>
    /// Offset of message entity within message (in <a href="https://corefork.telegram.org/api/entities#entity-length">UTF-16 code units</a>)
    ///</summary>
    int Offset { get; set; }

    ///<summary>
    /// Length of message entity within message (in <a href="https://corefork.telegram.org/api/entities#entity-length">UTF-16 code units</a>)
    ///</summary>
    int Length { get; set; }
}
