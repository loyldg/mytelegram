﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Mark mentions as read
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHANNEL_INVALID The provided channel is invalid.
/// 400 CHANNEL_PRIVATE You haven't joined this channel/supergroup.
/// 400 MSG_ID_INVALID Invalid message ID provided.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.readMentions" />
///</summary>
[TlObject(0x36e5bf4d)]
public sealed class RequestReadMentions : IRequest<MyTelegram.Schema.Messages.IAffectedHistory>
{
    public uint ConstructorId => 0x36e5bf4d;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Dialog
    /// See <a href="https://corefork.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }

    ///<summary>
    /// Mark as read only mentions within the specified <a href="https://corefork.telegram.org/api/forum#forum-topics">forum topic</a>
    ///</summary>
    public int? TopMsgId { get; set; }

    public void ComputeFlag()
    {
        if (/*TopMsgId != 0 && */TopMsgId.HasValue) { Flags[0] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Peer);
        if (Flags[0]) { writer.Write(TopMsgId.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Peer = reader.Read<MyTelegram.Schema.IInputPeer>();
        if (Flags[0]) { TopMsgId = reader.ReadInt32(); }
    }
}