namespace MyTelegram.Messenger.Services;

public class SetPrivacyOutput
{
    public SetPrivacyOutput(IReadOnlyList<IPrivacyRule> rules)
    {
        Rules = rules;
    }

    public IReadOnlyList<IPrivacyRule> Rules { get; }
}