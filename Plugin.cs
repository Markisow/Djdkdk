using System;
using System.Collections.Generic;

// Token: 0x020000D4 RID: 212
public class Plugin : BasePlugin<BasePluginState>
{
	// Token: 0x06000675 RID: 1653 RVA: 0x0000D1C4 File Offset: 0x0000B3C4
	public Plugin(string id, string path) : base(new BasePluginState
	{
		Path = path,
		IsEnabled = false
	})
	{
		this.Id = id;
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0000D1E6 File Offset: 0x0000B3E6
	public override void Initialize()
	{
		base.Initialize();
		this.SetState(new Dictionary<string, object>
		{
			{
				"isReady",
				true
			}
		});
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x000308D8 File Offset: 0x0002EAD8
	public override void OnEnableFailed(Exception exception)
	{
		base.OnEnableFailed(exception);
		Plugin.Logger.Error("Failed to enable plugin " + this.Id + ": " + exception.Message);
		EventManager.TriggerEvent("Event_OnPluginEnableFailed", new Dictionary<string, object>
		{
			{
				"plugin",
				this
			}
		});
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0003092C File Offset: 0x0002EB2C
	public override void OnDisableFailed(Exception exception)
	{
		base.OnDisableFailed(exception);
		Plugin.Logger.Error("Failed to disable plugin " + this.Id + ": " + exception.Message);
		EventManager.TriggerEvent("Event_OnPluginDisableFailed", new Dictionary<string, object>
		{
			{
				"plugin",
				this
			}
		});
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0000D20A File Offset: 0x0000B40A
	protected override void OnStateChanged(BasePluginState oldState, BasePluginState newState)
	{
		base.OnStateChanged(oldState, newState);
		EventManager.TriggerEvent("Event_OnPluginStateChanged", new Dictionary<string, object>
		{
			{
				"plugin",
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

	// Token: 0x040003F7 RID: 1015
	private static readonly Logger Logger = new Logger("Plugin");

	// Token: 0x040003F8 RID: 1016
	public readonly string Id;
}
