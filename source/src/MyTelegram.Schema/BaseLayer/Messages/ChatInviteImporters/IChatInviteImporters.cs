// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// List of users that imported a chat invitation link.
/// See <a href="https://corefork.telegram.org/constructor/messages.ChatInviteImporters" />
///</summary>
[JsonDerivedType(typeof(TChatInviteImporters), nameof(TChatInviteImporters))]
public interface IChatInviteImporters : IObject
{
    ///<summary>
    /// Number of users that joined
    ///</summary>
    int Count { get; set; }

    ///<summary>
    /// The users that joined
    /// See <a href="https://corefork.telegram.org/type/ChatInviteImporter" />
    ///</summary>
    TVector<MyTelegram.Schema.IChatInviteImporter> Importers { get; set; }

    ///<summary>
    /// The users that joined
    /// See <a href="https://corefork.telegram.org/type/User" />
    ///</summary>
    TVector<MyTelegram.Schema.IUser> Users { get; set; }
}
