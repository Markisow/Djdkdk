using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SocketIOClient;
using SocketIOClient.Common;
using SocketIOClient.Common.Messages;
using SocketIOClient.Protocol.WebSocket;

// Token: 0x0200015C RID: 348
public static class WebSocketManager
{
	// Token: 0x1700010A RID: 266
	// (get) Token: 0x06000A85 RID: 2693 RVA: 0x00010CBF File Offset: 0x0000EEBF
	public static bool IsConnected
	{
		get
		{
			return WebSocketManager.socket != null && WebSocketManager.socket.Connected;
		}
	}

	// Token: 0x1700010B RID: 267
	// (get) Token: 0x06000A86 RID: 2694 RVA: 0x00010CD4 File Offset: 0x0000EED4
	public static bool IsReconnecting
	{
		get
		{
			return WebSocketManager.socket != null && !WebSocketManager.socket.Connected && !WebSocketManager.IsConnectionInProgress;
		}
	}

	// Token: 0x1700010C RID: 268
	// (get) Token: 0x06000A87 RID: 2695 RVA: 0x00010CF3 File Offset: 0x0000EEF3
	public static bool IsConnectionInProgress
	{
		get
		{
			return WebSocketManager.cancellationTokenSource != null;
		}
	}

	// Token: 0x06000A88 RID: 2696 RVA: 0x0003D160 File Offset: 0x0003B360
	public static void Initialize()
	{
		bool flag;
		WebSocketManager.forcePolling = (bool.TryParse(Utils.GetCommandLineArgument("--polling", null), out flag) && flag);
		WebSocketManagerController.Initialize();
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x00010CFD File Offset: 0x0000EEFD
	public static void Dispose()
	{
		WebSocketManagerController.Dispose();
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0003D190 File Offset: 0x0003B390
	public static Task Connect(string url)
	{
		WebSocketManager.<Connect>d__14 <Connect>d__;
		<Connect>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<Connect>d__.url = url;
		<Connect>d__.<>1__state = -1;
		<Connect>d__.<>t__builder.Start<WebSocketManager.<Connect>d__14>(ref <Connect>d__);
		return <Connect>d__.<>t__builder.Task;
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0003D1D4 File Offset: 0x0003B3D4
	public static Task CancelConnection()
	{
		WebSocketManager.<CancelConnection>d__15 <CancelConnection>d__;
		<CancelConnection>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<CancelConnection>d__.<>1__state = -1;
		<CancelConnection>d__.<>t__builder.Start<WebSocketManager.<CancelConnection>d__15>(ref <CancelConnection>d__);
		return <CancelConnection>d__.<>t__builder.Task;
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0003D210 File Offset: 0x0003B410
	public static Task Disconnect()
	{
		WebSocketManager.<Disconnect>d__16 <Disconnect>d__;
		<Disconnect>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
		<Disconnect>d__.<>1__state = -1;
		<Disconnect>d__.<>t__builder.Start<WebSocketManager.<Disconnect>d__16>(ref <Disconnect>d__);
		return <Disconnect>d__.<>t__builder.Task;
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0003D24C File Offset: 0x0003B44C
	public static void CreateSocket(string url)
	{
		if (WebSocketManager.socket != null)
		{
			WebSocketManager.DisposeSocket();
		}
		SocketIOOptions socketIOOptions = new SocketIOOptions
		{
			ConnectionTimeout = TimeSpan.FromMilliseconds(5000.0),
			Transport = (WebSocketManager.forcePolling ? TransportProtocol.Polling : TransportProtocol.WebSocket),
			AutoUpgrade = false
		};
		WebSocketManager.Logger.Info(string.Format("Creating WebSocket (url: {0}, transport: {1})", url, socketIOOptions.Transport));
		WebSocketManager.socket = new SocketIO(new Uri(url), socketIOOptions, delegate(IServiceCollection services)
		{
			services.AddSystemTextJson(WebSocketManager.JsonOptions);
			if (!url.StartsWith("wss://", StringComparison.OrdinalIgnoreCase) && !url.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
			{
				WebSocketOptions webSocketOptions = new WebSocketOptions();
				webSocketOptions.RemoteCertificateValidationCallback = ((object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true);
				services.AddSingleton(webSocketOptions);
			}
		});
		WebSocketManager.socket.OnConnected += WebSocketManager.OnConnected;
		WebSocketManager.socket.OnDisconnected += WebSocketManager.OnDisconnected;
		WebSocketManager.socket.OnError += WebSocketManager.OnError;
		WebSocketManager.socket.OnReconnectAttempt += WebSocketManager.OnReconnectAttempt;
		WebSocketManager.socket.OnReconnectError += WebSocketManager.OnReconnectError;
		WebSocketManager.socket.OnAny(new Func<string, IEventContext, Task>(WebSocketManager.OnAny));
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0003D370 File Offset: 0x0003B570
	public static void DisposeSocket()
	{
		if (WebSocketManager.socket == null)
		{
			return;
		}
		WebSocketManager.Logger.Info("Disposing WebSocket");
		WebSocketManager.socket.OnConnected -= WebSocketManager.OnConnected;
		WebSocketManager.socket.OnDisconnected -= WebSocketManager.OnDisconnected;
		WebSocketManager.socket.OnError -= WebSocketManager.OnError;
		WebSocketManager.socket.OnReconnectAttempt -= WebSocketManager.OnReconnectAttempt;
		WebSocketManager.socket.OnReconnectError -= WebSocketManager.OnReconnectError;
		WebSocketManager.socket.OffAny(new Func<string, IEventContext, Task>(WebSocketManager.OnAny));
		WebSocketManager.socket = null;
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0003D420 File Offset: 0x0003B620
	public static void Emit(string messageName, Dictionary<string, object> data = null, string responseMessageName = null)
	{
		OutMessage outMessage = new OutMessage(messageName, data, responseMessageName);
		if (outMessage.IsRequestMessage)
		{
			WebSocketManager.Logger.Info(string.Format("WebSocket sending request message {0} ({1})", outMessage.MessageName, outMessage));
			Func<IDataMessage, Task> ack = (IDataMessage dataMessage) => WebSocketManager.OnCallback(outMessage, dataMessage);
			SocketIO socketIO = WebSocketManager.socket;
			string messageName2 = outMessage.MessageName;
			object[] data2;
			if (outMessage.Data != null)
			{
				(data2 = new object[1])[0] = outMessage.Data;
			}
			else
			{
				data2 = Array.Empty<object>();
			}
			socketIO.EmitAsync(messageName2, data2, ack);
		}
		else
		{
			WebSocketManager.Logger.Info(string.Format("WebSocket sending message {0} ({1})", outMessage.MessageName, outMessage));
			SocketIO socketIO2 = WebSocketManager.socket;
			string messageName3 = outMessage.MessageName;
			object[] data3;
			if (outMessage.Data != null)
			{
				(data3 = new object[1])[0] = outMessage.Data;
			}
			else
			{
				data3 = Array.Empty<object>();
			}
			socketIO2.EmitAsync(messageName3, data3);
		}
		WebSocketManager.TriggerMessage("emit", new Dictionary<string, object>
		{
			{
				"messageName",
				outMessage.MessageName
			}
		});
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x00010D04 File Offset: 0x0000EF04
	private static void OnConnected(object sender, EventArgs args)
	{
		WebSocketManager.Logger.Info("WebSocket connected");
		WebSocketManager.TriggerMessage("connected", new Dictionary<string, object>
		{
			{
				"socket",
				WebSocketManager.socket
			}
		});
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x00010D34 File Offset: 0x0000EF34
	private static void OnDisconnected(object sender, string reason)
	{
		WebSocketManager.Logger.Info("WebSocket disconnected (" + reason + ")");
		WebSocketManager.TriggerMessage("disconnected", null);
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x00010D5B File Offset: 0x0000EF5B
	private static void OnError(object sender, string error)
	{
		WebSocketManager.Logger.Error("WebSocket error: " + error);
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x00010D72 File Offset: 0x0000EF72
	private static void OnReconnectAttempt(object sender, int attempt)
	{
		WebSocketManager.Logger.Info(string.Format("WebSocket reconnect attempt: {0}", attempt));
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x00010D8E File Offset: 0x0000EF8E
	private static void OnReconnectError(object sender, Exception exception)
	{
		WebSocketManager.Logger.Error(string.Format("WebSocket reconnect error: {0}", exception));
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0003D54C File Offset: 0x0003B74C
	private static Task OnAny(string messageName, IEventContext eventContext)
	{
		InMessage inMessage = new InMessage(messageName, eventContext, null);
		WebSocketManager.Logger.Info(string.Format("WebSocket received message {0} ({1})", messageName, inMessage));
		WebSocketManager.TriggerMessage(messageName, new Dictionary<string, object>
		{
			{
				"inMessage",
				inMessage
			}
		});
		return Task.CompletedTask;
	}

	// Token: 0x06000A96 RID: 2710 RVA: 0x0003D594 File Offset: 0x0003B794
	private static Task OnCallback(OutMessage outMessage, IDataMessage dataMessage)
	{
		InMessage inMessage = new InMessage(outMessage.ResponseMessageName, null, dataMessage);
		WebSocketManager.Logger.Info(string.Format("WebSocket received response to message {0} -> {1} ({2})", outMessage.MessageName, inMessage.MessageName, inMessage));
		WebSocketManager.TriggerMessage(inMessage.MessageName, new Dictionary<string, object>
		{
			{
				"outMessage",
				outMessage
			},
			{
				"inMessage",
				inMessage
			}
		});
		return Task.CompletedTask;
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0003D600 File Offset: 0x0003B800
	public static void AddMessageListener(string messageName, Action<Dictionary<string, object>> listener)
	{
		if (WebSocketManager.events.ContainsKey(messageName))
		{
			Dictionary<string, Action<Dictionary<string, object>>> dictionary = WebSocketManager.events;
			dictionary[messageName] = (Action<Dictionary<string, object>>)Delegate.Combine(dictionary[messageName], listener);
			return;
		}
		Action<Dictionary<string, object>> action = null;
		action = (Action<Dictionary<string, object>>)Delegate.Combine(action, listener);
		WebSocketManager.events.Add(messageName, action);
	}

	// Token: 0x06000A98 RID: 2712 RVA: 0x0003D658 File Offset: 0x0003B858
	public static void RemoveMessageListener(string messageName, Action<Dictionary<string, object>> listener)
	{
		if (WebSocketManager.events.ContainsKey(messageName))
		{
			Dictionary<string, Action<Dictionary<string, object>>> dictionary = WebSocketManager.events;
			dictionary[messageName] = (Action<Dictionary<string, object>>)Delegate.Remove(dictionary[messageName], listener);
		}
	}

	// Token: 0x06000A99 RID: 2713 RVA: 0x0003D694 File Offset: 0x0003B894
	public static void TriggerMessage(string messageName, Dictionary<string, object> message = null)
	{
		MonoBehaviourSingleton<ThreadManager>.Instance.Enqueue(delegate()
		{
			if (WebSocketManager.events.ContainsKey(messageName))
			{
				Action<Dictionary<string, object>> action = WebSocketManager.events[messageName];
				if (action == null)
				{
					return;
				}
				action(message);
			}
		});
	}

	// Token: 0x04000626 RID: 1574
	private static readonly Logger Logger = new Logger("WebsocketManager");

	// Token: 0x04000627 RID: 1575
	public static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
	{
		Converters = 
		{
			new JsonStringEnumConverter()
		}
	};

	// Token: 0x04000628 RID: 1576
	private static Dictionary<string, Action<Dictionary<string, object>>> events = new Dictionary<string, Action<Dictionary<string, object>>>();

	// Token: 0x04000629 RID: 1577
	private static SocketIO socket = null;

	// Token: 0x0400062A RID: 1578
	private static CancellationTokenSource cancellationTokenSource = null;

	// Token: 0x0400062B RID: 1579
	private static bool forcePolling = false;
}
