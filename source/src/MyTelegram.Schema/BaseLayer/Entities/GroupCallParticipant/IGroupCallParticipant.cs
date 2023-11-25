// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a group call participant
/// See <a href="https://corefork.telegram.org/constructor/GroupCallParticipant" />
///</summary>
[JsonDerivedType(typeof(TGroupCallParticipant), nameof(TGroupCallParticipant))]
public interface IGroupCallParticipant : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Whether the participant is muted
    ///</summary>
    bool Muted { get; set; }

    ///<summary>
    /// Whether the participant has left
    ///</summary>
    bool Left { get; set; }

    ///<summary>
    /// Whether the participant can unmute themselves
    ///</summary>
    bool CanSelfUnmute { get; set; }

    ///<summary>
    /// Whether the participant has just joined
    ///</summary>
    bool JustJoined { get; set; }

    ///<summary>
    /// If set, and <a href="https://corefork.telegram.org/constructor/updateGroupCallParticipants">updateGroupCallParticipants</a>.version &lt; locally stored call.version, info about this participant should be ignored. If (...), and <a href="https://corefork.telegram.org/constructor/updateGroupCallParticipants">updateGroupCallParticipants</a>.version &gt; call.version+1, the participant list should be refetched using <a href="https://corefork.telegram.org/method/phone.getGroupParticipants">phone.getGroupParticipants</a>.
    ///</summary>
    bool Versioned { get; set; }

    ///<summary>
    /// If not set, the <code>volume</code> and <code>muted_by_you</code> fields can be safely used to overwrite locally cached information; otherwise, <code>volume</code> will contain valid information only if <code>volume_by_admin</code> is set both in the cache and in the received constructor.
    ///</summary>
    bool Min { get; set; }

    ///<summary>
    /// Whether this participant was muted by the current user
    ///</summary>
    bool MutedByYou { get; set; }

    ///<summary>
    /// Whether our volume can only changed by an admin
    ///</summary>
    bool VolumeByAdmin { get; set; }

    ///<summary>
    /// Whether this participant is the current user
    ///</summary>
    bool Self { get; set; }

    ///<summary>
    /// Whether this participant is currently broadcasting video
    ///</summary>
    bool VideoJoined { get; set; }

    ///<summary>
    /// Peer information
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// When did this participant join the group call
    ///</summary>
    int Date { get; set; }

    ///<summary>
    /// When was this participant last active in the group call
    ///</summary>
    int? ActiveDate { get; set; }

    ///<summary>
    /// Source ID
    ///</summary>
    int Source { get; set; }

    ///<summary>
    /// Volume, if not set the volume is set to 100%.
    ///</summary>
    int? Volume { get; set; }

    ///<summary>
    /// Info about this participant
    ///</summary>
    string? About { get; set; }

    ///<summary>
    /// Specifies the UI visualization order of peers with raised hands: peers with a higher rating should be showed first in the list.
    ///</summary>
    long? RaiseHandRating { get; set; }

    ///<summary>
    /// Info about the video stream the participant is currently broadcasting
    /// See <a href="https://corefork.telegram.org/type/GroupCallParticipantVideo" />
    ///</summary>
    MyTelegram.Schema.IGroupCallParticipantVideo? Video { get; set; }

    ///<summary>
    /// Info about the screen sharing stream the participant is currently broadcasting
    /// See <a href="https://corefork.telegram.org/type/GroupCallParticipantVideo" />
    ///</summary>
    MyTelegram.Schema.IGroupCallParticipantVideo? Presentation { get; set; }
}
