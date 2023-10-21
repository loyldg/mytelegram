﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// The chat theme was changed
/// See <a href="https://corefork.telegram.org/constructor/messageActionSetChatTheme" />
///</summary>
[TlObject(0xaa786345)]
public sealed class TMessageActionSetChatTheme : IMessageAction
{
    public uint ConstructorId => 0xaa786345;
    ///<summary>
    /// The emoji that identifies a chat theme
    ///</summary>
    public string Emoticon { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Emoticon);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Emoticon = reader.ReadString();
    }
}