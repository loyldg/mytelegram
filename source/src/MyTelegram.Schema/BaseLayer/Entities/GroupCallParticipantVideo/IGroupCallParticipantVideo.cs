// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a video stream
/// See <a href="https://corefork.telegram.org/constructor/GroupCallParticipantVideo" />
///</summary>
[JsonDerivedType(typeof(TGroupCallParticipantVideo), nameof(TGroupCallParticipantVideo))]
public interface IGroupCallParticipantVideo : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the stream is currently paused
    ///</summary>
    bool Paused { get; set; }

    ///<summary>
    /// Endpoint
    ///</summary>
    string Endpoint { get; set; }

    ///<summary>
    /// Source groups
    /// See <a href="https://corefork.telegram.org/type/GroupCallParticipantVideoSourceGroup" />
    ///</summary>
    TVector<MyTelegram.Schema.IGroupCallParticipantVideoSourceGroup> SourceGroups { get; set; }

    ///<summary>
    /// Audio source ID
    ///</summary>
    int? AudioSource { get; set; }
}
