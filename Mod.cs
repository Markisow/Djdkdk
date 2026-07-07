using System;
using System.Collections.Generic;

// Token: 0x020000CD RID: 205
public class Mod : BasePlugin<BasePluginState>
{
	// Token: 0x170000A6 RID: 166
	// (get) Token: 0x0600063E RID: 1598 RVA: 0x0000CF48 File Offset: 0x0000B148
	public string Id
	{
		get
		{
			return this.SteamWorkshopItem.Id;
		}
	}

	// Token: 0x0600063F RID: 1599 RVA: 0x0000CF55 File Offset: 0x0000B155
	public Mod(SteamWorkshopItem steamWorkshopItem) : base(new BasePluginState
	{
		Path = null,
		IsReady = false,
		IsEnabled = false
	})
	{
		this.SteamWorkshopItem = steamWorkshopItem;
	}

	// Token: 0x06000640 RID: 1600 RVA: 0x0002FDDC File Offset: 0x0002DFDC
	public override void Initialize()
	{
		base.Initialize();
		SteamWorkshopItem steamWorkshopItem = this.SteamWorkshopItem;
		steamWorkshopItem.StateChanged = (Action<SteamWorkshopItemState, SteamWorkshopItemState>)Delegate.Combine(steamWorkshopItem.StateChanged, new Action<SteamWorkshopItemState, SteamWorkshopItemState>(this.OnSteamWorkshopItemStateChanged));
		SteamWorkshopItem steamWorkshopItem2 = this.SteamWorkshopItem;
		steamWorkshopItem2.DetailsStateChanged = (Action<SteamWorkshopItemDetailsState, SteamWorkshopItemDetailsState>)Delegate.Combine(steamWorkshopItem2.DetailsStateChanged, new Action<SteamWorkshopItemDetailsState, SteamWorkshopItemDetailsState>(this.OnSteamWorkshopItemDetailsStateChanged));
		this.SetState(new Dictionary<string, object>
		{
			{
				"path",
				this.SteamWorkshopItem.Path
			},
			{
				"isReady",
				this.SteamWorkshopItem.Phase == SteamWorkshopItemPhase.Installed
			}
		});
	}

	// Token: 0x06000641 RID: 1601 RVA: 0x0002FE7C File Offset: 0x0002E07C
	public override void OnEnableFailed(Exception exception)
	{
		base.OnEnableFailed(exception);
		Mod.Logger.Error("Failed to enable mod " + this.Id + ": " + exception.Message);
		EventManager.TriggerEvent("Event_OnModEnableFailed", new Dictionary<string, object>
		{
			{
				"mod",
				this
			}
		});
	}

	// Token: 0x06000642 RID: 1602 RVA: 0x0002FED0 File Offset: 0x0002E0D0
	public override void OnDisableFailed(Exception exception)
	{
		base.OnDisableFailed(exception);
		Mod.Logger.Error("Failed to disable mod " + this.Id + ": " + exception.Message);
		EventManager.TriggerEvent("Event_OnModDisableFailed", new Dictionary<string, object>
		{
			{
				"mod",
				this
			}
		});
	}

	// Token: 0x06000643 RID: 1603 RVA: 0x0000CF7E File Offset: 0x0000B17E
	protected override void OnStateChanged(BasePluginState oldState, BasePluginState newState)
	{
		base.OnStateChanged(oldState, newState);
		EventManager.TriggerEvent("Event_OnModStateChanged", new Dictionary<string, object>
		{
			{
				"mod",
				this
			},
			{
				"oldState",
				oldState
			},
			{
				"newState",
				newState
			}
		});
	}

	// Token: 0x06000644 RID: 1604 RVA: 0x0002FF24 File Offset: 0x0002E124
	private void OnSteamWorkshopItemStateChanged(SteamWorkshopItemState oldState, SteamWorkshopItemState newState)
	{
		EventManager.TriggerEvent("Event_OnModSteamWorkshopItemStateChanged", new Dictionary<string, object>
		{
			{
				"mod",
				this
			},
			{
				"oldState",
				oldState
			},
			{
				"newState",
				newState
			}
		});
		this.SetState(new Dictionary<string, object>
		{
			{
				"path",
				newState.Path
			},
			{
				"isReady",
				newState.Phase == SteamWorkshopItemPhase.Installed
			}
		});
	}

	// Token: 0x06000645 RID: 1605 RVA: 0x0000CFBB File Offset: 0x0000B1BB
	private void OnSteamWorkshopItemDetailsStateChanged(SteamWorkshopItemDetailsState oldState, SteamWorkshopItemDetailsState newState)
	{
		EventManager.TriggerEvent("Event_OnModSteamWorkshopItemDetailsStateChanged", new Dictionary<string, object>
		{
			{
				"mod",
				this
			},
			{
				"oldState",
				oldState
			},
			{
				"newState",
				newState
			}
		});
	}

	// Token: 0x040003E7 RID: 999
	private static readonly Logger Logger = new Logger("Mod");

	// Token: 0x040003E8 RID: 1000
	public readonly SteamWorkshopItem SteamWorkshopItem;
}
