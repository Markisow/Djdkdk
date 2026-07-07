using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200014C RID: 332
public class SynchronizedObjectManagerController : MonoBehaviour
{
	// Token: 0x060009FE RID: 2558 RVA: 0x0003B420 File Offset: 0x00039620
	private void Awake()
	{
		this.synchronizedObjectManager = base.GetComponent<SynchronizedObjectManager>();
		EventManager.AddEventListener("Event_OnUseNetworkSmoothingChanged", new Action<Dictionary<string, object>>(this.Event_OnUseNetworkSmoothingChanged));
		EventManager.AddEventListener("Event_OnNetworkSmoothingStrengthChanged", new Action<Dictionary<string, object>>(this.Event_OnNetworkSmoothingStrengthChanged));
		EventManager.AddEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		EventManager.AddEventListener("Event_Everyone_OnSynchronizedObjectSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnSynchronizedObjectSpawned));
		EventManager.AddEventListener("Event_Everyone_OnSynchronizedObjectDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnSynchronizedObjectDespawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerDespawned));
		EventManager.AddEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.AddEventListener("Event_Server_OnClientSceneSynchronizeComplete", new Action<Dictionary<string, object>>(this.Event_Server_OnClientSceneSynchronizeComplete));
	}

	// Token: 0x060009FF RID: 2559 RVA: 0x00010541 File Offset: 0x0000E741
	private void Start()
	{
		this.synchronizedObjectManager.UseNetworkSmoothing = SettingsManager.UseNetworkSmoothing;
		this.synchronizedObjectManager.NetworkSmoothingStrength = SettingsManager.NetworkSmoothingStrength;
	}

	// Token: 0x06000A00 RID: 2560 RVA: 0x0003B500 File Offset: 0x00039700
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnUseNetworkSmoothingChanged", new Action<Dictionary<string, object>>(this.Event_OnUseNetworkSmoothingChanged));
		EventManager.RemoveEventListener("Event_OnNetworkSmoothingStrengthChanged", new Action<Dictionary<string, object>>(this.Event_OnNetworkSmoothingStrengthChanged));
		EventManager.RemoveEventListener("Event_OnClientStopped", new Action<Dictionary<string, object>>(this.Event_OnClientStopped));
		EventManager.RemoveEventListener("Event_Everyone_OnSynchronizedObjectSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnSynchronizedObjectSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnSynchronizedObjectDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnSynchronizedObjectDespawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerDespawned));
		EventManager.RemoveEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.RemoveEventListener("Event_Server_OnClientSceneSynchronizeComplete", new Action<Dictionary<string, object>>(this.Event_Server_OnClientSceneSynchronizeComplete));
	}

	// Token: 0x06000A01 RID: 2561 RVA: 0x0003B5D4 File Offset: 0x000397D4
	private void Event_OnUseNetworkSmoothingChanged(Dictionary<string, object> message)
	{
		bool useNetworkSmoothing = (bool)message["value"];
		this.synchronizedObjectManager.UseNetworkSmoothing = useNetworkSmoothing;
	}

	// Token: 0x06000A02 RID: 2562 RVA: 0x0003B600 File Offset: 0x00039800
	private void Event_OnNetworkSmoothingStrengthChanged(Dictionary<string, object> message)
	{
		int networkSmoothingStrength = (int)message["value"];
		this.synchronizedObjectManager.NetworkSmoothingStrength = networkSmoothingStrength;
	}

	// Token: 0x06000A03 RID: 2563 RVA: 0x00010563 File Offset: 0x0000E763
	private void Event_OnClientStopped(Dictionary<string, object> message)
	{
		this.synchronizedObjectManager.Dispose();
	}

	// Token: 0x06000A04 RID: 2564 RVA: 0x0003B62C File Offset: 0x0003982C
	private void Event_Everyone_OnSynchronizedObjectSpawned(Dictionary<string, object> message)
	{
		SynchronizedObject synchronizedObject = (SynchronizedObject)message["synchronizedObject"];
		this.synchronizedObjectManager.AddSynchronizedObject(synchronizedObject);
	}

	// Token: 0x06000A05 RID: 2565 RVA: 0x0003B658 File Offset: 0x00039858
	private void Event_Everyone_OnSynchronizedObjectDespawned(Dictionary<string, object> message)
	{
		SynchronizedObject synchronizedObject = (SynchronizedObject)message["synchronizedObject"];
		this.synchronizedObjectManager.RemoveSynchronizedObject(synchronizedObject);
	}

	// Token: 0x06000A06 RID: 2566 RVA: 0x0003B684 File Offset: 0x00039884
	private void Event_Everyone_OnPlayerSpawned(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (player.IsReplay.Value)
		{
			return;
		}
		if (player.OwnerClientId == 0UL)
		{
			return;
		}
		this.synchronizedObjectManager.Server_AddSynchronizedClientId(player.OwnerClientId);
	}

	// Token: 0x06000A07 RID: 2567 RVA: 0x0003B6CC File Offset: 0x000398CC
	private void Event_Everyone_OnPlayerDespawned(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (player.IsReplay.Value)
		{
			return;
		}
		if (player.OwnerClientId == 0UL)
		{
			return;
		}
		this.synchronizedObjectManager.Server_RemoveSynchronizedClientId(player.OwnerClientId);
	}

	// Token: 0x06000A08 RID: 2568 RVA: 0x0003B714 File Offset: 0x00039914
	private void Event_Server_OnServerStarted(Dictionary<string, object> message)
	{
		ServerConfig serverConfig = (ServerConfig)message["serverConfig"];
		this.synchronizedObjectManager.TickRate = serverConfig.tickRate;
	}

	// Token: 0x06000A09 RID: 2569 RVA: 0x0003B744 File Offset: 0x00039944
	private void Event_Server_OnClientSceneSynchronizeComplete(Dictionary<string, object> message)
	{
		ulong num = (ulong)message["clientId"];
		if (num == 0UL)
		{
			return;
		}
		this.synchronizedObjectManager.Server_ForceSynchronizeClientId(num);
	}

	// Token: 0x040005D6 RID: 1494
	private SynchronizedObjectManager synchronizedObjectManager;
}
