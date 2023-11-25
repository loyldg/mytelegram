// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Number of online users in a chat
/// See <a href="https://corefork.telegram.org/constructor/ChatOnlines" />
///</summary>
[JsonDerivedType(typeof(TChatOnlines), nameof(TChatOnlines))]
public interface IChatOnlines : IObject
{
    ///<summary>
    /// Number of online users
    ///</summary>
    int Onlines { get; set; }
}
