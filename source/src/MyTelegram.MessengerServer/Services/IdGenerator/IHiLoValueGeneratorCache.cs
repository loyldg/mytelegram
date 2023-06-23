namespace MyTelegram.MessengerServer.Services.IdGenerator;

public interface IHiLoValueGeneratorCache
{
    HiLoValueGeneratorState GetOrAdd(IdType idType,
        long key);
}