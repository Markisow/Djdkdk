using System;
using System.Collections.Generic;

// Token: 0x020001BE RID: 446
internal class UIPositionSelectController : UIViewController<UIPositionSelect>
{
	// Token: 0x06000D17 RID: 3351 RVA: 0x0004689C File Offset: 0x00044A9C
	public override void Awake()
	{
		base.Awake();
		this.uiPositionSelect = base.GetComponent<UIPositionSelect>();
		EventManager.AddEventListener("Event_Everyone_OnPlayerPositionSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerPositionDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionDespawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerPositionClaimedByPlayerChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionClaimedByPlayerChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
	}

	// Token: 0x06000D18 RID: 3352 RVA: 0x0004692C File Offset: 0x00044B2C
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerPositionSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerPositionDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionDespawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerPositionClaimedByPlayerChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionClaimedByPlayerChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		base.OnDestroy();
	}

	// Token: 0x06000D19 RID: 3353 RVA: 0x000469B0 File Offset: 0x00044BB0
	private void HandlePlayerGameState(Player player)
	{
		PlayerGameState value = player.GameState.Value;
		this.uiPositionSelect.Team = value.Team;
	}

	// Token: 0x06000D1A RID: 3354 RVA: 0x000469DC File Offset: 0x00044BDC
	private void Event_Everyone_OnPlayerPositionSpawned(Dictionary<string, object> message)
	{
		PlayerPosition playerPosition = (PlayerPosition)message["playerPosition"];
		this.uiPositionSelect.AddPosition(playerPosition);
	}

	// Token: 0x06000D1B RID: 3355 RVA: 0x00046A08 File Offset: 0x00044C08
	private void Event_Everyone_OnPlayerPositionDespawned(Dictionary<string, object> message)
	{
		PlayerPosition playerPosition = (PlayerPosition)message["playerPosition"];
		this.uiPositionSelect.RemovePosition(playerPosition);
	}

	// Token: 0x06000D1C RID: 3356 RVA: 0x00046A34 File Offset: 0x00044C34
	private void Event_Everyone_OnPlayerPositionClaimedByPlayerChanged(Dictionary<string, object> message)
	{
		PlayerPosition playerPosition = (PlayerPosition)message["playerPosition"];
		this.uiPositionSelect.StylePosition(playerPosition);
	}

	// Token: 0x06000D1D RID: 3357 RVA: 0x00046A60 File Offset: 0x00044C60
	private void Event_Everyone_OnPlayerSpawned(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (!player.IsLocalPlayer)
		{
			return;
		}
		this.HandlePlayerGameState(player);
	}

	// Token: 0x06000D1E RID: 3358 RVA: 0x00046A90 File Offset: 0x00044C90
	private void Event_Everyone_OnPlayerGameStateChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		PlayerGameState playerGameState = (PlayerGameState)message["oldGameState"];
		PlayerGameState playerGameState2 = (PlayerGameState)message["newGameState"];
		if (!player.IsLocalPlayer)
		{
			return;
		}
		if (playerGameState.Team == playerGameState2.Team)
		{
			return;
		}
		this.HandlePlayerGameState(player);
	}

	// Token: 0x040007D9 RID: 2009
	private UIPositionSelect uiPositionSelect;
}
