namespace MyTelegram.ReadModel.Interfaces;

public interface ILanguageTextReadModel : IReadModel
{
    DeviceType Platform { get; }
    string LanguageCode { get; }
    string Key { get; }
    string? Value { get; }
    string? ZeroValue { get; }
    string? OneValue { get; }
    string? TwoValue { get; }
    string? FewValue { get; }
    string? ManyValue { get; }
    string? OtherValue { get; }
    int LanguageVersion { get; }
}