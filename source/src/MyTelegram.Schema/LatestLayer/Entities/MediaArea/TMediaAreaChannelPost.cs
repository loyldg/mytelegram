﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/mediaAreaChannelPost" />
///</summary>
[TlObject(0x770416af)]
public sealed class TMediaAreaChannelPost : IMediaArea
{
    public uint ConstructorId => 0x770416af;
    public MyTelegram.Schema.IMediaAreaCoordinates Coordinates { get; set; }
    public long ChannelId { get; set; }
    public int MsgId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Coordinates);
        writer.Write(ChannelId);
        writer.Write(MsgId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Coordinates = reader.Read<MyTelegram.Schema.IMediaAreaCoordinates>();
        ChannelId = reader.ReadInt64();
        MsgId = reader.ReadInt32();
    }
}