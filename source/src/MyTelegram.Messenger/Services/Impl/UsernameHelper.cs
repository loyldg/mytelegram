namespace MyTelegram.Messenger.Services.Impl;

public class UsernameHelper : IUsernameHelper, ITransientDependency
{
    private static readonly TimeSpan RxTimeout = TimeSpan.FromMilliseconds(150);

    // Validation for actual usernames (Telegram-like)
    private static readonly Regex UsernameValidation = new(
        @"^(?=.{5,32}$)[a-z0-9_]+$",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
        RxTimeout);

    // Same URL + email regexes as service (kept private here for exclusion)
    private static readonly Regex UrlRegex = new(
        """
        (?xi)
        (?<![@\w./-])
        (?:https?://)?(?:www\.)?
        (?: (?:25[0-5]|2[0-4]\d|1?\d?\d)\.(?:25[0-5]|2[0-4]\d|1?\d?\d)\.(?:25[0-5]|2[0-4]\d|1?\d?\d)\.(?:25[0-5]|2[0-4]\d|1?\d?\d)
            |
            [a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?(?:\.[a-z0-9](?:[a-z0-9-]{0,61}[a-z0-9])?)+
        )
        (?::\d{2,5})?
        (?:/(?:[^\s<>()\[\]{}"'`]+|\([^\s<>()\[\]{}"'`]*\))*)?
        (?=\s|$|[)\]\}.,!?;:])
        """,
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
        RxTimeout);

    private static readonly Regex EmailRegex = new(
        @"(?xi)\b[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,24}\b",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
        RxTimeout);

    // Mention detection with email/TLD safeguard; final exclusion via overlap below
    private static readonly Regex MentionRegex = new(
        @"(?xi)(?<![\w.\-])@([a-z0-9_]{4,40})(?!\.[a-z]{2,24}\b)",
        RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase,
        RxTimeout);

    public bool IsValidUsername(string username) => UsernameValidation.IsMatch(username ?? string.Empty);

    public IEnumerable<(int Offset, int Length, string Username)> FindMentions(string text)
    {
        if (string.IsNullOrEmpty(text))
            yield break;

        var used = new bool[text.Length];

        // Mark URL + email spans as excluded
        foreach (Match m in UrlRegex.Matches(text))
            Mark(used, m.Index, m.Length);
        foreach (Match m in EmailRegex.Matches(text))
            Mark(used, m.Index, m.Length);

        foreach (Match m in MentionRegex.Matches(text))
        {
            int start = m.Index, len = m.Length;
            if (Overlaps(used, start, len))
                continue;

            var uname = m.Groups[1].Value;
            yield return (start, len, uname);
        }

        static void Mark(bool[] a, int s, int l)
        {
            int e = Math.Min(a.Length, s + l);
            for (int i = Math.Max(0, s); i < e; i++) a[i] = true;
        }

        static bool Overlaps(bool[] a, int s, int l)
        {
            int e = Math.Min(a.Length, s + l);
            for (int i = Math.Max(0, s); i < e; i++)
                if (a[i]) return true;
            return false;
        }
    }
}
