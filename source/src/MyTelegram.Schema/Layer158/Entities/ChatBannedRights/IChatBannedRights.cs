// ReSharper disable All

namespace MyTelegram.Schema;

public interface IChatBannedRights : IObject
{
    BitArray Flags { get; set; }
    bool ViewMessages { get; set; }
    bool SendMessages { get; set; }
    bool SendMedia { get; set; }
    bool SendStickers { get; set; }
    bool SendGifs { get; set; }
    bool SendGames { get; set; }
    bool SendInline { get; set; }
    bool EmbedLinks { get; set; }
    bool SendPolls { get; set; }
    bool ChangeInfo { get; set; }
    bool InviteUsers { get; set; }
    bool PinMessages { get; set; }
    bool ManageTopics { get; set; }
    bool SendPhotos { get; set; }
    bool SendVideos { get; set; }
    bool SendRoundvideos { get; set; }
    bool SendAudios { get; set; }
    bool SendVoices { get; set; }
    bool SendDocs { get; set; }
    bool SendPlain { get; set; }
    int UntilDate { get; set; }
}
