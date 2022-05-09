// ReSharper disable All

namespace MyTelegram.Schema;

public interface IGroupCallParticipantVideoSourceGroup : IObject
{
    string Semantics { get; set; }
    TVector<int> Sources { get; set; }

}
