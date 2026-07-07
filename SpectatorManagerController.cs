using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000137 RID: 311
public class SpectatorManagerController : MonoBehaviour
{
	// Token: 0x06000928 RID: 2344 RVA: 0x00037F9C File Offset: 0x0003619C
	private void Awake()
	{
		this.spectatorManager = base.GetComponent<SpectatorManager>();
		EventManager.AddEventListener("Event_OnSpectatorPositionSpawned", new Action<Dictionary<string, object>>(this.Event_OnSpectatorPositionSpawned));
		EventManager.AddEventListener("Event_OnSpectatorPositionDespawned", new Action<Dictionary<string, object>>(this.Event_OnSpectatorPositionDespawned));
		EventManager.AddEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		EventManager.AddEventListener("Event_Everyone_OnGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGameStateChanged));
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x00038010 File Offset: 0x00036210
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnSpectatorPositionSpawned", new Action<Dictionary<string, object>>(this.Event_OnSpectatorPositionSpawned));
		EventManager.RemoveEventListener("Event_OnSpectatorPositionDespawned", new Action<Dictionary<string, object>>(this.Event_OnSpectatorPositionDespawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPuckSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPuckSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGameStateChanged));
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00038078 File Offset: 0x00036278
	private void Event_OnSpectatorPositionSpawned(Dictionary<string, object> message)
	{
		SpectatorPosition position = (SpectatorPosition)message["spectatorPosition"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		this.spectatorManager.RegisterSpectatorPosition(position);
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x000380AC File Offset: 0x000362AC
	private void Event_OnSpectatorPositionDespawned(Dictionary<string, object> message)
	{
		SpectatorPosition position = (SpectatorPosition)message["spectatorPosition"];
		if (ApplicationManager.IsDedicatedGameServer)
		{
			return;
		}
		this.spectatorManager.UnregisterSpectatorPosition(position);
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x000380E0 File Offset: 0x000362E0
	private void Event_Everyone_OnPuckSpawned(Dictionary<string, object> message)
	{
		Puck puck = (Puck)message["puck"];
		this.spectatorManager.SetSpectatorLookTarget(puck.transform);
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x00038110 File Offset: 0x00036310
	private void Event_Everyone_OnGameStateChanged(Dictionary<string, object> message)
	{
		ref GameState ptr = (GameState)message["oldGameState"];
		GameState gameState = (GameState)message["newGameState"];
		if (ptr.Phase == gameState.Phase)
		{
			return;
		}
		GamePhase phase = gameState.Phase;
		if (phase - GamePhase.BlueScore <= 1)
		{
			this.spectatorManager.SetSpectatorAnimation("Cheering");
			return;
		}
		this.spectatorManager.SetSpectatorAnimation("Seated");
	}

	// Token: 0x04000570 RID: 1392
	private SpectatorManager spectatorManager;
}
