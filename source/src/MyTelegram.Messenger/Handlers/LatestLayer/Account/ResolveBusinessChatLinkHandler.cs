namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// Resolve a <a href="https://corefork.telegram.org/api/business#business-chat-links">business chat deep link »</a>.
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHATLINK_SLUG_EMPTY The specified slug is empty.
/// 400 CHATLINK_SLUG_EXPIRED The specified <a href="https://corefork.telegram.org/api/business#business-chat-links">business chat link</a> has expired.
/// See <a href="https://corefork.telegram.org/method/account.resolveBusinessChatLink" />
///</summary>
internal sealed class ResolveBusinessChatLinkHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestResolveBusinessChatLink, MyTelegram.Schema.Account.IResolvedBusinessChatLinks>
{
    protected override Task<MyTelegram.Schema.Account.IResolvedBusinessChatLinks> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestResolveBusinessChatLink obj)
    {
        throw new NotImplementedException();
    }
}
