namespace MyTelegram.Domain.ValueObjects;

public class ChatBannedRights : ValueObject
{
    public static readonly ChatBannedRights Default = new(false, false, false, false, false, false, false, false, false, true, true, false, int.MaxValue, true);

    public ChatBannedRights()
    {
        ChangeInfo = true;
        InviteUsers = true;
        ManageTopic = true;
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
        int untilDate,
        bool manageTopic
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
        ManageTopic = manageTopic;
    }

    public bool ChangeInfo { get; init; } = true;

    public bool EmbedLinks { get; init; }

    public bool InviteUsers { get; init; } = true;

    public bool PinMessages { get; init; } = true;

    public bool SendGames { get; init; }

    public bool SendGifs { get; init; }

    public bool SendInline { get; init; }

    public bool SendMedia { get; init; }
    public bool SendMessages { get; init; }

    public bool SendPolls { get; init; }

    public bool SendStickers { get; init; }

    public int UntilDate { get; init; } = int.MaxValue;
    public bool ManageTopic { get; }

    //public BitArray Flags { get; init; } = new(32);
    public bool ViewMessages { get; init; }

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
            untilDate,
            flags[18]
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
        if (ViewMessages) { flag[0] = true; }

        if (SendMessages) { flag[1] = true; }

        if (SendMedia) { flag[2] = true; }

        if (SendStickers) { flag[3] = true; }

        if (SendGifs) { flag[4] = true; }

        if (SendGames) { flag[5] = true; }

        if (SendInline) { flag[6] = true; }

        if (EmbedLinks) { flag[7] = true; }

        if (SendPolls) { flag[8] = true; }

        if (ChangeInfo) { flag[10] = true; }

        if (InviteUsers) { flag[15] = true; }

        if (PinMessages) { flag[17] = true; }

        if (ManageTopic)
        {
            flag[18] = true;
        }

        return flag;
    }
}
