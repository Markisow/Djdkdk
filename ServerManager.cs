using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

// Token: 0x0200012B RID: 299
[RequireComponent(typeof(EdgegapManager))]
[RequireComponent(typeof(ConnectionApprovalManager))]
[RequireComponent(typeof(TimeoutManager))]
[RequireComponent(typeof(BanManager))]
[RequireComponent(typeof(AdminManager))]
public class ServerManager : NetworkBehaviourSingleton<ServerManager>
{
	// Token: 0x0600085C RID: 2140 RVA: 0x000354CC File Offset: 0x000336CC
	public override void Awake()
	{
		base.Awake();
		if (ApplicationManager.IsDedicatedGameServer)
		{
			this.LoadConfig("./server_config.json", "--serverConfigPath", "--serverConfig", "PUCK_SERVER_CONFIG");
		}
		this.EdgegapManager = base.GetComponent<EdgegapManager>();
		this.ConnectionApprovalManager = base.GetComponent<ConnectionApprovalManager>();
		this.TimeoutManager = base.GetComponent<TimeoutManager>();
		this.BanManager = base.GetComponent<BanManager>();
		this.AdminManager = base.GetComponent<AdminManager>();
		this.WhitelistManager = base.GetComponent<WhitelistManager>();
		uPnPHelper.DebugMode = true;
		uPnPHelper.LogErrors = true;
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x00035554 File Offset: 0x00033754
	private void Start()
	{
		this.UnityTransport = NetworkManager.Singleton.GetComponent<UnityTransport>();
		NetworkManager.Singleton.OnServerStarted += this.Server_OnServerStarted;
		NetworkManager.Singleton.OnServerStopped += this.Server_OnServerStopped;
		NetworkManager.Singleton.OnClientConnectedCallback += this.OnClientConnected;
		NetworkManager.Singleton.OnClientDisconnectCallback += this.OnClientDisconnected;
		NetworkManager.Singleton.OnTransportFailure += this.OnTransportFailure;
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x000355E0 File Offset: 0x000337E0
	protected override void OnNetworkPreSpawn(ref NetworkManager networkManager)
	{
		if (this.Server == null)
		{
			this.Server = new NetworkVariable<Server>(default(Server), NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
		}
		if (networkManager.IsServer)
		{
			this.Server.Value = default(Server);
		}
		base.OnNetworkPreSpawn(ref networkManager);
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0000E7EC File Offset: 0x0000C9EC
	public override void OnNetworkSpawn()
	{
		NetworkVariable<Server> server = this.Server;
		server.OnValueChanged = (NetworkVariable<Server>.OnValueChangedDelegate)Delegate.Combine(server.OnValueChanged, new NetworkVariable<Server>.OnValueChangedDelegate(this.OnServerChanged));
		base.OnNetworkSpawn();
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0000E81B File Offset: 0x0000CA1B
	protected override void OnNetworkPostSpawn()
	{
		if (NetworkManager.Singleton.IsServer || NetworkManager.Singleton.IsConnectedClient)
		{
			this.ProcessInitialNetworkVariableValues();
		}
		base.OnNetworkPostSpawn();
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0000E841 File Offset: 0x0000CA41
	protected override void OnNetworkSessionSynchronized()
	{
		this.ProcessInitialNetworkVariableValues();
		base.OnNetworkSessionSynchronized();
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0000E84F File Offset: 0x0000CA4F
	public override void OnNetworkDespawn()
	{
		NetworkVariable<Server> server = this.Server;
		server.OnValueChanged = (NetworkVariable<Server>.OnValueChangedDelegate)Delegate.Remove(server.OnValueChanged, new NetworkVariable<Server>.OnValueChangedDelegate(this.OnServerChanged));
		base.OnNetworkDespawn();
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x00035630 File Offset: 0x00033830
	public override void OnDestroy()
	{
		if (NetworkManager.Singleton != null)
		{
			NetworkManager.Singleton.OnServerStarted -= this.Server_OnServerStarted;
			NetworkManager.Singleton.OnServerStopped -= this.Server_OnServerStopped;
			NetworkManager.Singleton.OnClientConnectedCallback -= this.OnClientConnected;
			NetworkManager.Singleton.OnClientDisconnectCallback -= this.OnClientDisconnected;
			NetworkManager.Singleton.OnTransportFailure -= this.OnTransportFailure;
		}
		uPnPHelper.CloseAll();
		Utils.PrintUPnPLogs();
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x000356C4 File Offset: 0x000338C4
	private void ProcessInitialNetworkVariableValues()
	{
		this.OnServerChanged(default(Server), this.Server.Value);
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x000356EC File Offset: 0x000338EC
	private void LoadConfig(string defaultFilePath, string filePathCliArgument = null, string cliArgument = null, string envVariable = null)
	{
		string environmentVariable = Environment.GetEnvironmentVariable(envVariable);
		string commandLineArgument = Utils.GetCommandLineArgument(cliArgument, null);
		if (!string.IsNullOrEmpty(environmentVariable))
		{
			ServerManager.Logger.Info("Deserializing server config from environment variable (" + envVariable + ")");
			this.ServerConfig = ConfigUtils.LoadConfigFromSerializedString<ServerConfig>(environmentVariable);
			return;
		}
		if (!string.IsNullOrEmpty(commandLineArgument))
		{
			ServerManager.Logger.Info("Deserializing server config from CLI argument (" + cliArgument + ")");
			this.ServerConfig = ConfigUtils.LoadConfigFromSerializedString<ServerConfig>(commandLineArgument);
			return;
		}
		string text = Utils.GetCommandLineArgument(filePathCliArgument, null) ?? defaultFilePath;
		ServerManager.Logger.Info("Deserializing server config from file (" + text + ")");
		this.ServerConfig = ConfigUtils.LoadConfigFromFile<ServerConfig>(text, true);
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0003579C File Offset: 0x0003399C
	private bool StartListener(ushort port, bool forwardPorts = false)
	{
		if (NetworkManager.Singleton.IsListening)
		{
			return false;
		}
		if (forwardPorts)
		{
			this.StartPortForwarding(port);
		}
		ServerManager.Logger.Info(string.Format("Starting Puck listener ({0})", ApplicationManager.Version));
		this.UnityTransport.SetConnectionData("0.0.0.0", port, null);
		return true;
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x000357F4 File Offset: 0x000339F4
	public void StartHost(ushort port, string name, int maxPlayers, string password, bool isPublic, bool useVoip, bool forwardPorts = false)
	{
		if (!this.StartListener(port, forwardPorts))
		{
			return;
		}
		this.ServerConfig = new ServerConfig
		{
			port = port,
			name = name,
			maxPlayers = maxPlayers,
			password = password,
			isPublic = isPublic,
			useVoip = useVoip
		};
		ConnectionData connectionData = new ConnectionData();
		connectionData.SteamId = BackendManager.PlayerState.PlayerData.steamId;
		connectionData.Key = BackendManager.PlayerState.Key;
		connectionData.Password = password;
		connectionData.EnabledModIds = (from mod in ModManager.EnabledMods
		select mod.Id).ToArray<string>();
		connectionData.Handedness = SettingsManager.Handedness;
		connectionData.FlagID = SettingsManager.FlagID;
		connectionData.HeadgearIDBlueAttacker = SettingsManager.HeadgearIDBlueAttacker;
		connectionData.HeadgearIDRedAttacker = SettingsManager.HeadgearIDRedAttacker;
		connectionData.HeadgearIDBlueGoalie = SettingsManager.HeadgearIDBlueGoalie;
		connectionData.HeadgearIDRedGoalie = SettingsManager.HeadgearIDRedGoalie;
		connectionData.MustacheID = SettingsManager.MustacheID;
		connectionData.BeardID = SettingsManager.BeardID;
		connectionData.JerseyIDBlueAttacker = SettingsManager.JerseyIDBlueAttacker;
		connectionData.JerseyIDRedAttacker = SettingsManager.JerseyIDRedAttacker;
		connectionData.JerseyIDBlueGoalie = SettingsManager.JerseyIDBlueGoalie;
		connectionData.JerseyIDRedGoalie = SettingsManager.JerseyIDRedGoalie;
		connectionData.StickSkinIDBlueAttacker = SettingsManager.StickSkinIDBlueAttacker;
		connectionData.StickSkinIDRedAttacker = SettingsManager.StickSkinIDRedAttacker;
		connectionData.StickSkinIDBlueGoalie = SettingsManager.StickSkinIDBlueGoalie;
		connectionData.StickSkinIDRedGoalie = SettingsManager.StickSkinIDRedGoalie;
		connectionData.StickShaftTapeIDBlueAttacker = SettingsManager.StickShaftTapeIDBlueAttacker;
		connectionData.StickShaftTapeIDRedAttacker = SettingsManager.StickShaftTapeIDRedAttacker;
		connectionData.StickShaftTapeIDBlueGoalie = SettingsManager.StickShaftTapeIDBlueGoalie;
		connectionData.StickShaftTapeIDRedGoalie = SettingsManager.StickShaftTapeIDRedGoalie;
		connectionData.StickBladeTapeIDBlueAttacker = SettingsManager.StickBladeTapeIDBlueAttacker;
		connectionData.StickBladeTapeIDRedAttacker = SettingsManager.StickBladeTapeIDRedAttacker;
		connectionData.StickBladeTapeIDBlueGoalie = SettingsManager.StickBladeTapeIDBlueGoalie;
		connectionData.StickBladeTapeIDRedGoalie = SettingsManager.StickBladeTapeIDRedGoalie;
		string s = JsonSerializer.Serialize<ConnectionData>(connectionData, null);
		NetworkManager.Singleton.NetworkConfig.ConnectionData = Encoding.UTF8.GetBytes(s);
		this.IsHostStartInProgress = true;
		this.Authenticate();
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0000E87E File Offset: 0x0000CA7E
	public void StartServer(ushort port, bool forwardPorts = false)
	{
		if (!this.StartListener(port, forwardPorts))
		{
			return;
		}
		this.IsServerStartInProgress = true;
		this.Authenticate();
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0000E898 File Offset: 0x0000CA98
	public void StartPortForwarding(ushort port)
	{
		ServerManager.Logger.Info(string.Format("Starting uPnP port forwarding for TCP & UDP port {0}", port));
		uPnPHelper.Start(uPnPHelper.Protocol.UDP, (int)port, 0, "Puck");
		Utils.PrintUPnPLogs();
		uPnPHelper.Start(uPnPHelper.Protocol.TCP, (int)port, 0, "Puck");
		Utils.PrintUPnPLogs();
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0000E8D8 File Offset: 0x0000CAD8
	public void StopPortForwarding()
	{
		ServerManager.Logger.Info("Stopping uPnP port forwarding");
		uPnPHelper.CloseAll();
		Utils.PrintUPnPLogs();
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x000359E4 File Offset: 0x00033BE4
	public void Authenticate()
	{
		string value = Environment.GetEnvironmentVariable("PUCK_MATCH_ID") ?? Utils.GetCommandLineArgument("--matchId", null);
		WebSocketManager.Emit("serverAuthenticateRequest", new Dictionary<string, object>
		{
			{
				"port",
				this.EdgegapManager.IsEdgegap ? this.EdgegapManager.ArbitriumPortPuckExternal : this.ServerConfig.port
			},
			{
				"isPublic",
				this.ServerConfig.isPublic
			},
			{
				"requestId",
				this.EdgegapManager.IsEdgegap ? this.EdgegapManager.RequestId : null
			},
			{
				"matchId",
				value
			}
		}, "serverAuthenticateResponse");
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0000E8F3 File Offset: 0x0000CAF3
	public void Unauthenticate()
	{
		WebSocketManager.Emit("serverUnauthenticateRequest", null, "serverUnauthenticateResponse");
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0000E905 File Offset: 0x0000CB05
	private void OnServerChanged(Server oldServer, Server newServer)
	{
		EventManager.TriggerEvent("Event_Everyone_OnServerChanged", new Dictionary<string, object>
		{
			{
				"oldServer",
				oldServer
			},
			{
				"newServer",
				newServer
			}
		});
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0000E938 File Offset: 0x0000CB38
	public void StartTcpServer(ushort port)
	{
		this.TcpServer = new TCPServer(port);
		this.TcpServer.OnMessageReceived += delegate(string ipPort, string message)
		{
			try
			{
				if (JsonSerializer.Deserialize<TCPServerMessage>(message, null).type == TCPServerMessageType.PreviewRequest)
				{
					JsonSerializer.Deserialize<TCPServerPreviewRequest>(message, null);
					string message2 = JsonSerializer.Serialize<TCPServerPreviewResponse>(new TCPServerPreviewResponse
					{
						name = this.ServerConfig.name,
						players = NetworkManager.Singleton.ConnectedClientsList.Count,
						maxPlayers = this.ServerConfig.maxPlayers,
						isPasswordProtected = !string.IsNullOrEmpty(this.ServerConfig.password),
						clientRequiredModIds = this.ServerConfig.ClientRequiredModIds
					}, null);
					this.TcpServer.SendMessageAsync(ipPort, message2);
				}
			}
			catch (Exception ex)
			{
				ServerManager.Logger.Error("Error parsing message from " + ipPort + ": " + ex.Message);
			}
		};
		this.TcpServer.StartAsync();
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0000E968 File Offset: 0x0000CB68
	public void StopTcpServer()
	{
		this.TcpServer.StopAsync();
		this.TcpServer = null;
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0000E97C File Offset: 0x0000CB7C
	private void Server_OnServerStarted()
	{
		EventManager.TriggerEvent("Event_Server_OnServerStarted", new Dictionary<string, object>
		{
			{
				"serverConfig",
				this.ServerConfig
			}
		});
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0000E99E File Offset: 0x0000CB9E
	private void Server_OnServerStopped(bool wasHost)
	{
		EventManager.TriggerEvent("Event_Server_OnServerStopped", new Dictionary<string, object>
		{
			{
				"wasHost",
				wasHost
			}
		});
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
	private void OnClientConnected(ulong clientId)
	{
		if (NetworkManager.Singleton.LocalClientId == clientId)
		{
			EventManager.TriggerEvent("Event_OnClientConnected", null);
		}
		EventManager.TriggerEvent("Event_Everyone_OnClientConnected", new Dictionary<string, object>
		{
			{
				"clientId",
				clientId
			}
		});
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0000E9FA File Offset: 0x0000CBFA
	private void OnClientDisconnected(ulong clientId)
	{
		EventManager.TriggerEvent("Event_Everyone_OnClientDisconnected", new Dictionary<string, object>
		{
			{
				"clientId",
				clientId
			}
		});
		if (NetworkManager.Singleton.LocalClientId == clientId)
		{
			EventManager.TriggerEvent("Event_OnClientDisconnected", null);
		}
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0000EA34 File Offset: 0x0000CC34
	private void OnTransportFailure()
	{
		EventManager.TriggerEvent("Event_OnTransportFailure", null);
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x00035AA4 File Offset: 0x00033CA4
	public void Server_KickPlayer(Player player, DisconnectionCode disconnectionCode = DisconnectionCode.Kicked, string message = null, bool applyTimeout = true)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		string steamId = player.SteamId.Value.ToString();
		if (player.OwnerClientId == 0UL)
		{
			NetworkManager.Singleton.Shutdown(true);
			return;
		}
		if (applyTimeout)
		{
			this.TimeoutManager.AddSteamIdTimeout(steamId, 60f);
		}
		string reason = JsonSerializer.Serialize<Disconnection>(new Disconnection
		{
			code = disconnectionCode,
			message = message
		}, null);
		NetworkManager.Singleton.DisconnectClient(player.OwnerClientId, reason);
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x00035B2C File Offset: 0x00033D2C
	public void Server_BanPlayer(Player player)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		string steamId = player.SteamId.Value.ToString();
		this.Server_BanSteamId(steamId);
		this.Server_KickPlayer(player, DisconnectionCode.Banned, null, false);
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0000EA41 File Offset: 0x0000CC41
	public void Server_BanSteamId(string steamId)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.BanManager.AddBannedSteamId(steamId);
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0000EA5C File Offset: 0x0000CC5C
	public void Server_UnbanSteamId(string steamId)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.BanManager.RemoveBannedSteamId(steamId);
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x00035C4C File Offset: 0x00033E4C
	protected override void __initializeVariables()
	{
		bool flag = this.Server == null;
		if (flag)
		{
			throw new Exception("ServerManager.Server cannot be null. All NetworkVariableBase instances must be initialized.");
		}
		this.Server.Initialize(this);
		base.__nameNetworkVariable(this.Server, "Server");
		this.NetworkVariableFields.Add(this.Server);
		base.__initializeVariables();
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0000EA90 File Offset: 0x0000CC90
	protected override void __initializeRpcs()
	{
		base.__initializeRpcs();
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0000EA9A File Offset: 0x0000CC9A
	protected internal override string __getTypeName()
	{
		return "ServerManager";
	}

	// Token: 0x04000509 RID: 1289
	private static readonly global::Logger Logger = new global::Logger("ServerManager");

	// Token: 0x0400050A RID: 1290
	[HideInInspector]
	public UnityTransport UnityTransport;

	// Token: 0x0400050B RID: 1291
	[HideInInspector]
	public EdgegapManager EdgegapManager;

	// Token: 0x0400050C RID: 1292
	[HideInInspector]
	public ConnectionApprovalManager ConnectionApprovalManager;

	// Token: 0x0400050D RID: 1293
	[HideInInspector]
	public TimeoutManager TimeoutManager;

	// Token: 0x0400050E RID: 1294
	[HideInInspector]
	public BanManager BanManager;

	// Token: 0x0400050F RID: 1295
	[HideInInspector]
	public AdminManager AdminManager;

	// Token: 0x04000510 RID: 1296
	[HideInInspector]
	public WhitelistManager WhitelistManager;

	// Token: 0x04000511 RID: 1297
	[HideInInspector]
	public NetworkVariable<Server> Server;

	// Token: 0x04000512 RID: 1298
	[HideInInspector]
	public bool IsHostStartInProgress;

	// Token: 0x04000513 RID: 1299
	[HideInInspector]
	public bool IsServerStartInProgress;

	// Token: 0x04000514 RID: 1300
	[HideInInspector]
	public TCPServer TcpServer;

	// Token: 0x04000515 RID: 1301
	[HideInInspector]
	public string IpAddress;

	// Token: 0x04000516 RID: 1302
	[HideInInspector]
	public ServerConfig ServerConfig;
}
