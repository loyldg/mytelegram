namespace MyTelegram.Domain.ValueObjects;

public class ChatAdmin : ValueObject
{
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

    public ChatAdminRights AdminRights { get; internal set; }

    /// <summary>
    ///     Can this admin promote other admins with the same permissions?
    /// </summary>
    public bool CanEdit { get; init; }

    /// <summary>
    ///     User that promoted the user to admin
    /// </summary>
    public long PromotedBy { get; init; }

    public string Rank { get; init; }
    public long UserId { get; init; }

    public void SetAdminRights(ChatAdminRights adminRights)
    {
        AdminRights = adminRights;
    }
}
