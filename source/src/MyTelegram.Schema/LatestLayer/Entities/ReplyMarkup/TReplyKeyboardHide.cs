﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Hide sent bot keyboard
/// See <a href="https://corefork.telegram.org/constructor/replyKeyboardHide" />
///</summary>
[TlObject(0xa03e5b85)]
public sealed class TReplyKeyboardHide : IReplyMarkup
{
    public uint ConstructorId => 0xa03e5b85;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Use this flag if you want to remove the keyboard for specific users only. Targets: 1) users that are @mentioned in the text of the Message object; 2) if the bot's message is a reply (has reply_to_message_id), sender of the original message.<br><br>Example: A user votes in a poll, bot returns confirmation message in reply to the vote and removes the keyboard for that user, while still showing the keyboard with poll options to users who haven't voted yet
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Selective { get; set; }

    public void ComputeFlag()
    {
        if (Selective) { Flags[2] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);

    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[2]) { Selective = true; }
    }
}