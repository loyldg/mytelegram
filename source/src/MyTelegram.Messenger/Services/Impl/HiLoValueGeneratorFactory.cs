using MyTelegram.Messenger.Services.IdGenerator;

namespace MyTelegram.Messenger.Services.Impl;

public class HiLoValueGeneratorFactory : IHiLoValueGeneratorFactory
{
    private readonly IHiLoHighValueGenerator _highValueGenerator;
    private readonly ILogger<DefaultHiLoValueGenerator> _logger;

    public HiLoValueGeneratorFactory(
        ILogger<DefaultHiLoValueGenerator> logger,
        IHiLoHighValueGenerator highValueGenerator)
    {
        _logger = logger;
        _highValueGenerator = highValueGenerator;
    }

    public HiLoValueGenerator<long> Create(HiLoValueGeneratorState state)
    {
        return new DefaultHiLoValueGenerator(state, _logger, _highValueGenerator);
    }
}
