using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000114 RID: 276
public class ReplayRecorderController : MonoBehaviour
{
	// Token: 0x06000790 RID: 1936 RVA: 0x00033900 File Offset: 0x00031B00
	private void Awake()
	{
		this.replayRecorder = base.GetComponent<ReplayRecorder>();
		EventManager.AddEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerDespawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerBodySpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerBodyDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodyDespawned));
		EventManager.AddEventListener("Event_Everyone_OnStickSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnStickSpawned));
		EventManager.AddEventListener("Event_Everyone_OnStickDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnStickDespawned));
		EventManager.AddEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPuckDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckDespawned));
	}

	// Token: 0x06000791 RID: 1937 RVA: 0x0000895D File Offset: 0x00006B5D
	private void Start()
	{
	}

	// Token: 0x06000792 RID: 1938 RVA: 0x000339CC File Offset: 0x00031BCC
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerDespawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerBodySpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodySpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerBodyDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerBodyDespawned));
		EventManager.RemoveEventListener("Event_Everyone_OnStickSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnStickSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnStickDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnStickDespawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPuckDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckDespawned));
	}

	// Token: 0x06000793 RID: 1939 RVA: 0x00033A8C File Offset: 0x00031C8C
	private void Event_Everyone_OnPlayerSpawned(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (player.IsReplay.Value)
		{
			return;
		}
		this.replayRecorder.Server_AddPlayerSpawnedEvent(player);
	}

	// Token: 0x06000794 RID: 1940 RVA: 0x00033AD4 File Offset: 0x00031CD4
	private void Event_Everyone_OnPlayerDespawned(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (player.IsReplay.Value)
		{
			return;
		}
		this.replayRecorder.Server_AddPlayerDespawnedEvent(player);
	}

	// Token: 0x06000795 RID: 1941 RVA: 0x00033B1C File Offset: 0x00031D1C
	private void Event_Everyone_OnPlayerBodySpawned(Dictionary<string, object> message)
	{
		PlayerBody playerBody = (PlayerBody)message["playerBody"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (playerBody.Player.IsReplay.Value)
		{
			return;
		}
		this.replayRecorder.Server_AddPlayerBodySpawnedEvent(playerBody);
	}

	// Token: 0x06000796 RID: 1942 RVA: 0x00033B68 File Offset: 0x00031D68
	private void Event_Everyone_OnPlayerBodyDespawned(Dictionary<string, object> message)
	{
		PlayerBody playerBody = (PlayerBody)message["playerBody"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (playerBody.Player.IsReplay.Value)
		{
			return;
		}
		this.replayRecorder.Server_AddPlayerBodyDespawnedEvent(playerBody);
	}

	// Token: 0x06000797 RID: 1943 RVA: 0x00033BB4 File Offset: 0x00031DB4
	private void Event_Everyone_OnStickSpawned(Dictionary<string, object> message)
	{
		Stick stick = (Stick)message["stick"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (stick.Player.IsReplay.Value)
		{
			return;
		}
		this.replayRecorder.Server_AddStickSpawnedEvent(stick);
	}

	// Token: 0x06000798 RID: 1944 RVA: 0x00033C00 File Offset: 0x00031E00
	private void Event_Everyone_OnStickDespawned(Dictionary<string, object> message)
	{
		Stick stick = (Stick)message["stick"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (stick.Player.IsReplay.Value)
		{
			return;
		}
		this.replayRecorder.Server_AddStickDespawnedEvent(stick);
	}

	// Token: 0x06000799 RID: 1945 RVA: 0x00033C4C File Offset: 0x00031E4C
	private void Event_Everyone_OnPuckSpawned(Dictionary<string, object> message)
	{
		Puck puck = (Puck)message["puck"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (puck.IsReplay.Value)
		{
			return;
		}
		this.replayRecorder.Server_AddPuckSpawnedEvent(puck);
	}

	// Token: 0x0600079A RID: 1946 RVA: 0x00033C94 File Offset: 0x00031E94
	private void Event_Everyone_OnPuckDespawned(Dictionary<string, object> message)
	{
		Puck puck = (Puck)message["puck"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (puck.IsReplay.Value)
		{
			return;
		}
		this.replayRecorder.Server_AddPuckDespawnedEvent(puck);
	}

	// Token: 0x040004B5 RID: 1205
	private ReplayRecorder replayRecorder;
}
