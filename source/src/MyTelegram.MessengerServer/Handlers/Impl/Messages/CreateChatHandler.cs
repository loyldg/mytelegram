using MyTelegram.Domain.Commands.Chat;
using MyTelegram.Handlers.Messages;
using MyTelegram.Schema.Messages;

namespace MyTelegram.MessengerServer.Handlers.Impl.Messages;

public class CreateChatHandler : RpcResultObjectHandler<RequestCreateChat, IUpdates>,
    ICreateChatHandler, IProcessedHandler //, IShouldCacheRequest
{
    private readonly ICommandBus _commandBus;
    private readonly IIdGenerator _idGenerator;
    private readonly IRandomHelper _randomHelper;

    public CreateChatHandler(ICommandBus commandBus,
        IIdGenerator idGenerator,
        IRandomHelper randomHelper)
    {
        _commandBus = commandBus;
        _idGenerator = idGenerator;
        _randomHelper = randomHelper;
    }

    protected override async Task<IUpdates> HandleCoreAsync(IRequestInput input,
        RequestCreateChat obj)
    {
        var memberUidList = new List<long>();
        foreach (var inputUser in obj.Users)
            if (inputUser is TInputUser u)
                memberUidList.Add(u.UserId);

        memberUidList = memberUidList.Distinct().ToList();

        var chatId = await _idGenerator.NextLongIdAsync(IdType.ChatId);
        var randomId = _randomHelper.NextLong();
        var messageActionData =
            new TMessageActionChatCreate { Title = obj.Title, Users = new TVector<long>(memberUidList) }.ToBytes()
                .ToHexString();

        var command = new CreateChatCommand(ChatId.Create(chatId),
            input.ToRequestInfo(),
            chatId,
            input.UserId,
            obj.Title,
            memberUidList,
            CurrentDate,
            randomId,
            messageActionData,
            Guid.NewGuid());
        await _commandBus.PublishAsync(command, CancellationToken.None);
        return null!;
    }
}