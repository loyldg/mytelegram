namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Allow a user to send us messages without paying if <a href="https://corefork.telegram.org/api/paid-messages">paid messages »</a> are enabled.
/// Possible errors
/// Code Type Description
/// 400 PARENT_PEER_INVALID The specified <code>parent_peer</code> is invalid.
/// 400 UNSUPPORTED <code>require_payment</code> cannot be <em>set</em> by users, only by monoforums: users must instead use the <a href="https://corefork.telegram.org/constructor/inputPrivacyKeyNoPaidMessages">inputPrivacyKeyNoPaidMessages</a> privacy setting to remove a previously added exemption.
/// 400 USER_ID_INVALID The provided user ID is invalid.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.toggleNoPaidMessagesException"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class ToggleNoPaidMessagesExceptionHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestToggleNoPaidMessagesException, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestToggleNoPaidMessagesException obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}