namespace MyTelegram;

public enum ChatJoinType
{
    Unknown,

    /// <summary>
    ///     Invited by admin
    /// </summary>
    InvitedByAdmin,

    /// <summary>
    ///     Joined by self request
    /// </summary>
    BySelf,

    /// <summary>
    ///     Joined By link
    /// </summary>
    ByChatInvite,

    /// <summary>
    ///     Approved by admin
    /// </summary>
    ByRequest
}