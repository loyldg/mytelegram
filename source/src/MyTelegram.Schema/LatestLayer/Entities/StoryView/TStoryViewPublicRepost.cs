﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/storyViewPublicRepost" />
///</summary>
[TlObject(0xbd74cf49)]
public sealed class TStoryViewPublicRepost : IStoryView
{
    public uint ConstructorId => 0xbd74cf49;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool Blocked { get; set; }
    public bool BlockedMyStoriesFrom { get; set; }
    public MyTelegram.Schema.IPeer PeerId { get; set; }
    public MyTelegram.Schema.IStoryItem Story { get; set; }

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
        writer.Write(PeerId);
        writer.Write(Story);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Blocked = true; }
        if (Flags[1]) { BlockedMyStoriesFrom = true; }
        PeerId = reader.Read<MyTelegram.Schema.IPeer>();
        Story = reader.Read<MyTelegram.Schema.IStoryItem>();
    }
}