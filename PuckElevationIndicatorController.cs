using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200005B RID: 91
public class PuckElevationIndicatorController : MonoBehaviour
{
	// Token: 0x06000309 RID: 777 RVA: 0x0000AD17 File Offset: 0x00008F17
	private void Awake()
	{
		this.puckElevationIndicator = base.GetComponent<PuckElevationIndicator>();
	}

	// Token: 0x0600030A RID: 778 RVA: 0x0000AD25 File Offset: 0x00008F25
	private void Start()
	{
		EventManager.AddEventListener("Event_OnShowPuckElevationChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckElevationChanged));
		this.puckElevationIndicator.IsVisible = SettingsManager.ShowPuckElevation;
	}

	// Token: 0x0600030B RID: 779 RVA: 0x0000AD4D File Offset: 0x00008F4D
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnShowPuckElevationChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckElevationChanged));
	}

	// Token: 0x0600030C RID: 780 RVA: 0x00023E80 File Offset: 0x00022080
	private void Event_OnShowPuckElevationChanged(Dictionary<string, object> message)
	{
		bool isVisible = (bool)message["value"];
		this.puckElevationIndicator.IsVisible = isVisible;
	}

	// Token: 0x04000224 RID: 548
	private PuckElevationIndicator puckElevationIndicator;
}
