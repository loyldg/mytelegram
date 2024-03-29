﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Some <a href="https://corefork.telegram.org/api/scheduled-messages">scheduled messages</a> were deleted from the schedule queue of a chat
/// See <a href="https://corefork.telegram.org/constructor/updateDeleteScheduledMessages" />
///</summary>
[TlObject(0x90866cee)]
public sealed class TUpdateDeleteScheduledMessages : IUpdate
{
    public uint ConstructorId => 0x90866cee;
    ///<summary>
    /// Peer
    /// See <a href="https://corefork.telegram.org/type/Peer" />
    ///</summary>
    public MyTelegram.Schema.IPeer Peer { get; set; }

    ///<summary>
    /// Deleted scheduled messages
    ///</summary>
    public TVector<int> Messages { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Peer);
        writer.Write(Messages);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Peer = reader.Read<MyTelegram.Schema.IPeer>();
        Messages = reader.Read<TVector<int>>();
    }
}