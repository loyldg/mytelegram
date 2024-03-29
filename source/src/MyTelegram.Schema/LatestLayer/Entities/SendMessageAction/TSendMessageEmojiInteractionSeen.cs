﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// User is watching an animated emoji reaction triggered by another user, <a href="https://corefork.telegram.org/api/animated-emojis#emoji-reactions">click here for more info »</a>.
/// See <a href="https://corefork.telegram.org/constructor/sendMessageEmojiInteractionSeen" />
///</summary>
[TlObject(0xb665902e)]
public sealed class TSendMessageEmojiInteractionSeen : ISendMessageAction
{
    public uint ConstructorId => 0xb665902e;
    ///<summary>
    /// Emoji
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