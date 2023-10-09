namespace MyTelegram.Domain.ValueObjects;

public class ChatBannedRights : ValueObject
{
    public static readonly ChatBannedRights Default = new(false, false, false, false, false, false, false, false, false,
        true, true, true, true, false, false, false, false, false, false, false, int.MaxValue);

    //private BitArray _flags = new(32);

    //public ChatBannedRights(BitArray flags)
    //{
    //    _flags = flags;
    //}

    public ChatBannedRights()
    {
        ChangeInfo = true;
        InviteUsers = true;
        PinMessages = true;
        ManageTopics = true;
        UntilDate = int.MaxValue;
    }

    public ChatBannedRights(
        bool viewMessages,
        bool sendMessages,
        bool sendMedia,
        bool sendStickers,
        bool sendGifs,
        bool sendGames,
        bool sendInline,
        bool embedLinks,
        bool sendPolls,
        bool changeInfo,
        bool inviteUsers,
        bool pinMessages,
        bool manageTopics,
        bool sendPhotos,
        bool sendVideos,
        bool sendRoundVideos,
        bool sendAudios,
        bool sendVoices,
        bool sendDocs,
        bool sendPlain,
        int untilDate
    )
    {
        ViewMessages = viewMessages;
        SendMessages = sendMessages;
        SendMedia = sendMedia;
        SendStickers = sendStickers;
        SendGifs = sendGifs;
        SendGames = sendGames;
        SendInline = sendInline;
        EmbedLinks = embedLinks;
        SendPolls = sendPolls;
        ChangeInfo = changeInfo;
        InviteUsers = inviteUsers;
        PinMessages = pinMessages;
        UntilDate = untilDate;
        ManageTopics = manageTopics;
        SendPhotos = sendPhotos;
        SendVideos = sendVideos;
        SendRoundVideos = sendRoundVideos;
        SendAudios = sendAudios;
        SendVoices = sendVoices;
        SendDocs = sendDocs;
        SendPlain = sendPlain;
    }

    public bool ChangeInfo { get; set; } = true;

    public bool EmbedLinks { get; set; }

    public bool InviteUsers { get; set; } = true;

    public bool PinMessages { get; set; } = true;

    public bool SendGames { get; set; }

    public bool SendGifs { get; set; }

    public bool SendInline { get; set; }

    public bool SendMedia { get; set; }
    public bool SendMessages { get; set; }

    public bool SendPolls { get; set; }

    public bool SendStickers { get; set; }

    public int UntilDate { get; set; } = int.MaxValue;
    public bool ManageTopics { get; set; }
    public bool SendPhotos { get; set; }
    public bool SendVideos { get; set; }
    public bool SendRoundVideos { get; set; }
    public bool SendAudios { get; set; }
    public bool SendVoices { get; set; }
    public bool SendDocs { get; set; }
    public bool SendPlain { get; set; }

    //public BitArray Flags { get; init; } = new(32);
    public bool ViewMessages { get; set; }

    public static ChatBannedRights FromValue(int value,
        int untilDate)
    {
        var flags = new BitArray(BitConverter.GetBytes(value));
        var rights = new ChatBannedRights(
            flags[0],
            flags[1],
            flags[2],
            flags[3],
            flags[4],
            flags[5],
            flags[6],
            flags[7],
            flags[8],
            flags[10],
            flags[15],
            flags[17],
            flags[18],
            flags[19],
            flags[20],
            flags[21],
            flags[22],
            flags[23],
            flags[24],
            flags[25],
            untilDate
        );
        return rights;
    }

    public int ToIntValue()
    {
        var flag = ComputeFlag();
        var data = new byte[(flag.Length - 1) / 8 + 1];
        flag.CopyTo(data, 0);

        return BitConverter.ToInt32(data);
    }

    private BitArray ComputeFlag()
    {
        var flag = new BitArray(32);
        if (ViewMessages) flag[0] = true;

        if (SendMessages) flag[1] = true;

        if (SendMedia) flag[2] = true;

        if (SendStickers) flag[3] = true;

        if (SendGifs) flag[4] = true;

        if (SendGames) flag[5] = true;

        if (SendInline) flag[6] = true;

        if (EmbedLinks) flag[7] = true;

        if (SendPolls) flag[8] = true;

        if (ChangeInfo) flag[10] = true;

        if (InviteUsers) flag[15] = true;

        if (PinMessages) flag[17] = true;

        if (ManageTopics) flag[18] = true;

        if (SendPhotos) flag[19] = true;

        if (SendVideos) flag[20] = true;

        if (SendRoundVideos) flag[21] = true;

        if (SendAudios) flag[22] = true;

        if (SendVoices) flag[23] = true;

        if (SendDocs) flag[24] = true;

        if (SendPlain) flag[25] = true;

        return flag;
    }
}