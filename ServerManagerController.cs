using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200012D RID: 301
public class ServerManagerController : MonoBehaviour
{
	// Token: 0x06000882 RID: 2178 RVA: 0x00035CB0 File Offset: 0x00033EB0
	private void Awake()
	{
		this.serverManager = base.GetComponent<ServerManager>();
		EventManager.AddEventListener("Event_Everyone_OnClientConnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientConnected));
		EventManager.AddEventListener("Event_Everyone_OnClientDisconnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientDisconnected));
		EventManager.AddEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.AddEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
		EventManager.AddEventListener("Event_OnServerStateChanged", new Action<Dictionary<string, object>>(this.Event_OnServerStateChanged));
		EventManager.AddEventListener("Event_OnTransportFailure", new Action<Dictionary<string, object>>(this.Event_OnTransportFailure));
		EventManager.AddEventListener("Event_OnMainMenuClickHostServer", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickHostServer));
		EventManager.AddEventListener("Event_OnNewServerClickStart", new Action<Dictionary<string, object>>(this.Event_OnNewServerClickStart));
		EventManager.AddEventListener("Event_OnPlayClickPractice", new Action<Dictionary<string, object>>(this.Event_OnPlayClickPractice));
		WebSocketManager.AddMessageListener("connected", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnConnected));
		WebSocketManager.AddMessageListener("serverKickPlayer", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnServerKickPlayer));
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Start()
	{
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x00035DBC File Offset: 0x00033FBC
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnClientConnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientConnected));
		EventManager.RemoveEventListener("Event_Everyone_OnClientDisconnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientDisconnected));
		EventManager.RemoveEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.RemoveEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
		EventManager.RemoveEventListener("Event_OnServerStateChanged", new Action<Dictionary<string, object>>(this.Event_OnServerStateChanged));
		EventManager.RemoveEventListener("Event_OnTransportFailure", new Action<Dictionary<string, object>>(this.Event_OnTransportFailure));
		EventManager.RemoveEventListener("Event_OnMainMenuClickHostServer", new Action<Dictionary<string, object>>(this.Event_OnMainMenuClickHostServer));
		EventManager.RemoveEventListener("Event_OnNewServerClickStart", new Action<Dictionary<string, object>>(this.Event_OnNewServerClickStart));
		EventManager.RemoveEventListener("Event_OnPlayClickPractice", new Action<Dictionary<string, object>>(this.Event_OnPlayClickPractice));
		WebSocketManager.RemoveMessageListener("connected", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnConnected));
		WebSocketManager.RemoveMessageListener("serverKickPlayer", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnServerKickPlayer));
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x00035EBC File Offset: 0x000340BC
	private void Event_Everyone_OnClientConnected(Dictionary<string, object> message)
	{
		ulong num = (ulong)message["clientId"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		ServerManagerController.Logger.Info(string.Format("Client connected ({0}) {1}/{2}", num, NetworkManager.Singleton.ConnectedClientsList.Count, this.serverManager.ServerConfig.maxPlayers));
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x00035F2C File Offset: 0x0003412C
	private void Event_Everyone_OnClientDisconnected(Dictionary<string, object> message)
	{
		ulong num = (ulong)message["clientId"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		ServerManagerController.Logger.Info(string.Format("Client disconnected ({0}) {1}/{2}", num, NetworkManager.Singleton.ConnectedClientsList.Count, this.serverManager.ServerConfig.maxPlayers));
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x00035F9C File Offset: 0x0003419C
	private void Event_OnServerStateChanged(Dictionary<string, object> message)
	{
		ref ServerState ptr = (ServerState)message["oldServerState"];
		ServerState serverState = (ServerState)message["newServerState"];
		if (ptr.AuthenticationPhase == AuthenticationPhase.None && serverState.AuthenticationPhase == AuthenticationPhase.Authenticated)
		{
			if (this.serverManager.IsServerStartInProgress)
			{
				NetworkManager.Singleton.StartServer();
				this.serverManager.IsServerStartInProgress = false;
			}
			if (this.serverManager.IsHostStartInProgress)
			{
				NetworkManager.Singleton.StartHost();
				this.serverManager.IsHostStartInProgress = false;
			}
		}
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0000EAAD File Offset: 0x0000CCAD
	private void Event_OnTransportFailure(Dictionary<string, object> message)
	{
		if (this.serverManager.IsHostStartInProgress)
		{
			this.serverManager.IsHostStartInProgress = false;
		}
		if (this.serverManager.IsServerStartInProgress)
		{
			this.serverManager.IsServerStartInProgress = false;
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x00036024 File Offset: 0x00034224
	private void Event_Server_OnServerStarted(Dictionary<string, object> message)
	{
		this.serverManager.Server.Value = new Server
		{
			IpAddress = this.serverManager.IpAddress,
			Port = this.serverManager.ServerConfig.port,
			Name = this.serverManager.ServerConfig.name,
			MaxPlayers = this.serverManager.ServerConfig.maxPlayers,
			TickRate = this.serverManager.ServerConfig.tickRate,
			UseVoip = this.serverManager.ServerConfig.useVoip
		};
		this.serverManager.StartTcpServer(this.serverManager.ServerConfig.port);
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0000EAE1 File Offset: 0x0000CCE1
	private void Event_Server_OnServerStopped(Dictionary<string, object> message)
	{
		this.serverManager.StopTcpServer();
		this.serverManager.StopPortForwarding();
		this.serverManager.Unauthenticate();
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x000360F4 File Offset: 0x000342F4
	private void Event_OnMainMenuClickHostServer(Dictionary<string, object> message)
	{
		ushort port = (ushort)message["port"];
		string password = (string)message["password"];
		this.serverManager.StartHost(port, "MY PUCK SERVER", 12, password, true, true, true);
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0003613C File Offset: 0x0003433C
	private void Event_OnNewServerClickStart(Dictionary<string, object> message)
	{
		if ((string)message["type"] != "selfHosted")
		{
			return;
		}
		int num = (int)message["port"];
		string name = (string)message["name"];
		int maxPlayers = (int)message["maxPlayers"];
		string password = (string)message["password"];
		bool useVoip = (bool)message["useVoip"];
		this.serverManager.StartHost((ushort)num, name, maxPlayers, password, true, useVoip, true);
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0000EB04 File Offset: 0x0000CD04
	private void Event_OnPlayClickPractice(Dictionary<string, object> message)
	{
		this.serverManager.StartHost(30609, "PRACTICE", 1, null, false, false, false);
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0000EB20 File Offset: 0x0000CD20
	private void WebSocket_Event_OnConnected(Dictionary<string, object> message)
	{
		if (ApplicationManager.IsDedicatedGameServer)
		{
			if (NetworkManager.Singleton.IsServer)
			{
				this.serverManager.Authenticate();
				return;
			}
			this.serverManager.StartServer(this.serverManager.ServerConfig.port, true);
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x000361D0 File Offset: 0x000343D0
	private void WebSocket_Event_OnServerKickPlayer(Dictionary<string, object> message)
	{
		ServerKickPlayer data = ((InMessage)message["inMessage"]).GetData<ServerKickPlayer>();
		Player playerBySteamId = MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayerBySteamId(data.steamId);
		if (!playerBySteamId)
		{
			return;
		}
		this.serverManager.Server_KickPlayer(playerBySteamId, DisconnectionCode.Kicked, null, false);
	}

	// Token: 0x04000519 RID: 1305
	private static readonly global::Logger Logger = new global::Logger("ServerManagerController");

	// Token: 0x0400051A RID: 1306
	private ServerManager serverManager;
}
