using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000FF RID: 255
public class PuckManagerController : MonoBehaviour
{
	// Token: 0x0600075B RID: 1883 RVA: 0x00032088 File Offset: 0x00030288
	private void Awake()
	{
		this.puckManager = base.GetComponent<PuckManager>();
		EventManager.AddEventListener("Event_Everyone_OnPuckPositionSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckPositionSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPuckPositionDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckPositionDespawned));
		EventManager.AddEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPuckDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckDespawned));
	}

	// Token: 0x0600075C RID: 1884 RVA: 0x000320FC File Offset: 0x000302FC
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPuckPositionSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckPositionSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPuckPositionDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckPositionDespawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPuckDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckDespawned));
	}

	// Token: 0x0600075D RID: 1885 RVA: 0x00032164 File Offset: 0x00030364
	private void Event_Everyone_OnPuckPositionSpawned(Dictionary<string, object> message)
	{
		PuckPosition puckPosition = (PuckPosition)message["puckPosition"];
		this.puckManager.AddPuckPosition(puckPosition);
	}

	// Token: 0x0600075E RID: 1886 RVA: 0x00032190 File Offset: 0x00030390
	private void Event_Everyone_OnPuckPositionDespawned(Dictionary<string, object> message)
	{
		PuckPosition puckPosition = (PuckPosition)message["puckPosition"];
		this.puckManager.RemovePuckPosition(puckPosition);
	}

	// Token: 0x0600075F RID: 1887 RVA: 0x000321BC File Offset: 0x000303BC
	private void Event_Everyone_OnPuckSpawned(Dictionary<string, object> message)
	{
		Puck puck = (Puck)message["puck"];
		this.puckManager.AddPuck(puck);
	}

	// Token: 0x06000760 RID: 1888 RVA: 0x000321E8 File Offset: 0x000303E8
	private void Event_Everyone_OnPuckDespawned(Dictionary<string, object> message)
	{
		Puck puck = (Puck)message["puck"];
		this.puckManager.RemovePuck(puck);
	}

	// Token: 0x0400046A RID: 1130
	private PuckManager puckManager;
}
