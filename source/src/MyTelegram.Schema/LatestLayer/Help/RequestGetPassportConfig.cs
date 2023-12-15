﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Get <a href="https://corefork.telegram.org/passport">passport</a> configuration
/// See <a href="https://corefork.telegram.org/method/help.getPassportConfig" />
///</summary>
[TlObject(0xc661ad08)]
public sealed class RequestGetPassportConfig : IRequest<MyTelegram.Schema.Help.IPassportConfig>
{
    public uint ConstructorId => 0xc661ad08;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/offsets#hash-generation">Hash for pagination, for more info click here</a>
    ///</summary>
    public int Hash { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Hash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Hash = reader.ReadInt32();
    }
}