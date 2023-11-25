// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// An exported <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
/// See <a href="https://corefork.telegram.org/constructor/ExportedChatlistInvite" />
///</summary>
[JsonDerivedType(typeof(TExportedChatlistInvite), nameof(TExportedChatlistInvite))]
public interface IExportedChatlistInvite : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Name of the link
    ///</summary>
    string Title { get; set; }

    ///<summary>
    /// The <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
    ///</summary>
    string Url { get; set; }

    ///<summary>
    /// Peers to import
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    TVector<MyTelegram.Schema.IPeer> Peers { get; set; }
}
