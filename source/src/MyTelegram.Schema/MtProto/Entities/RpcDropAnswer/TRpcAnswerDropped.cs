﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


[TlObject(0xa43ad8b7)]
public sealed class TRpcAnswerDropped : IRpcDropAnswer
{
    public uint ConstructorId => 0xa43ad8b7;
    public long MsgId { get; set; }
    public int SeqNo { get; set; }
    public int Bytes { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(MsgId);
        writer.Write(SeqNo);
        writer.Write(Bytes);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        MsgId = reader.ReadInt64();
        SeqNo = reader.ReadInt32();
        Bytes = reader.ReadInt32();
    }
}