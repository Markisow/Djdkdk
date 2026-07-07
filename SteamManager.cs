using System;
using System.Net;
using DG.Tweening;
using Steamworks;

// Token: 0x0200013B RID: 315
public static class SteamManager
{
	// Token: 0x06000967 RID: 2407 RVA: 0x0000FDE2 File Offset: 0x0000DFE2
	public static void Initialize()
	{
		SteamManagerController.Initialize();
		SteamManager.InitializeSteam();
	}

	// Token: 0x06000968 RID: 2408 RVA: 0x0000FDEE File Offset: 0x0000DFEE
	public static void Dispose()
	{
		SteamManager.DisposeSteam();
		SteamManagerController.Dispose();
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x000392D0 File Offset: 0x000374D0
	private static void InitializeSteam()
	{
		if (SteamManager.IsInitialized)
		{
			return;
		}
		SteamManager.Logger.Info("Initializing Steam");
		EventManager.TriggerEvent("Event_OnSteamInitializationStarted", null);
		if (ApplicationManager.IsDedicatedGameServer)
		{
			SteamManager.IsInitialized = GameServer.Init(BitConverter.ToUInt32(IPAddress.Any.GetAddressBytes(), 0), 0, 0, EServerMode.eServerModeNoAuthentication, null);
		}
		else
		{
			SteamManager.IsInitialized = SteamAPI.Init();
		}
		if (!SteamManager.IsInitialized)
		{
			if (ApplicationManager.IsDedicatedGameServer)
			{
				SteamManager.Logger.Info("Failed to initialize as game server");
			}
			else
			{
				SteamManager.Logger.Info("Failed to initialize as client");
			}
			EventManager.TriggerEvent("Event_OnSteamInitializationFailed", null);
			Tween tween = SteamManager.steamInitializationRetryTween;
			if (tween != null)
			{
				tween.Kill(false);
			}
			SteamManager.steamInitializationRetryTween = DOVirtual.DelayedCall(5f, delegate
			{
				SteamManager.Logger.Info("Retrying Steam initialization");
				SteamManager.InitializeSteam();
			}, true);
			return;
		}
		SteamManager.RegisterCallbacks();
		SteamManager.StartCallbackLoop();
		if (ApplicationManager.IsDedicatedGameServer)
		{
			SteamManager.Logger.Info("Initialized as game server");
			EventManager.TriggerEvent("Event_OnSteamInitialized", null);
			SteamGameServer.LogOnAnonymous();
			return;
		}
		SteamManager.Logger.Info("Initialized as client");
		EventManager.TriggerEvent("Event_OnSteamInitialized", null);
		SteamManager.OnSteamServersConnected(default(SteamServersConnected_t));
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0000FDFA File Offset: 0x0000DFFA
	private static void DisposeSteam()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		Tween tween = SteamManager.steamInitializationRetryTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		if (ApplicationManager.IsDedicatedGameServer)
		{
			GameServer.Shutdown();
		}
		else
		{
			SteamAPI.Shutdown();
		}
		SteamManager.StopCallbackLoop();
		SteamManager.UnregisterCallbacks();
		SteamManager.IsInitialized = false;
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x00039404 File Offset: 0x00037604
	private static void RegisterCallbacks()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		if (ApplicationManager.IsDedicatedGameServer)
		{
			SteamManager.steamServersConnectedCallback = Callback<SteamServersConnected_t>.CreateGameServer(new Callback<SteamServersConnected_t>.DispatchDelegate(SteamManager.OnSteamServersConnected));
			SteamManager.steamServerConnectFailureCallback = Callback<SteamServerConnectFailure_t>.CreateGameServer(new Callback<SteamServerConnectFailure_t>.DispatchDelegate(SteamManager.OnSteamServerConnectFailure));
			SteamManager.steamServersDisconnectedCallback = Callback<SteamServersDisconnected_t>.CreateGameServer(new Callback<SteamServersDisconnected_t>.DispatchDelegate(SteamManager.OnSteamServersDisconnected));
			return;
		}
		SteamManager.steamServersConnectedCallback = Callback<SteamServersConnected_t>.Create(new Callback<SteamServersConnected_t>.DispatchDelegate(SteamManager.OnSteamServersConnected));
		SteamManager.steamServerConnectFailureCallback = Callback<SteamServerConnectFailure_t>.Create(new Callback<SteamServerConnectFailure_t>.DispatchDelegate(SteamManager.OnSteamServerConnectFailure));
		SteamManager.steamServersDisconnectedCallback = Callback<SteamServersDisconnected_t>.Create(new Callback<SteamServersDisconnected_t>.DispatchDelegate(SteamManager.OnSteamServersDisconnected));
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0000FE38 File Offset: 0x0000E038
	private static void UnregisterCallbacks()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		SteamManager.steamServersConnectedCallback.Unregister();
		SteamManager.steamServerConnectFailureCallback.Unregister();
		SteamManager.steamServersDisconnectedCallback.Unregister();
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x000394A8 File Offset: 0x000376A8
	private static void StartCallbackLoop()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		Tween tween = SteamManager.callbackTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		SteamManager.callbackTween = DOVirtual.DelayedCall(SteamManager.RunCallbackInterval, delegate
		{
			if (ApplicationManager.IsDedicatedGameServer)
			{
				GameServer.RunCallbacks();
			}
			else
			{
				SteamAPI.RunCallbacks();
			}
			SteamManager.StartCallbackLoop();
		}, true);
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0000FE60 File Offset: 0x0000E060
	private static void StopCallbackLoop()
	{
		if (!SteamManager.IsInitialized)
		{
			return;
		}
		Tween tween = SteamManager.callbackTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		SteamManager.callbackTween = null;
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0000FE81 File Offset: 0x0000E081
	private static void OnSteamServersConnected(SteamServersConnected_t callback)
	{
		SteamManager.Logger.Info("Connected to Steam");
		SteamIntegrationManager.Initialize();
		SteamWorkshopManager.Initialize();
		SteamManager.IsConnected = true;
		EventManager.TriggerEvent("Event_OnSteamConnected", null);
	}

	// Token: 0x06000970 RID: 2416 RVA: 0x0000FEAD File Offset: 0x0000E0AD
	private static void OnSteamServerConnectFailure(SteamServerConnectFailure_t callback)
	{
		SteamManager.Logger.Error(string.Format("Failed to connect to Steam: {0}", callback.m_eResult));
		EventManager.TriggerEvent("Event_OnSteamConnectionFailed", null);
	}

	// Token: 0x06000971 RID: 2417 RVA: 0x0000FED9 File Offset: 0x0000E0D9
	private static void OnSteamServersDisconnected(SteamServersDisconnected_t callback)
	{
		SteamManager.Logger.Warning(string.Format("Disconnected from Steam: {0}", callback.m_eResult));
		SteamManager.IsConnected = false;
		SteamWorkshopManager.Dispose();
		SteamIntegrationManager.Dispose();
		EventManager.TriggerEvent("Event_OnSteamDisconnected", null);
	}

	// Token: 0x04000580 RID: 1408
	private static readonly Logger Logger = new Logger("SteamManager");

	// Token: 0x04000581 RID: 1409
	public static bool IsInitialized = false;

	// Token: 0x04000582 RID: 1410
	public static float RunCallbackInterval = 0.033333335f;

	// Token: 0x04000583 RID: 1411
	public static bool IsConnected = false;

	// Token: 0x04000584 RID: 1412
	private static Callback<SteamServersConnected_t> steamServersConnectedCallback;

	// Token: 0x04000585 RID: 1413
	private static Callback<SteamServerConnectFailure_t> steamServerConnectFailureCallback;

	// Token: 0x04000586 RID: 1414
	private static Callback<SteamServersDisconnected_t> steamServersDisconnectedCallback;

	// Token: 0x04000587 RID: 1415
	private static Tween callbackTween;

	// Token: 0x04000588 RID: 1416
	private static Tween steamInitializationRetryTween;
}
