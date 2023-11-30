﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A new incoming event; for bots only
/// See <a href="https://corefork.telegram.org/constructor/updateBotWebhookJSON" />
///</summary>
[TlObject(0x8317c0c3)]
public sealed class TUpdateBotWebhookJSON : IUpdate
{
    public uint ConstructorId => 0x8317c0c3;
    ///<summary>
    /// The event
    /// See <a href="https://corefork.telegram.org/type/DataJSON" />
    ///</summary>
    public MyTelegram.Schema.IDataJSON Data { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Data);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Data = reader.Read<MyTelegram.Schema.IDataJSON>();
    }
}