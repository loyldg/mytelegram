namespace MyTelegram.Messenger.Services.IdGenerator;

public interface IHiLoValueGeneratorCache
{
    HiLoValueGeneratorState GetOrAdd(IdType idType, long key);
    Task<HiLoValueGeneratorState> GetOrAddAsync(IdType idType, long key, Func<Task<HiLoValueGeneratorState>> createStateFactory);
}