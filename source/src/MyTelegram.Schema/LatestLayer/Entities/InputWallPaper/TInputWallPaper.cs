﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/wallpapers">Wallpaper</a>
/// See <a href="https://corefork.telegram.org/constructor/inputWallPaper" />
///</summary>
[TlObject(0xe630b979)]
public sealed class TInputWallPaper : IInputWallPaper
{
    public uint ConstructorId => 0xe630b979;
    ///<summary>
    /// <a href="https://corefork.telegram.org/api/wallpapers">Wallpaper</a> ID
    ///</summary>
    public long Id { get; set; }

    ///<summary>
    /// Access hash
    ///</summary>
    public long AccessHash { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Id);
        writer.Write(AccessHash);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Id = reader.ReadInt64();
        AccessHash = reader.ReadInt64();
    }
}