﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Get featured stickers
/// See <a href="https://corefork.telegram.org/method/messages.getFeaturedStickers" />
///</summary>
[TlObject(0x64780b14)]
public sealed class RequestGetFeaturedStickers : IRequest<MyTelegram.Schema.Messages.IFeaturedStickers>
{
    public uint ConstructorId => 0x64780b14;
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