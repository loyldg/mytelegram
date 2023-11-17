// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Find out if a media message's caption can be edited
/// <para>Possible errors</para>
/// Code Type Description
/// 400 CHAT_ADMIN_REQUIRED You must be an admin in this chat to do this.
/// 403 CHAT_WRITE_FORBIDDEN You can't write in this chat.
/// 403 MESSAGE_AUTHOR_REQUIRED Message author required.
/// 400 MESSAGE_ID_INVALID The provided message id is invalid.
/// 400 PEER_ID_INVALID The provided peer id is invalid.
/// See <a href="https://corefork.telegram.org/method/messages.getMessageEditData" />
///</summary>
internal sealed class GetMessageEditDataHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetMessageEditData, MyTelegram.Schema.Messages.IMessageEditData>,
    Messages.IGetMessageEditDataHandler
{
    private readonly IOptions<MyTelegramMessengerServerOptions> _options;
    private readonly IQueryProcessor _queryProcessor;
    private readonly IAccessHashHelper _accessHashHelper;
    public GetMessageEditDataHandler(IQueryProcessor queryProcessor,
        IOptions<MyTelegramMessengerServerOptions> options,
        IAccessHashHelper accessHashHelper)
    {
        _queryProcessor = queryProcessor;
        _options = options;
        _accessHashHelper = accessHashHelper;
    }

    protected override async Task<IMessageEditData> HandleCoreAsync(IRequestInput input,
        RequestGetMessageEditData obj)
    {
        await _accessHashHelper.CheckAccessHashAsync(obj.Peer);
        var message = await _queryProcessor
            .ProcessAsync(
                new GetMessageByIdQuery(
                    MessageId.Create(input.UserId, obj.Id).Value),
                default);
        var canEdit = message != null && message.Date + _options.Value.EditTimeLimit > CurrentDate;
        return new TMessageEditData { Caption = canEdit };
    }
}
