namespace MyTelegram.Services.Services;

public interface IMessageIdGenerator
{
    Task<long> GenerateServerMessageIdAsync();
}