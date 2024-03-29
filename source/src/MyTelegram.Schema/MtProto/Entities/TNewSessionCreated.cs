﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


[TlObject(0x9ec20908)]
public sealed class TNewSessionCreated : IObject
{
    public uint ConstructorId => 0x9ec20908;
    public long FirstMsgId { get; set; }
    public long UniqueId { get; set; }
    public long ServerSalt { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(FirstMsgId);
        writer.Write(UniqueId);
        writer.Write(ServerSalt);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        FirstMsgId = reader.ReadInt64();
        UniqueId = reader.ReadInt64();
        ServerSalt = reader.ReadInt64();
    }
}