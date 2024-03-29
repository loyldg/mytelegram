﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

[TlObject(0xb921bd04)]
public sealed class RequestGetFutureSalts : IRequest<MyTelegram.Schema.IFutureSalts>
{
    public uint ConstructorId => 0xb921bd04;
    public int Num { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Num);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Num = reader.ReadInt32();
    }
}
