﻿// <auto-generated/>
// ReSharper disable All

namespace MyTelegram.Schema.Help;

///<summary>
/// Telegram Premium promotion informationNote that the <code>video_sections</code>+<code>videos</code> fields are a list of videos, and the corresponding premium feature identifiers.<br>
/// They're equivalent to a section =&gt; video dictionary, with keys from <code>video_section</code> and values from <code>videos</code>.<br>
/// The keys in <code>video_sections</code> correspond to a specific feature identifier, and the associated promotional video should be shown when the associated feature row is clicked.
/// See <a href="https://corefork.telegram.org/constructor/help.premiumPromo" />
///</summary>
[TlObject(0x5334759c)]
public sealed class TPremiumPromo : IPremiumPromo
{
    public uint ConstructorId => 0x5334759c;
    ///<summary>
    /// Description of the current state of the user's Telegram Premium subscription
    ///</summary>
    public string StatusText { get; set; }

    ///<summary>
    /// <a href="https://corefork.telegram.org/api/entities">Message entities for styled text</a>
    ///</summary>
    public TVector<MyTelegram.Schema.IMessageEntity> StatusEntities { get; set; }

    ///<summary>
    /// A list of <a href="https://corefork.telegram.org/api/premium">premium feature identifiers »</a>, associated to each video
    ///</summary>
    public TVector<string> VideoSections { get; set; }

    ///<summary>
    /// A list of videos
    ///</summary>
    public TVector<MyTelegram.Schema.IDocument> Videos { get; set; }

    ///<summary>
    /// Telegram Premium subscription options
    ///</summary>
    public TVector<MyTelegram.Schema.IPremiumSubscriptionOption> PeriodOptions { get; set; }

    ///<summary>
    /// Related user information
    ///</summary>
    public TVector<MyTelegram.Schema.IUser> Users { get; set; }

    public void ComputeFlag()
    {

    }

    public void Serialize(IBufferWriter<byte> writer)
    {
        ComputeFlag();
        writer.Write(ConstructorId);
        writer.Write(StatusText);
        writer.Write(StatusEntities);
        writer.Write(VideoSections);
        writer.Write(Videos);
        writer.Write(PeriodOptions);
        writer.Write(Users);
    }

    public void Deserialize(ref SequenceReader<byte> reader)
    {
        StatusText = reader.ReadString();
        StatusEntities = reader.Read<TVector<MyTelegram.Schema.IMessageEntity>>();
        VideoSections = reader.Read<TVector<string>>();
        Videos = reader.Read<TVector<MyTelegram.Schema.IDocument>>();
        PeriodOptions = reader.Read<TVector<MyTelegram.Schema.IPremiumSubscriptionOption>>();
        Users = reader.Read<TVector<MyTelegram.Schema.IUser>>();
    }
}