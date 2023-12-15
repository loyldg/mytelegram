﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Get the number of results that would be found by a <a href="https://corefork.telegram.org/method/messages.search">messages.search</a> call with the same parameters
/// <para>Possible errors</para>
/// Code Type Description
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getSearchCounters" />
///</summary>
[TlObject(0xae7cc1)]
public sealed class RequestGetSearchCounters : IRequest<TVector<MyTelegram.Schema.Messages.ISearchCounter>>
{
    public uint ConstructorId => 0xae7cc1;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Peer where to search
    /// See <a href="https://corefork.telegram.org/type/InputPeer" />
    ///</summary>
    public MyTelegram.Schema.IInputPeer Peer { get; set; }

    ///<summary>
    /// If set, consider only messages within the specified <a href="https://corefork.telegram.org/api/forum#forum-topics">forum topic</a>
    ///</summary>
    public int? TopMsgId { get; set; }

    ///<summary>
    /// Search filters
    ///</summary>
    public TVector<MyTelegram.Schema.IMessagesFilter> Filters { get; set; }

    public void ComputeFlag()
    {
        if (/*TopMsgId != 0 && */TopMsgId.HasValue) { Flags[0] = true; }

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Peer);
        if (Flags[0]) { writer.Write(TopMsgId.Value); }
        writer.Write(Filters);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Peer = reader.Read<MyTelegram.Schema.IInputPeer>();
        if (Flags[0]) { TopMsgId = reader.ReadInt32(); }
        Filters = reader.Read<TVector<MyTelegram.Schema.IMessagesFilter>>();
    }
}