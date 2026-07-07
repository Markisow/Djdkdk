using System;
using UnityEngine;

// Token: 0x020000C6 RID: 198
public static class LifecycleManager
{
	// Token: 0x06000615 RID: 1557 RVA: 0x0000CD10 File Offset: 0x0000AF10
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void SubsystemRegistration()
	{
		LogManager.Initialize();
		Application.quitting += LifecycleManager.Dispose;
	}

	// Token: 0x06000616 RID: 1558 RVA: 0x0000CD28 File Offset: 0x0000AF28
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	private static void BeforeSceneLoad()
	{
		PatchManager.Initialize();
		EventManager.Initialize();
		GlobalStateManager.Initialize();
		SaveManager.Initialize();
		InputManager.Initialize();
		SettingsManager.Initialize();
		ApplicationManager.Initialize();
		BackendManager.Initialize();
		ItemManager.Initialize();
		CameraManager.Initialize();
	}

	// Token: 0x06000617 RID: 1559 RVA: 0x0000CD5C File Offset: 0x0000AF5C
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	private static void AfterSceneLoad()
	{
		SceneManager.Initialize();
		WebSocketManager.Initialize();
		SteamManager.Initialize();
		ModManager.Initialize();
	}

	// Token: 0x06000618 RID: 1560 RVA: 0x0002F7B0 File Offset: 0x0002D9B0
	public static void Dispose()
	{
		ModManager.Dispose();
		SteamManager.Dispose();
		WebSocketManager.Dispose();
		SceneManager.Dispose();
		CameraManager.Dispose();
		ItemManager.Dispose();
		BackendManager.Dispose();
		ApplicationManager.Dispose();
		SettingsManager.Dispose();
		InputManager.Dispose();
		SaveManager.Dispose();
		GlobalStateManager.Dispose();
		EventManager.Dispose();
		PatchManager.Dispose();
		LogManager.Dispose();
	}
}
