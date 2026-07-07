using System;
using System.Text;
using System.Threading.Tasks;
using SuperSimpleTcp;

// Token: 0x02000202 RID: 514
public class TCPServer
{
	// Token: 0x1400000B RID: 11
	// (add) Token: 0x06000F13 RID: 3859 RVA: 0x0004D71C File Offset: 0x0004B91C
	// (remove) Token: 0x06000F14 RID: 3860 RVA: 0x0004D754 File Offset: 0x0004B954
	public event Action<ushort> OnServerStarted;

	// Token: 0x1400000C RID: 12
	// (add) Token: 0x06000F15 RID: 3861 RVA: 0x0004D78C File Offset: 0x0004B98C
	// (remove) Token: 0x06000F16 RID: 3862 RVA: 0x0004D7C4 File Offset: 0x0004B9C4
	public event Action<Exception> OnServerStartFailed;

	// Token: 0x1400000D RID: 13
	// (add) Token: 0x06000F17 RID: 3863 RVA: 0x0004D7FC File Offset: 0x0004B9FC
	// (remove) Token: 0x06000F18 RID: 3864 RVA: 0x0004D834 File Offset: 0x0004BA34
	public event Action<ushort> OnServerStopped;

	// Token: 0x1400000E RID: 14
	// (add) Token: 0x06000F19 RID: 3865 RVA: 0x0004D86C File Offset: 0x0004BA6C
	// (remove) Token: 0x06000F1A RID: 3866 RVA: 0x0004D8A4 File Offset: 0x0004BAA4
	public event Action<string> OnClientConnected;

	// Token: 0x1400000F RID: 15
	// (add) Token: 0x06000F1B RID: 3867 RVA: 0x0004D8DC File Offset: 0x0004BADC
	// (remove) Token: 0x06000F1C RID: 3868 RVA: 0x0004D914 File Offset: 0x0004BB14
	public event Action<string> OnClientDisconnected;

	// Token: 0x14000010 RID: 16
	// (add) Token: 0x06000F1D RID: 3869 RVA: 0x0004D94C File Offset: 0x0004BB4C
	// (remove) Token: 0x06000F1E RID: 3870 RVA: 0x0004D984 File Offset: 0x0004BB84
	public event Action<string, string> OnMessageReceived;

	// Token: 0x14000011 RID: 17
	// (add) Token: 0x06000F1F RID: 3871 RVA: 0x0004D9BC File Offset: 0x0004BBBC
	// (remove) Token: 0x06000F20 RID: 3872 RVA: 0x0004D9F4 File Offset: 0x0004BBF4
	public event Action<string, string> OnMessageSent;

	// Token: 0x06000F21 RID: 3873 RVA: 0x0004DA2C File Offset: 0x0004BC2C
	public TCPServer(ushort port)
	{
		this.Server = new SimpleTcpServer("0.0.0.0", (int)port);
		this.Server.Settings.IdleClientTimeoutMs = 1000;
		this.Server.Settings.NoDelay = true;
		this.Server.Settings.UseAsyncDataReceivedEvents = false;
		this.Server.Events.ClientConnected += delegate(object sender, ConnectionEventArgs args)
		{
			Action<string> onClientConnected = this.OnClientConnected;
			if (onClientConnected == null)
			{
				return;
			}
			onClientConnected(args.IpPort);
		};
		this.Server.Events.ClientDisconnected += delegate(object sender, ConnectionEventArgs args)
		{
			Action<string> onClientDisconnected = this.OnClientDisconnected;
			if (onClientDisconnected == null)
			{
				return;
			}
			onClientDisconnected(args.IpPort);
		};
		this.Server.Events.DataReceived += delegate(object sender, DataReceivedEventArgs args)
		{
			this.OnDataReceived(sender, args);
		};
	}

	// Token: 0x06000F22 RID: 3874 RVA: 0x0004DADC File Offset: 0x0004BCDC
	public void Start()
	{
		try
		{
			if (!this.Server.IsListening)
			{
				this.Server.Start();
				TCPServer.Logger.Info(string.Format("Server started on port {0}", this.Server.Port));
				Action<ushort> onServerStarted = this.OnServerStarted;
				if (onServerStarted != null)
				{
					onServerStarted((ushort)this.Server.Port);
				}
			}
		}
		catch (Exception ex)
		{
			TCPServer.Logger.Error(string.Format("Server start failed on port {0}: {1}", this.Server.Port, ex.Message));
			Action<Exception> onServerStartFailed = this.OnServerStartFailed;
			if (onServerStartFailed != null)
			{
				onServerStartFailed(ex);
			}
		}
	}

	// Token: 0x06000F23 RID: 3875 RVA: 0x00014216 File Offset: 0x00012416
	public void StartAsync()
	{
		Task.Run(delegate()
		{
			this.Start();
		});
	}

	// Token: 0x06000F24 RID: 3876 RVA: 0x0004DB98 File Offset: 0x0004BD98
	public void Stop()
	{
		this.Server.Stop();
		TCPServer.Logger.Info(string.Format("Server stopped on port {0}", this.Server.Port));
		Action<ushort> onServerStopped = this.OnServerStopped;
		if (onServerStopped == null)
		{
			return;
		}
		onServerStopped((ushort)this.Server.Port);
	}

	// Token: 0x06000F25 RID: 3877 RVA: 0x0001422A File Offset: 0x0001242A
	public void StopAsync()
	{
		Task.Run(delegate()
		{
			this.Stop();
		});
	}

	// Token: 0x06000F26 RID: 3878 RVA: 0x0004DBF0 File Offset: 0x0004BDF0
	public void SendMessage(string ipPort, string message)
	{
		try
		{
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			this.Server.Send(ipPort, bytes);
			Action<string, string> onMessageSent = this.OnMessageSent;
			if (onMessageSent != null)
			{
				onMessageSent(ipPort, message);
			}
		}
		catch (Exception ex)
		{
			TCPServer.Logger.Error("Error sending message to client " + ipPort + ": " + ex.Message);
		}
	}

	// Token: 0x06000F27 RID: 3879 RVA: 0x0001423E File Offset: 0x0001243E
	public void SendMessageAsync(string ipPort, string message)
	{
		Task.Run(delegate()
		{
			this.SendMessage(ipPort, message);
		});
	}

	// Token: 0x06000F28 RID: 3880 RVA: 0x0004DC60 File Offset: 0x0004BE60
	public void DisconnectClient(string ipPort)
	{
		try
		{
			this.Server.DisconnectClient(ipPort);
		}
		catch (Exception ex)
		{
			TCPServer.Logger.Error("Error disconnecting client " + ipPort + ": " + ex.Message);
		}
	}

	// Token: 0x06000F29 RID: 3881 RVA: 0x0004DCB0 File Offset: 0x0004BEB0
	private void OnDataReceived(object sender, DataReceivedEventArgs args)
	{
		try
		{
			string @string = Encoding.UTF8.GetString(args.Data);
			Action<string, string> onMessageReceived = this.OnMessageReceived;
			if (onMessageReceived != null)
			{
				onMessageReceived(args.IpPort, @string);
			}
		}
		catch (Exception ex)
		{
			TCPServer.Logger.Error("Error deserializing message from client " + args.IpPort + ": " + ex.Message);
		}
	}

	// Token: 0x04000934 RID: 2356
	private static readonly Logger Logger = new Logger("TCPServer");

	// Token: 0x0400093C RID: 2364
	public SimpleTcpServer Server;
}
