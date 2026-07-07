using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200011C RID: 284
public class BanManagerController : MonoBehaviour
{
	// Token: 0x060007E6 RID: 2022 RVA: 0x0000E251 File Offset: 0x0000C451
	public void Awake()
	{
		this.banManager = base.GetComponent<BanManager>();
		EventManager.AddEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.AddEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Start()
	{
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x0000E28B File Offset: 0x0000C48B
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.RemoveEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
	}

	// Token: 0x060007E9 RID: 2025 RVA: 0x0000E2B9 File Offset: 0x0000C4B9
	private void Event_Server_OnServerStarted(Dictionary<string, object> message)
	{
		this.banManager.LoadBannedSteamIds();
		this.banManager.LoadBannedIpAddresses();
	}

	// Token: 0x060007EA RID: 2026 RVA: 0x0000E2D1 File Offset: 0x0000C4D1
	private void Event_Server_OnServerStopped(Dictionary<string, object> message)
	{
		this.banManager.Dispose();
	}

	// Token: 0x040004C8 RID: 1224
	private BanManager banManager;
}
