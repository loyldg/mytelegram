// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// See <a href="https://corefork.telegram.org/constructor/X" />
///</summary>
//[JsonDerivedType(typeof(TInitConnection), nameof(TInitConnection))]
public interface IX : IObject
{
    ///<summary>
    /// Flags, see <a href="https://corefork.telegram.org/mtproto/TL-combinators#conditional-fields">TL conditional fields</a>
    ///</summary>
    BitArray Flags { get; set; }

    ///<summary>
    /// Application identifier (see. <a href="https://corefork.telegram.org/myapp">App configuration</a>)
    ///</summary>
    int ApiId { get; set; }

    ///<summary>
    /// Device model
    ///</summary>
    string DeviceModel { get; set; }

    ///<summary>
    /// Operation system version
    ///</summary>
    string SystemVersion { get; set; }

    ///<summary>
    /// Application version
    ///</summary>
    string AppVersion { get; set; }

    ///<summary>
    /// Code for the language used on the device's OS, ISO 639-1 standard
    ///</summary>
    string SystemLangCode { get; set; }

    ///<summary>
    /// Language pack to use
    ///</summary>
    string LangPack { get; set; }

    ///<summary>
    /// Code for the language used on the client, ISO 639-1 standard
    ///</summary>
    string LangCode { get; set; }

    ///<summary>
    /// Info about an MTProto proxy
    /// See <a href="https://corefork.telegram.org/type/InputClientProxy" />
    ///</summary>
    MyTelegram.Schema.IInputClientProxy? Proxy { get; set; }

    ///<summary>
    /// The query itself
    ///</summary>
    IObject Query { get; set; }
}
