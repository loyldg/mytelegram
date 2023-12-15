﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Contacts;

///<summary>
/// Returns the list of blocked users.
/// See <a href="https://corefork.telegram.org/method/contacts.getBlocked" />
///</summary>
[TlObject(0x9a868f80)]
public sealed class RequestGetBlocked : IRequest<MyTelegram.Schema.Contacts.IBlocked>
{
    public uint ConstructorId => 0x9a868f80;
    public BitArray Flags { get; set; } = new BitArray(32);
    public bool MyStoriesFrom { get; set; }

    ///<summary>
    /// The number of list elements to be skipped
    ///</summary>
    public int Offset { get; set; }

    ///<summary>
    /// The number of list elements to be returned
    ///</summary>
    public int Limit { get; set; }

    public void ComputeFlag()
    {
        if (MyStoriesFrom) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Offset);
        writer.Write(Limit);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { MyStoriesFrom = true; }
        Offset = reader.ReadInt32();
        Limit = reader.ReadInt32();
    }
}