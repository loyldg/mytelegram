﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/storyViewPublicForward" />
///</summary>
[TlObject(0x9083670b)]
public sealed class TStoryViewPublicForward : IStoryView
{
    public uint ConstructorId => 0x9083670b;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool Blocked { get; set; }
    public bool BlockedMyStoriesFrom { get; set; }
    public MyTelegram.Schema.IMessage Message { get; set; }

    public void ComputeFlag()
    {
        if (Blocked) { Flags[0] = true; }
        if (BlockedMyStoriesFrom) { Flags[1] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Message);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Blocked = true; }
        if (Flags[1]) { BlockedMyStoriesFrom = true; }
        Message = reader.Read<MyTelegram.Schema.IMessage>();
    }
}