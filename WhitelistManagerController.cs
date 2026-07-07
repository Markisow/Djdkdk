using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000132 RID: 306
public class WhitelistManagerController : MonoBehaviour
{
	// Token: 0x060008AC RID: 2220 RVA: 0x0000ED12 File Offset: 0x0000CF12
	public void Awake()
	{
		this.whitelistManager = base.GetComponent<WhitelistManager>();
		EventManager.AddEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.AddEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Start()
	{
	}

	// Token: 0x060008AE RID: 2222 RVA: 0x0000ED4C File Offset: 0x0000CF4C
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.RemoveEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
	}

	// Token: 0x060008AF RID: 2223 RVA: 0x0000ED7A File Offset: 0x0000CF7A
	private void Event_Server_OnServerStarted(Dictionary<string, object> message)
	{
		this.whitelistManager.LoadWhitelistedSteamIds();
	}

	// Token: 0x060008B0 RID: 2224 RVA: 0x0000ED87 File Offset: 0x0000CF87
	private void Event_Server_OnServerStopped(Dictionary<string, object> message)
	{
		this.whitelistManager.Dispose();
	}

	// Token: 0x04000523 RID: 1315
	private WhitelistManager whitelistManager;
}
