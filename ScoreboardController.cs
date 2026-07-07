using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class ScoreboardController : MonoBehaviour
{
	// Token: 0x06000360 RID: 864 RVA: 0x0000B145 File Offset: 0x00009345
	private void Awake()
	{
		this.scoreboard = base.GetComponent<Scoreboard>();
		EventManager.AddEventListener("Event_Everyone_OnGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGameStateChanged));
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0000B169 File Offset: 0x00009369
	private void Start()
	{
		this.scoreboard.TurnOff();
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0000B176 File Offset: 0x00009376
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGameStateChanged));
	}

	// Token: 0x06000363 RID: 867 RVA: 0x000249DC File Offset: 0x00022BDC
	private void Event_Everyone_OnGameStateChanged(Dictionary<string, object> message)
	{
		GameState gameState = (GameState)message["newGameState"];
		GamePhase phase = gameState.Phase;
		if (phase - GamePhase.PreGame <= 8)
		{
			this.scoreboard.TurnOn();
		}
		else
		{
			this.scoreboard.TurnOff();
		}
		this.scoreboard.SetTick(gameState.Tick);
		this.scoreboard.SetPeriod(gameState.Period);
		this.scoreboard.SetBlueScore(gameState.BlueScore);
		this.scoreboard.SetRedScore(gameState.RedScore);
	}

	// Token: 0x0400025E RID: 606
	private Scoreboard scoreboard;
}
