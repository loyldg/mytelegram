﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// <a href="https://instantview.telegram.org/">Instant view</a> page
/// See <a href="https://corefork.telegram.org/constructor/page" />
///</summary>
[TlObject(0x98657f0d)]
public sealed class TPage : IPage
{
    public uint ConstructorId => 0x98657f0d;
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    public BitArray Flags { get; set; } = new BitArray(32);

    ///<summary>
    /// Indicates that not full page preview is available to the client and it will need to fetch full Instant View from the server using <a href="https://corefork.telegram.org/method/messages.getWebPagePreview">messages.getWebPagePreview</a>.
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Part { get; set; }

    ///<summary>
    /// Whether the page contains RTL text
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool Rtl { get; set; }

    ///<summary>
    /// Whether this is an <a href="https://instantview.telegram.org/docs#what-39s-new-in-2-0">IV v2</a> page
    /// See <a href="https://corefork.telegram.org/type/true" />
    ///</summary>
    public bool V2 { get; set; }

    ///<summary>
    /// Original page HTTP URL
    ///</summary>
    public string Url { get; set; }

    ///<summary>
    /// Page elements (like with HTML elements, only as TL constructors)
    ///</summary>
    public TVector<MyTelegram.Schema.IPageBlock> Blocks { get; set; }

    ///<summary>
    /// Photos in page
    ///</summary>
    public TVector<MyTelegram.Schema.IPhoto> Photos { get; set; }

    ///<summary>
    /// Media in page
    ///</summary>
    public TVector<MyTelegram.Schema.IDocument> Documents { get; set; }

    ///<summary>
    /// View count
    ///</summary>
    public int? Views { get; set; }

    public void ComputeFlag()
    {
        if (Part) { Flags[0] = true; }
        if (Rtl) { Flags[1] = true; }
        if (V2) { Flags[2] = true; }
        if (/*Views != 0 && */Views.HasValue) { Flags[3] = true; }
    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(Flags);
        writer.Write(Url);
        writer.Write(Blocks);
        writer.Write(Photos);
        writer.Write(Documents);
        if (Flags[3]) { writer.Write(Views.Value); }
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        Flags = reader.ReadBitArray();
        if (Flags[0]) { Part = true; }
        if (Flags[1]) { Rtl = true; }
        if (Flags[2]) { V2 = true; }
        Url = reader.ReadString();
        Blocks = reader.Read<TVector<MyTelegram.Schema.IPageBlock>>();
        Photos = reader.Read<TVector<MyTelegram.Schema.IPhoto>>();
        Documents = reader.Read<TVector<MyTelegram.Schema.IDocument>>();
        if (Flags[3]) { Views = reader.ReadInt32(); }
    }
}