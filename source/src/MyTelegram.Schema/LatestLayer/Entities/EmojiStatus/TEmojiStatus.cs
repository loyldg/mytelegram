﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// An <a href="https://corefork.telegram.org/api/emoji-status">emoji status</a>
/// See <a href="https://corefork.telegram.org/constructor/emojiStatus" />
///</summary>
[TlObject(0x929b619d)]
public sealed class TEmojiStatus : IEmojiStatus
{
    public uint ConstructorId => 0x929b619d;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/custom-emoji">Custom emoji document ID</a>
    ///</summary>
    public long DocumentId { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(DocumentId);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        DocumentId = reader.ReadInt64();
    }
}