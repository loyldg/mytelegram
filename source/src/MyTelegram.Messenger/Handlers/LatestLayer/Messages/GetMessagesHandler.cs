using MyTelegram.Messenger.Converters.ConverterServices.Messages;
using RequestGetMessages = MyTelegram.Schema.Messages.RequestGetMessages;

namespace MyTelegram.Messenger.Handlers.LatestLayer.Messages;

/// <summary>
///     Returns the list of messages by their IDs.
///     See <a href="https://corefork.telegram.org/method/messages.getMessages" />
/// </summary>
internal sealed class GetMessagesHandler(
    IMessageAppService messageAppService,
    IGetHistoryConverterService getHistoryConverterService)
    : RpcResultObjectHandler<RequestGetMessages, Schema.Messages.IMessages>
{
    protected override async Task<IMessages> HandleCoreAsync(IRequestInput input,
        RequestGetMessages obj)
    {
        var idList = new List<int>();
        foreach (var inputMessage in obj.Id)
        {
            if (inputMessage is TInputMessageID inputMessageId)
            {
                idList.Add(inputMessageId.Id);
            }
        }

        var getMessageOutput = await messageAppService
            .GetMessagesAsync(new GetMessagesInput(input.UserId, input.UserId, idList, null) { Limit = 50 });

        return getHistoryConverterService.ToMessages(input, getMessageOutput, input.Layer);
    }
}