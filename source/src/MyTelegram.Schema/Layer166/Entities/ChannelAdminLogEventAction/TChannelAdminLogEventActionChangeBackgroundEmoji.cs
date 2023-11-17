﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/channelAdminLogEventActionChangeBackgroundEmoji" />
///</summary>
[TlObject(0x445fc434)]
public sealed class TChannelAdminLogEventActionChangeBackgroundEmoji : IChannelAdminLogEventAction
{
    public uint ConstructorId => 0x445fc434;
    public long PrevValue { get; set; }
    public long NewValue { get; set; }

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
        PrevValue = reader.ReadInt64();
        NewValue = reader.ReadInt64();
    }
}