// ReSharper disable All

namespace MyTelegram.Schema;

public interface IDialogFilter : IObject
{
    BitArray Flags { get; set; }
    bool Contacts { get; set; }
    bool NonContacts { get; set; }
    bool Groups { get; set; }
    bool Broadcasts { get; set; }
    bool Bots { get; set; }
    bool ExcludeMuted { get; set; }
    bool ExcludeRead { get; set; }
    bool ExcludeArchived { get; set; }
    int Id { get; set; }
    string Title { get; set; }
    string? Emoticon { get; set; }
    TVector<MyTelegram.Schema.IInputPeer> PinnedPeers { get; set; }
    TVector<MyTelegram.Schema.IInputPeer> IncludePeers { get; set; }
    TVector<MyTelegram.Schema.IInputPeer> ExcludePeers { get; set; }

}
