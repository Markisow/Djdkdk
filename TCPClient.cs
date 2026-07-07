using System;
using System.Text;
using System.Threading.Tasks;
using SuperSimpleTcp;

// Token: 0x02000200 RID: 512
public class TCPClient
{
	// Token: 0x14000006 RID: 6
	// (add) Token: 0x06000EF6 RID: 3830 RVA: 0x0004D234 File Offset: 0x0004B434
	// (remove) Token: 0x06000EF7 RID: 3831 RVA: 0x0004D26C File Offset: 0x0004B46C
	public event Action OnConnected;

	// Token: 0x14000007 RID: 7
	// (add) Token: 0x06000EF8 RID: 3832 RVA: 0x0004D2A4 File Offset: 0x0004B4A4
	// (remove) Token: 0x06000EF9 RID: 3833 RVA: 0x0004D2DC File Offset: 0x0004B4DC
	public event Action OnConnectionFailed;

	// Token: 0x14000008 RID: 8
	// (add) Token: 0x06000EFA RID: 3834 RVA: 0x0004D314 File Offset: 0x0004B514
	// (remove) Token: 0x06000EFB RID: 3835 RVA: 0x0004D34C File Offset: 0x0004B54C
	public event Action OnDisconnected;

	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06000EFC RID: 3836 RVA: 0x0004D384 File Offset: 0x0004B584
	// (remove) Token: 0x06000EFD RID: 3837 RVA: 0x0004D3BC File Offset: 0x0004B5BC
	public event Action<string> OnMessageReceived;

	// Token: 0x1400000A RID: 10
	// (add) Token: 0x06000EFE RID: 3838 RVA: 0x0004D3F4 File Offset: 0x0004B5F4
	// (remove) Token: 0x06000EFF RID: 3839 RVA: 0x0004D42C File Offset: 0x0004B62C
	public event Action<string> OnMessageSent;

	// Token: 0x17000130 RID: 304
	// (get) Token: 0x06000F00 RID: 3840 RVA: 0x00014148 File Offset: 0x00012348
	// (set) Token: 0x06000F01 RID: 3841 RVA: 0x00014150 File Offset: 0x00012350
	public bool IsConnecting { get; private set; }

	// Token: 0x17000131 RID: 305
	// (get) Token: 0x06000F02 RID: 3842 RVA: 0x00014159 File Offset: 0x00012359
	public bool IsConnected
	{
		get
		{
			return this.Client.IsConnected;
		}
	}

	// Token: 0x06000F03 RID: 3843 RVA: 0x0004D464 File Offset: 0x0004B664
	public TCPClient(EndPoint endPoint, int connectTimeoutMs = 1000, int readTimeoutMs = 1000)
	{
		this.EndPoint = endPoint;
		this.Client = new SimpleTcpClient(this.EndPoint.ipAddress, (int)this.EndPoint.port);
		this.Client.Settings.NoDelay = true;
		this.Client.Settings.UseAsyncDataReceivedEvents = false;
		this.Client.Settings.ConnectTimeoutMs = connectTimeoutMs;
		this.Client.Settings.ReadTimeoutMs = readTimeoutMs;
		this.Client.Events.Connected += delegate(object sender, ConnectionEventArgs args)
		{
			Action onConnected = this.OnConnected;
			if (onConnected == null)
			{
				return;
			}
			onConnected();
		};
		this.Client.Events.Disconnected += delegate(object sender, ConnectionEventArgs args)
		{
			Action onDisconnected = this.OnDisconnected;
			if (onDisconnected == null)
			{
				return;
			}
			onDisconnected();
		};
		this.Client.Events.DataReceived += delegate(object sender, DataReceivedEventArgs args)
		{
			this.OnDataReceived(sender, args);
		};
	}

	// Token: 0x06000F04 RID: 3844 RVA: 0x0004D538 File Offset: 0x0004B738
	public void Connect()
	{
		try
		{
			if (!this.IsConnecting)
			{
				this.IsConnecting = true;
				this.Client.Connect();
				this.IsConnecting = false;
			}
		}
		catch (TimeoutException)
		{
			this.IsConnecting = false;
			TCPClient.Logger.Error(string.Format("Connection to server {0} timed out", this.EndPoint));
			Action onConnectionFailed = this.OnConnectionFailed;
			if (onConnectionFailed != null)
			{
				onConnectionFailed();
			}
		}
		catch (Exception ex)
		{
			this.IsConnecting = false;
			TCPClient.Logger.Error(string.Format("Connection to server {0} failed: {1}", this.EndPoint, ex.Message));
			Action onConnectionFailed2 = this.OnConnectionFailed;
			if (onConnectionFailed2 != null)
			{
				onConnectionFailed2();
			}
		}
	}

	// Token: 0x06000F05 RID: 3845 RVA: 0x00014166 File Offset: 0x00012366
	public void ConnectAsync()
	{
		Task.Run(delegate()
		{
			this.Connect();
		});
	}

	// Token: 0x06000F06 RID: 3846 RVA: 0x0004D5FC File Offset: 0x0004B7FC
	public void Disconnect()
	{
		try
		{
			this.Client.Disconnect();
		}
		catch (Exception ex)
		{
			TCPClient.Logger.Error("Error disconnecting: " + ex.Message);
		}
	}

	// Token: 0x06000F07 RID: 3847 RVA: 0x0001417A File Offset: 0x0001237A
	public void DisconnectAsync()
	{
		Task.Run(delegate()
		{
			this.Disconnect();
		});
	}

	// Token: 0x06000F08 RID: 3848 RVA: 0x0004D644 File Offset: 0x0004B844
	public void SendMessage(string message)
	{
		try
		{
			byte[] bytes = Encoding.UTF8.GetBytes(message);
			this.Client.Send(bytes);
			Action<string> onMessageSent = this.OnMessageSent;
			if (onMessageSent != null)
			{
				onMessageSent(message);
			}
		}
		catch (Exception ex)
		{
			TCPClient.Logger.Error(string.Format("Error sending message to server {0}: {1}", this.EndPoint, ex.Message));
		}
	}

	// Token: 0x06000F09 RID: 3849 RVA: 0x0001418E File Offset: 0x0001238E
	public void SendMessageAsync(string message)
	{
		Task.Run(delegate()
		{
			this.SendMessage(message);
		});
	}

	// Token: 0x06000F0A RID: 3850 RVA: 0x0004D6B0 File Offset: 0x0004B8B0
	private void OnDataReceived(object sender, DataReceivedEventArgs args)
	{
		try
		{
			string @string = Encoding.UTF8.GetString(args.Data);
			Action<string> onMessageReceived = this.OnMessageReceived;
			if (onMessageReceived != null)
			{
				onMessageReceived(@string);
			}
		}
		catch (Exception ex)
		{
			TCPClient.Logger.Error(string.Format("Error deserializing message from server {0}: {1}", this.EndPoint, ex.Message));
		}
	}

	// Token: 0x04000929 RID: 2345
	private static readonly Logger Logger = new Logger("TCPClient");

	// Token: 0x0400092F RID: 2351
	public SimpleTcpClient Client;

	// Token: 0x04000930 RID: 2352
	public EndPoint EndPoint;
}
