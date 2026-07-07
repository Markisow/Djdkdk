using System;
using System.Collections.Generic;
using Unity.Netcode;

// Token: 0x020001D7 RID: 471
public class UIToastManagerController : UIViewController<UIToastManager>
{
	// Token: 0x06000E18 RID: 3608 RVA: 0x0004AABC File Offset: 0x00048CBC
	public override void Awake()
	{
		base.Awake();
		this.uiToastManager = base.GetComponent<UIToastManager>();
		EventManager.AddEventListener("Event_Everyone_OnClientConnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientConnected));
		EventManager.AddEventListener("Event_OnSteamInitializationStarted", new Action<Dictionary<string, object>>(this.Event_OnSteamInitializationStarted));
		EventManager.AddEventListener("Event_OnSteamInitializationFailed", new Action<Dictionary<string, object>>(this.Event_OnSteamInitializationFailed));
		EventManager.AddEventListener("Event_OnSteamInitialized", new Action<Dictionary<string, object>>(this.Event_OnSteamInitialized));
		EventManager.AddEventListener("Event_OnSteamConnectionFailed", new Action<Dictionary<string, object>>(this.Event_OnSteamConnectionFailed));
		EventManager.AddEventListener("Event_OnSteamConnected", new Action<Dictionary<string, object>>(this.Event_OnSteamConnected));
		EventManager.AddEventListener("Event_OnSteamDisconnected", new Action<Dictionary<string, object>>(this.Event_OnSteamDisconnected));
		EventManager.AddEventListener("Event_OnPlayerStateChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerStateChanged));
		EventManager.AddEventListener("Event_OnTransportFailure", new Action<Dictionary<string, object>>(this.Event_OnTransportFailure));
		EventManager.AddEventListener("Event_OnClientStarted", new Action<Dictionary<string, object>>(this.Event_OnClientStarted));
		EventManager.AddEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		EventManager.AddEventListener("Event_OnConnectionRejected", new Action<Dictionary<string, object>>(this.Event_OnConnectionRejected));
		EventManager.AddEventListener("Event_OnDisconnected", new Action<Dictionary<string, object>>(this.Event_OnDisconnected));
		EventManager.AddEventListener("Event_OnPluginEnableFailed", new Action<Dictionary<string, object>>(this.Event_OnPluginEnableFailed));
		EventManager.AddEventListener("Event_OnPluginDisableFailed", new Action<Dictionary<string, object>>(this.Event_OnPluginDisableFailed));
		EventManager.AddEventListener("Event_OnModEnableFailed", new Action<Dictionary<string, object>>(this.Event_OnModEnableFailed));
		EventManager.AddEventListener("Event_OnModDisableFailed", new Action<Dictionary<string, object>>(this.Event_OnModDisableFailed));
		WebSocketManager.AddMessageListener("emit", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnEmit));
		WebSocketManager.AddMessageListener("connecting", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnConnecting));
		WebSocketManager.AddMessageListener("connected", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnConnected));
		WebSocketManager.AddMessageListener("disconnected", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnDisconnected));
		WebSocketManager.AddMessageListener("PlayerStartTransactionResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerStartTransactionResponse));
		WebSocketManager.AddMessageListener("playerDeployServerResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerDeployServerResponse));
		WebSocketManager.AddMessageListener("playerSetIdentityResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerSetIdentityResponse));
		WebSocketManager.AddMessageListener("playerJoinPartyResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerJoinPartyResponse));
		WebSocketManager.AddMessageListener("playerStartMatchmakingResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerStartMatchmakingResponse));
	}

	// Token: 0x06000E19 RID: 3609 RVA: 0x0004AD18 File Offset: 0x00048F18
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnClientConnected", new Action<Dictionary<string, object>>(this.Event_Everyone_OnClientConnected));
		EventManager.RemoveEventListener("Event_OnSteamInitializationStarted", new Action<Dictionary<string, object>>(this.Event_OnSteamInitializationStarted));
		EventManager.RemoveEventListener("Event_OnSteamInitializationFailed", new Action<Dictionary<string, object>>(this.Event_OnSteamInitializationFailed));
		EventManager.RemoveEventListener("Event_OnSteamInitialized", new Action<Dictionary<string, object>>(this.Event_OnSteamInitialized));
		EventManager.RemoveEventListener("Event_OnSteamConnectionFailed", new Action<Dictionary<string, object>>(this.Event_OnSteamConnectionFailed));
		EventManager.RemoveEventListener("Event_OnSteamConnected", new Action<Dictionary<string, object>>(this.Event_OnSteamConnected));
		EventManager.RemoveEventListener("Event_OnSteamDisconnected", new Action<Dictionary<string, object>>(this.Event_OnSteamDisconnected));
		EventManager.RemoveEventListener("Event_OnPlayerStateChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerStateChanged));
		EventManager.RemoveEventListener("Event_OnTransportFailure", new Action<Dictionary<string, object>>(this.Event_OnTransportFailure));
		EventManager.RemoveEventListener("Event_OnClientStarted", new Action<Dictionary<string, object>>(this.Event_OnClientStarted));
		EventManager.RemoveEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		EventManager.RemoveEventListener("Event_OnConnectionRejected", new Action<Dictionary<string, object>>(this.Event_OnConnectionRejected));
		EventManager.RemoveEventListener("Event_OnDisconnected", new Action<Dictionary<string, object>>(this.Event_OnDisconnected));
		EventManager.RemoveEventListener("Event_OnPluginEnableFailed", new Action<Dictionary<string, object>>(this.Event_OnPluginEnableFailed));
		EventManager.RemoveEventListener("Event_OnPluginDisableFailed", new Action<Dictionary<string, object>>(this.Event_OnPluginDisableFailed));
		EventManager.RemoveEventListener("Event_OnModEnableFailed", new Action<Dictionary<string, object>>(this.Event_OnModEnableFailed));
		EventManager.RemoveEventListener("Event_OnModDisableFailed", new Action<Dictionary<string, object>>(this.Event_OnModDisableFailed));
		WebSocketManager.RemoveMessageListener("emit", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnEmit));
		WebSocketManager.RemoveMessageListener("connecting", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnConnecting));
		WebSocketManager.RemoveMessageListener("connected", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnConnected));
		WebSocketManager.RemoveMessageListener("disconnected", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnDisconnected));
		WebSocketManager.RemoveMessageListener("PlayerStartTransactionResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerStartTransactionResponse));
		WebSocketManager.RemoveMessageListener("playerDeployServerResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerDeployServerResponse));
		WebSocketManager.RemoveMessageListener("playerSetIdentityResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerSetIdentityResponse));
		WebSocketManager.RemoveMessageListener("playerJoinPartyResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerJoinPartyResponse));
		WebSocketManager.RemoveMessageListener("playerStartMatchmakingResponse", new Action<Dictionary<string, object>>(this.WebSocket_Event_OnPlayerStartMatchmakingResponse));
		base.OnDestroy();
	}

	// Token: 0x06000E1A RID: 3610 RVA: 0x0004AF68 File Offset: 0x00049168
	private void Event_Everyone_OnClientConnected(Dictionary<string, object> message)
	{
		ulong num = (ulong)message["clientId"];
		if (NetworkManager.Singleton.LocalClientId != num)
		{
			return;
		}
		this.uiToastManager.HideToast("serverConnection");
	}

	// Token: 0x06000E1B RID: 3611 RVA: 0x00013A10 File Offset: 0x00011C10
	private void Event_OnSteamInitializationStarted(Dictionary<string, object> message)
	{
		this.uiToastManager.ShowToast("steamInitialization", "Initializing Steam...", float.PositiveInfinity);
	}

	// Token: 0x06000E1C RID: 3612 RVA: 0x00013A2C File Offset: 0x00011C2C
	private void Event_OnSteamInitializationFailed(Dictionary<string, object> message)
	{
		this.uiToastManager.ShowToast("steamInitialization", "Failed to initialize Steam, retrying...", float.PositiveInfinity);
	}

	// Token: 0x06000E1D RID: 3613 RVA: 0x00013A48 File Offset: 0x00011C48
	private void Event_OnSteamInitialized(Dictionary<string, object> message)
	{
		this.uiToastManager.HideToast("steamInitialization");
		this.uiToastManager.ShowToast("steamConnection", "Connecting to Steam...", float.PositiveInfinity);
	}

	// Token: 0x06000E1E RID: 3614 RVA: 0x00013A74 File Offset: 0x00011C74
	private void Event_OnSteamConnectionFailed(Dictionary<string, object> message)
	{
		this.uiToastManager.ShowToast("steamConnection", "Failed to connect to Steam, retrying...", float.PositiveInfinity);
	}

	// Token: 0x06000E1F RID: 3615 RVA: 0x00013A90 File Offset: 0x00011C90
	private void Event_OnSteamConnected(Dictionary<string, object> message)
	{
		this.uiToastManager.HideToast("steamConnection");
	}

	// Token: 0x06000E20 RID: 3616 RVA: 0x00013AA2 File Offset: 0x00011CA2
	private void Event_OnSteamDisconnected(Dictionary<string, object> message)
	{
		this.uiToastManager.ShowToast("steamConnection", "Disconnected from Steam, reconnecting...", float.PositiveInfinity);
	}

	// Token: 0x06000E21 RID: 3617 RVA: 0x0004AFA4 File Offset: 0x000491A4
	private void Event_OnPlayerStateChanged(Dictionary<string, object> message)
	{
		if (((PlayerState)message["newPlayerState"]).AuthenticationPhase == AuthenticationPhase.Authenticating)
		{
			this.uiToastManager.ShowToast("playerAuthentication", "Authenticating...", float.PositiveInfinity);
			return;
		}
		this.uiToastManager.HideToast("playerAuthentication");
	}

	// Token: 0x06000E22 RID: 3618 RVA: 0x00013ABE File Offset: 0x00011CBE
	private void Event_OnTransportFailure(Dictionary<string, object> message)
	{
		this.uiToastManager.ShowToast("transportFailure", "Network transport failure", 3f);
	}

	// Token: 0x06000E23 RID: 3619 RVA: 0x00013ADA File Offset: 0x00011CDA
	private void Event_OnClientStarted(Dictionary<string, object> message)
	{
		this.uiToastManager.ShowToast("serverConnection", "Connecting to server...", float.PositiveInfinity);
	}

	// Token: 0x06000E24 RID: 3620 RVA: 0x00013AF6 File Offset: 0x00011CF6
	private void Event_OnClientStopped(Dictionary<string, object> message)
	{
		this.uiToastManager.HideToast("serverConnection");
	}

	// Token: 0x06000E25 RID: 3621 RVA: 0x0004AFF4 File Offset: 0x000491F4
	private void Event_OnConnectionRejected(Dictionary<string, object> message)
	{
		ConnectionRejection connectionRejection = (ConnectionRejection)message["connectionRejection"];
		if (connectionRejection.code == ConnectionRejectionCode.MissingPassword || connectionRejection.code == ConnectionRejectionCode.MissingMods)
		{
			return;
		}
		this.uiToastManager.ShowToast("connectionRejected", "Connection rejected: " + Utils.GetConnectionRejectionMessage(connectionRejection.code, connectionRejection.message), 3f);
	}

	// Token: 0x06000E26 RID: 3622 RVA: 0x0004B058 File Offset: 0x00049258
	private void Event_OnDisconnected(Dictionary<string, object> message)
	{
		Disconnection disconnection = (Disconnection)message["disconnection"];
		if (disconnection.code != DisconnectionCode.Disconnected)
		{
			this.uiToastManager.ShowToast("disconnected", "Disconnected: " + Utils.GetDisconnectionMessage(disconnection.code, disconnection.message), 3f);
		}
	}

	// Token: 0x06000E27 RID: 3623 RVA: 0x0004B0B0 File Offset: 0x000492B0
	private void Event_OnPluginEnableFailed(Dictionary<string, object> message)
	{
		Plugin plugin = (Plugin)message["plugin"];
		this.uiToastManager.ShowToast("pluginEnableFailed_" + plugin.Id, "Failed to enable plugin " + plugin.Id, 3f);
	}

	// Token: 0x06000E28 RID: 3624 RVA: 0x0004B100 File Offset: 0x00049300
	private void Event_OnPluginDisableFailed(Dictionary<string, object> message)
	{
		Plugin plugin = (Plugin)message["plugin"];
		this.uiToastManager.ShowToast("pluginDisableFailed_" + plugin.Id, "Failed to disable plugin " + plugin.Id, 3f);
	}

	// Token: 0x06000E29 RID: 3625 RVA: 0x0004B150 File Offset: 0x00049350
	private void Event_OnModEnableFailed(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		SteamWorkshopItemDetails details = mod.SteamWorkshopItem.Details;
		string str = ((details != null) ? details.Title : null) ?? mod.Id;
		this.uiToastManager.ShowToast("modEnableFailed_" + mod.Id, "Failed to enable mod " + str, 3f);
	}

	// Token: 0x06000E2A RID: 3626 RVA: 0x0004B1BC File Offset: 0x000493BC
	private void Event_OnModDisableFailed(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		SteamWorkshopItemDetails details = mod.SteamWorkshopItem.Details;
		string str = ((details != null) ? details.Title : null) ?? mod.Id;
		this.uiToastManager.ShowToast("modDisableFailed_" + mod.Id, "Failed to disable mod " + str, 3f);
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x00013B08 File Offset: 0x00011D08
	private void WebSocket_Event_OnEmit(Dictionary<string, object> message)
	{
		if ((string)message["messageName"] == "playerDeployServerRequest")
		{
			this.uiToastManager.ShowToast("playerDeployServer", "Deploying server...", float.PositiveInfinity);
		}
	}

	// Token: 0x06000E2C RID: 3628 RVA: 0x00013B40 File Offset: 0x00011D40
	private void WebSocket_Event_OnConnecting(Dictionary<string, object> message)
	{
		this.uiToastManager.ShowToast("webSocketConnection", "Connecting to Puck backend...", float.PositiveInfinity);
	}

	// Token: 0x06000E2D RID: 3629 RVA: 0x00013B5C File Offset: 0x00011D5C
	private void WebSocket_Event_OnConnected(Dictionary<string, object> message)
	{
		this.uiToastManager.HideToast("webSocketConnection");
	}

	// Token: 0x06000E2E RID: 3630 RVA: 0x00013B6E File Offset: 0x00011D6E
	private void WebSocket_Event_OnDisconnected(Dictionary<string, object> message)
	{
		this.uiToastManager.ShowToast("webSocketConnection", "Disconnected from Puck backend, reconnecting...", float.PositiveInfinity);
	}

	// Token: 0x06000E2F RID: 3631 RVA: 0x0004B228 File Offset: 0x00049428
	private void WebSocket_Event_OnPlayerStartTransactionResponse(Dictionary<string, object> message)
	{
		PlayerStartTransactionResponse data = ((InMessage)message["inMessage"]).GetData<PlayerStartTransactionResponse>();
		if (!data.success)
		{
			this.uiToastManager.ShowToast("playerStartTransaction", "Failed to start transaction: " + data.errorData.message, 3f);
		}
	}

	// Token: 0x06000E30 RID: 3632 RVA: 0x0004B280 File Offset: 0x00049480
	private void WebSocket_Event_OnPlayerDeployServerResponse(Dictionary<string, object> message)
	{
		PlayerDeployServerResponse data = ((InMessage)message["inMessage"]).GetData<PlayerDeployServerResponse>();
		if (data.success)
		{
			this.uiToastManager.ShowToast("playerDeployServer", "Server deployed!", 3f);
			return;
		}
		this.uiToastManager.ShowToast("playerDeployServer", "Failed to deploy server: " + data.errorData.message, 3f);
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0004B2F0 File Offset: 0x000494F0
	private void WebSocket_Event_OnPlayerSetIdentityResponse(Dictionary<string, object> message)
	{
		OutMessage outMessage = (OutMessage)message["outMessage"];
		InMessage inMessage = (InMessage)message["inMessage"];
		PlayerSetIdentityResponse data = inMessage.GetData<PlayerSetIdentityResponse>();
		string text = (string)outMessage.Data["username"];
		int num = (int)outMessage.Data["number"];
		if (data.success && BackendManager.PlayerState.PlayerData != null)
		{
			BackendManager.PlayerState.PlayerData.username = text;
			BackendManager.PlayerState.PlayerData.number = num;
		}
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x0004B348 File Offset: 0x00049548
	private void WebSocket_Event_OnPlayerJoinPartyResponse(Dictionary<string, object> message)
	{
		PlayerJoinPartyResponse data = ((InMessage)message["inMessage"]).GetData<PlayerJoinPartyResponse>();
		if (!data.success)
		{
			this.uiToastManager.ShowToast("playerJoinParty", "Failed to join party: " + data.errorData.message, 3f);
		}
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x0004B3A0 File Offset: 0x000495A0
	private void WebSocket_Event_OnPlayerStartMatchmakingResponse(Dictionary<string, object> message)
	{
		PlayerStartMatchmakingResponse data = ((InMessage)message["inMessage"]).GetData<PlayerStartMatchmakingResponse>();
		if (!data.success)
		{
			this.uiToastManager.ShowToast("playerStartMatchmaking", "Failed to start matchmaking: " + data.errorData.message, 3f);
		}
	}

	// Token: 0x04000863 RID: 2147
	private UIToastManager uiToastManager;
}
