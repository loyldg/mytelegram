namespace MyTelegram.Domain.ValueObjects;

public class ChatBannedRights : ValueObject
{
    public static readonly ChatBannedRights Default = new();
    public ChatBannedRights() { }

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
        int untilDate)
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
    }

    public bool ChangeInfo { get; private set; } = true;

    public bool EmbedLinks { get; private set; }

    public bool InviteUsers { get; private set; } = true;

    public bool PinMessages { get; private set; } = true;

    public bool SendGames { get; private set; }

    public bool SendGifs { get; private set; }

    public bool SendInline { get; private set; }

    public bool SendMedia { get; private set; }
    public bool SendMessages { get; private set; }

    public bool SendPolls { get; private set; }

    public bool SendStickers { get; private set; }

    public int UntilDate { get; private set; } = int.MaxValue;

    //public BitArray Flags { get; private set; } = new(32);
    public bool ViewMessages { get; private set; }

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

        return flag;
    }
}
