using Volo.Abp.EventBus;

namespace MyTelegram.SmsSender;

[EventName("MyTelegram.Core.AppCodeCreatedIntegrationEvent")]
public record AppCodeCreatedIntegrationEvent(long UserId,
    string PhoneNumber,
    string Code,
    int Expire);
