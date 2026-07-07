using System;
using System.Collections.Generic;

// Token: 0x02000189 RID: 393
public class UIDebugController : UIViewController<UIDebug>
{
	// Token: 0x06000B78 RID: 2936 RVA: 0x000115AD File Offset: 0x0000F7AD
	public override void Awake()
	{
		base.Awake();
		this.uiDebug = base.GetComponent<UIDebug>();
		EventManager.AddEventListener("Event_OnDebugChanged", new Action<Dictionary<string, object>>(this.Event_OnDebugChanged));
	}

	// Token: 0x06000B79 RID: 2937 RVA: 0x000115D7 File Offset: 0x0000F7D7
	private void Start()
	{
		this.uiDebug.SetBuild(string.Format("PUCK B{0} {1:yyyy-MM-dd HH:mm:ss}", ApplicationManager.Version, DateTime.UtcNow));
	}

	// Token: 0x06000B7A RID: 2938 RVA: 0x00011602 File Offset: 0x0000F802
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnDebugChanged", new Action<Dictionary<string, object>>(this.Event_OnDebugChanged));
		base.OnDestroy();
	}

	// Token: 0x06000B7B RID: 2939 RVA: 0x00011620 File Offset: 0x0000F820
	private void Event_OnDebugChanged(Dictionary<string, object> message)
	{
		if ((bool)message["value"])
		{
			this.uiDebug.Show();
			return;
		}
		this.uiDebug.Hide();
	}

	// Token: 0x040006E1 RID: 1761
	private UIDebug uiDebug;
}
