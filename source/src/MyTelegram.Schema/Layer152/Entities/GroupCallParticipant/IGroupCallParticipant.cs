// ReSharper disable All

namespace MyTelegram.Schema;

public interface IGroupCallParticipant : IObject
{
    BitArray Flags { get; set; }
    bool Muted { get; set; }
    bool Left { get; set; }
    bool CanSelfUnmute { get; set; }
    bool JustJoined { get; set; }
    bool Versioned { get; set; }
    bool Min { get; set; }
    bool MutedByYou { get; set; }
    bool VolumeByAdmin { get; set; }
    bool Self { get; set; }
    bool VideoJoined { get; set; }
    MyTelegram.Schema.IPeer Peer { get; set; }
    int Date { get; set; }
    int? ActiveDate { get; set; }
    int Source { get; set; }
    int? Volume { get; set; }
    string? About { get; set; }
    long? RaiseHandRating { get; set; }
    MyTelegram.Schema.IGroupCallParticipantVideo? Video { get; set; }
    MyTelegram.Schema.IGroupCallParticipantVideo? Presentation { get; set; }
}
