namespace MyTelegram.Domain.Aggregates.Dialog;

public record DialogFilter
(
    int Id,
    bool Contacts,
    bool NonContacts,
    bool Groups,
    bool Broadcasts,
    bool Bots,
    bool ExcludeMuted,
    bool ExcludeRead,
    bool ExcludeArchived,
    string Title,
    string? Emoticon,
    IList<InputPeer> PinnedPeers,
    IList<InputPeer> IncludePeers,
    IList<InputPeer> ExcludePeers);
