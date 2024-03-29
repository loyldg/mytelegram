﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Get MTProxy/Public Service Announcement information
/// See <a href="https://corefork.telegram.org/method/help.getPromoData" />
///</summary>
[TlObject(0xc0977421)]
public sealed class RequestGetPromoData : IRequest<MyTelegram.Schema.Help.IPromoData>
{
    public uint ConstructorId => 0xc0977421;

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {

    }
}
