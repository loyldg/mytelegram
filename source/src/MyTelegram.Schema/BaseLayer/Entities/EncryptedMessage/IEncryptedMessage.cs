// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains encrypted message.
/// See <a href="https://corefork.telegram.org/constructor/EncryptedMessage" />
///</summary>
[JsonDerivedType(typeof(TEncryptedMessage), nameof(TEncryptedMessage))]
[JsonDerivedType(typeof(TEncryptedMessageService), nameof(TEncryptedMessageService))]
public interface IEncryptedMessage : IObject
{
    ///<summary>
    /// Random message ID, assigned by the author of message
    ///</summary>
    long RandomId { get; set; }

    ///<summary>
    /// ID of encrypted chat
    ///</summary>
    int ChatId { get; set; }

    ///<summary>
    /// Date of sending
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// TL-serialization of the <a href="https://corefork.telegram.org/type/DecryptedMessage">DecryptedMessage</a> type, encrypted with the key created at chat initialization
    ///</summary>
    byte[] Bytes { get; set; }
}
