﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Account;

///<summary>
/// Update theme
/// <para>Possible errors</para>
/// Code Type Description
/// 400 THEME_INVALID Invalid theme provided.
/// See <a href="https://corefork.telegram.org/method/account.updateTheme" />
///</summary>
[TlObject(0x2bf40ccc)]
public sealed class RequestUpdateTheme : IRequest<MyTelegram.Schema.ITheme>
{
    public uint ConstructorId => 0x2bf40ccc;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Theme format, a string that identifies the theming engines supported by the client
    ///</summary>
    public string Format { get; set; }

    ///<summary>
    /// Theme to update
    /// See <a href="https://corefork.telegram.org/type/InputTheme" />
    ///</summary>
    public MyTelegram.Schema.IInputTheme Theme { get; set; }

    ///<summary>
    /// Unique theme ID
    ///</summary>
    public string? Slug { get; set; }

    ///<summary>
    /// Theme name
    ///</summary>
    public string? Title { get; set; }

    ///<summary>
    /// Theme file
    /// See <a href="https://corefork.telegram.org/type/InputDocument" />
    ///</summary>
    public MyTelegram.Schema.IInputDocument? Document { get; set; }

    ///<summary>
    /// Theme settings
    ///</summary>
    public TVector<MyTelegram.Schema.IInputThemeSettings>? Settings { get; set; }

    public void ComputeFlag()
    {
        if (Slug != null) { Flags[0] = true; }
        if (Title != null) { Flags[1] = true; }
        if (Document != null) { Flags[2] = true; }
        if (Settings?.Count > 0) { Flags[3] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Format);
        writer.Write(Theme);
        if (Flags[0]) { writer.Write(Slug); }
        if (Flags[1]) { writer.Write(Title); }
        if (Flags[2]) { writer.Write(Document); }
        if (Flags[3]) { writer.Write(Settings); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        Format = reader.ReadString();
        Theme = reader.Read<MyTelegram.Schema.IInputTheme>();
        if (Flags[0]) { Slug = reader.ReadString(); }
        if (Flags[1]) { Title = reader.ReadString(); }
        if (Flags[2]) { Document = reader.Read<MyTelegram.Schema.IInputDocument>(); }
        if (Flags[3]) { Settings = reader.Read<TVector<MyTelegram.Schema.IInputThemeSettings>>(); }
    }
}
