namespace MyTelegram.Domain.ValueObjects;

public class ChatAdminRights : ValueObject
{
    public static ChatAdminRights GetCreatorRights() => new(true,
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true,
        true);

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
        bool other)
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
    }

    public bool AddAdmins { get; }
    public bool Anonymous { get; }
    public bool BanUsers { get; }

    public bool ChangeInfo { get; }
    public bool DeleteMessages { get; }
    public bool EditMessages { get; }
    public bool InviteUsers { get; }
    public bool ManageCall { get; }
    public bool Other { get; }
    public bool PinMessages { get; }
    public bool PostMessages { get; }

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
               !Other;
    }
}
