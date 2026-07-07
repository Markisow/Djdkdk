using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000130 RID: 304
public class TimeoutManagerController : MonoBehaviour
{
	// Token: 0x06000899 RID: 2201 RVA: 0x0000EBD2 File Offset: 0x0000CDD2
	public void Awake()
	{
		this.timeoutManager = base.GetComponent<TimeoutManager>();
		EventManager.AddEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
	}

	// Token: 0x0600089A RID: 2202 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Start()
	{
	}

	// Token: 0x0600089B RID: 2203 RVA: 0x0000EBF6 File Offset: 0x0000CDF6
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x0000EC0E File Offset: 0x0000CE0E
	private void Event_Server_OnServerStopped(Dictionary<string, object> message)
	{
		this.timeoutManager.Dispose();
	}

	// Token: 0x0400051E RID: 1310
	private TimeoutManager timeoutManager;
}
