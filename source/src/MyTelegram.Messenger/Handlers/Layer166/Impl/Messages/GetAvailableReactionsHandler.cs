// ReSharper disable All

namespace MyTelegram.Handlers.Messages;

///<summary>
/// Obtain available <a href="https://corefork.telegram.org/api/reactions">message reactions »</a>
/// See <a href="https://corefork.telegram.org/method/messages.getAvailableReactions" />
///</summary>
internal sealed class GetAvailableReactionsHandler : RpcResultObjectHandler<MyTelegram.Schema.Messages.RequestGetAvailableReactions, MyTelegram.Schema.Messages.IAvailableReactions>,
    Messages.IGetAvailableReactionsHandler
{
    protected override Task<MyTelegram.Schema.Messages.IAvailableReactions> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Messages.RequestGetAvailableReactions obj)
    {
        //return Task.FromResult<IAvailableReactions>(new TAvailableReactions
        //{
        //    Reactions = new()
        //});

        var documentIds = new List<long>
        {
            5044395559658455501,//thumb up static
            5051049885634134593,//appear_animation
            5051019936827179425,//select_animation
            4986041492570112461,//activate_animation
            5044395559658455501,//effect_animation
            5098493726473323040,//around_animation
            5100483636361167223,//center_icon

            5125227088982311312,//thumb down
            5051235660149555648,//appear_animation
            5053080370078024140,//select_animation
            4985860914965119486,//activate_animation
            5125227088982311312,//effect_animation
            5098223680404587072,//around_animation
            5098584835614573070,//center_icon
        };

        var thumbsUp = new TAvailableReaction
        {
            Reaction = "👍",
            Title = "Thumbs Up",
            Inactive = false,
            Premium = false,

            StaticIcon = new TDocumentEmpty { Id = 5044395559658455501 },
            AppearAnimation = new TDocumentEmpty { Id = 5044395559658455501 },
            SelectAnimation = new TDocumentEmpty { Id = 5044395559658455501 },
            ActivateAnimation = new TDocumentEmpty { Id = 5044395559658455501 },
            EffectAnimation = new TDocumentEmpty { Id = 5044395559658455501 },

            //StaticIcon = new TDocument
            //{
            //    Id = 5044395559658455501,
            //},
            //AppearAnimation=new TDocument
            //{
            //    Id= 5044395559658455501,
            //},
            //SelectAnimation= new TDocument
            //{
            //    Id= 5044395559658455501,
            //},
            //ActivateAnimation = new TDocument
            //{
            //    Id = 5044395559658455501,
            //},
            //EffectAnimation = new TDocument
            //{
            //    Id = 5044395559658455501,
            //},

        };
        //var thumbsDown = new TAvailableReaction
        //{
        //    Reaction = "👎",
        //    Title = "Thumbs Down",
        //};

        var r = new TAvailableReactions
        {
            Reactions =new (),// new TVector<IAvailableReaction>(thumbsUp)
        };

        return Task.FromResult<IAvailableReactions>(r);
    }
}
