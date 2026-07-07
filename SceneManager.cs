using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.SceneManagement;

// Token: 0x02000117 RID: 279
public static class SceneManager
{
	// Token: 0x170000D5 RID: 213
	// (get) Token: 0x060007AC RID: 1964 RVA: 0x0000DD6C File Offset: 0x0000BF6C
	public static bool IsNetworkSceneManagerAvailable
	{
		get
		{
			return NetworkManager.Singleton != null && NetworkManager.Singleton.SceneManager != null && NetworkManager.Singleton.IsServer;
		}
	}

	// Token: 0x060007AD RID: 1965 RVA: 0x0000DD93 File Offset: 0x0000BF93
	public static void Initialize()
	{
		UnityEngine.SceneManagement.SceneManager.sceneLoaded += global::SceneManager.OnSceneLoaded;
		UnityEngine.SceneManagement.SceneManager.sceneUnloaded += global::SceneManager.OnSceneUnloaded;
		SceneManagerController.Initialize();
	}

	// Token: 0x060007AE RID: 1966 RVA: 0x0000DDBC File Offset: 0x0000BFBC
	public static void Dispose()
	{
		UnityEngine.SceneManagement.SceneManager.sceneLoaded -= global::SceneManager.OnSceneLoaded;
		UnityEngine.SceneManagement.SceneManager.sceneUnloaded -= global::SceneManager.OnSceneUnloaded;
		SceneManagerController.Dispose();
	}

	// Token: 0x060007AF RID: 1967 RVA: 0x0000DDE5 File Offset: 0x0000BFE5
	public static void InitializeServer()
	{
		if (!global::SceneManager.IsNetworkSceneManagerAvailable)
		{
			return;
		}
		global::SceneManager.IsSceneLoadInProgress = false;
		global::SceneManager.IsInitialSceneLoaded = false;
		NetworkManager.Singleton.SceneManager.OnSceneEvent += global::SceneManager.Server_OnSceneEvent;
	}

	// Token: 0x060007B0 RID: 1968 RVA: 0x0000DE16 File Offset: 0x0000C016
	public static void DisposeServer()
	{
		if (!global::SceneManager.IsNetworkSceneManagerAvailable)
		{
			return;
		}
		NetworkManager.Singleton.SceneManager.OnSceneEvent -= global::SceneManager.Server_OnSceneEvent;
		global::SceneManager.IsSceneLoadInProgress = false;
		global::SceneManager.IsInitialSceneLoaded = false;
	}

	// Token: 0x060007B1 RID: 1969 RVA: 0x00033D84 File Offset: 0x00031F84
	public static void LoadScene(string sceneName)
	{
		if (global::SceneManager.IsNetworkSceneManagerAvailable)
		{
			global::SceneManager.Logger.Info("Loading server scene " + sceneName);
			NetworkManager.Singleton.SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
			return;
		}
		global::SceneManager.Logger.Info("Loading scene " + sceneName);
		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
	}

	// Token: 0x060007B2 RID: 1970 RVA: 0x0000DE47 File Offset: 0x0000C047
	private static void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
	{
		EventManager.TriggerEvent("Event_OnSceneLoaded", new Dictionary<string, object>
		{
			{
				"scene",
				scene
			}
		});
	}

	// Token: 0x060007B3 RID: 1971 RVA: 0x0000DE69 File Offset: 0x0000C069
	private static void OnSceneUnloaded(Scene scene)
	{
		EventManager.TriggerEvent("Event_OnSceneUnloaded", new Dictionary<string, object>
		{
			{
				"scene",
				scene
			}
		});
	}

