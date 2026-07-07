using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x020000AB RID: 171
public class GameModeManagerController : MonoBehaviour
{
	// Token: 0x0600057B RID: 1403 RVA: 0x0002D7EC File Offset: 0x0002B9EC
	private void Awake()
	{
		this.gameModeManager = base.GetComponent<GameModeManager>();
		EventManager.AddEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.AddEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
		EventManager.AddEventListener("Event_Server_OnLoadSceneEventCompleted", new Action<Dictionary<string, object>>(this.Event_Server_OnLoadSceneEventCompleted));
		EventManager.AddEventListener("Event_Everyone_OnLevelSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnLevelSpawned));
		EventManager.AddEventListener("Event_Everyone_OnLevelDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnLevelDespawned));
	}

	// Token: 0x0600057C RID: 1404 RVA: 0x0002D874 File Offset: 0x0002BA74
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_Server_OnServerStarted", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStarted));
		EventManager.RemoveEventListener("Event_Server_OnServerStopped", new Action<Dictionary<string, object>>(this.Event_Server_OnServerStopped));
		EventManager.RemoveEventListener("Event_Server_OnLoadSceneEventCompleted", new Action<Dictionary<string, object>>(this.Event_Server_OnLoadSceneEventCompleted));
		EventManager.RemoveEventListener("Event_Everyone_OnLevelSpawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnLevelSpawned));
		EventManager.RemoveEventListener("Event_Everyone_OnLevelDespawned", new Action<Dictionary<string, object>>(this.Event_Everyone_OnLevelDespawned));
	}

	// Token: 0x0600057D RID: 1405 RVA: 0x0002D8F0 File Offset: 0x0002BAF0
	private void Event_Server_OnServerStarted(Dictionary<string, object> message)
	{
		ServerConfig serverConfig = (ServerConfig)message["serverConfig"];
		this.gameModeManager.SelectGameMode(serverConfig.gameMode);
	}

	// Token: 0x0600057E RID: 1406 RVA: 0x0000C58B File Offset: 0x0000A78B
	private void Event_Server_OnServerStopped(Dictionary<string, object> message)
	{
		this.gameModeManager.DisableSelectedGameMode();
		this.gameModeManager.DeselectGameMode();
	}

	// Token: 0x0600057F RID: 1407 RVA: 0x0000C5A3 File Offset: 0x0000A7A3
	private void Event_Server_OnLoadSceneEventCompleted(Dictionary<string, object> message)
	{
		this.gameModeManager.EnableSelectedGameMode();
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x0002D920 File Offset: 0x0002BB20
	private void Event_Everyone_OnLevelSpawned(Dictionary<string, object> message)
	{
		Level level = (Level)message["level"];
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.gameModeManager.Level = level;
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
	private void Event_Everyone_OnLevelDespawned(Dictionary<string, object> message)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		this.gameModeManager.Level = null;
	}

	// Token: 0x04000363 RID: 867
	private GameModeManager gameModeManager;
}
