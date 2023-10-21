using MyTelegram.Messenger.Services;

namespace MyTelegram.Messenger.CommandServer.DomainEventHandlers;
public class UserDomainEventHandler :
    ISubscribeSynchronousTo<UserAggregate, UserId, UserCreatedEvent>
{
    private readonly IMessageAppService _messageAppService;
    private readonly IRandomHelper _randomHelper;
    public UserDomainEventHandler(IMessageAppService messageAppService,
        IRandomHelper randomHelper)
    {
        _messageAppService = messageAppService;
        _randomHelper = randomHelper;
    }

    public async Task HandleAsync(IDomainEvent<UserAggregate, UserId, UserCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        if (!domainEvent.AggregateEvent.Bot)
        {
            var welcomeMessage = "Welcome to use MyTelegram!";
            var sendMessageInput = new SendMessageInput(new RequestInfo(0,
                    MyTelegramServerDomainConsts.OfficialUserId,
                    domainEvent.AggregateEvent.RequestInfo.AuthKeyId,
                    domainEvent.AggregateEvent.RequestInfo.PermAuthKeyId,
                    Guid.NewGuid(), 0, DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()),
                MyTelegramServerDomainConsts.OfficialUserId,
                new Peer(PeerType.User, domainEvent.AggregateEvent.UserId,domainEvent.AggregateEvent.AccessHash),
                welcomeMessage,
                _randomHelper.NextLong());

            await _messageAppService.SendMessageAsync(sendMessageInput);
        }
    }
}