	// Token: 0x060007B4 RID: 1972 RVA: 0x00033DDC File Offset: 0x00031FDC
	private static void Server_OnSceneEvent(SceneEvent sceneEvent)
	{
		switch (sceneEvent.SceneEventType)
		{
		case SceneEventType.Load:
			global::SceneManager.Server_OnLoadScene();
			return;
		case SceneEventType.Unload:
		case SceneEventType.Synchronize:
		case SceneEventType.ReSynchronize:
		case SceneEventType.UnloadEventCompleted:
			break;
		case SceneEventType.LoadEventCompleted:
			global::SceneManager.Server_OnLoadSceneEventCompleted(sceneEvent.ClientsThatCompleted, sceneEvent.ClientsThatTimedOut);
			break;
		case SceneEventType.LoadComplete:
			global::SceneManager.Server_OnClientSceneLoadComplete(sceneEvent.ClientId);
			return;
		case SceneEventType.UnloadComplete:
			global::SceneManager.Server_OnClientSceneUnloadComplete(sceneEvent.ClientId);
			return;
		case SceneEventType.SynchronizeComplete:
			global::SceneManager.Server_OnClientSceneSynchronizeComplete(sceneEvent.ClientId);
			return;
		default:
			return;
		}
	}

	// Token: 0x060007B5 RID: 1973 RVA: 0x0000DE8B File Offset: 0x0000C08B
	private static void Server_OnLoadScene()
	{
		global::SceneManager.IsSceneLoadInProgress = true;
		global::SceneManager.Logger.Info("Server started loading scene");
		EventManager.TriggerEvent("Event_Server_OnLoadScene", null);
	}

	// Token: 0x060007B6 RID: 1974 RVA: 0x0000DEAD File Offset: 0x0000C0AD
	private static void Server_OnClientSceneLoadComplete(ulong clientId)
	{
		global::SceneManager.Logger.Info(string.Format("Client {0} completed scene load", clientId));
		EventManager.TriggerEvent("Event_Server_OnClientSceneLoadComplete", new Dictionary<string, object>
		{
			{
				"clientId",
				clientId
			}
		});
	}

	// Token: 0x060007B7 RID: 1975 RVA: 0x0000DEE9 File Offset: 0x0000C0E9
	private static void Server_OnClientSceneUnloadComplete(ulong clientId)
	{
		global::SceneManager.Logger.Info(string.Format("Client {0} completed scene unload", clientId));
		EventManager.TriggerEvent("Event_Server_OnClientSceneUnloadComplete", new Dictionary<string, object>
		{
			{
				"clientId",
				clientId
			}
		});
	}

	// Token: 0x060007B8 RID: 1976 RVA: 0x0000DF25 File Offset: 0x0000C125
	private static void Server_OnClientSceneSynchronizeComplete(ulong clientId)
	{
		global::SceneManager.Logger.Info(string.Format("Client {0} completed scene synchronization", clientId));
		EventManager.TriggerEvent("Event_Server_OnClientSceneSynchronizeComplete", new Dictionary<string, object>
		{
			{
				"clientId",
				clientId
			}
		});
	}

	// Token: 0x060007B9 RID: 1977 RVA: 0x00033E58 File Offset: 0x00032058
	private static void Server_OnLoadSceneEventCompleted(List<ulong> clientsThatCompleted, List<ulong> clientsThatTimedOut)
	{
		global::SceneManager.Logger.Info("Scene load event completed on server");
		global::SceneManager.IsSceneLoadInProgress = false;
		bool flag = !global::SceneManager.IsInitialSceneLoaded;
		global::SceneManager.IsInitialSceneLoaded = true;
		EventManager.TriggerEvent("Event_Server_OnLoadSceneEventCompleted", new Dictionary<string, object>
		{
			{
				"clientsThatCompleted",
				clientsThatCompleted
			},
			{
				"clientsThatTimedOut",
				clientsThatTimedOut
			},
			{
				"isInitialScene",
				flag
			}
		});
	}

	// Token: 0x040004B9 RID: 1209
	private static readonly Logger Logger = new Logger("SceneManager");

	// Token: 0x040004BA RID: 1210
	public static bool IsSceneLoadInProgress;

	// Token: 0x040004BB RID: 1211
	public static bool IsInitialSceneLoaded;
}
