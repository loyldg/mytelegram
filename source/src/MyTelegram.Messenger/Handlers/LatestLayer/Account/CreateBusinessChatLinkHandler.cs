namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;
/// <summary>
/// Create a <a href="https://corefork.telegram.org/api/business#business-chat-links">business chat deep link »</a>.
/// Possible errors
/// Code Type Description
/// 400 CHATLINKS_TOO_MUCH Too many <a href="https://corefork.telegram.org/api/business#business-chat-links">business chat links</a> were created, please delete some older links.
/// 403 PREMIUM_ACCOUNT_REQUIRED A premium account is required to execute this action.
/// <para><c>See <a href="https://corefork.telegram.org/method/account.createBusinessChatLink"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class CreateBusinessChatLinkHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestCreateBusinessChatLink, MyTelegram.Schema.IBusinessChatLink>
{
    protected override Task<MyTelegram.Schema.IBusinessChatLink> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Account.RequestCreateBusinessChatLink obj)
    {
        throw new NotImplementedException();
    }
}