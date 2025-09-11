// ReSharper disable All
namespace MyTelegram.Schema;

public interface ILayeredExportedChatInvite : IExportedChatInvite
{
    ///<summary>
    /// Chat invitation link
    ///</summary>
    string Link { get; set; }
    ///<summary>
    /// Whether this chat invite was revoked
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    bool Revoked { get; set; }
}