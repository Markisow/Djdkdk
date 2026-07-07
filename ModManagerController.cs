using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x020000D2 RID: 210
public static class ModManagerController
{
	// Token: 0x06000665 RID: 1637 RVA: 0x00030344 File Offset: 0x0002E544
	public static void Initialize()
	{
		EventManager.AddEventListener("Event_OnSteamWorkshopItemAdded", new Action<Dictionary<string, object>>(ModManagerController.Event_OnSteamWorkshopItemAdded));
		EventManager.AddEventListener("Event_OnSteamWorkshopItemRemoved", new Action<Dictionary<string, object>>(ModManagerController.Event_OnSteamWorkshopItemRemoved));
		EventManager.AddEventListener("Event_OnModsPluginEnabled", new Action<Dictionary<string, object>>(ModManagerController.Event_OnModsPluginEnabled));
		EventManager.AddEventListener("Event_OnModsPluginDisabled", new Action<Dictionary<string, object>>(ModManagerController.Event_OnModsPluginDisabled));
		EventManager.AddEventListener("Event_OnModsModEnabled", new Action<Dictionary<string, object>>(ModManagerController.Event_OnModsModEnabled));
		EventManager.AddEventListener("Event_OnModsModDisabled", new Action<Dictionary<string, object>>(ModManagerController.Event_OnModsModDisabled));
		EventManager.AddEventListener("Event_OnModStateChanged", new Action<Dictionary<string, object>>(ModManagerController.Event_OnModStateChanged));
		EventManager.AddEventListener("Event_OnPluginStateChanged", new Action<Dictionary<string, object>>(ModManagerController.Event_OnPluginStateChanged));
		EventManager.AddEventListener("Event_OnConnectionStateChanged", new Action<Dictionary<string, object>>(ModManagerController.Event_OnConnectionStateChanged));
		EventManager.AddEventListener("Event_OnReconnectionStateChanged", new Action<Dictionary<string, object>>(ModManagerController.Event_OnReconnectionStateChanged));
		if (!ApplicationManager.IsDedicatedGameServer)
		{
			ModManager.LoadModsStatus();
		}
		ModManager.LoadPlugins();
		SteamWorkshopManager.Items.ForEach(delegate(SteamWorkshopItem item)
		{
			ModManager.AddMod(item);
		});
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x00030468 File Offset: 0x0002E668
	public static void Dispose()
	{
		EventManager.RemoveEventListener("Event_OnSteamWorkshopItemAdded", new Action<Dictionary<string, object>>(ModManagerController.Event_OnSteamWorkshopItemAdded));
		EventManager.RemoveEventListener("Event_OnSteamWorkshopItemRemoved", new Action<Dictionary<string, object>>(ModManagerController.Event_OnSteamWorkshopItemRemoved));
		EventManager.RemoveEventListener("Event_OnModsPluginEnabled", new Action<Dictionary<string, object>>(ModManagerController.Event_OnModsPluginEnabled));
		EventManager.RemoveEventListener("Event_OnModsPluginDisabled", new Action<Dictionary<string, object>>(ModManagerController.Event_OnModsPluginDisabled));
		EventManager.RemoveEventListener("Event_OnModsModEnabled", new Action<Dictionary<string, object>>(ModManagerController.Event_OnModsModEnabled));
		EventManager.RemoveEventListener("Event_OnModsModDisabled", new Action<Dictionary<string, object>>(ModManagerController.Event_OnModsModDisabled));
		EventManager.RemoveEventListener("Event_OnModStateChanged", new Action<Dictionary<string, object>>(ModManagerController.Event_OnModStateChanged));
		EventManager.RemoveEventListener("Event_OnPluginStateChanged", new Action<Dictionary<string, object>>(ModManagerController.Event_OnPluginStateChanged));
		EventManager.RemoveEventListener("Event_OnConnectionStateChanged", new Action<Dictionary<string, object>>(ModManagerController.Event_OnConnectionStateChanged));
		EventManager.RemoveEventListener("Event_OnReconnectionStateChanged", new Action<Dictionary<string, object>>(ModManagerController.Event_OnReconnectionStateChanged));
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0000D169 File Offset: 0x0000B369
	private static void Event_OnSteamWorkshopItemAdded(Dictionary<string, object> message)
	{
		ModManager.AddMod((SteamWorkshopItem)message["item"]);
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0000D181 File Offset: 0x0000B381
	private static void Event_OnSteamWorkshopItemRemoved(Dictionary<string, object> message)
	{
		ModManager.RemoveMod(((SteamWorkshopItem)message["item"]).Id);
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x00030554 File Offset: 0x0002E754
	private static void Event_OnModsPluginEnabled(Dictionary<string, object> message)
	{
		Plugin plugin = (Plugin)message["plugin"];
		Plugin pluginById = ModManager.GetPluginById(plugin.Id);
		bool? flag = (pluginById != null) ? new bool?(pluginById.Enable()) : null;
		if (flag != null && flag.Value)
		{
			ModManager.SetModStatus(plugin.Id, true);
		}
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x000305B8 File Offset: 0x0002E7B8
	private static void Event_OnModsPluginDisabled(Dictionary<string, object> message)
	{
		Plugin plugin = (Plugin)message["plugin"];
		Plugin pluginById = ModManager.GetPluginById(plugin.Id);
		bool? flag = (pluginById != null) ? new bool?(pluginById.Disable()) : null;
		if (flag != null && flag.Value)
		{
			ModManager.SetModStatus(plugin.Id, false);
		}
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0003061C File Offset: 0x0002E81C
	private static void Event_OnModsModEnabled(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		Mod modById = ModManager.GetModById(mod.Id);
		bool? flag = (modById != null) ? new bool?(modById.Enable()) : null;
		if (flag != null && flag.Value)
		{
			ModManager.SetModStatus(mod.Id, true);
		}
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x00030680 File Offset: 0x0002E880
	private static void Event_OnModsModDisabled(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		Mod modById = ModManager.GetModById(mod.Id);
		bool? flag = (modById != null) ? new bool?(modById.Disable()) : null;
		if (flag != null && flag.Value)
		{
			ModManager.SetModStatus(mod.Id, false);
		}
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x000306E4 File Offset: 0x0002E8E4
	private static void Event_OnModStateChanged(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		BasePluginState basePluginState = (BasePluginState)message["oldState"];
		BasePluginState basePluginState2 = (BasePluginState)message["newState"];
		if (basePluginState.IsReady != basePluginState2.IsReady && basePluginState2.IsReady)
		{
			if (ApplicationManager.IsDedicatedGameServer)
			{
				mod.Enable();
				return;
			}
			if (ModManager.GetModStatus(mod.Id) && !GlobalStateManager.ReconnectionState.IsPendingModId(mod.Id))
			{
				mod.Enable();
			}
		}
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x00030778 File Offset: 0x0002E978
	private static void Event_OnPluginStateChanged(Dictionary<string, object> message)
	{
		Plugin plugin = (Plugin)message["plugin"];
		BasePluginState basePluginState = (BasePluginState)message["oldState"];
		BasePluginState basePluginState2 = (BasePluginState)message["newState"];
		if (basePluginState.IsReady != basePluginState2.IsReady && basePluginState2.IsReady)
		{
			if (ApplicationManager.IsDedicatedGameServer)
			{
				plugin.Enable();
				return;
			}
			if (ModManager.GetModStatus(plugin.Id))
			{
				plugin.Enable();
			}
		}
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x000307F4 File Offset: 0x0002E9F4
	private static void Event_OnConnectionStateChanged(Dictionary<string, object> message)
	{
		ConnectionState connectionState = (ConnectionState)message["newConnectionState"];
		if (((ConnectionState)message["oldConnectionState"]).Phase != connectionState.Phase && connectionState.Phase == ConnectionPhase.Disconnected)
		{
			ModManager.ApplyModStatus();
		}
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x00030844 File Offset: 0x0002EA44
	private static void Event_OnReconnectionStateChanged(Dictionary<string, object> message)
	{
		ReconnectionState reconnectionState = (ReconnectionState)message["newReconnectionState"];
		ReconnectionState reconnectionState2 = (ReconnectionState)message["oldReconnectionState"];
		ReconnectionPhase phase = reconnectionState.Phase;
		if (phase != ReconnectionPhase.None)
		{
			if (phase != ReconnectionPhase.AwaitingMods)
			{
				return;
			}
			if (!reconnectionState2.PendingEnablingModIds.SequenceEqual(reconnectionState.PendingEnablingModIds) && reconnectionState.PendingReadinessModIds.Length == 0)
			{
				Mod modById = ModManager.GetModById(reconnectionState.PendingEnablingModIds.FirstOrDefault<string>());
				if (modById != null)
				{
					modById.Enable();
				}
			}
		}
		else if (reconnectionState2.Phase == ReconnectionPhase.AwaitingMods && reconnectionState2.PendingModIds.Length != 0)
		{
			ModManager.ApplyModStatus();
			return;
		}
	}

	// Token: 0x040003F4 RID: 1012
	private static readonly Logger Logger = new Logger("ModManagerController");
}
