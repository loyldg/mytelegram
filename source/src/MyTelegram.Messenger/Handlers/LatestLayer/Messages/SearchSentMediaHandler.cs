namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;
/// <summary>
/// View and search recently sent media.<br/>
/// This method does not support pagination.
/// Possible errors
/// Code Type Description
/// 400 FILTER_NOT_SUPPORTED The specified filter cannot be used in this context.
/// <para><c>See <a href="https://corefork.telegram.org/method/messages.searchSentMedia"/> </c></para>
/// </summary>
/// <remarks>
/// Access: [User ✔] [Bot ✖] [Anonymous ✖]
/// </remarks>
internal sealed class SearchSentMediaHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestSearchSentMedia, MyTelegram.Schema.Messages.IMessages>
{
    protected override Task<MyTelegram.Schema.Messages.IMessages> HandleCoreAsync(IRequestInput input, MyTelegram.Schema.Messages.RequestSearchSentMedia obj)
    {
        return Task.FromResult<IMessages>(new TMessages { Chats = [], Messages = [], Users = [], Topics = [] });
    }
}