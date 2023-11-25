// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Object contains info on an encrypted chat.
/// See <a href="https://corefork.telegram.org/constructor/EncryptedChat" />
///</summary>
[JsonDerivedType(typeof(TEncryptedChatEmpty), nameof(TEncryptedChatEmpty))]
[JsonDerivedType(typeof(TEncryptedChatWaiting), nameof(TEncryptedChatWaiting))]
[JsonDerivedType(typeof(TEncryptedChatRequested), nameof(TEncryptedChatRequested))]
[JsonDerivedType(typeof(TEncryptedChat), nameof(TEncryptedChat))]
[JsonDerivedType(typeof(TEncryptedChatDiscarded), nameof(TEncryptedChatDiscarded))]
public interface IEncryptedChat : IObject
{
    ///<summary>
    /// Chat ID
    ///</summary>
    int Id { get; set; }
}
