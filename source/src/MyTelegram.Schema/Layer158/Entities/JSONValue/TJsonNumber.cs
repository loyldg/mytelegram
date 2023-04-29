﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/jsonNumber" />
///</summary>
[TlObject(0x2be0dfa4)]
public sealed class TJsonNumber : IJSONValue
{
    public uint ConstructorId => 0x2be0dfa4;
    public double Value { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        bw.Serialize(Value);
    }

    public void Deserialize(BinaryReader br)
    {
        Value = br.ReadDouble();
    }
}