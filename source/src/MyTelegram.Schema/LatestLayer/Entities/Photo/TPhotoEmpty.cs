﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Empty constructor, non-existent photo
/// See <a href="https://corefork.telegram.org/constructor/photoEmpty" />
///</summary>
[TlObject(0x2331b22d)]
public sealed class TPhotoEmpty : IPhoto,IEmpty
{
    public uint ConstructorId => 0x2331b22d;
    ///<summary>
    /// Photo identifier
    ///</summary>
    public long Id { get; set; }

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
        Id = reader.ReadInt64();
    }
}