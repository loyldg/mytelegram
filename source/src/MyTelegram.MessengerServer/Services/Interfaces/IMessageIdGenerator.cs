namespace MyTelegram.MessengerServer.Services.Interfaces;

public interface IMessageIdGenerator
{
    Task<long> GenerateServerMessageIdAsync();
}
