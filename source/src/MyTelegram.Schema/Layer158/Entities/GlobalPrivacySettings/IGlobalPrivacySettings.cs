// ReSharper disable All

namespace MyTelegram.Schema;

public interface IGlobalPrivacySettings : IObject
{
    BitArray Flags { get; set; }
    bool? ArchiveAndMuteNewNoncontactPeers { get; set; }
}
