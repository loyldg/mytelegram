namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Delete a <a href="https://corefork.telegram.org/api/business#business-chat-links">business chat deep link »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHATLINK_SLUG_EMPTY The specified slug is empty.
/// 400 CHATLINK_SLUG_EXPIRED The specified <a href="https://corefork.telegram.org/api/business#business-chat-links">business chat link</a> has expired.
/// See <a href="https://corefork.telegram.org/method/account.deleteBusinessChatLink" />
///</summary>
internal sealed class DeleteBusinessChatLinkHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestDeleteBusinessChatLink, IBool>
{
    protected override Task<IBool> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestDeleteBusinessChatLink obj)
    {
        return Task.FromResult<IBool>(new TBoolTrue());
    }
}
