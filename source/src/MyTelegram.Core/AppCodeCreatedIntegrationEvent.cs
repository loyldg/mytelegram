namespace MyTelegram.Core;

public record AppCodeCreatedIntegrationEvent(long UserId,
    string PhoneNumber,
    string Code,
    int Expire);
