using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MyTelegram.Domain.Aggregates.Language;
using MyTelegram.Domain.Aggregates.PeerNotifySetting;
using MyTelegram.Domain.Aggregates.PeerSetting;
using MyTelegram.Domain.Aggregates.Photo;
using MyTelegram.Schema;
using System.Reflection;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MyTelegram.EventFlow.MongoDB.Extensions;
using MyTelegram.ReadModel.Extensions;

namespace MyTelegram.ReadModel.MongoDB;

public static class MyTelegramServerReadModelMongoDbExtensions
{
    public static void RegisterMongoDbSerializer(this IServiceCollection services)
    {
        var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true)
        };
        ConventionRegistry.Register("IgnoreExtraElements", pack, _ => true);

        var baseType = typeof(IObject);

        var objectSerializer = new ObjectSerializer(type => type.IsAssignableTo(baseType));
        //var guidSerializer = new GuidSerializer(GuidRepresentation.Standard);

        BsonSerializer.RegisterSerializer(objectSerializer);
        //BsonSerializer.RegisterSerializer(guidSerializer);

        var asm = baseType.Assembly;
        var baseInterfaceTypes = asm
            .GetTypes()
            .Where(t => t.IsInterface && t.IsAssignableTo(baseType) &&
                        t.GetCustomAttributes<JsonDerivedTypeAttribute>().Any())
            .ToList();

        var types = asm.GetTypes()
                .Where(t => baseInterfaceTypes.Any(t.IsAssignableTo) &&
                            t is { IsAbstract: false, IsInterface: false })
            ;

        foreach (var type in types)
        {
            var discriminator = type.Name;
            var ns = type.Namespace;
            if (!string.IsNullOrEmpty(ns))
            {
                var lastItem = ns.Split(".").Last();
                if (lastItem.StartsWith("Layer"))
                {
                    discriminator = $"{type.Name}{lastItem}";
                }
            }
            var cm = new BsonClassMap(type);
            cm.AutoMap();
            cm.SetDiscriminator(discriminator);
            var memberMap = cm.GetMemberMap("Flags");
            if (memberMap != null)
            {
                cm.UnmapMember(memberMap.MemberInfo);
            }
            var memberMap2 = cm.GetMemberMap("Flags2");
            if (memberMap2 != null)
            {
                cm.UnmapMember(memberMap2.MemberInfo);
            }
            BsonClassMap.RegisterClassMap(cm);
        }
    }

    public static IEventFlowOptions AddMyTelegramMongoDbReadModel(this IEventFlowOptions options)
    {
        var pack = new ConventionPack
        {
            new IgnoreExtraElementsConvention(true)
        };
        ConventionRegistry.Register("IgnoreExtraElements", pack, _ => true);
        options.ServiceCollection.RegisterServices(typeof(MyTelegramServerReadModelMongoDbExtensions).Assembly);
        options.ServiceCollection.AddMyTelegramReadModel();

        return options.AddDefaults(typeof(MyTelegramServerReadModelMongoDbExtensions).Assembly)
            .UseMongoDbReadModel<AppCodeAggregate, AppCodeId, AppCodeReadModel>()
            .UseMongoDbReadModel<DialogReadModel, IDialogReadModelLocator>()
            .UseMongoDbReadModel<MessageReadModel, IMessageIdLocator>()
            .UseMongoDbReadModel<PeerNotifySettingsAggregate, PeerNotifySettingsId, PeerNotifySettingsReadModel>()
            //.UseMongoDbReadModel<PtsReadModel, IPtsReadModelLocator>()
            .UseMongoDbReadModel<UserReadModel, IUserReadModelLocator>()
            //.UseMongoDbReadModel<BotAggregate, BotId, BotReadModel>()
            .UseMongoDbReadModel<ChannelReadModel, IChannelReadModelLocator>()
            .UseMongoDbReadModel<ChannelFullReadModel, IChannelFullReadModelLocator>()
            .UseMongoDbReadModel<ChannelMemberAggregate, ChannelMemberId, ChannelMemberReadModel>()
            .UseMongoDbReadModel<UserNameAggregate, UserNameId, UserNameReadModel>()
            .UseMongoDbReadModel<DeviceAggregate, DeviceId, DeviceReadModel>()
            .UseMongoDbReadModel<PushDeviceAggregate, PushDeviceId, PushDeviceReadModel>()
            //.UseMongoDbReadModel<DialogAggregate, DialogId, DraftReadModel>()
            .UseMongoDbReadModel<DraftReadModel, DraftReadModelLocator>()
            .UseMongoDbReadModel<ReadingHistoryAggregate, ReadingHistoryId, ReadingHistoryReadModel>()
            //.UseMongoDbReadModel<RpcResultAggregate, RpcResultId, RpcResultReadModel>()
            .UseMongoDbReadModel<ReplyReadModel, IReplyReadModelLocator>()

            .UseMongoDbReadModel<DialogFilterAggregate, DialogFilterId, DialogFilterReadModel>()
            .UseMongoDbReadModel<PollAggregate, PollId, PollReadModel>()
            .UseMongoDbReadModel<PollAnswerVoterReadModel, IPollAnswerVoterReadModelLocator>()
            .UseMongoDbReadModel<AccessHashReadModel, IAccessHashReadModelLocator>()
            .UseMongoDbReadModel<PeerSettingsAggregate, PeerSettingsId, PeerSettingsReadModel>()
            .UseMongoDbReadModel<ChatAdminReadModel, IChatAdminReadModelLocator>()
            .UseMongoDbReadModel<ChatInviteImporterReadModel, IChatInviteImporterReadModelLocator>()

            .UseMongoDbReadModel<ChatInviteAggregate, ChatInviteId, ChatInviteReadModel>()
            // photo created by file server
            .UseMongoDbReadModel<PhotoAggregate, PhotoId, PhotoReadModel>()

            .UseMongoDbReadModel<ContactAggregate, ContactId, ContactReadModel>()
            .UseMongoDbReadModel<ImportedContactAggregate, ImportedContactId, ImportedContactReadModel>()
            .UseMongoDbReadModel<PtsAggregate, PtsId, PtsReadModel>()
            .UseMongoDbReadModel<PtsAggregate, PtsId, PtsForAuthKeyIdReadModel>()
            .UseMongoDbReadModel<LanguageAggregate, LanguageId, LanguageReadModel>()
            .UseMongoDbReadModel<LanguageTextAggregate, LanguageTextId, LanguageTextReadModel>()
            .UseMongoDbReadModel<JoinChannelAggregate, JoinChannelId, JoinChannelRequestReadModel>()

            .UseMongoDbReadModel<UserConfigAggregate, UserConfigId, UserConfigReadModel>()
            .UseMongoDbReadModel<MessageTokenAggregate, MessageTokenId, MessageTokenReadModel>()

            ;
    }
}
