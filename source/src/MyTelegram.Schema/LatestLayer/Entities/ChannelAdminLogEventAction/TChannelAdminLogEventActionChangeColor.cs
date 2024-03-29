﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeColor" />
///</summary>
[TlObject(0x3c2b247b)]
public sealed class TChannelAdminLogEventActionChangeColor : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0x3c2b247b;
    public int PrevValue { get; set; }
    public int NewValue { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(PrevValue);
        writer.Write(NewValue);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        PrevValue = reader.ReadInt32();
        NewValue = reader.ReadInt32();
    }
}