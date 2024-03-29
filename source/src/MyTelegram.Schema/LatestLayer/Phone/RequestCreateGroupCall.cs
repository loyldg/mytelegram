﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Phone;

///<summary>
/// Create a group call or livestream
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 400 CREATE_CALL_FAILED An error occurred while creating the call.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// 400 SCHEDULE_DATE_INVALID Invalid schedule date provided.
/// See <a href="https://corefork.telegram.org/method/phone.createGroupCall" />
///</summary>
[TlObject(0x48cdc6d8)]
public sealed class RequestCreateGroupCall : IRequest<MyTelegram.Schema.IUpdates>
{
    public uint ConstructorId => 0x48cdc6d8;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether RTMP stream support should be enabled: only the <a href="https://corefork.telegram.org/api/channel">group/supergroup/channel</a> owner can use this flag.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool RtmpStream { get; set; }

    ///<summary>
    /// Associate the group call or livestream to the provided <a href="https://corefork.telegram.org/api/channel">group/supergroup/channel</a>
    /// See <a href="https://corefork.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }

    ///<summary>
    /// Unique client message ID required to prevent creation of duplicate group calls
    ///</summary>
    public int RandomId { get; set; }

    ///<summary>
    /// Call title
    ///</summary>
    public string? Title { get; set; }

    ///<summary>
    /// For scheduled group call or livestreams, the absolute date when the group call will start
    ///</summary>
    public int? ScheduleDate { get; set; }

    public void ComputeFlag()
    {
        if (RtmpStream) { Flags[2] = true; }
        if (Title != null) { Flags[0] = true; }
        if (/*ScheduleDate != 0 && */ScheduleDate.HasValue) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Peer);
        writer.Write(RandomId);
        if (Flags[0]) { writer.Write(Title); }
        if (Flags[1]) { writer.Write(ScheduleDate.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[2]) { RtmpStream = true; }
        Peer = reader.Read<MyTelegram.Schema.IInputPeer>();
        RandomId = reader.ReadInt32();
        if (Flags[0]) { Title = reader.ReadString(); }
        if (Flags[1]) { ScheduleDate = reader.ReadInt32(); }
    }
}
