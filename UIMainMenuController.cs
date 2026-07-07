using System;
using System.Collections.Generic;

// Token: 0x02000198 RID: 408
public class UIMainMenuController : UIViewController<UIMainMenu>
{
	// Token: 0x06000BE4 RID: 3044 RVA: 0x00011C9D File Offset: 0x0000FE9D
	public override void Awake()
	{
		base.Awake();
		this.uiMainMenu = base.GetComponent<UIMainMenu>();
		EventManager.AddEventListener("Event_OnDebugChanged", new Action<Dictionary<string, object>>(this.Event_OnDebugChanged));
	}

	// Token: 0x06000BE5 RID: 3045 RVA: 0x00011CC7 File Offset: 0x0000FEC7
	private void Start()
	{
		if (SettingsManager.Debug)
		{
			this.uiMainMenu.ShowDebug();
			return;
		}
		this.uiMainMenu.HideDebug();
	}

	// Token: 0x06000BE6 RID: 3046 RVA: 0x00011CE7 File Offset: 0x0000FEE7
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnDebugChanged", new Action<Dictionary<string, object>>(this.Event_OnDebugChanged));
		base.OnDestroy();
	}

	// Token: 0x06000BE7 RID: 3047 RVA: 0x00011D05 File Offset: 0x0000FF05
	private void Event_OnDebugChanged(Dictionary<string, object> message)
	{
		if ((bool)message["value"])
		{
			this.uiMainMenu.ShowDebug();
			return;
		}
		this.uiMainMenu.HideDebug();
	}

	// Token: 0x04000726 RID: 1830
	private UIMainMenu uiMainMenu;
}
