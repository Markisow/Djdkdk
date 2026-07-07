using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

// Token: 0x020000CE RID: 206
public static class ModManager
{
	// Token: 0x170000A7 RID: 167
	// (get) Token: 0x06000647 RID: 1607 RVA: 0x0000D001 File Offset: 0x0000B201
	public static Plugin[] ReadyPlugins
	{
		get
		{
			return (from plugin in ModManager.Plugins
			where plugin.IsReady
			select plugin).ToArray<Plugin>();
		}
	}

	// Token: 0x170000A8 RID: 168
	// (get) Token: 0x06000648 RID: 1608 RVA: 0x0000D031 File Offset: 0x0000B231
	public static Plugin[] EnabledPlugins
	{
		get
		{
			return (from plugin in ModManager.Plugins
			where plugin.IsEnabled
			select plugin).ToArray<Plugin>();
		}
	}

	// Token: 0x170000A9 RID: 169
	// (get) Token: 0x06000649 RID: 1609 RVA: 0x0000D061 File Offset: 0x0000B261
	public static Mod[] ReadyMods
	{
		get
		{
			return (from mod in ModManager.Mods
			where mod.IsReady
			select mod).ToArray<Mod>();
		}
	}

	// Token: 0x170000AA RID: 170
	// (get) Token: 0x0600064A RID: 1610 RVA: 0x0000D091 File Offset: 0x0000B291
	public static Mod[] EnabledMods
	{
		get
		{
			return (from mod in ModManager.Mods
			where mod.IsEnabled
			select mod).ToArray<Mod>();
		}
	}

	// Token: 0x170000AB RID: 171
	// (get) Token: 0x0600064B RID: 1611 RVA: 0x0000D0C1 File Offset: 0x0000B2C1
	private static string pluginsDirectoryPath
	{
		get
		{
			return Path.Combine(Path.GetFullPath("."), "Plugins");
		}
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0000D0D7 File Offset: 0x0000B2D7
	public static void Initialize()
	{
		ModManagerController.Initialize();
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0000D0DE File Offset: 0x0000B2DE
	public static void Dispose()
	{
		ModManagerController.Dispose();
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0002FF9C File Offset: 0x0002E19C
	public static void LoadModsStatus()
	{
		try
		{
			string @string = SaveManager.GetString("modsStatus", null);
			if (string.IsNullOrEmpty(@string))
			{
				throw new Exception("No mods status found");
			}
			ModManager.ModsStatus = JsonSerializer.Deserialize<Dictionary<string, bool>>(@string, null);
		}
		catch (Exception ex)
		{
			ModManager.Logger.Error("Failed to load mods status: " + ex.Message);
			ModManager.SaveModsStatus();
			ModManager.LoadModsStatus();
		}
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0003000C File Offset: 0x0002E20C
	public static bool GetModStatus(string id)
	{
		bool flag;
		return ModManager.ModsStatus.TryGetValue(id, out flag) && flag;
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0000D0E5 File Offset: 0x0000B2E5
	public static void SetModStatus(string id, bool isEnabled)
	{
		ModManager.ModsStatus[id] = isEnabled;
		ModManager.SaveModsStatus();
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0003002C File Offset: 0x0002E22C
	private static void SaveModsStatus()
	{
		try
		{
			string value = JsonSerializer.Serialize<Dictionary<string, bool>>(ModManager.ModsStatus, null);
			SaveManager.SetString("modsStatus", value);
		}
		catch (Exception ex)
		{
			ModManager.Logger.Error("Failed to save mods status: " + ex.Message);
		}
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x00030080 File Offset: 0x0002E280
	public static void ApplyModStatus()
	{
		foreach (Mod mod in ModManager.Mods)
		{
			if (ModManager.GetModStatus(mod.Id))
			{
				mod.Enable();
			}
			if (!ModManager.GetModStatus(mod.Id))
			{
				mod.Disable();
			}
		}
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x000300F4 File Offset: 0x0002E2F4
	public static void LoadPlugins()
	{
		ModManager.Logger.Info("Loading plugins from " + ModManager.pluginsDirectoryPath);
		if (!Directory.Exists(ModManager.pluginsDirectoryPath))
		{
			Directory.CreateDirectory(ModManager.pluginsDirectoryPath);
		}
		foreach (string path in Directory.GetDirectories(ModManager.pluginsDirectoryPath))
		{
			ModManager.AddPlugin(Path.GetFileName(path), path);
		}
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0003015C File Offset: 0x0002E35C
	public static Plugin AddPlugin(string id, string path)
	{
		ModManager.Logger.Info("Adding plugin " + id);
		if (ModManager.GetPluginById(id) != null)
		{
			return null;
		}
		Plugin plugin = new Plugin(id, path);
		ModManager.Plugins.Add(plugin);
		EventManager.TriggerEvent("Event_OnPluginAdded", new Dictionary<string, object>
		{
			{
				"plugin",
				plugin
			}
		});
		plugin.Initialize();
		return plugin;
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x000301C0 File Offset: 0x0002E3C0
	public static Plugin RemovePlugin(string id)
	{
		ModManager.Logger.Info("Removing plugin " + id);
		Plugin pluginById = ModManager.GetPluginById(id);
		if (pluginById == null)
		{
			return null;
		}
		ModManager.Plugins.Remove(pluginById);
		EventManager.TriggerEvent("Event_OnPluginRemoved", new Dictionary<string, object>
		{
			{
				"plugin",
				pluginById
			}
		});
		pluginById.Dispose();
		return pluginById;
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x0003021C File Offset: 0x0002E41C
	public static Mod AddMod(SteamWorkshopItem item)
	{
		ModManager.Logger.Info("Adding mod " + item.Id);
		if (ModManager.GetModById(item.Id) != null)
		{
			return null;
		}
		Mod mod = new Mod(item);
		ModManager.Mods.Add(mod);
		EventManager.TriggerEvent("Event_OnModAdded", new Dictionary<string, object>
		{
			{
				"mod",
				mod
			}
		});
		mod.Initialize();
		return mod;
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00030288 File Offset: 0x0002E488
	public static Mod RemoveMod(string id)
	{
		ModManager.Logger.Info("Removing mod " + id);
		Mod modById = ModManager.GetModById(id);
		if (modById == null)
		{
			return null;
		}
		ModManager.Mods.Remove(modById);
		EventManager.TriggerEvent("Event_OnModRemoved", new Dictionary<string, object>
		{
			{
				"mod",
				modById
			}
		});
		modById.Dispose();
		return modById;
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x000302E4 File Offset: 0x0002E4E4
	public static Plugin GetPluginById(string id)
	{
		return ModManager.Plugins.Find((Plugin plugin) => plugin.Id == id);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x00030314 File Offset: 0x0002E514
	public static Mod GetModById(string id)
	{
		return ModManager.Mods.Find((Mod mod) => mod.Id == id);
	}

	// Token: 0x040003E9 RID: 1001
	private static readonly Logger Logger = new Logger("ModManager");

	// Token: 0x040003EA RID: 1002
	public static List<Plugin> Plugins = new List<Plugin>();

	// Token: 0x040003EB RID: 1003
	public static List<Mod> Mods = new List<Mod>();

	// Token: 0x040003EC RID: 1004
	public static Dictionary<string, bool> ModsStatus = new Dictionary<string, bool>();
}
