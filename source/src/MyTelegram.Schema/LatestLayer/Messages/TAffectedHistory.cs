﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Messages;

///<summary>
/// Affected part of communication history with the user or in a chat.
/// See <a href="https://corefork.telegram.org/constructor/messages.affectedHistory" />
///</summary>
[TlObject(0xb45c69d1)]
public sealed class TAffectedHistory : IAffectedHistory
{
    public uint ConstructorId => 0xb45c69d1;
    ///<summary>
    /// Number of events occurred in a text box
    ///</summary>
    public int Pts { get; set; }

    ///<summary>
    /// Number of affected events
    ///</summary>
    public int PtsCount { get; set; }

    ///<summary>
    /// If a parameter contains positive value, it is necessary to repeat the method call using the given value; during the proceeding of all the history the value itself shall gradually decrease
    ///</summary>
    public int Offset { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Pts);
        writer.Write(PtsCount);
        writer.Write(Offset);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Pts = reader.ReadInt32();
        PtsCount = reader.ReadInt32();
        Offset = reader.ReadInt32();
    }
}