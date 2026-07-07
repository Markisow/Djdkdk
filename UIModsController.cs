using System;
using System.Collections.Generic;

// Token: 0x020001A0 RID: 416
public class UIModsController : UIViewController<UIMods>
{
	// Token: 0x06000C3C RID: 3132 RVA: 0x00043C58 File Offset: 0x00041E58
	public override void Awake()
	{
		base.Awake();
		this.uiMods = base.GetComponent<UIMods>();
		EventManager.AddEventListener("Event_OnPluginAdded", new Action<Dictionary<string, object>>(this.Event_OnPluginAdded));
		EventManager.AddEventListener("Event_OnPluginStateChanged", new Action<Dictionary<string, object>>(this.Event_OnPluginStateChanged));
		EventManager.AddEventListener("Event_OnPluginEnableFailed", new Action<Dictionary<string, object>>(this.Event_OnPluginEnableFailed));
		EventManager.AddEventListener("Event_OnPluginDisableFailed", new Action<Dictionary<string, object>>(this.Event_OnPluginDisableFailed));
		EventManager.AddEventListener("Event_OnPluginRemoved", new Action<Dictionary<string, object>>(this.Event_OnPluginRemoved));
		EventManager.AddEventListener("Event_OnModAdded", new Action<Dictionary<string, object>>(this.Event_OnModAdded));
		EventManager.AddEventListener("Event_OnModStateChanged", new Action<Dictionary<string, object>>(this.Event_OnModStateChanged));
		EventManager.AddEventListener("Event_OnModSteamWorkshopItemStateChanged", new Action<Dictionary<string, object>>(this.Event_OnModSteamWorkshopItemStateChanged));
		EventManager.AddEventListener("Event_OnModSteamWorkshopItemDetailsStateChanged", new Action<Dictionary<string, object>>(this.Event_OnModSteamWorkshopItemDetailsStateChanged));
		EventManager.AddEventListener("Event_OnModEnableFailed", new Action<Dictionary<string, object>>(this.Event_OnModEnableFailed));
		EventManager.AddEventListener("Event_OnModDisableFailed", new Action<Dictionary<string, object>>(this.Event_OnModDisableFailed));
		EventManager.AddEventListener("Event_OnModRemoved", new Action<Dictionary<string, object>>(this.Event_OnModRemoved));
	}

	// Token: 0x06000C3D RID: 3133 RVA: 0x000120D2 File Offset: 0x000102D2
	private void Start()
	{
		ModManager.Plugins.ForEach(delegate(Plugin plugin)
		{
			this.uiMods.AddPlugin(plugin);
		});
		ModManager.Mods.ForEach(delegate(Mod mod)
		{
			this.uiMods.AddMod(mod);
		});
	}

