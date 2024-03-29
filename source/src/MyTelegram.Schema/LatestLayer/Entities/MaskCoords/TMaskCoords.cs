﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Position on a photo where a mask should be placed when <a href="https://corefork.telegram.org/api/stickers#attached-stickers">attaching stickers to media »</a>The <code>n</code> position indicates where the mask should be placed:
/// See <a href="https://corefork.telegram.org/constructor/maskCoords" />
///</summary>
[TlObject(0xaed6dbb2)]
public sealed class TMaskCoords : IMaskCoords
{
    public uint ConstructorId => 0xaed6dbb2;
    ///<summary>
    /// Part of the face, relative to which the mask should be placed
    ///</summary>
    public int N { get; set; }

    ///<summary>
    /// Shift by X-axis measured in widths of the mask scaled to the face size, from left to right. (For example, -1.0 will place the mask just to the left of the default mask position)
    ///</summary>
    public double X { get; set; }

    ///<summary>
    /// Shift by Y-axis measured in widths of the mask scaled to the face size, from left to right. (For example, -1.0 will place the mask just below the default mask position)
    ///</summary>
    public double Y { get; set; }

    ///<summary>
    /// Mask scaling coefficient. (For example, 2.0 means a doubled size)
    ///</summary>
    public double Zoom { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(N);
        writer.Write(X);
        writer.Write(Y);
        writer.Write(Zoom);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        N = reader.ReadInt32();
        X = reader.ReadDouble();
        Y = reader.ReadDouble();
        Zoom = reader.ReadDouble();
    }
}