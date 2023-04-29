﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;


///<summary>
///See <a href="https://core.telegram.org/constructor/phoneCallEmpty" />
///</summary>
[TlObject(0x5366c915)]
public sealed class TPhoneCallEmpty : IPhoneCall,IEmpty
{
    public uint ConstructorId => 0x5366c915;
    public long Id { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(BinaryWriter bw)
    {
        ComputeFlag();
        bw.Write(ConstructorId);
        bw.Write(Id);
    }

    public void Deserialize(BinaryReader br)
    {
        Id = br.ReadInt64();
    }
}