	// Token: 0x06000C3E RID: 3134 RVA: 0x00043D80 File Offset: 0x00041F80
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnPluginAdded", new Action<Dictionary<string, object>>(this.Event_OnPluginAdded));
		EventManager.RemoveEventListener("Event_OnPluginStateChanged", new Action<Dictionary<string, object>>(this.Event_OnPluginStateChanged));
		EventManager.RemoveEventListener("Event_OnPluginEnableFailed", new Action<Dictionary<string, object>>(this.Event_OnPluginEnableFailed));
		EventManager.RemoveEventListener("Event_OnPluginDisableFailed", new Action<Dictionary<string, object>>(this.Event_OnPluginDisableFailed));
		EventManager.RemoveEventListener("Event_OnPluginRemoved", new Action<Dictionary<string, object>>(this.Event_OnPluginRemoved));
		EventManager.RemoveEventListener("Event_OnModAdded", new Action<Dictionary<string, object>>(this.Event_OnModAdded));
		EventManager.RemoveEventListener("Event_OnModStateChanged", new Action<Dictionary<string, object>>(this.Event_OnModStateChanged));
		EventManager.RemoveEventListener("Event_OnModSteamWorkshopItemStateChanged", new Action<Dictionary<string, object>>(this.Event_OnModSteamWorkshopItemStateChanged));
		EventManager.RemoveEventListener("Event_OnModSteamWorkshopItemDetailsStateChanged", new Action<Dictionary<string, object>>(this.Event_OnModSteamWorkshopItemDetailsStateChanged));
		EventManager.RemoveEventListener("Event_OnModEnableFailed", new Action<Dictionary<string, object>>(this.Event_OnModEnableFailed));
		EventManager.RemoveEventListener("Event_OnModDisableFailed", new Action<Dictionary<string, object>>(this.Event_OnModDisableFailed));
		EventManager.RemoveEventListener("Event_OnModRemoved", new Action<Dictionary<string, object>>(this.Event_OnModRemoved));
		base.OnDestroy();
	}

	// Token: 0x06000C3F RID: 3135 RVA: 0x00043E9C File Offset: 0x0004209C
	private void Event_OnPluginAdded(Dictionary<string, object> message)
	{
		Plugin plugin = (Plugin)message["plugin"];
		this.uiMods.AddPlugin(plugin);
	}

	// Token: 0x06000C40 RID: 3136 RVA: 0x00043EC8 File Offset: 0x000420C8
	private void Event_OnPluginStateChanged(Dictionary<string, object> message)
	{
		Plugin plugin = (Plugin)message["plugin"];
		this.uiMods.UpdatePlugin(plugin);
	}

	// Token: 0x06000C41 RID: 3137 RVA: 0x00043EC8 File Offset: 0x000420C8
	private void Event_OnPluginEnableFailed(Dictionary<string, object> message)
	{
		Plugin plugin = (Plugin)message["plugin"];
		this.uiMods.UpdatePlugin(plugin);
	}

	// Token: 0x06000C42 RID: 3138 RVA: 0x00043EC8 File Offset: 0x000420C8
	private void Event_OnPluginDisableFailed(Dictionary<string, object> message)
	{
		Plugin plugin = (Plugin)message["plugin"];
		this.uiMods.UpdatePlugin(plugin);
	}

	// Token: 0x06000C43 RID: 3139 RVA: 0x00043EF4 File Offset: 0x000420F4
	private void Event_OnPluginRemoved(Dictionary<string, object> message)
	{
		Plugin plugin = (Plugin)message["plugin"];
		this.uiMods.RemovePlugin(plugin);
	}

	// Token: 0x06000C44 RID: 3140 RVA: 0x00043F20 File Offset: 0x00042120
	private void Event_OnModAdded(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		this.uiMods.AddMod(mod);
	}

	// Token: 0x06000C45 RID: 3141 RVA: 0x00043F4C File Offset: 0x0004214C
	private void Event_OnModStateChanged(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		this.uiMods.UpdateMod(mod);
	}

	// Token: 0x06000C46 RID: 3142 RVA: 0x00043F4C File Offset: 0x0004214C
	private void Event_OnModSteamWorkshopItemStateChanged(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		this.uiMods.UpdateMod(mod);
	}

	// Token: 0x06000C47 RID: 3143 RVA: 0x00043F4C File Offset: 0x0004214C
	private void Event_OnModSteamWorkshopItemDetailsStateChanged(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		this.uiMods.UpdateMod(mod);
	}

	// Token: 0x06000C48 RID: 3144 RVA: 0x00043F4C File Offset: 0x0004214C
	private void Event_OnModEnableFailed(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		this.uiMods.UpdateMod(mod);
	}

	// Token: 0x06000C49 RID: 3145 RVA: 0x00043F4C File Offset: 0x0004214C
	private void Event_OnModDisableFailed(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		this.uiMods.UpdateMod(mod);
	}

	// Token: 0x06000C4A RID: 3146 RVA: 0x00043F78 File Offset: 0x00042178
	private void Event_OnModRemoved(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		this.uiMods.RemoveMod(mod);
	}

	// Token: 0x0400074F RID: 1871
	private UIMods uiMods;
}
