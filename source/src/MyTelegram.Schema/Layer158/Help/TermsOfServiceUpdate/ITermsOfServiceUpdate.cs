// ReSharper disable All

namespace MyTelegram.Schema.Help;

public interface ITermsOfServiceUpdate : IObject
{
    int Expires { get; set; }
}
