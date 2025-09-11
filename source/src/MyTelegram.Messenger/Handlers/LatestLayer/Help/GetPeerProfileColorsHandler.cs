namespace MyTelegram.Messenger.Handlers.LatestLayer.Help;

/// <summary>
///     Get the set of <a href="https://corefork.telegram.org/api/colors">accent color palettes »</a> that can be used in
///     profile page backgrounds.
///     See <a href="https://corefork.telegram.org/method/help.getPeerProfileColors" />
/// </summary>
internal sealed class GetPeerProfileColorsHandler : RpcResultObjectHandler<RequestGetPeerProfileColors, IPeerColors>
{
    private static readonly IPeerColors Colors = new TPeerColors
    {
        Colors = new TVector<IPeerColorOption>(new TPeerColorOption
        {
            ColorId = 5,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [4888278],
                BgColors = [5935035],
                StoryColors = [7264511, 7405535]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [3375297],
                BgColors = [4682132],
                StoryColors = [9029631, 7536638]
            }
        }, new TPeerColorOption
        {
            ColorId = 3,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [5485111],
                BgColors = [4825941],
                StoryColors = [7991418, 13299018]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [3972130],
                BgColors = [3371323],
                StoryColors = [7919501, 12703080]
            }
        }, new TPeerColorOption
        {
            ColorId = 1,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [14386489],
                BgColors = [12745790],
                StoryColors = [16756531, 16240230]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [12807972],
                BgColors = [9723436],
                StoryColors = [16756531, 16240230]
            }
        }, new TPeerColorOption
        {
            ColorId = 0,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [13722204],
                BgColors = [12211792],
                StoryColors = [16752253, 16758622]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [12209223],
                BgColors = [10241344],
                StoryColors = [16744573, 16745797]
            }
        }, new TPeerColorOption
        {
            ColorId = 2,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [10513887],
                BgColors = [9792200],
                StoryColors = [16030463, 16753387]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [9000906],
                BgColors = [7426201],
                StoryColors = [15366399, 16755185]
            }
        }, new TPeerColorOption
        {
            ColorId = 4,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [4565185],
                BgColors = [4102061],
                StoryColors = [5036799, 4325314]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [3646897],
                BgColors = [3702407],
                StoryColors = [4708863, 3342285]
            }
        }, new TPeerColorOption
        {
            ColorId = 6,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [13460119],
                BgColors = [12079992],
                StoryColors = [16746153, 16754323]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [11947138],
                BgColors = [9717603],
                StoryColors = [16675727, 16751486]
            }
        }, new TPeerColorOption
        {
            ColorId = 7,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [9477803],
                BgColors = [8358805],
                StoryColors = [12834019, 15726847]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [7964822],
                BgColors = [4412001],
                StoryColors = [10071235, 14871283]
            }
        }, new TPeerColorOption
        {
            ColorId = 13,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [3574481, 8246256],
                BgColors = [5475266, 5089469],
                StoryColors = [7264511, 7405535]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [5148620, 7525869],
                BgColors = [3694988, 4557729],
                StoryColors = [9029631, 7536638]
            }
        }, new TPeerColorOption
        {
            ColorId = 11,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [2599184, 11000919],
                BgColors = [4036437, 9021008],
                StoryColors = [7991418, 13299018]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [4036437, 10932055],
                BgColors = [2714179, 6262596],
                StoryColors = [7919501, 12703080]
            }
        }, new TPeerColorOption
        {
            ColorId = 9,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [14712875, 15842348],
                BgColors = [13595204, 13407283],
                StoryColors = [16756531, 16240230]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [13595204, 15247677],
                BgColors = [9393455, 10580530],
                StoryColors = [16756531, 16240230]
            }
        }, new TPeerColorOption
        {
            ColorId = 8,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [14966882, 16363107],
                BgColors = [13194845, 14253143],
                StoryColors = [16752253, 16758622]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [13194845, 16486759],
                BgColors = [10044227, 11294782],
                StoryColors = [16744573, 16745797]
            }
        }, new TPeerColorOption
        {
            ColorId = 10,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [10510323, 16027647],
                BgColors = [9855700, 12150454],
                StoryColors = [16030463, 16753387]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [9855700, 15236580],
                BgColors = [6506129, 9588898],
                StoryColors = [15366399, 16755185]
            }
        }, new TPeerColorOption
        {
            ColorId = 12,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [2600142, 9234906],
                BgColors = [4036026, 5287320],
                StoryColors = [5036799, 4325314]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [3774400, 7986629],
                BgColors = [3173500, 4102270],
                StoryColors = [4708863, 3342285]
            }
        }, new TPeerColorOption
        {
            ColorId = 14,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [13715826, 16760479],
                BgColors = [11554676, 13723245],
                StoryColors = [16746153, 16754323]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [12865666, 15830166],
                BgColors = [8929632, 10900057],
                StoryColors = [16675727, 16751486]
            }
        }, new TPeerColorOption
        {
            ColorId = 15,
            Colors = new TPeerColorProfileSet
            {
                PaletteColors = [7108740, 11384769],
                BgColors = [6517890, 8096407],
                StoryColors = [12834019, 15726847]
            },
            DarkColors = new TPeerColorProfileSet
            {
                PaletteColors = [7108740, 11384769],
                BgColors = [5464174, 3688020],
                StoryColors = [10071235, 14871283]
            }
        })
    };

    protected override Task<IPeerColors> HandleCoreAsync(IRequestInput input,
        RequestGetPeerProfileColors obj)
    {
        return Task.FromResult(Colors);
    }
}