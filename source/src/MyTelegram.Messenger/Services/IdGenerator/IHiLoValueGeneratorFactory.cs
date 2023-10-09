namespace MyTelegram.Messenger.Services.IdGenerator;

public interface IHiLoValueGeneratorFactory
{
    HiLoValueGenerator<long> Create(HiLoValueGeneratorState state);
}