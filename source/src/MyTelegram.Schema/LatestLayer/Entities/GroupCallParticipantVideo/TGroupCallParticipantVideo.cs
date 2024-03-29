﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Info about a video stream
/// See <a href="https://corefork.telegram.org/constructor/groupCallParticipantVideo" />
///</summary>
[TlObject(0x67753ac8)]
public sealed class TGroupCallParticipantVideo : IGroupCallParticipantVideo
{
    public uint ConstructorId => 0x67753ac8;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Whether the stream is currently paused
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Paused { get; set; }

    ///<summary>
    /// Endpoint
    ///</summary>
    public string Endpoint { get; set; }

    ///<summary>
    /// Source groups
    ///</summary>
    public TVector<MyTelegram.Schema.IGroupCallParticipantVideoSourceGroup> SourceGroups { get; set; }

    ///<summary>
    /// Audio source ID
    ///</summary>
    public int? AudioSource { get; set; }

    public void ComputeFlag()
    {
        if (Paused) { Flags[0] = true; }
        if (/*AudioSource != 0 && */AudioSource.HasValue) { Flags[1] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Endpoint);
        writer.Write(SourceGroups);
        if (Flags[1]) { writer.Write(AudioSource.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Paused = true; }
        Endpoint = reader.ReadString();
        SourceGroups = reader.Read<TVector<MyTelegram.Schema.IGroupCallParticipantVideoSourceGroup>>();
        if (Flags[1]) { AudioSource = reader.ReadInt32(); }
    }
}