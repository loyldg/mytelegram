﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Group.
/// See <a href="https://corefork.telegram.org/constructor/peerChat" />
///</summary>
[TlObject(0x36c6019a)]
public sealed class TPeerChat : IPeer
{
    public uint ConstructorId => 0x36c6019a;
    ///<summary>
    /// Group identifier
    ///</summary>
    public long ChatId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(ChatId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        ChatId = reader.ReadInt64();
    }
}