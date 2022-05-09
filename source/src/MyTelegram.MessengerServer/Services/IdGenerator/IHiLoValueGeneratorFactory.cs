namespace MyTelegram.MessengerServer.Services.IdGenerator;

public interface IHiLoValueGeneratorFactory
{
    HiLoValueGenerator<long> Create(HiLoValueGeneratorState state);
}