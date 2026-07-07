using System;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x0200019D RID: 413
public class UIMods : UIView
{
	// Token: 0x06000C24 RID: 3108 RVA: 0x000435BC File Offset: 0x000417BC
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("ModsView", null);
		this.mods = base.View.Query("Mods", null);
		this.modsList = this.mods.Query("ModsList", null);
		this.noMods = this.mods.Query("NoMods", null);
		this.closeIconButton = this.mods.Query("CloseIconButtonContainer", null).First().Query(null, null);
		this.closeIconButton.clicked += this.OnClickClose;
		this.findModsButton = this.mods.Query("FindModsButton", null);
		this.findModsButton.clicked += this.OnClickFindMods;
		this.refreshButton = this.mods.Query("RefreshButton", null);
		this.refreshButton.clicked += this.OnClickRefresh;
		this.modsList.Clear();
	}

	// Token: 0x06000C25 RID: 3109 RVA: 0x00011F50 File Offset: 0x00010150
	public override bool Show()
	{
		bool flag = base.Show();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnModsShow", null);
		}
		return flag;
	}

	// Token: 0x06000C26 RID: 3110 RVA: 0x000436E8 File Offset: 0x000418E8
	public void AddPlugin(Plugin plugin)
	{
		if (this.pluginTemplateContainerMap.ContainsKey(plugin))
		{
			return;
		}
		TemplateContainer templateContainer = this.modAsset.Instantiate();
		UI.Mod uiMod = templateContainer.Query(null, null);
		UI.Mod uiMod2 = uiMod;
		EventCallback<ChangeEvent<bool>> <>9__1;
		uiMod2.Ready = (Action)Delegate.Combine(uiMod2.Ready, new Action(delegate()
		{
			uiMod.Toggle.value = plugin.IsEnabled;
			INotifyValueChanged<bool> toggle = uiMod.Toggle;
			EventCallback<ChangeEvent<bool>> callback;
			if ((callback = <>9__1) == null)
			{
				callback = (<>9__1 = delegate(ChangeEvent<bool> e)
				{
					this.OnPluginToggleChanged(plugin, e.newValue);
				});
			}
			toggle.RegisterValueChangedCallback(callback);
			this.UpdatePlugin(plugin);
		}));
		this.pluginTemplateContainerMap.Add(plugin, templateContainer);
		this.modsList.Add(templateContainer);
		this.UpdateNoMods();
	}

	// Token: 0x06000C27 RID: 3111 RVA: 0x00043788 File Offset: 0x00041988
	public void UpdatePlugin(Plugin plugin)
	{
		if (!this.pluginTemplateContainerMap.ContainsKey(plugin))
		{
			return;
		}
		UI.Mod mod = this.pluginTemplateContainerMap[plugin].Query(null, null);
		mod.Description = string.Empty;
		mod.PreviewTexture = this.defaultPreviewTexture;
		mod.Toggle.enabledSelf = plugin.HasAssembly;
		mod.Toggle.value = plugin.IsEnabled;
		mod.ModPreview.Link.enabledSelf = false;
		mod.ModPreview.Link.Text = plugin.Id;
		mod.ModPreview.IsStatisticsVisible = false;
	}

	// Token: 0x06000C28 RID: 3112 RVA: 0x00043828 File Offset: 0x00041A28
	public void RemovePlugin(Plugin plugin)
	{
		if (!this.pluginTemplateContainerMap.ContainsKey(plugin))
		{
			return;
		}
		TemplateContainer element = this.pluginTemplateContainerMap[plugin];
		this.modsList.Remove(element);
		this.pluginTemplateContainerMap.Remove(plugin);
		this.UpdateNoMods();
	}

	// Token: 0x06000C29 RID: 3113 RVA: 0x00043870 File Offset: 0x00041A70
	public void AddMod(global::Mod mod)
	{
		if (this.modTemplateContainerMap.ContainsKey(mod))
		{
			return;
		}
		TemplateContainer templateContainer = this.modAsset.Instantiate();
		UI.Mod uiMod = templateContainer.Query(null, null);
		UI.Mod uiMod2 = uiMod;
		EventCallback<ChangeEvent<bool>> <>9__1;
		Action <>9__2;
		uiMod2.Ready = (Action)Delegate.Combine(uiMod2.Ready, new Action(delegate()
		{
			uiMod.Toggle.value = mod.IsEnabled;
			INotifyValueChanged<bool> toggle = uiMod.Toggle;
			EventCallback<ChangeEvent<bool>> callback;
			if ((callback = <>9__1) == null)
			{
				callback = (<>9__1 = delegate(ChangeEvent<bool> e)
				{
					this.OnModToggleChanged(mod, e.newValue);
				});
			}
			toggle.RegisterValueChangedCallback(callback);
			Link link = uiMod.ModPreview.Link;
			Delegate clicked = link.Clicked;
			Action b;
			if ((b = <>9__2) == null)
			{
				b = (<>9__2 = delegate()
				{
					this.OnModPreviewLinkClicked(mod);
				});
			}
			link.Clicked = (Action)Delegate.Combine(clicked, b);
			this.UpdateMod(mod);
		}));
		this.modTemplateContainerMap.Add(mod, templateContainer);
		this.modsList.Add(templateContainer);
		this.UpdateNoMods();
	}

	// Token: 0x06000C2A RID: 3114 RVA: 0x00043910 File Offset: 0x00041B10
	public void UpdateMod(global::Mod mod)
	{
		if (!this.modTemplateContainerMap.ContainsKey(mod))
		{
			return;
		}
		UI.Mod mod2 = this.modTemplateContainerMap[mod].Query(null, null);
		SteamWorkshopItem steamWorkshopItem = mod.SteamWorkshopItem;
		string text;
		if (steamWorkshopItem == null)
		{
			text = null;
		}
		else
		{
			SteamWorkshopItemDetails details = steamWorkshopItem.Details;
			text = ((details != null) ? details.Description : null);
		}
		mod2.Description = (text ?? string.Empty);
		SteamWorkshopItem steamWorkshopItem2 = mod.SteamWorkshopItem;
		Texture2D texture2D;
		if (steamWorkshopItem2 == null)
		{
			texture2D = null;
		}
		else
		{
			SteamWorkshopItemDetails details2 = steamWorkshopItem2.Details;
			texture2D = ((details2 != null) ? details2.PreviewTexture : null);
		}
		Texture2D texture2D2 = texture2D;
		mod2.PreviewTexture = ((texture2D2 != null) ? texture2D2 : this.defaultPreviewTexture);
		mod2.Toggle.enabledSelf = mod.HasAssembly;
		mod2.Toggle.value = mod.IsEnabled;
		Link link = mod2.ModPreview.Link;
		SteamWorkshopItem steamWorkshopItem3 = mod.SteamWorkshopItem;
		string text2;
		if (steamWorkshopItem3 == null)
		{
			text2 = null;
		}
		else
		{
			SteamWorkshopItemDetails details3 = steamWorkshopItem3.Details;
			text2 = ((details3 != null) ? details3.Title : null);
		}
		link.Text = (text2 ?? mod.Id);
		ModPreview modPreview = mod2.ModPreview;
		SteamWorkshopItem steamWorkshopItem4 = mod.SteamWorkshopItem;
		modPreview.IsStatisticsVisible = (((steamWorkshopItem4 != null) ? steamWorkshopItem4.Details : null) != null);
		ModPreview modPreview2 = mod2.ModPreview;
		SteamWorkshopItem steamWorkshopItem5 = mod.SteamWorkshopItem;
		int? num;
		if (steamWorkshopItem5 == null)
		{
			num = null;
		}
		else
		{
			SteamWorkshopItemDetails details4 = steamWorkshopItem5.Details;
			num = ((details4 != null) ? new int?(details4.Subscriptions) : null);
		}
		int? num2 = num;
		modPreview2.Subscriptions = num2.GetValueOrDefault();
		ModPreview modPreview3 = mod2.ModPreview;
		SteamWorkshopItem steamWorkshopItem6 = mod.SteamWorkshopItem;
		int? num3;
		if (steamWorkshopItem6 == null)
		{
			num3 = null;
		}
		else
		{
			SteamWorkshopItemDetails details5 = steamWorkshopItem6.Details;
			num3 = ((details5 != null) ? new int?(details5.Upvotes) : null);
		}
		num2 = num3;
		modPreview3.Upvotes = num2.GetValueOrDefault();
		ModPreview modPreview4 = mod2.ModPreview;
		SteamWorkshopItem steamWorkshopItem7 = mod.SteamWorkshopItem;
		int? num4;
		if (steamWorkshopItem7 == null)
		{
			num4 = null;
		}
		else
		{
			SteamWorkshopItemDetails details6 = steamWorkshopItem7.Details;
			num4 = ((details6 != null) ? new int?(details6.Downvotes) : null);
		}
		num2 = num4;
		modPreview4.Downvotes = num2.GetValueOrDefault();
	}

	// Token: 0x06000C2B RID: 3115 RVA: 0x00043AF4 File Offset: 0x00041CF4
	public void RemoveMod(global::Mod mod)
	{
		if (!this.modTemplateContainerMap.ContainsKey(mod))
		{
			return;
		}
		TemplateContainer element = this.modTemplateContainerMap[mod];
		this.modsList.Remove(element);
		this.modTemplateContainerMap.Remove(mod);
		this.UpdateNoMods();
	}

	// Token: 0x06000C2C RID: 3116 RVA: 0x00011F66 File Offset: 0x00010166
	private void UpdateNoMods()
	{
		this.noMods.style.display = ((this.modTemplateContainerMap.Count > 0 || this.pluginTemplateContainerMap.Count > 0) ? DisplayStyle.None : DisplayStyle.Flex);
	}

	// Token: 0x06000C2D RID: 3117 RVA: 0x00011F9D File Offset: 0x0001019D
	private void OnClickClose()
	{
		EventManager.TriggerEvent("Event_OnModsClickClose", null);
	}

	// Token: 0x06000C2E RID: 3118 RVA: 0x00011FAA File Offset: 0x000101AA
	private void OnClickFindMods()
	{
		EventManager.TriggerEvent("Event_OnModsClickFindMods", null);
	}

	// Token: 0x06000C2F RID: 3119 RVA: 0x00011FB7 File Offset: 0x000101B7
	private void OnClickRefresh()
	{
		EventManager.TriggerEvent("Event_OnModsClickRefresh", null);
	}

	// Token: 0x06000C30 RID: 3120 RVA: 0x00011FC4 File Offset: 0x000101C4
	private void OnModToggleChanged(global::Mod mod, bool value)
	{
		if (value)
		{
			EventManager.TriggerEvent("Event_OnModsModEnabled", new Dictionary<string, object>
			{
				{
					"mod",
					mod
				}
			});
			return;
		}
		EventManager.TriggerEvent("Event_OnModsModDisabled", new Dictionary<string, object>
		{
			{
				"mod",
				mod
			}
		});
	}

	// Token: 0x06000C31 RID: 3121 RVA: 0x00012000 File Offset: 0x00010200
	private void OnModPreviewLinkClicked(global::Mod mod)
	{
		EventManager.TriggerEvent("Event_OnModPreviewLinkClicked", new Dictionary<string, object>
		{
			{
				"id",
				mod.Id
			}
		});
	}

	// Token: 0x06000C32 RID: 3122 RVA: 0x00012022 File Offset: 0x00010222
	private void OnPluginToggleChanged(Plugin plugin, bool value)
	{
		if (value)
		{
			EventManager.TriggerEvent("Event_OnModsPluginEnabled", new Dictionary<string, object>
			{
				{
					"plugin",
					plugin
				}
			});
			return;
		}
		EventManager.TriggerEvent("Event_OnModsPluginDisabled", new Dictionary<string, object>
		{
			{
				"plugin",
				plugin
			}
		});
	}

	// Token: 0x0400073B RID: 1851
	private static readonly global::Logger Logger = new global::Logger("UIMods");

	// Token: 0x0400073C RID: 1852
	[Header("References")]
	[SerializeField]
	private VisualTreeAsset modAsset;

	// Token: 0x0400073D RID: 1853
	[SerializeField]
	private StyleBackground defaultPreviewTexture;

	// Token: 0x0400073E RID: 1854
	private VisualElement mods;

	// Token: 0x0400073F RID: 1855
	private VisualElement modsList;

	// Token: 0x04000740 RID: 1856
	private VisualElement noMods;

	// Token: 0x04000741 RID: 1857
	private IconButton closeIconButton;

	// Token: 0x04000742 RID: 1858
	private Button findModsButton;

	// Token: 0x04000743 RID: 1859
	private Button refreshButton;

	// Token: 0x04000744 RID: 1860
	private Dictionary<global::Mod, TemplateContainer> modTemplateContainerMap = new Dictionary<global::Mod, TemplateContainer>();

	// Token: 0x04000745 RID: 1861
	private Dictionary<Plugin, TemplateContainer> pluginTemplateContainerMap = new Dictionary<Plugin, TemplateContainer>();
}
