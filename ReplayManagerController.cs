using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class ReplayManagerController : MonoBehaviour
{
	// Token: 0x06000769 RID: 1897 RVA: 0x0000DB88 File Offset: 0x0000BD88
	private void Awake()
	{
		this.replayManager = base.GetComponent<ReplayManager>();
		EventManager.AddEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x0000DBAC File Offset: 0x0000BDAC
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
	private void Event_Server_OnServerStopped(Dictionary<string, object> message)
	{
		this.replayManager.Server_StopReplaying();
		this.replayManager.Server_StopRecording();
	}

	// Token: 0x0400049F RID: 1183
	private ReplayManager replayManager;
}
