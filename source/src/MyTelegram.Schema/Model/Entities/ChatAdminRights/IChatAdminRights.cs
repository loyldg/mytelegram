// ReSharper disable All

namespace MyTelegram.Schema;

public interface IChatAdminRights : IObject
{
    BitArray Flags { get; set; }
    bool ChangeInfo { get; set; }
    bool PostMessages { get; set; }
    bool EditMessages { get; set; }
    bool DeleteMessages { get; set; }
    bool BanUsers { get; set; }
    bool InviteUsers { get; set; }
    bool PinMessages { get; set; }
    bool AddAdmins { get; set; }
    bool Anonymous { get; set; }
    bool ManageCall { get; set; }
    bool Other { get; set; }

}
