//namespace MyTelegram.MessengerServer.DomainEventHandlers;

//public class SendEmailDomainEventHandler :
//    ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, NewCloudPasswordUpdatedEvent>,
//    ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, PasswordConfirmEmailResentEvent>,
//    ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, RecoveryPasswordRequestedEvent>,
//    ISubscribeSynchronousTo<UserPasswordAggregate, UserPasswordId, RecoveryEmailChangedEvent>
//{
//    private readonly ILogger<SendEmailDomainEventHandler> _logger;

//    public SendEmailDomainEventHandler(ILogger<SendEmailDomainEventHandler> logger)
//    {
//        _logger = logger;
//    }

//    public Task HandleAsync(
//        IDomainEvent<UserPasswordAggregate, UserPasswordId, NewCloudPasswordUpdatedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        if (!string.IsNullOrEmpty(domainEvent.AggregateEvent.EmailConfirmCode))
//        {
//            _logger.LogInformation(
//                "######[Set new cloud password] send email code:[{EmailConfirmCode}] to mail:[{Email}],only for test,mail wil not be sent",
//                domainEvent.AggregateEvent.EmailConfirmCode,
//                domainEvent.AggregateEvent.Email
//            );
//        }

//        return Task.CompletedTask;
//    }

//    public Task HandleAsync(
//        IDomainEvent<UserPasswordAggregate, UserPasswordId, PasswordConfirmEmailResentEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        _logger.LogInformation(
//            "######[Confirm email resent] send email code:[{EmailConfirmCode}] to mail:[{Email}],only for test,mail wil not be sent",
//            domainEvent.AggregateEvent.EmailConfirmCode,
//            domainEvent.AggregateEvent.Email
//        );
//        return Task.CompletedTask;
//    }

//    public Task HandleAsync(IDomainEvent<UserPasswordAggregate, UserPasswordId, RecoveryEmailChangedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        _logger.LogInformation(
//            "######[Change recovery email] send email code:[{EmailConfirmCode}] to mail:[{NewEmail}],only for test,mail wil not be sent",
//            domainEvent.AggregateEvent.EmailConfirmCode,
//            domainEvent.AggregateEvent.NewEmail
//        );
//        return Task.CompletedTask;
//    }

//    public Task HandleAsync(
//        IDomainEvent<UserPasswordAggregate, UserPasswordId, RecoveryPasswordRequestedEvent> domainEvent,
//        CancellationToken cancellationToken)
//    {
//        _logger.LogInformation(
//            "######[Recovery password request] send email code:[{RecoveryPasswordCode}] to mail:[{Email}],only for test,mail wil not be sent",
//            domainEvent.AggregateEvent.RecoveryPasswordCode,
//            domainEvent.AggregateEvent.Email
//        );
//        return Task.CompletedTask;
//    }
//}
