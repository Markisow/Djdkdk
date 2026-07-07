using System;
using System.Collections.Generic;

// Token: 0x020000B1 RID: 177
public static class GlobalStateManager
{
	// Token: 0x17000085 RID: 133
	// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000C7B5 File Offset: 0x0000A9B5
	// (set) Token: 0x06000599 RID: 1433 RVA: 0x0000C7BC File Offset: 0x0000A9BC
	public static UIState UIState
	{
		get
		{
			return GlobalStateManager.uiState;
		}
		set
		{
			if (GlobalStateManager.uiState.Equals(value))
			{
				return;
			}
			UIState oldUIState = GlobalStateManager.uiState;
			GlobalStateManager.uiState = value;
			GlobalStateManager.OnUIStateChanged(oldUIState, GlobalStateManager.uiState);
		}
	}

	// Token: 0x17000086 RID: 134
	// (get) Token: 0x0600059A RID: 1434 RVA: 0x0000C7E1 File Offset: 0x0000A9E1
	// (set) Token: 0x0600059B RID: 1435 RVA: 0x0000C7E8 File Offset: 0x0000A9E8
	public static ConnectionState ConnectionState
	{
		get
		{
			return GlobalStateManager.connectionState;
		}
		set
		{
			if (GlobalStateManager.connectionState.Equals(value))
			{
				return;
			}
			ConnectionState oldConnectionState = GlobalStateManager.connectionState;
			GlobalStateManager.connectionState = value;
			GlobalStateManager.OnConnectionStateChanged(oldConnectionState, GlobalStateManager.connectionState);
		}
	}

