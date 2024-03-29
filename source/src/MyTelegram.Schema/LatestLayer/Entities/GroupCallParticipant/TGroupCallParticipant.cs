﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a group call participant
/// See <a href="https://corefork.telegram.org/constructor/groupCallParticipant" />
///</summary>
[TlObject(0xeba636fe)]
public sealed class TGroupCallParticipant : IGroupCallParticipant
{
    public uint ConstructorId => 0xeba636fe;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether the participant is muted
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Muted { get; set; }

    ///<summary>
    /// Whether the participant has left
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Left { get; set; }

    ///<summary>
    /// Whether the participant can unmute themselves
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool CanSelfUnmute { get; set; }

    ///<summary>
    /// Whether the participant has just joined
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool JustJoined { get; set; }

    ///<summary>
    /// If set, and <a href="https://corefork.telegram.org/constructor/updateGroupCallParticipants">updateGroupCallParticipants</a>.version &lt; locally stored call.version, info about this participant should be ignored. If (...), and <a href="https://corefork.telegram.org/constructor/updateGroupCallParticipants">updateGroupCallParticipants</a>.version &gt; call.version+1, the participant list should be refetched using <a href="https://corefork.telegram.org/method/phone.getGroupParticipants">phone.getGroupParticipants</a>.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Versioned { get; set; }

    ///<summary>
    /// If not set, the <code>volume</code> and <code>muted_by_you</code> fields can be safely used to overwrite locally cached information; otherwise, <code>volume</code> will contain valid information only if <code>volume_by_admin</code> is set both in the cache and in the received constructor.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Min { get; set; }

    ///<summary>
    /// Whether this participant was muted by the current user
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool MutedByYou { get; set; }

    ///<summary>
    /// Whether our volume can only changed by an admin
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool VolumeByAdmin { get; set; }

    ///<summary>
    /// Whether this participant is the current user
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Self { get; set; }

    ///<summary>
    /// Whether this participant is currently broadcasting video
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool VideoJoined { get; set; }

    ///<summary>
    /// Peer information
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    public MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// When did this participant join the group call
    ///</summary>
    public int Date { get; set; }

    ///<summary>
    /// When was this participant last active in the group call
    ///</summary>
    public int? ActiveDate { get; set; }

    ///<summary>
    /// Source ID
    ///</summary>
    public int Source { get; set; }

    ///<summary>
    /// Volume, if not set the volume is set to 100%.
    ///</summary>
    public int? Volume { get; set; }

    ///<summary>
    /// Info about this participant
    ///</summary>
    public string? About { get; set; }

    ///<summary>
    /// Specifies the UI visualization order of peers with raised hands: peers with a higher rating should be showed first in the list.
    ///</summary>
    public long? RaiseHandRating { get; set; }

    ///<summary>
    /// Info about the video stream the participant is currently broadcasting
    /// See <a href="https://corefork.telegram.org/type/GroupCallParticipantVideo" />
    ///</summary>
    public MyTelegram.Schema.IGroupCallParticipantVideo? Video { get; set; }

    ///<summary>
    /// Info about the screen sharing stream the participant is currently broadcasting
    /// See <a href="https://corefork.telegram.org/type/GroupCallParticipantVideo" />
    ///</summary>
    public MyTelegram.Schema.IGroupCallParticipantVideo? Presentation { get; set; }

    public void ComputeFlag()
    {
        if (Muted) { Flags[0] = true; }
        if (Left) { Flags[1] = true; }
        if (CanSelfUnmute) { Flags[2] = true; }
        if (JustJoined) { Flags[4] = true; }
        if (Versioned) { Flags[5] = true; }
        if (Min) { Flags[8] = true; }
        if (MutedByYou) { Flags[9] = true; }
        if (VolumeByAdmin) { Flags[10] = true; }
        if (Self) { Flags[12] = true; }
        if (VideoJoined) { Flags[15] = true; }
        if (/*ActiveDate != 0 && */ActiveDate.HasValue) { Flags[3] = true; }
        if (/*Volume != 0 && */Volume.HasValue) { Flags[7] = true; }
        if (About != null) { Flags[11] = true; }
        if (/*RaiseHandRating != 0 &&*/ RaiseHandRating.HasValue) { Flags[13] = true; }
        if (Video != null) { Flags[6] = true; }
        if (Presentation != null) { Flags[14] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Peer);
        writer.Write(Date);
        if (Flags[3]) { writer.Write(ActiveDate.Value); }
        writer.Write(Source);
        if (Flags[7]) { writer.Write(Volume.Value); }
        if (Flags[11]) { writer.Write(About); }
        if (Flags[13]) { writer.Write(RaiseHandRating.Value); }
        if (Flags[6]) { writer.Write(Video); }
        if (Flags[14]) { writer.Write(Presentation); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Muted = true; }
        if (Flags[1]) { Left = true; }
        if (Flags[2]) { CanSelfUnmute = true; }
        if (Flags[4]) { JustJoined = true; }
        if (Flags[5]) { Versioned = true; }
        if (Flags[8]) { Min = true; }
        if (Flags[9]) { MutedByYou = true; }
        if (Flags[10]) { VolumeByAdmin = true; }
        if (Flags[12]) { Self = true; }
        if (Flags[15]) { VideoJoined = true; }
        Peer = reader.Read<MyTelegram.Schema.IPeer>();
        Date = reader.ReadInt32();
        if (Flags[3]) { ActiveDate = reader.ReadInt32(); }
        Source = reader.ReadInt32();
        if (Flags[7]) { Volume = reader.ReadInt32(); }
        if (Flags[11]) { About = reader.ReadString(); }
        if (Flags[13]) { RaiseHandRating = reader.ReadInt64(); }
        if (Flags[6]) { Video = reader.Read<MyTelegram.Schema.IGroupCallParticipantVideo>(); }
        if (Flags[14]) { Presentation = reader.Read<MyTelegram.Schema.IGroupCallParticipantVideo>(); }
    }
}