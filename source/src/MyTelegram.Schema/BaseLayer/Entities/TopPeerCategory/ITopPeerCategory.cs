// ReSharper disable All

namespace MyTelegram.Schema;

///<summary>
/// Top peer category
/// See <a href="https://corefork.telegram.org/constructor/TopPeerCategory" />
///</summary>
[JsonDerivedType(typeof(TTopPeerCategoryBotsPM), nameof(TTopPeerCategoryBotsPM))]
[JsonDerivedType(typeof(TTopPeerCategoryBotsInline), nameof(TTopPeerCategoryBotsInline))]
[JsonDerivedType(typeof(TTopPeerCategoryCorrespondents), nameof(TTopPeerCategoryCorrespondents))]
[JsonDerivedType(typeof(TTopPeerCategoryGroups), nameof(TTopPeerCategoryGroups))]
[JsonDerivedType(typeof(TTopPeerCategoryChannels), nameof(TTopPeerCategoryChannels))]
[JsonDerivedType(typeof(TTopPeerCategoryPhoneCalls), nameof(TTopPeerCategoryPhoneCalls))]
[JsonDerivedType(typeof(TTopPeerCategoryForwardUsers), nameof(TTopPeerCategoryForwardUsers))]
[JsonDerivedType(typeof(TTopPeerCategoryForwardChats), nameof(TTopPeerCategoryForwardChats))]
public interface ITopPeerCategory : IObject
{

}
