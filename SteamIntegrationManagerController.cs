using System;
using System.Collections.Generic;
using Steamworks;

// Token: 0x0200013A RID: 314
public static class SteamIntegrationManagerController
{
	// Token: 0x06000955 RID: 2389 RVA: 0x00038B8C File Offset: 0x00036D8C
	public static void Initialize()
	{
		EventManager.AddEventListener("Event_Everyone_OnGamePhaseChanged", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_Everyone_OnGamePhaseChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.AddEventListener("Event_Everyone_OnServerChanged", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_Everyone_OnServerChanged));
		EventManager.AddEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnClientStopped));
		EventManager.AddEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnPlayerDataChanged));
		EventManager.AddEventListener("Event_OnPlayerPartyDataChanged", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnPlayerPartyDataChanged));
		EventManager.AddEventListener("Event_OnScoreboardClickPlayer", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnScoreboardClickPlayer));
		EventManager.AddEventListener("Event_OnModsClickFindMods", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnModsClickFindMods));
		EventManager.AddEventListener("Event_OnModPreviewLinkClicked", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnModPreviewLinkClicked));
		EventManager.AddEventListener("Event_OnFriendInviteButtonClicked", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnFriendInviteButtonClicked));
		EventManager.AddEventListener("Event_OnGameLobbyJoinRequested", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnGameLobbyJoinRequested));
		EventManager.AddEventListener("Event_OnAppearanceClickPurchaseItem", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnAppearanceClickPurchaseItem));
		EventManager.AddEventListener("Event_OnMicroTxnAuthorizationResponse", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnMicroTxnAuthorizationResponse));
		WebSocketManager.AddMessageListener("connected", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.WebSocket_Event_OnConnected));
		WebSocketManager.AddMessageListener("playerAuthenticateResponse", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.WebSocket_Event_OnPlayerAuthenticateResponse));
		WebSocketManager.AddMessageListener("playerJoinPartyResponse", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.WebSocket_Event_OnPlayerJoinPartyResponse));
	}

	// Token: 0x06000956 RID: 2390 RVA: 0x00038CFC File Offset: 0x00036EFC
	public static void Dispose()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnGamePhaseChanged", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_Everyone_OnGamePhaseChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnServerChanged", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_Everyone_OnServerChanged));
		EventManager.RemoveEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnClientStopped));
		EventManager.RemoveEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnPlayerDataChanged));
		EventManager.RemoveEventListener("Event_OnPlayerPartyDataChanged", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnPlayerPartyDataChanged));
		EventManager.RemoveEventListener("Event_OnScoreboardClickPlayer", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnScoreboardClickPlayer));
		EventManager.RemoveEventListener("Event_OnModsClickFindMods", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnModsClickFindMods));
		EventManager.RemoveEventListener("Event_OnModPreviewLinkClicked", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnModPreviewLinkClicked));
		EventManager.RemoveEventListener("Event_OnFriendInviteButtonClicked", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnFriendInviteButtonClicked));
		EventManager.RemoveEventListener("Event_OnGameLobbyJoinRequested", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnGameLobbyJoinRequested));
		EventManager.RemoveEventListener("Event_OnAppearanceClickPurchaseItem", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnAppearanceClickPurchaseItem));
		EventManager.RemoveEventListener("Event_OnMicroTxnAuthorizationResponse", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.Event_OnMicroTxnAuthorizationResponse));
		WebSocketManager.RemoveMessageListener("connected", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.WebSocket_Event_OnConnected));
		WebSocketManager.RemoveMessageListener("playerAuthenticateResponse", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.WebSocket_Event_OnPlayerAuthenticateResponse));
		WebSocketManager.RemoveMessageListener("playerJoinPartyResponse", new Action<Dictionary<string, object>>(SteamIntegrationManagerController.WebSocket_Event_OnPlayerJoinPartyResponse));
	}

	// Token: 0x06000957 RID: 2391 RVA: 0x00038E6C File Offset: 0x0003706C
	private static void Event_Everyone_OnGamePhaseChanged(Dictionary<string, object> message)
	{
		GameState gameState = (GameState)message["gameState"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		SteamIntegrationManager.UpdateRichPresencePhase(gameState.Phase);
		SteamIntegrationManager.UpdateRichPresenceScore(gameState.Phase != GamePhase.Warmup, gameState.Period, gameState.BlueScore, gameState.RedScore);
	}

	// Token: 0x06000958 RID: 2392 RVA: 0x00038EC0 File Offset: 0x000370C0
	private static void Event_Everyone_OnPlayerGameStateChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		Server value = NetworkBehaviourSingleton<ServerManager>.Instance.Server.Value;
		GameState value2 = NetworkBehaviourSingleton<GameManager>.Instance.GameState.Value;
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		if (!player.IsLocalPlayer)
		{
			return;
		}
		PlayerTeam team = player.Team;
		if (team - PlayerTeam.Blue <= 1)
		{
			SteamIntegrationManager.UpdateRichPresencePhase(value2.Phase);
			SteamIntegrationManager.UpdateRichPresenceTeam(player.Team);
			SteamIntegrationManager.UpdateRichPresenceRole(player.Role);
			SteamIntegrationManager.UpdateRichPresenceScore(value2.Phase != GamePhase.Warmup, value2.Period, value2.BlueScore, value2.RedScore);
			SteamIntegrationManager.SetRichPresencePlaying(value, MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayers(false).Count);
			return;
		}
		SteamIntegrationManager.UpdateRichPresenceScore(value2.Phase != GamePhase.Warmup, value2.Period, value2.BlueScore, value2.RedScore);
		SteamIntegrationManager.SetRichPresenceSpectating(value, MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayers(false).Count);
	}

	// Token: 0x06000959 RID: 2393 RVA: 0x0000FD55 File Offset: 0x0000DF55
	private static void Event_Everyone_OnServerChanged(Dictionary<string, object> message)
	{
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		SteamIntegrationManager.UpdateRichPresenceScore(false, 0, 0, 0);
		SteamIntegrationManager.SetRichPresenceSpectating(NetworkBehaviourSingleton<ServerManager>.Instance.Server.Value, 1);
	}

	// Token: 0x0600095A RID: 2394 RVA: 0x0000FD7D File Offset: 0x0000DF7D
	private static void Event_OnClientStopped(Dictionary<string, object> message)
	{
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		SteamIntegrationManager.SetRichPresenceMainMenu();
	}

	// Token: 0x0600095B RID: 2395 RVA: 0x00038FB0 File Offset: 0x000371B0
	private static void Event_OnPlayerDataChanged(Dictionary<string, object> message)
	{
		bool flag = (PlayerData)message["oldPlayerData"] != null;
		PlayerData playerData = (PlayerData)message["newPlayerData"];
		if (!flag && playerData != null)
		{
			SteamIntegrationManager.GetLaunchCommandLine();
		}
	}

	// Token: 0x0600095C RID: 2396 RVA: 0x00038FE8 File Offset: 0x000371E8
	private static void Event_OnPlayerPartyDataChanged(Dictionary<string, object> message)
	{
		PlayerPartyData playerPartyData = (PlayerPartyData)message["oldPlayerPartyData"];
		PlayerPartyData playerPartyData2 = (PlayerPartyData)message["newPlayerPartyData"];
		bool flag = playerPartyData != null;
		bool flag2 = playerPartyData2 != null;
		bool flag3 = flag && playerPartyData.steamLobbyId != null;
		bool flag4 = flag2 && playerPartyData2.steamLobbyId != null;
		bool flag5 = flag2 && playerPartyData2.ownerSteamId == BackendManager.PlayerState.PlayerData.steamId;
		bool flag6 = ((playerPartyData != null) ? playerPartyData.steamLobbyId : null) != ((playerPartyData2 != null) ? playerPartyData2.steamLobbyId : null);
		bool flag7 = flag4 && flag6;
		bool flag8 = flag3 && flag6;
		bool flag9 = flag2 && !flag4 && flag5;
		if (flag8)
		{
			SteamIntegrationManager.LeaveLobby(playerPartyData.steamLobbyId);
		}
		if (flag9)
		{
			SteamIntegrationManager.CreateLobby();
		}
		if (flag7)
		{
			SteamIntegrationManager.JoinLobby(playerPartyData2.steamLobbyId);
		}
	}

	// Token: 0x0600095D RID: 2397 RVA: 0x000390C0 File Offset: 0x000372C0
	private static void Event_OnScoreboardClickPlayer(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		SteamFriends.ActivateGameOverlayToUser("steamID", new CSteamID(ulong.Parse(player.SteamId.Value.ToString())));
	}

	// Token: 0x0600095E RID: 2398 RVA: 0x0000FD8C File Offset: 0x0000DF8C
	private static void Event_OnModsClickFindMods(Dictionary<string, object> message)
	{
		SteamFriends.ActivateGameOverlayToWebPage("https://steamcommunity.com/app/2994020/workshop/", EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
	}

	// Token: 0x0600095F RID: 2399 RVA: 0x0003910C File Offset: 0x0003730C
	private static void Event_OnModPreviewLinkClicked(Dictionary<string, object> message)
	{
		string str = (string)message["id"];
		SteamFriends.ActivateGameOverlayToWebPage("https://steamcommunity.com/sharedfiles/filedetails/?id=" + str, EActivateGameOverlayToWebPageMode.k_EActivateGameOverlayToWebPageMode_Default);
	}

	// Token: 0x06000960 RID: 2400 RVA: 0x0003913C File Offset: 0x0003733C
	private static void Event_OnFriendInviteButtonClicked(Dictionary<string, object> message)
	{
		string invitedSteamId = (string)message["steamId"];
		PlayerPartyData partyData = BackendManager.PlayerState.PartyData;
		if (partyData == null)
		{
			return;
		}
		if (partyData.steamLobbyId != null)
		{
			SteamIntegrationManager.InviteToLobby(BackendManager.PlayerState.PartyData.steamLobbyId, invitedSteamId);
		}
	}

	// Token: 0x06000961 RID: 2401 RVA: 0x0000FD99 File Offset: 0x0000DF99
	private static void Event_OnGameLobbyJoinRequested(Dictionary<string, object> message)
	{
		SteamIntegrationManager.JoinLobby((string)message["lobbyId"]);
	}

	// Token: 0x06000962 RID: 2402 RVA: 0x00039188 File Offset: 0x00037388
	private static void Event_OnAppearanceClickPurchaseItem(Dictionary<string, object> message)
	{
		Item item = (Item)message["item"];
		EventManager.TriggerEvent("Event_OnTransactionStarting", new Dictionary<string, object>
		{
			{
				"itemId",
				item.id
			}
		});
		if (!SteamUtils.IsOverlayEnabled())
		{
			EventManager.TriggerEvent("Event_OnTransactionStartFailed", new Dictionary<string, object>
			{
				{
					"error",
					"Steam overlay disabled"
				}
			});
			return;
		}
		WebSocketManager.Emit("playerStartPurchaseRequest", new Dictionary<string, object>
		{
			{
				"itemId",
				item.id
			}
		}, "playerStartPurchaseResponse");
	}

	// Token: 0x06000963 RID: 2403 RVA: 0x0003921C File Offset: 0x0003741C
	private static void Event_OnMicroTxnAuthorizationResponse(Dictionary<string, object> message)
	{
		bool flag = (bool)message["authorized"];
		ulong num = (ulong)message["orderId"];
		if (flag)
		{
			WebSocketManager.Emit("playerCompletePurchaseRequest", new Dictionary<string, object>
			{
				{
					"orderId",
					num
				}
			}, "playerCompletePurchaseResponse");
		}
	}

	// Token: 0x06000964 RID: 2404 RVA: 0x0000FDB0 File Offset: 0x0000DFB0
	private static void WebSocket_Event_OnConnected(Dictionary<string, object> message)
	{
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		SteamIntegrationManager.GetTicketForWebApi();
	}

	// Token: 0x06000965 RID: 2405 RVA: 0x0000FDBF File Offset: 0x0000DFBF
	private static void WebSocket_Event_OnPlayerAuthenticateResponse(Dictionary<string, object> message)
	{
		if (((InMessage)message["inMessage"]).GetData<PlayerAuthenticateResponse>().success)
		{
			SteamIntegrationManager.SetRichPresenceMainMenu();
		}
	}

	// Token: 0x06000966 RID: 2406 RVA: 0x00039274 File Offset: 0x00037474
	private static void WebSocket_Event_OnPlayerJoinPartyResponse(Dictionary<string, object> message)
	{
		OutMessage outMessage = (OutMessage)message["outMessage"];
		Response<object, BaseErrorResponseData> data = ((InMessage)message["inMessage"]).GetData<PlayerJoinPartyResponse>();
		string text = (string)outMessage.Data["steamLobbyId"];
		if (!data.success && text != null)
		{
			SteamIntegrationManager.LeaveLobby(text);
		}
	}
}
