using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000012 RID: 18
public class LevelController : MonoBehaviour
{
	// Token: 0x0600004F RID: 79 RVA: 0x00008D20 File Offset: 0x00006F20
	public virtual void Awake()
	{
		this.level = base.GetComponent<Level>();
		EventManager.AddEventListener("Event_Everyone_OnGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGameStateChanged));
	}

	// Token: 0x06000050 RID: 80 RVA: 0x00008D44 File Offset: 0x00006F44
	public virtual void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGameStateChanged));
	}

	// Token: 0x06000051 RID: 81 RVA: 0x00015FB0 File Offset: 0x000141B0
	private void Event_Everyone_OnGameStateChanged(Dictionary<string, object> eventParams)
	{
		ref GameState ptr = (GameState)eventParams["oldGameState"];
		GameState gameState = (GameState)eventParams["newGameState"];
		if (ptr.Phase == gameState.Phase)
		{
			return;
		}
		switch (gameState.Phase)
		{
		case GamePhase.Warmup:
		case GamePhase.PreGame:
		case GamePhase.FaceOff:
			this.level.SetBlueGoalLightEnabled(false);
			this.level.SetRedGoalLightEnabled(false);
			return;
		case GamePhase.Play:
			break;
		case GamePhase.BlueScore:
			this.level.SetRedGoalLightEnabled(true);
			return;
		case GamePhase.RedScore:
			this.level.SetBlueGoalLightEnabled(true);
			break;
		default:
			return;
		}
	}

	// Token: 0x0400002C RID: 44
	private Level level;
}
