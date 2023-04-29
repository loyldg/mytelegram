// ReSharper disable All

namespace MyTelegram.Schema;

public interface IGroupCallParticipantVideo : IObject
{
    BitArray Flags { get; set; }
    bool Paused { get; set; }
    string Endpoint { get; set; }
    TVector<Schema.IGroupCallParticipantVideoSourceGroup> SourceGroups { get; set; }
    int? AudioSource { get; set; }
}
