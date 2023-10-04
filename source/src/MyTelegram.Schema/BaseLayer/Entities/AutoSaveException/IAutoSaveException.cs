// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Peer-specific autosave settings
/// See <a href="https://corefork.telegram.org/constructor/AutoSaveException" />
///</summary>
public interface IAutoSaveException : IObject
{
    ///<summary>
    /// The peer
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// Media autosave settings
    /// See <a href="https://corefork.telegram.org/type/AutoSaveSettings" />
    ///</summary>
    MyTelegram.Schema.IAutoSaveSettings Settings { get; set; }
}
