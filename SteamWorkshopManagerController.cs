using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000147 RID: 327
public static class SteamWorkshopManagerController
{
	// Token: 0x060009D2 RID: 2514 RVA: 0x0003A598 File Offset: 0x00038798
	public static void Initialize()
	{
		EventManager.AddEventListener("Event_OnSteamConnected", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_OnSteamConnected));
		EventManager.AddEventListener("Event_OnSteamWorkshopSubscribedItemsListChanged", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_OnSteamWorkshopSubscribedItemsListChanged));
		EventManager.AddEventListener("Event_OnSteamWorkshopItemDownloaded", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_OnSteamWorkshopItemDownloaded));
		EventManager.AddEventListener("Event_OnModsClickRefresh", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_OnModsClickRefresh));
		EventManager.AddEventListener("Event_OnReconnectionStateChanged", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_OnReconnectionStateChanged));
		EventManager.AddEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_Server_OnServerStarted));
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0003A62C File Offset: 0x0003882C
	public static void Dispose()
	{
		EventManager.RemoveEventListener("Event_OnSteamConnected", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_OnSteamConnected));
		EventManager.RemoveEventListener("Event_OnSteamWorkshopSubscribedItemsListChanged", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_OnSteamWorkshopSubscribedItemsListChanged));
		EventManager.RemoveEventListener("Event_OnSteamWorkshopItemDownloaded", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_OnSteamWorkshopItemDownloaded));
		EventManager.RemoveEventListener("Event_OnModsClickRefresh", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_OnModsClickRefresh));
		EventManager.RemoveEventListener("Event_OnReconnectionStateChanged", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_OnReconnectionStateChanged));
		EventManager.RemoveEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(SteamWorkshopManagerController.Event_Server_OnServerStarted));
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x000103FA File Offset: 0x0000E5FA
	private static void Event_OnSteamConnected(Dictionary<string, object> message)
	{
		if (!ApplicationManager.IsDedicatedGameServer)
		{
			SteamWorkshopManager.VerifyIntegrity();
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x00010408 File Offset: 0x0000E608
	private static void Event_OnSteamWorkshopSubscribedItemsListChanged(Dictionary<string, object> message)
	{
		SteamWorkshopManager.VerifyIntegrity();
	}

	// Token: 0x060009D6 RID: 2518 RVA: 0x0001040F File Offset: 0x0000E60F
	private static void Event_OnSteamWorkshopItemDownloaded(Dictionary<string, object> message)
	{
		SteamWorkshopManager.VerifyItemIntegrity((string)message["itemId"]);
	}

	// Token: 0x060009D7 RID: 2519 RVA: 0x00010408 File Offset: 0x0000E608
	private static void Event_OnModsClickRefresh(Dictionary<string, object> message)
	{
		SteamWorkshopManager.VerifyIntegrity();
	}

	// Token: 0x060009D8 RID: 2520 RVA: 0x0003A6C0 File Offset: 0x000388C0
	private static void Event_OnReconnectionStateChanged(Dictionary<string, object> message)
	{
		ReconnectionState reconnectionState = (ReconnectionState)message["oldReconnectionState"];
		ReconnectionState reconnectionState2 = (ReconnectionState)message["newReconnectionState"];
		if (reconnectionState2.Phase == ReconnectionPhase.AwaitingMods && !reconnectionState.PendingReadinessModIds.SequenceEqual(reconnectionState2.PendingReadinessModIds))
		{
			foreach (string text in reconnectionState2.PendingReadinessModIds.Except(reconnectionState.PendingReadinessModIds).ToArray<string>())
			{
				if (ModManager.GetModById(text) == null)
				{
					SteamWorkshopManager.SubscribeItem(text);
				}
			}
		}
	}

	// Token: 0x060009D9 RID: 2521 RVA: 0x0003A748 File Offset: 0x00038948
	private static void Event_Server_OnServerStarted(Dictionary<string, object> message)
	{
		ServerConfig serverConfig = (ServerConfig)message["serverConfig"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			string[] enabledModIds = serverConfig.EnabledModIds;
			for (int i = 0; i < enabledModIds.Length; i++)
			{
				SteamWorkshopManager.DownloadItem(enabledModIds[i]);
			}
		}
	}

	// Token: 0x040005B7 RID: 1463
	private static readonly Logger Logger = new Logger("SteamWorkshopManagerController");
}
