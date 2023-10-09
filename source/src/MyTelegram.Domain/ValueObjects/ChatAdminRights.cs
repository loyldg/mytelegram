namespace MyTelegram.Domain.ValueObjects;

public record ReplyToMsgItem(long UserId, int MessageId);

public class ChatAdminRights : ValueObject
{
    private readonly BitArray _flags = new(32);

    public static ChatAdminRights GetCreatorRights() => new(true,
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        false,
        true,
        true);

    public ChatAdminRights()
    {

    }

    public ChatAdminRights(BitArray flags)
    {
        _flags = flags;
        InitFromFlags();
    }

    public ChatAdminRights(bool changeInfo,
        bool postMessages,
        bool editMessages,
        bool deleteMessages,
        bool banUsers,
        bool inviteUsers,
        bool pinMessages,
        bool addAdmins,
        bool anonymous,
        bool manageCall,
        bool other,
        bool manageTopics = true
    )
    {
        ChangeInfo = changeInfo;
        PostMessages = postMessages;
        EditMessages = editMessages;
        DeleteMessages = deleteMessages;
        BanUsers = banUsers;
        InviteUsers = inviteUsers;
        PinMessages = pinMessages;
        AddAdmins = addAdmins;
        Anonymous = anonymous;
        ManageCall = manageCall;
        Other = other;
        ManageTopics = manageTopics;

        ComputeFlag();
    }

    public bool AddAdmins { get; set; }
    public bool Anonymous { get; set; }
    public bool BanUsers { get; set; }

    public bool ChangeInfo { get; set; }
    public bool DeleteMessages { get; set; }
    public bool EditMessages { get; set; }
    public bool InviteUsers { get; set; }
    public bool ManageCall { get; set; }
    public bool Other { get; set; }
    public bool ManageTopics { get; set; }
    public bool PinMessages { get; set; }
    public bool PostMessages { get; set; }

    public bool HasNoRights()
    {
        return !ChangeInfo &&
               !PostMessages &&
               !EditMessages &&
               !DeleteMessages &&
               !BanUsers &&
               !InviteUsers &&
               !PinMessages &&
               !AddAdmins &&
               !Anonymous &&
               !ManageCall &&
               !Other &&
               !ManageTopics
            ;
    }

    public void ComputeFlag()
    {
        if (ChangeInfo) { _flags[0] = true; }
        if (PostMessages) { _flags[1] = true; }
        if (EditMessages) { _flags[2] = true; }
        if (DeleteMessages) { _flags[3] = true; }
        if (BanUsers) { _flags[4] = true; }
        if (InviteUsers) { _flags[5] = true; }
        if (PinMessages) { _flags[7] = true; }
        if (AddAdmins) { _flags[9] = true; }
        if (Anonymous) { _flags[10] = true; }
        if (ManageCall) { _flags[11] = true; }
        if (Other) { _flags[12] = true; }
        if (ManageTopics) { _flags[13] = true; }
    }

    public BitArray GetFlags()
    {
        ComputeFlag();

        return _flags;
    }

    private void InitFromFlags()
    {
        if (_flags[0]) { ChangeInfo = true; }
        if (_flags[1]) { PostMessages = true; }
        if (_flags[2]) { EditMessages = true; }
        if (_flags[3]) { DeleteMessages = true; }
        if (_flags[4]) { BanUsers = true; }
        if (_flags[5]) { InviteUsers = true; }
        if (_flags[7]) { PinMessages = true; }
        if (_flags[9]) { AddAdmins = true; }
        if (_flags[10]) { Anonymous = true; }
        if (_flags[11]) { ManageCall = true; }
        if (_flags[12]) { Other = true; }
        if (_flags[13]) { ManageTopics = true; }
    }
}