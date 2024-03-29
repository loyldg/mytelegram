﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// A contact
/// See <a href="https://corefork.telegram.org/constructor/inputBotInlineMessageMediaContact" />
///</summary>
[TlObject(0xa6edbffd)]
public sealed class TInputBotInlineMessageMediaContact : IInputBotInlineMessage
{
    public uint ConstructorId => 0xa6edbffd;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Phone number
    ///</summary>
    public string PhoneNumber { get; set; }

    ///<summary>
    /// First name
    ///</summary>
    public string FirstName { get; set; }

    ///<summary>
    /// Last name
    ///</summary>
    public string LastName { get; set; }

    ///<summary>
    /// VCard info
    ///</summary>
    public string Vcard { get; set; }

    ///<summary>
    /// Inline keyboard
    /// See <a href="https://corefork.telegram.org/type/ReplyMarkup" />
    ///</summary>
    public MyTelegram.Schema.IReplyMarkup? ReplyMarkup { get; set; }

    public void ComputeFlag()
    {
        if (ReplyMarkup != null) { Flags[2] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(PhoneNumber);
        writer.Write(FirstName);
        writer.Write(LastName);
        writer.Write(Vcard);
        if (Flags[2]) { writer.Write(ReplyMarkup); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        PhoneNumber = reader.ReadString();
        FirstName = reader.ReadString();
        LastName = reader.ReadString();
        Vcard = reader.ReadString();
        if (Flags[2]) { ReplyMarkup = reader.Read<MyTelegram.Schema.IReplyMarkup>(); }
    }
}