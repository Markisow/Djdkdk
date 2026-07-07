using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011A RID: 282
public class AdminManagerController : MonoBehaviour
{
	// Token: 0x060007CC RID: 1996 RVA: 0x0000E0AE File Offset: 0x0000C2AE
	public void Awake()
	{
		this.adminManager = base.GetComponent<AdminManager>();
		EventManager.AddEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.AddEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
	}

	// Token: 0x060007CD RID: 1997 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Start()
	{
	}

	// Token: 0x060007CE RID: 1998 RVA: 0x0000E0E8 File Offset: 0x0000C2E8
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.RemoveEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
	}

	// Token: 0x060007CF RID: 1999 RVA: 0x0000E116 File Offset: 0x0000C316
	private void Event_Server_OnServerStarted(Dictionary<string, object> message)
	{
		this.adminManager.LoadAdminSteamIds();
	}

	// Token: 0x060007D0 RID: 2000 RVA: 0x0000E123 File Offset: 0x0000C323
	private void Event_Server_OnServerStopped(Dictionary<string, object> message)
	{
		this.adminManager.Dispose();
	}

	// Token: 0x040004C0 RID: 1216
	private AdminManager adminManager;
}
