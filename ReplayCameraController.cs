using System;
using System.Collections.Generic;

// Token: 0x02000064 RID: 100
public class ReplayCameraController : BaseCameraController
{
	// Token: 0x06000355 RID: 853 RVA: 0x0000B0C1 File Offset: 0x000092C1
	public override void Awake()
	{
		base.Awake();
		this.replayCamera = base.GetComponent<ReplayCamera>();
		EventManager.AddEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0000B0EB File Offset: 0x000092EB
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		base.OnDestroy();
	}

	// Token: 0x06000357 RID: 855 RVA: 0x00024890 File Offset: 0x00022A90
	private void Event_Everyone_OnPuckSpawned(Dictionary<string, object> message)
	{
		Puck puck = (Puck)message["puck"];
		this.replayCamera.Target = puck.transform;
	}

	// Token: 0x04000258 RID: 600
	private ReplayCamera replayCamera;
}
