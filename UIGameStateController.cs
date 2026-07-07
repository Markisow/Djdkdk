using System;
using System.Collections.Generic;

// Token: 0x02000192 RID: 402
public class UIGameStateController : UIViewController<UIGameState>
{
	// Token: 0x06000BB2 RID: 2994 RVA: 0x00011908 File Offset: 0x0000FB08
	public override void Awake()
	{
		base.Awake();
		this.uiGameState = base.GetComponent<UIGameState>();
		EventManager.AddEventListener("Event_Everyone_OnGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGameStateChanged));
		EventManager.AddEventListener("Event_OnShowGameUserInterfaceChanged", new Action<Dictionary<string, object>>(this.Event_OnShowGameUserInterfaceChanged));
	}

	// Token: 0x06000BB3 RID: 2995 RVA: 0x00011948 File Offset: 0x0000FB48
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGameStateChanged));
		EventManager.RemoveEventListener("Event_OnShowGameUserInterfaceChanged", new Action<Dictionary<string, object>>(this.Event_OnShowGameUserInterfaceChanged));
		base.OnDestroy();
	}

	// Token: 0x06000BB4 RID: 2996 RVA: 0x00041B4C File Offset: 0x0003FD4C
	private void Event_Everyone_OnGameStateChanged(Dictionary<string, object> message)
	{
		GameState gameState = (GameState)message["newGameState"];
		this.uiGameState.SetPhase(Utils.GetHumanizedGamePhase(gameState.Phase, gameState.Period, gameState.IsOvertime));
		this.uiGameState.SetTick(gameState.Tick);
		this.uiGameState.SetScore(PlayerTeam.Blue, gameState.BlueScore);
		this.uiGameState.SetScore(PlayerTeam.Red, gameState.RedScore);
	}

	// Token: 0x06000BB5 RID: 2997 RVA: 0x0001197C File Offset: 0x0000FB7C
	private void Event_OnShowGameUserInterfaceChanged(Dictionary<string, object> message)
	{
		if (GlobalStateManager.UIState.Phase == UIPhase.LockerRoom)
		{
			return;
		}
		if ((bool)message["value"])
		{
			this.uiGameState.Show();
			return;
		}
		this.uiGameState.Hide();
	}

	// Token: 0x04000706 RID: 1798
	private UIGameState uiGameState;
}