	// Token: 0x17000087 RID: 135
	// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000C80D File Offset: 0x0000AA0D
	// (set) Token: 0x0600059D RID: 1437 RVA: 0x0000C814 File Offset: 0x0000AA14
	public static ReconnectionState ReconnectionState
	{
		get
		{
			return GlobalStateManager.reconnectionState;
		}
		set
		{
			if (GlobalStateManager.reconnectionState.Equals(value))
			{
				return;
			}
			ReconnectionState oldReconnectionState = GlobalStateManager.reconnectionState;
			GlobalStateManager.reconnectionState = value;
			GlobalStateManager.OnReconnectionStateChanged(oldReconnectionState, GlobalStateManager.reconnectionState);
		}
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0000C839 File Offset: 0x0000AA39
	public static void Initialize()
	{
		GlobalStateManagerController.Initialize();
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x0000C840 File Offset: 0x0000AA40
	public static void Dispose()
	{
		GlobalStateManagerController.Dispose();
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x0002DC94 File Offset: 0x0002BE94
	public static void SetUIState(Dictionary<string, object> updates)
	{
		GlobalStateManager.UIState = new UIState
		{
			Phase = (updates.ContainsKey("phase") ? ((UIPhase)updates["phase"]) : GlobalStateManager.UIState.Phase),
			IsMouseRequired = (updates.ContainsKey("isMouseRequired") ? ((bool)updates["isMouseRequired"]) : GlobalStateManager.UIState.IsMouseRequired),
			IsMouseOverUI = (updates.ContainsKey("isMouseOverUI") ? ((bool)updates["isMouseOverUI"]) : GlobalStateManager.UIState.IsMouseOverUI),
			InteractingViews = (updates.ContainsKey("interactingViews") ? ((List<UIView>)updates["interactingViews"]) : GlobalStateManager.UIState.InteractingViews)
		};
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x0000C847 File Offset: 0x0000AA47
	public static void ClearUIState()
	{
		GlobalStateManager.UIState = new UIState();
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x0002DD70 File Offset: 0x0002BF70
	public static void SetConnectionState(Dictionary<string, object> updates)
	{
		GlobalStateManager.ConnectionState = new ConnectionState
		{
			Connection = (updates.ContainsKey("connection") ? ((Connection)updates["connection"]) : GlobalStateManager.ConnectionState.Connection),
			LastConnection = (updates.ContainsKey("lastConnection") ? ((Connection)updates["lastConnection"]) : GlobalStateManager.ConnectionState.LastConnection),
			ConnectionRejection = (updates.ContainsKey("connectionRejection") ? ((ConnectionRejection)updates["connectionRejection"]) : GlobalStateManager.ConnectionState.ConnectionRejection),
			Disconnection = (updates.ContainsKey("disconnection") ? ((Disconnection)updates["disconnection"]) : GlobalStateManager.ConnectionState.Disconnection),
			PendingConnection = (updates.ContainsKey("pendingConnection") ? ((Connection)updates["pendingConnection"]) : GlobalStateManager.ConnectionState.PendingConnection),
			Phase = (updates.ContainsKey("phase") ? ((ConnectionPhase)updates["phase"]) : GlobalStateManager.ConnectionState.Phase)
		};
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0000C853 File Offset: 0x0000AA53
	public static void ClearConnectionState()
	{
		GlobalStateManager.ConnectionState = new ConnectionState();
	}

	// Token: 0x060005A4 RID: 1444 RVA: 0x0002DEAC File Offset: 0x0002C0AC
	public static void SetReconnectionState(Dictionary<string, object> updates)
	{
		GlobalStateManager.ReconnectionState = new ReconnectionState
		{
			Phase = (updates.ContainsKey("phase") ? ((ReconnectionPhase)updates["phase"]) : GlobalStateManager.ReconnectionState.Phase),
			Password = (updates.ContainsKey("password") ? ((string)updates["password"]) : GlobalStateManager.ReconnectionState.Password),
			ClientRequiredModIds = (updates.ContainsKey("clientRequiredModIds") ? ((string[])updates["clientRequiredModIds"]) : GlobalStateManager.ReconnectionState.ClientRequiredModIds),
			PendingReadinessModIds = (updates.ContainsKey("pendingReadinessModIds") ? ((string[])updates["pendingReadinessModIds"]) : GlobalStateManager.ReconnectionState.PendingReadinessModIds),
			PendingEnablingModIds = (updates.ContainsKey("pendingEnablingModIds") ? ((string[])updates["pendingEnablingModIds"]) : GlobalStateManager.ReconnectionState.PendingEnablingModIds)
		};
	}

	// Token: 0x060005A5 RID: 1445 RVA: 0x0000C85F File Offset: 0x0000AA5F
	public static void ClearReconnectionState()
	{
		GlobalStateManager.ReconnectionState = new ReconnectionState();
	}

	// Token: 0x060005A6 RID: 1446 RVA: 0x0000C86B File Offset: 0x0000AA6B
	private static void OnUIStateChanged(UIState oldUIState, UIState newUIState)
	{
		EventManager.TriggerEvent("Event_OnUIStateChanged", new Dictionary<string, object>
		{
			{
				"oldUIState",
				oldUIState
			},
			{
				"newUIState",
				newUIState
			}
		});
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x0000C89E File Offset: 0x0000AA9E
	private static void OnConnectionStateChanged(ConnectionState oldConnectionState, ConnectionState newConnectionState)
	{
		EventManager.TriggerEvent("Event_OnConnectionStateChanged", new Dictionary<string, object>
		{
			{
				"oldConnectionState",
				oldConnectionState
			},
			{
				"newConnectionState",
				newConnectionState
			}
		});
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0002DFB8 File Offset: 0x0002C1B8
	private static void OnReconnectionStateChanged(ReconnectionState oldReconnectionState, ReconnectionState newReconnectionState)
	{
		GlobalStateManager.Logger.Info(string.Format("Reconnection state changed \nFrom:{{{0}}} \nTo:{{{1}}}", oldReconnectionState, newReconnectionState));
		EventManager.TriggerEvent("Event_OnReconnectionStateChanged", new Dictionary<string, object>
		{
			{
				"oldReconnectionState",
				oldReconnectionState
			},
			{
				"newReconnectionState",
				newReconnectionState
			}
		});
	}

	// Token: 0x0400037B RID: 891
	private static readonly Logger Logger = new Logger("GlobalStateManager");

	// Token: 0x0400037C RID: 892
	private static UIState uiState = new UIState();

	// Token: 0x0400037D RID: 893
	private static ConnectionState connectionState = new ConnectionState();

	// Token: 0x0400037E RID: 894
	private static ReconnectionState reconnectionState = new ReconnectionState();
}
