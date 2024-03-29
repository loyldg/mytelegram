﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A map
/// See <a href="https://corefork.telegram.org/constructor/pageBlockMap" />
///</summary>
[TlObject(0xa44f3ef6)]
public sealed class TPageBlockMap : IPageBlock
{
    public uint ConstructorId => 0xa44f3ef6;
    ///<summary>
    /// Location of the map center
    /// See <a href="https://corefork.telegram.org/type/GeoPoint" />
    ///</summary>
    public MyTelegram.Schema.IGeoPoint Geo { get; set; }

    ///<summary>
    /// Map zoom level; 13-20
    ///</summary>
    public int Zoom { get; set; }

    ///<summary>
    /// Map width in pixels before applying scale; 16-102
    ///</summary>
    public int W { get; set; }

    ///<summary>
    /// Map height in pixels before applying scale; 16-1024
    ///</summary>
    public int H { get; set; }

    ///<summary>
    /// Caption
    /// See <a href="https://corefork.telegram.org/type/PageCaption" />
    ///</summary>
    public MyTelegram.Schema.IPageCaption Caption { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Geo);
        writer.Write(Zoom);
        writer.Write(W);
        writer.Write(H);
        writer.Write(Caption);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Geo = reader.Read<MyTelegram.Schema.IGeoPoint>();
        Zoom = reader.ReadInt32();
        W = reader.ReadInt32();
        H = reader.ReadInt32();
        Caption = reader.Read<MyTelegram.Schema.IPageCaption>();
    }
}