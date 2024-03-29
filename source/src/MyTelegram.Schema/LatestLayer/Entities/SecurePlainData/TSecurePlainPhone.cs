﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Phone number to use in <a href="https://corefork.telegram.org/passport">telegram passport</a>: <a href="https://corefork.telegram.org/passport/encryption#secureplaindata">it must be verified, first »</a>.
/// See <a href="https://corefork.telegram.org/constructor/securePlainPhone" />
///</summary>
[TlObject(0x7d6099dd)]
public sealed class TSecurePlainPhone : ISecurePlainData
{
    public uint ConstructorId => 0x7d6099dd;
    ///<summary>
    /// Phone number
    ///</summary>
    public string Phone { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Phone);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Phone = reader.ReadString();
    }
}