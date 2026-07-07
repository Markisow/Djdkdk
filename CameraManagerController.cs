using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

// Token: 0x0200009D RID: 157
public static class CameraManagerController
{
	// Token: 0x0600051D RID: 1309 RVA: 0x0002BA68 File Offset: 0x00029C68
	public static void Initialize()
	{
		EventManager.AddEventListener("Event_OnBaseCameraStarted", new Action<Dictionary<string, object>>(CameraManagerController.Event_OnBaseCameraStarted));
		EventManager.AddEventListener("Event_OnBaseCameraDestroyed", new Action<Dictionary<string, object>>(CameraManagerController.Event_OnBaseCameraDestroyed));
		EventManager.AddEventListener("Event_OnSceneLoaded", new Action<Dictionary<string, object>>(CameraManagerController.Event_OnSceneLoaded));
		EventManager.AddEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(CameraManagerController.Event_Everyone_OnPlayerSpawned));
		EventManager.AddEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(CameraManagerController.Event_Everyone_OnPlayerGameStateChanged));
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x0002BAE4 File Offset: 0x00029CE4
	public static void Dispose()
	{
		EventManager.RemoveEventListener("Event_OnBaseCameraStarted", new Action<Dictionary<string, object>>(CameraManagerController.Event_OnBaseCameraStarted));
		EventManager.RemoveEventListener("Event_OnBaseCameraDestroyed", new Action<Dictionary<string, object>>(CameraManagerController.Event_OnBaseCameraDestroyed));
		EventManager.RemoveEventListener("Event_OnSceneLoaded", new Action<Dictionary<string, object>>(CameraManagerController.Event_OnSceneLoaded));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerSpawned", new Action<Dictionary<string, object>>(CameraManagerController.Event_Everyone_OnPlayerSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(CameraManagerController.Event_Everyone_OnPlayerGameStateChanged));
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0002BB60 File Offset: 0x00029D60
	private static void HandlePlayerGameState(Player player)
	{
		PlayerGameState value = player.GameState.Value;
		switch (value.Phase)
		{
		case PlayerPhase.TeamSelect:
			CameraManager.SetActiveCamera(CameraType.Cinematic, null);
			return;
		case PlayerPhase.PositionSelect:
			if (value.Team == PlayerTeam.Blue)
			{
				CameraManager.SetActiveCamera(CameraType.BluePositionSelection, null);
				return;
			}
			if (value.Team == PlayerTeam.Red)
			{
				CameraManager.SetActiveCamera(CameraType.RedPositionSelection, null);
				return;
			}
			CameraManager.SetActiveCamera(CameraType.Cinematic, null);
			return;
		case PlayerPhase.Play:
			CameraManager.SetActiveCamera(CameraType.Player, new ulong?(player.OwnerClientId));
			return;
		case PlayerPhase.Replay:
			CameraManager.SetActiveCamera(CameraType.Replay, null);
			return;
		case PlayerPhase.Spectate:
			CameraManager.SetActiveCamera(CameraType.Spectator, new ulong?(player.OwnerClientId));
			return;
		default:
			CameraManager.SetActiveCamera(CameraType.Cinematic, null);
			return;
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x0000C28E File Offset: 0x0000A48E
	private static void Event_OnBaseCameraStarted(Dictionary<string, object> eventParams)
	{
		CameraManager.RegisterCamera((BaseCamera)eventParams["baseCamera"]);
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x0000C2A5 File Offset: 0x0000A4A5
	private static void Event_OnBaseCameraDestroyed(Dictionary<string, object> eventParams)
	{
		CameraManager.UnregisterCamera((BaseCamera)eventParams["baseCamera"]);
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x0002BC38 File Offset: 0x00029E38
	private static void Event_OnSceneLoaded(Dictionary<string, object> eventParams)
	{
		if (((Scene)eventParams["scene"]).name == "locker_room")
		{
			CameraManager.SetActiveCamera(CameraType.LockerRoom, null);
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x0002BC78 File Offset: 0x00029E78
	private static void Event_Everyone_OnPlayerSpawned(Dictionary<string, object> eventParams)
	{
		Player player = (Player)eventParams["player"];
		if (!player.IsLocalPlayer)
		{
			return;
		}
		CameraManagerController.HandlePlayerGameState(player);
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x0002BCA8 File Offset: 0x00029EA8
	private static void Event_Everyone_OnPlayerGameStateChanged(Dictionary<string, object> eventParams)
	{
		Player player = (Player)eventParams["player"];
		PlayerGameState playerGameState = (PlayerGameState)eventParams["oldGameState"];
		PlayerGameState playerGameState2 = (PlayerGameState)eventParams["newGameState"];
		if (!player.IsLocalPlayer)
		{
			return;
		}
		if (playerGameState.Phase == playerGameState2.Phase && playerGameState.Team == playerGameState2.Team)
		{
			return;
		}
		CameraManagerController.HandlePlayerGameState(player);
	}
}
