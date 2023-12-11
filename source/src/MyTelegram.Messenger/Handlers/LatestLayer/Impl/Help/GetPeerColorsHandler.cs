// ReSharper disable All

namespace MyTelegram.Handlers.Help;

///<summary>
/// See <a href="https://corefork.telegram.org/method/help.getPeerColors" />
///</summary>
internal sealed class GetPeerColorsHandler : RpcResultObjectHandler<MyTelegram.Schema.Help.RequestGetPeerColors, MyTelegram.Schema.Help.IPeerColors>,
    Help.IGetPeerColorsHandler
{
    protected override Task<MyTelegram.Schema.Help.IPeerColors> HandleCoreAsync(IRequestInput input,
        MyTelegram.Schema.Help.RequestGetPeerColors obj)
    {
        var colors = new List<TPeerColorOption>
        {
            new TPeerColorOption{ColorId = 5},
            new TPeerColorOption{ColorId = 3},
            new TPeerColorOption{ColorId = 1},
            new TPeerColorOption{ColorId = 0},
            new TPeerColorOption{ColorId = 2},
            new TPeerColorOption{ColorId = 4},
            new TPeerColorOption{ColorId = 6},

            new TPeerColorOption
            {
                ColorId = 12,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        3379668,
                        8246256
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        5423103,
                        742548
                    }
                }
            },
            new TPeerColorOption
            {
                ColorId = 10,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        2599184,
                        11000919
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        11004782,
                        1474093
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 8,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        14712875,
                        16434484
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        15511630,
                        12801812
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 7,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        14766162,
                        16363107
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        16749440,
                        10039095
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 9,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        10510323,
                        16027647
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        13015039,
                        6173128
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 11,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        2600142,
                        8579286
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        4249808,
                        285823
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 13,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        14500721,
                        16760479
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        16746150,
                        9320046
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 14,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        2391021,
                        15747158,
                        16777215
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        4170494,
                        15024719,
                        16777215
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 15,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        14055202,
                        2007057,
                        16777215
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        16748638,
                        3319079,
                        16777215
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 16,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        1547842,
                        15223359,
                        16777215
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        6738788,
                        13976655,
                        16777215
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 17,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        2659503,
                        7324758,
                        16777215
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        2276578,
                        4039232,
                        16777215
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 18,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        826035,
                        16756117,
                        16770741
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        2276578,
                        16750456,
                        16767595
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 19,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        7821270,
                        16225808,
                        16768654
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        9933311,
                        15889181,
                        16767833
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 20,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        1410511,
                        15903517,
                        16777215
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        4040427,
                        15639837,
                        16777215
                    }
                }
            },

            new TPeerColorOption
            {
                ColorId = 21,
                Colors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        0xfc4528,
                        0x4fd57e,
                        0xf62e7b
                    }
                },
                DarkColors = new TPeerColorSet
                {
                    Colors = new TVector<int>
                    {
                        0x640db5,
                        0x498141,
                        0xbf5103
                    }
                }
            },

        };

        return Task.FromResult<MyTelegram.Schema.Help.IPeerColors>(new TPeerColors
        {
            Colors = new TVector<IPeerColorOption>(colors)
        });
    }
}
