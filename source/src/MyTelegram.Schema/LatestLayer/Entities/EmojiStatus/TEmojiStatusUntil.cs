﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// An <a href="https://corefork.telegram.org/api/emoji-status">emoji status</a> valid until the specified date
/// See <a href="https://corefork.telegram.org/constructor/emojiStatusUntil" />
///</summary>
[TlObject(0xfa30a8c7)]
public sealed class TEmojiStatusUntil : IEmojiStatus
{
    public uint ConstructorId => 0xfa30a8c7;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/custom-emoji">Custom emoji document ID</a>
    ///</summary>
    public long DocumentId { get; set; }

    ///<summary>
    /// This status is valid until this date
    ///</summary>
    public int Until { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(DocumentId);
        writer.Write(Until);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        DocumentId = reader.ReadInt64();
        Until = reader.ReadInt32();
    }
}