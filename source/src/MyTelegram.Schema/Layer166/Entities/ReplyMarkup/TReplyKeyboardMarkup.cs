﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Bot keyboard
/// See <a href="https://corefork.telegram.org/constructor/replyKeyboardMarkup" />
///</summary>
[TlObject(0x85dd99d1)]
public sealed class TReplyKeyboardMarkup : IReplyMarkup
{
    public uint ConstructorId => 0x85dd99d1;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Requests clients to resize the keyboard vertically for optimal fit (e.g., make the keyboard smaller if there are just two rows of buttons). If not set, the custom keyboard is always of the same height as the app's standard keyboard.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Resize { get; set; }

    ///<summary>
    /// Requests clients to hide the keyboard as soon as it's been used. The keyboard will still be available, but clients will automatically display the usual letter-keyboard in the chat – the user can press a special button in the input field to see the custom keyboard again.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool SingleUse { get; set; }

    ///<summary>
    /// Use this parameter if you want to show the keyboard to specific users only. Targets: 1) users that are @mentioned in the text of the Message object; 2) if the bot's message is a reply (has reply_to_message_id), sender of the original message.<br><br>Example: A user requests to change the bot's language, bot replies to the request with a keyboard to select the new language. Other users in the group don't see the keyboard.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Selective { get; set; }

    ///<summary>
    /// Requests clients to always show the keyboard when the regular keyboard is hidden.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Persistent { get; set; }

    ///<summary>
    /// Button row
    ///</summary>
    public TVector<MyTelegram.Schema.IKeyboardButtonRow> Rows { get; set; }

    ///<summary>
    /// The placeholder to be shown in the input field when the keyboard is active; 1-64 characters.
    ///</summary>
    public string? Placeholder { get; set; }

    public void ComputeFlag()
    {
        if (Resize) { Flags[0] = true; }
        if (SingleUse) { Flags[1] = true; }
        if (Selective) { Flags[2] = true; }
        if (Persistent) { Flags[4] = true; }
        if (Placeholder != null) { Flags[3] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Rows);
        if (Flags[3]) { writer.Write(Placeholder); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Resize = true; }
        if (Flags[1]) { SingleUse = true; }
        if (Flags[2]) { Selective = true; }
        if (Flags[4]) { Persistent = true; }
        Rows = reader.Read<TVector<MyTelegram.Schema.IKeyboardButtonRow>>();
        if (Flags[3]) { Placeholder = reader.ReadString(); }
    }
}