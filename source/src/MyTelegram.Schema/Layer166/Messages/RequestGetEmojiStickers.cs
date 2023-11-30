﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Gets the list of currently installed <a href="https://corefork.telegram.org/api/custom-emoji">custom emoji stickersets</a>.
/// See <a href="https://corefork.telegram.org/method/messages.getEmojiStickers" />
///</summary>
[TlObject(0xfbfca18f)]
public sealed class RequestGetEmojiStickers : IRequest<MyTelegram.Schema.Messages.IAllStickers>
{
    public uint ConstructorId => 0xfbfca18f;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/offsets#hash-generation">Hash for pagination, for more info click here</a>
    ///</summary>
    public long Hash { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Hash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Hash = reader.ReadInt64();
    }
}