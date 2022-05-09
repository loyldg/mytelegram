namespace MyTelegram.Domain.ValueObjects;

public class ChatAdmin : ValueObject
{
    //public ChatAdmin()
    //{

    //}

    //[Newtonsoft.Json.JsonConstructor]
    //[JsonConstructor]
    public ChatAdmin(
        long promotedBy,
        bool canEdit,
        long userId,
        ChatAdminRights adminRights,
        string rank)
    {
        PromotedBy = promotedBy;
        CanEdit = canEdit;
        UserId = userId;
        AdminRights = adminRights;
        Rank = rank;
    }

    public ChatAdminRights AdminRights { get; private set; }

    /// <summary>
    ///     Can this admin promote other admins with the same permissions?
    /// </summary>
    public bool CanEdit { get; }

    /// <summary>
    ///     User that promoted the user to admin
    /// </summary>
    public long PromotedBy { get; }

    public string Rank { get; }
    public long UserId { get; }

    public void SetAdminRights(ChatAdminRights adminRights)
    {
        AdminRights = adminRights;
    }
}
