using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000126 RID: 294
public class EdgegapManagerController : MonoBehaviour
{
	// Token: 0x0600082D RID: 2093 RVA: 0x000350F0 File Offset: 0x000332F0
	public void Awake()
	{
		this.edgegapManager = base.GetComponent<EdgegapManager>();
		EventManager.AddEventListener("Event_OnServerStateChanged", new Action<Dictionary<string, object>>(this.Event_OnServerStateChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerAdded", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerAdded));
		EventManager.AddEventListener("Event_Everyone_OnPlayerRemoved", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerRemoved));
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x0000E691 File Offset: 0x0000C891
	private void Start()
	{
		this.edgegapManager.StartDependencyTimeout(EdgegapDependency.IsAuthenticated);
		this.edgegapManager.StartDependencyTimeout(EdgegapDependency.IsOccupied);
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0003514C File Offset: 0x0003334C
	private void OnDestroy()
	{
		this.edgegapManager.StopDependencyTimeout(EdgegapDependency.IsAuthenticated);
		this.edgegapManager.StopDependencyTimeout(EdgegapDependency.IsOccupied);
		EventManager.RemoveEventListener("Event_OnServerStateChanged", new Action<Dictionary<string, object>>(this.Event_OnServerStateChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerAdded", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerAdded));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerRemoved", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerRemoved));
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x000351B4 File Offset: 0x000333B4
	private void Event_OnServerStateChanged(Dictionary<string, object> message)
	{
		ref ServerState ptr = (ServerState)message["oldServerState"];
		ServerState serverState = (ServerState)message["newServerState"];
		if (ptr.AuthenticationPhase != serverState.AuthenticationPhase)
		{
			this.edgegapManager.SetDependency(EdgegapDependency.IsAuthenticated, serverState.AuthenticationPhase == AuthenticationPhase.Authenticated);
		}
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x0000E6AB File Offset: 0x0000C8AB
	private void Event_Everyone_OnPlayerAdded(Dictionary<string, object> message)
	{
		this.edgegapManager.SetDependency(EdgegapDependency.IsOccupied, true);
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0000E6BA File Offset: 0x0000C8BA
	private void Event_Everyone_OnPlayerRemoved(Dictionary<string, object> message)
	{
		if (MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayers(false).Count == 0)
		{
			this.edgegapManager.StartDependencyTimeout(EdgegapDependency.IsOccupied);
		}
	}

	// Token: 0x040004EF RID: 1263
	private EdgegapManager edgegapManager;
}
