﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


[TlObject(0xd0e8075c)]
public sealed class TServerDHParamsOk : IServerDHParams
{
    public uint ConstructorId => 0xd0e8075c;
    public byte[] Nonce { get; set; }
    public byte[] ServerNonce { get; set; }
    public byte[] EncryptedAnswer { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.WriteRawBytes(Nonce);
        writer.WriteRawBytes(ServerNonce);
        writer.Write(EncryptedAnswer);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Nonce = reader.ReadInt128();
        ServerNonce = reader.ReadInt128();
        EncryptedAnswer = reader.ReadBytes();
    }
}