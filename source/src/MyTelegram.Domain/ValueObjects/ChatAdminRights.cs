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

    public bool AddAdmins { get; private set; }
    public bool Anonymous { get; private set; }
    public bool BanUsers { get; private set; }

    public bool ChangeInfo { get; private set; }
    public bool DeleteMessages { get; private set; }
    public bool EditMessages { get; private set; }
    public bool InviteUsers { get; private set; }
    public bool ManageCall { get; private set; }
    public bool Other { get; private set; }
    public bool PinMessages { get; private set; }
    public bool PostMessages { get; private set; }

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
