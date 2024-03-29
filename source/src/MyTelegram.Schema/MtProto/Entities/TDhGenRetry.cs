﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


[TlObject(0x46dc1fb9)]
public sealed class TDhGenRetry : ISetClientDHParamsAnswer
{
    public uint ConstructorId => 0x46dc1fb9;
    public byte[] Nonce { get; set; }
    public byte[] ServerNonce { get; set; }
    public byte[] NewNonceHash2 { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.WriteRawBytes(Nonce);
        writer.WriteRawBytes(ServerNonce);
        writer.WriteRawBytes(NewNonceHash2);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Nonce = reader.ReadInt128();
        ServerNonce = reader.ReadInt128();
        NewNonceHash2 = reader.ReadInt128();
    }
}