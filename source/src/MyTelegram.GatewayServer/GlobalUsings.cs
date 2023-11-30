global using Microsoft.AspNetCore.Connections;
global using MyTelegram.Core;
global using MyTelegram.EventBus;
global using MyTelegram.GatewayServer;
global using MyTelegram.GatewayServer.BackgroundServices;
global using MyTelegram.GatewayServer.Configurations;
global using MyTelegram.GatewayServer.EventHandlers;
global using MyTelegram.GatewayServer.Extensions;
global using MyTelegram.GatewayServer.Services;
global using MyTelegram.MTProto;
global using Serilog;
global using Serilog.Sinks.SystemConsole.Themes;
global using System.Buffers;
global using System.Collections.Concurrent;
global using System.Diagnostics.CodeAnalysis;
global using System.IO.Pipelines;
global using System.Net;
global using System.Net.WebSockets;
global using System.Security.Cryptography.X509Certificates;

global using MyTelegram.EventBus.Rebus;
global using Rebus.Config;

global using UnencryptedMessage=MyTelegram.MTProto.UnencryptedMessage;
//global using UnencryptedMessageResponse = MyTelegram.MTProto.UnencryptedMessageResponse;
global using EncryptedMessage = MyTelegram.MTProto.EncryptedMessage;
//global using EncryptedMessageResponse = MyTelegram.MTProto.EncryptedMessageResponse;

global using CtrState=MyTelegram.MTProto.CtrState;
global using IAesHelper = MyTelegram.MTProto.IAesHelper;
global using AesHelper = MyTelegram.MTProto.AesHelper;
global using IMessageIdHelper=MyTelegram.MTProto.IMessageIdHelper;
global using MessageIdHelper = MyTelegram.MTProto.MessageIdHelper;