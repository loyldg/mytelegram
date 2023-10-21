﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a group call or livestream
/// See <a href="https://corefork.telegram.org/constructor/groupCall" />
///</summary>
[TlObject(0xd597650c)]
public sealed class TGroupCall : IGroupCall
{
    public uint ConstructorId => 0xd597650c;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether the user should be muted upon joining the call
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool JoinMuted { get; set; }

    ///<summary>
    /// Whether the current user can change the value of the <code>join_muted</code> flag using <a href="https://corefork.telegram.org/method/phone.toggleGroupCallSettings">phone.toggleGroupCallSettings</a>
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool CanChangeJoinMuted { get; set; }

    ///<summary>
    /// Specifies the ordering to use when locally sorting by date and displaying in the UI group call participants.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool JoinDateAsc { get; set; }

    ///<summary>
    /// Whether we subscribed to the scheduled call
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool ScheduleStartSubscribed { get; set; }

    ///<summary>
    /// Whether you can start streaming video into the call
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool CanStartVideo { get; set; }

    ///<summary>
    /// Whether the group call is currently being recorded
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool RecordVideoActive { get; set; }

    ///<summary>
    /// Whether RTMP streams are allowed
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool RtmpStream { get; set; }

    ///<summary>
    /// Whether the listeners list is hidden and cannot be fetched using <a href="https://corefork.telegram.org/method/phone.getGroupParticipants">phone.getGroupParticipants</a>. The <code>phone.groupParticipants.count</code> and <code>groupCall.participants_count</code> counters will still include listeners.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool ListenersHidden { get; set; }

    ///<summary>
    /// Group call ID
    ///</summary>
    public long Id { get; set; }

    ///<summary>
    /// Group call access hash
    ///</summary>
    public long AccessHash { get; set; }

    ///<summary>
    /// Participant count
    ///</summary>
    public int ParticipantsCount { get; set; }

    ///<summary>
    /// Group call title
    ///</summary>
    public string? Title { get; set; }

    ///<summary>
    /// DC ID to be used for livestream chunks
    ///</summary>
    public int? StreamDcId { get; set; }

    ///<summary>
    /// When was the recording started
    ///</summary>
    public int? RecordStartDate { get; set; }

    ///<summary>
    /// When is the call scheduled to start
    ///</summary>
    public int? ScheduleDate { get; set; }

    ///<summary>
    /// Number of people currently streaming video into the call
    ///</summary>
    public int? UnmutedVideoCount { get; set; }

    ///<summary>
    /// Maximum number of people allowed to stream video into the call
    ///</summary>
    public int UnmutedVideoLimit { get; set; }

    ///<summary>
    /// Version
    ///</summary>
    public int Version { get; set; }

    public void ComputeFlag()
    {
        if (JoinMuted) { Flags[1] = true; }
        if (CanChangeJoinMuted) { Flags[2] = true; }
        if (JoinDateAsc) { Flags[6] = true; }
        if (ScheduleStartSubscribed) { Flags[8] = true; }
        if (CanStartVideo) { Flags[9] = true; }
        if (RecordVideoActive) { Flags[11] = true; }
        if (RtmpStream) { Flags[12] = true; }
        if (ListenersHidden) { Flags[13] = true; }
        if (Title != null) { Flags[3] = true; }
        if (/*StreamDcId != 0 && */StreamDcId.HasValue) { Flags[4] = true; }
        if (/*RecordStartDate != 0 && */RecordStartDate.HasValue) { Flags[5] = true; }
        if (/*ScheduleDate != 0 && */ScheduleDate.HasValue) { Flags[7] = true; }
        if (/*UnmutedVideoCount != 0 && */UnmutedVideoCount.HasValue) { Flags[10] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Id);
        writer.Write(AccessHash);
        writer.Write(ParticipantsCount);
        if (Flags[3]) { writer.Write(Title); }
        if (Flags[4]) { writer.Write(StreamDcId.Value); }
        if (Flags[5]) { writer.Write(RecordStartDate.Value); }
        if (Flags[7]) { writer.Write(ScheduleDate.Value); }
        if (Flags[10]) { writer.Write(UnmutedVideoCount.Value); }
        writer.Write(UnmutedVideoLimit);
        writer.Write(Version);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[1]) { JoinMuted = true; }
        if (Flags[2]) { CanChangeJoinMuted = true; }
        if (Flags[6]) { JoinDateAsc = true; }
        if (Flags[8]) { ScheduleStartSubscribed = true; }
        if (Flags[9]) { CanStartVideo = true; }
        if (Flags[11]) { RecordVideoActive = true; }
        if (Flags[12]) { RtmpStream = true; }
        if (Flags[13]) { ListenersHidden = true; }
        Id = reader.ReadInt64();
        AccessHash = reader.ReadInt64();
        ParticipantsCount = reader.ReadInt32();
        if (Flags[3]) { Title = reader.ReadString(); }
        if (Flags[4]) { StreamDcId = reader.ReadInt32(); }
        if (Flags[5]) { RecordStartDate = reader.ReadInt32(); }
        if (Flags[7]) { ScheduleDate = reader.ReadInt32(); }
        if (Flags[10]) { UnmutedVideoCount = reader.ReadInt32(); }
        UnmutedVideoLimit = reader.ReadInt32();
        Version = reader.ReadInt32();
    }
}