using System.Text.RegularExpressions;

namespace MyTelegram.Domain.EventFlow;

public abstract class MyIdentity<T> : SingleValueObject<string>, IIdentity
    where T : MyIdentity<T>
{
    // ReSharper disable StaticMemberInGenericType
    private static readonly string Prefix;
    private static readonly Regex ValueValidation;
    // ReSharper enable StaticMemberInGenericType

    private static Func<string, T>? _createIdentityFunc;

    private readonly Lazy<Guid> _lazyGuid;

    static MyIdentity()
    {
        var name = typeof(T).Name;
        if (name.Equals("id", StringComparison.OrdinalIgnoreCase))
        {
            Prefix = string.Empty;
            ValueValidation = new Regex(
                @"^(?<guid>[a-f0-9]{8}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{12})$",
                RegexOptions.Compiled);
        }
        else
        {
            var nameReplace = new Regex("Id$");
            Prefix = nameReplace.Replace(typeof(T).Name, string.Empty).ToLowerInvariant() + "-";
            ValueValidation = new Regex(
                @"^[^\-]+\-(?<guid>[a-f0-9]{8}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{4}\-[a-f0-9]{12})$",
                RegexOptions.Compiled);
        }
    }

    protected MyIdentity(string value) : base(value)
    {
        var validationErrors = Validate(value).ToList();
        if (validationErrors.Any())
        {
            throw new ArgumentException($"Identity is invalid: {string.Join(", ", validationErrors)}");
        }

        _lazyGuid = new Lazy<Guid>(() => Guid.Parse(ValueValidation.Match(Value).Groups["guid"].Value));
    }

    public static T New => With(Guid.NewGuid());

    public static bool IsValid(string value)
    {
        return !Validate(value).Any();
    }

    public static T NewComb()
    {
        var guid = GuidFactories.Comb.CreateForString();
        return With(guid);
    }

    public static T NewDeterministic(Guid namespaceId, string name)
    {
        var guid = GuidFactories.Deterministic.Create(namespaceId, name);
        return With(guid);
    }

    public static T NewDeterministic(Guid namespaceId, byte[] nameBytes)
    {
        var guid = GuidFactories.Deterministic.Create(namespaceId, nameBytes);
        return With(guid);
    }
    public static IEnumerable<string> Validate(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            yield return $"Identity of type '{typeof(T).PrettyPrint()}' is null or empty";
            yield break;
        }

        if (!string.Equals(value.Trim(), value, StringComparison.OrdinalIgnoreCase))
        {
            yield return $"Identity '{value}' of type '{typeof(T).PrettyPrint()}' contains leading and/or trailing spaces";
        }

        if (!string.IsNullOrEmpty(Prefix) && !value.StartsWith(Prefix))
        {
            yield return $"Identity '{value}' of type '{typeof(T).PrettyPrint()}' does not start with '{Prefix}'";
        }

        if (!ValueValidation.IsMatch(value))
        {
            yield return $"Identity '{value}' of type '{typeof(T).PrettyPrint()}' does not follow the syntax '{Prefix}[GUID]' in lower case";
        }
    }

    public static T With(string value)
    {
        _createIdentityFunc ??= MyReflectionHelper.CompileConstructor<string, T>();

        return _createIdentityFunc(value);
    }

    public static T With(Guid guid)
    {
        var value = $"{Prefix}{guid:D}";
        return With(value);
    }
    public Guid GetGuid() => _lazyGuid.Value;
}