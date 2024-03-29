﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// SHA512 KDF algo
/// See <a href="https://corefork.telegram.org/constructor/securePasswordKdfAlgoSHA512" />
///</summary>
[TlObject(0x86471d92)]
public sealed class TSecurePasswordKdfAlgoSHA512 : ISecurePasswordKdfAlgo
{
    public uint ConstructorId => 0x86471d92;
    ///<summary>
    /// Salt
    ///</summary>
    public byte[] Salt { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Salt);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Salt = reader.ReadBytes();
    }
}