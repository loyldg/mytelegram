﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Password configuration not modified
/// See <a href="https://corefork.telegram.org/constructor/help.passportConfigNotModified" />
///</summary>
[TlObject(0xbfb9f457)]
public sealed class TPassportConfigNotModified : IPassportConfig
{
    public uint ConstructorId => 0xbfb9f457;


    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {

    }
}