﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Mark new featured stickers as read
/// See <a href="https://corefork.telegram.org/method/messages.readFeaturedStickers" />
///</summary>
[TlObject(0x5b118126)]
public sealed class RequestReadFeaturedStickers : IRequest<IBool>
{
    public uint ConstructorId => 0x5b118126;
    ///<summary>
    /// IDs of stickersets to mark as read
    ///</summary>
    public TVector<long> Id { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Id);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Id = reader.Read<TVector<long>>();
    }
}
