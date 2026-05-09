using MyTelegram.Domain.Aggregates.Language;

namespace MyTelegram.ReadModel.Impl;

public class LanguageReadModel : ReadModelBase, ILanguageReadModel,
    IAmReadModelFor<LanguageAggregate, LanguageId, LanguageCreatedEvent>
{
    public DeviceType Platform { get; private set; }
    public bool Rtl { get; private set; }
    public string Name { get; private set; } = null!;
    public string NativeName { get; private set; } = null!;
    public string LanguageCode { get; private set; } = null!;
    public string PluralCode { get; private set; } = null!;
    public string TranslationsUrl { get; private set; } = null!;
    public bool IsEnabled { get; private set; }
    public int TranslatedCount { get; private set; }
    public int LanguageVersion { get; private set; }
    public virtual string Id { get; private set; } = null!;
    public virtual long? Version { get; set; }
    public Task ApplyAsync(IReadModelContext context, IDomainEvent<LanguageAggregate, LanguageId, LanguageCreatedEvent> domainEvent, CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}