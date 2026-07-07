using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200000D RID: 13
public class GoalController : MonoBehaviour
{
	// Token: 0x06000036 RID: 54 RVA: 0x00008C02 File Offset: 0x00006E02
	private void Awake()
	{
		this.goal = base.GetComponent<Goal>();
		EventManager.AddEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPuckDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckDespawned));
	}

	// Token: 0x06000037 RID: 55 RVA: 0x00008C3C File Offset: 0x00006E3C
	public void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPuckDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckDespawned));
	}

	// Token: 0x06000038 RID: 56 RVA: 0x00015C78 File Offset: 0x00013E78
	private void Event_Everyone_OnPuckSpawned(Dictionary<string, object> message)
	{
		Puck puck = (Puck)message["puck"];
		if (!NetworkManager.Singleton.IsClient)
		{
			return;
		}
		this.goal.Client_AddNetClothSphereCollider(puck.NetSphereCollider);
	}

	// Token: 0x06000039 RID: 57 RVA: 0x00015CB4 File Offset: 0x00013EB4
	private void Event_Everyone_OnPuckDespawned(Dictionary<string, object> message)
	{
		Puck puck = (Puck)message["puck"];
		if (!NetworkManager.Singleton.IsClient)
		{
			return;
		}
		this.goal.Client_RemoveNetClothSphereCollider(puck.NetSphereCollider);
	}

	// Token: 0x0400001D RID: 29
	private Goal goal;
}
