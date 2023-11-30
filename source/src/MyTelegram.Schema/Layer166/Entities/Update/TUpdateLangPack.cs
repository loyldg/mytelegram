﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Language pack updated
/// See <a href="https://corefork.telegram.org/constructor/updateLangPack" />
///</summary>
[TlObject(0x56022f4d)]
public sealed class TUpdateLangPack : IUpdate
{
    public uint ConstructorId => 0x56022f4d;
    ///<summary>
    /// Changed strings
    /// See <a href="https://corefork.telegram.org/type/LangPackDifference" />
    ///</summary>
    public MyTelegram.Schema.ILangPackDifference Difference { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Difference);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Difference = reader.Read<MyTelegram.Schema.ILangPackDifference>();
    }
}