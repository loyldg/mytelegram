// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Describes a group of video synchronization source identifiers
/// See <a href="https://corefork.telegram.org/constructor/GroupCallParticipantVideoSourceGroup" />
///</summary>
[JsonDerivedType(typeof(TGroupCallParticipantVideoSourceGroup), nameof(TGroupCallParticipantVideoSourceGroup))]
public interface IGroupCallParticipantVideoSourceGroup : IObject
{
    ///<summary>
    /// SDP semantics
    ///</summary>
    string Semantics { get; set; }

    ///<summary>
    /// Source IDs
    ///</summary>
    TVector<int> Sources { get; set; }
}
