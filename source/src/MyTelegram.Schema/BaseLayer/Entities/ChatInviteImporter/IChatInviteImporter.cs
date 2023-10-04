// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// When and which user joined the chat using a chat invite
/// See <a href="https://corefork.telegram.org/constructor/ChatInviteImporter" />
///</summary>
public interface IChatInviteImporter : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether this user currently has a pending <a href="https://corefork.telegram.org/api/invites#join-requests">join request »</a>
    ///</summary>
    bool Requested { get; set; }

    ///<summary>
    /// The participant joined by importing a <a href="https://corefork.telegram.org/api/links#chat-folder-links">chat folder deep link »</a>.
    ///</summary>
    bool ViaChatlist { get; set; }

    ///<summary>
    /// The user
    ///</summary>
    long UserId { get; set; }

    ///<summary>
    /// When did the user join
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// For users with pending requests, contains bio of the user that requested to join
    ///</summary>
    string? About { get; set; }

    ///<summary>
    /// The administrator that approved the <a href="https://corefork.telegram.org/api/invites#join-requests">join request »</a> of the user
    ///</summary>
    long? ApprovedBy { get; set; }
}
