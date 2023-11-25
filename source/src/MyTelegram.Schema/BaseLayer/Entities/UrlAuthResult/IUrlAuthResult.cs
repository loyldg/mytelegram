// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// URL authorization result
/// See <a href="https://corefork.telegram.org/constructor/UrlAuthResult" />
///</summary>
[JsonDerivedType(typeof(TUrlAuthResultRequest), nameof(TUrlAuthResultRequest))]
[JsonDerivedType(typeof(TUrlAuthResultAccepted), nameof(TUrlAuthResultAccepted))]
[JsonDerivedType(typeof(TUrlAuthResultDefault), nameof(TUrlAuthResultDefault))]
public interface IUrlAuthResult : IObject
{

}
