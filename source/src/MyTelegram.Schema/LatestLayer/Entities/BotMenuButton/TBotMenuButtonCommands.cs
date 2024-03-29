﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://corefork.telegram.org/api/bots/menu">Bot menu button</a> that opens the bot command list when clicked.
/// See <a href="https://corefork.telegram.org/constructor/botMenuButtonCommands" />
///</summary>
[TlObject(0x4258c205)]
public sealed class TBotMenuButtonCommands : IBotMenuButton
{
    public uint ConstructorId => 0x4258c205;


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