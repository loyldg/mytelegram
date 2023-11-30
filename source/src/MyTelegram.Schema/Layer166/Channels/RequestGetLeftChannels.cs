﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Channels;

///<summary>
/// Get a list of <a href="https://corefork.telegram.org/api/channel">channels/supergroups</a> we left
/// <para>Possible errors</para>
/// Code Type Description
/// 403 TAKEOUT_REQUIRED A takeout session has to be initialized, first.
/// See <a href="https://corefork.telegram.org/method/channels.getLeftChannels" />
///</summary>
[TlObject(0x8341ecc0)]
public sealed class RequestGetLeftChannels : IRequest<MyTelegram.Schema.Messages.IChats>
{
    public uint ConstructorId => 0x8341ecc0;
    ///<summary>
    /// Offset for <a href="https://corefork.telegram.org/api/offsets">pagination</a>
    ///</summary>
    public int Offset { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Offset);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Offset = reader.ReadInt32();
    }
}