namespace MyTelegram.SmsSender;

[Volo.Abp.EventBus.EventName("MyTelegram.Core.AppCodeCreatedIntegrationEvent")]
public record AppCodeCreatedIntegrationEvent(long UserId, string PhoneNumber, string Code, int Expire);
