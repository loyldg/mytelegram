﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/geoPointEmpty" />
///</summary>
[TlObject(0x1117dd5f)]
public sealed class TGeoPointEmpty : IGeoPoint,IEmpty
{
    public uint ConstructorId => 0x1117dd5f;


    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);

    }

    public void Deserialize(BinaryReader br)
    {

    }
}