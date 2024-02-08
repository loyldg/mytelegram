namespace MyTelegram;

public enum ChatJoinType
{
    Unknown,
    /// <summary>
    /// Invited by admin
    /// </summary>
    InvitedByAdmin,

    /// <summary>
    /// Joined by self request
    /// </summary>
    ByRequest,

    /// <summary>
    /// Joined By link
    /// </summary>
    ByLink,

    /// <summary>
    /// Approved by admin
    /// </summary>
    ApprovedByAdmin,
}