namespace MyTelegram.Messenger.Services.Interfaces;

public interface IHashCalculator
{
    long GetHash(IEnumerable<long> ids);
}