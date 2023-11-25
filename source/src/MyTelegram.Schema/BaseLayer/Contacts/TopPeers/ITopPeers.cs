// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// Top peers
/// See <a href="https://corefork.telegram.org/constructor/contacts.TopPeers" />
///</summary>
[JsonDerivedType(typeof(TTopPeersNotModified), nameof(TTopPeersNotModified))]
[JsonDerivedType(typeof(TTopPeers), nameof(TTopPeers))]
[JsonDerivedType(typeof(TTopPeersDisabled), nameof(TTopPeersDisabled))]
public interface ITopPeers : IObject
{

}
