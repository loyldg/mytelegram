namespace MyTelegram.Messenger.Handlers.LatestLayer.Account;

///<summary>
/// List all created <a href="https://corefork.telegram.org/api/business#business-chat-links">business chat deep links »</a>.
/// See <a href="https://corefork.telegram.org/method/account.getBusinessChatLinks" />
///</summary>
internal sealed class GetBusinessChatLinksHandler : RpcResultObjectHandler<MyTelegram.Schema.Account.RequestGetBusinessChatLinks, MyTelegram.Schema.Account.IBusinessChatLinks>
{
    protected override Task<MyTelegram.Schema.Account.IBusinessChatLinks> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Account.RequestGetBusinessChatLinks obj)
    {
        return Task.FromResult<MyTelegram.Schema.Account.IBusinessChatLinks>(new TBusinessChatLinks
        {
            Chats = [],
            Links = [],
            Users = []
        });
    }
}
