using System.Collections;

namespace MyTelegram;

public class ChatAdminRights
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
        true,
        true,
        true,
        true,
        true,
        true
    );

    public ChatAdminRights()
    {

    }

    public ChatAdminRights(int flags) : this(flags.ToBitArray())
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
        bool manageTopics,
        bool postStories,
        bool editStories,
        bool deleteStories,
        bool manageDirectMessages
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
        PostStories = postStories;
        EditStories = editStories;
        DeleteStories = deleteStories;
        ManageDirectMessages = manageDirectMessages;

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
    public bool PostStories { get; set; }
    public bool EditStories { get; set; }
    public bool DeleteStories { get; set; }
    public bool ManageDirectMessages { get; set; }

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
               !ManageTopics &&
               !PostStories &&
               !EditStories &&
               !DeleteStories &&
               !ManageDirectMessages
            ;
    }

    public void ComputeFlag()
    {
        _flags[0] = ChangeInfo;
        _flags[1] = PostMessages;
        _flags[2] = EditMessages;
        _flags[3] = DeleteMessages;
        _flags[4] = BanUsers;
        _flags[5] = InviteUsers;
        _flags[7] = PinMessages;
        _flags[9] = AddAdmins;
        _flags[10] = Anonymous;
        _flags[11] = ManageCall;
        _flags[12] = Other;
        _flags[13] = ManageTopics;
        _flags[14] = PostStories;
        _flags[15] = EditStories;
        _flags[16] = DeleteStories;
        _flags[17] = ManageDirectMessages;
    }

    public BitArray GetFlags()
    {
        ComputeFlag();

        return _flags;
    }

    private void InitFromFlags()
    {
        ChangeInfo = _flags[0];
        PostMessages = _flags[1];
        EditMessages = _flags[2];
        DeleteMessages = _flags[3];
        BanUsers = _flags[4];
        InviteUsers = _flags[5];
        PinMessages = _flags[7];
        AddAdmins = _flags[9];
        Anonymous = _flags[10];
        ManageCall = _flags[11];
        Other = _flags[12];
        ManageTopics = _flags[13];
        PostStories = _flags[14];
        EditStories = _flags[15];
        DeleteStories = _flags[16];
        ManageDirectMessages = _flags[17];
    }
}