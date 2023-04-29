namespace MyTelegram.Domain.ValueObjects;

public class ChatAdminRights : ValueObject
{
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
        bool manageTopic = true
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
        ManageTopic = manageTopic;
    }

    public bool AddAdmins { get; init; }
    public bool Anonymous { get; init; }
    public bool BanUsers { get; init; }

    public bool ChangeInfo { get; init; }
    public bool DeleteMessages { get; init; }
    public bool EditMessages { get; init; }
    public bool InviteUsers { get; init; }
    public bool ManageCall { get; init; }
    public bool Other { get; init; }
    public bool ManageTopic { get; }
    public bool PinMessages { get; init; }
    public bool PostMessages { get; init; }

    public static ChatAdminRights GetCreatorRights()
    {
        return new ChatAdminRights(true,
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
    }

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
               !ManageTopic
            ;
    }
}
