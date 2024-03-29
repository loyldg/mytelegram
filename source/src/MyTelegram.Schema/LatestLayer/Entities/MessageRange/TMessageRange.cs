﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Indicates a range of chat messages
/// See <a href="https://corefork.telegram.org/constructor/messageRange" />
///</summary>
[TlObject(0xae30253)]
public sealed class TMessageRange : IMessageRange
{
    public uint ConstructorId => 0xae30253;
    ///<summary>
    /// Start of range (message ID)
    ///</summary>
    public int MinId { get; set; }

    ///<summary>
    /// End of range (message ID)
    ///</summary>
    public int MaxId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(MinId);
        writer.Write(MaxId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        MinId = reader.ReadInt32();
        MaxId = reader.ReadInt32();
    }
}