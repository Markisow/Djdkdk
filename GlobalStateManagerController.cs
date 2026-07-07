using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

// Token: 0x020000B2 RID: 178
public static class GlobalStateManagerController
{
	// Token: 0x060005AA RID: 1450 RVA: 0x0002E018 File Offset: 0x0002C218
	public static void Initialize()
	{
		EventManager.AddEventListener("Event_OnSceneLoaded", new Action<Dictionary<string, object>>(GlobalStateManagerController.Event_OnSceneLoaded));
		EventManager.AddEventListener("Event_OnPopupClickOk", new Action<Dictionary<string, object>>(GlobalStateManagerController.Event_OnPopupClickOk));
		EventManager.AddEventListener("Event_OnPopupClickClose", new Action<Dictionary<string, object>>(GlobalStateManagerController.Event_OnPopupClickClose));
		EventManager.AddEventListener("Event_OnModStateChanged", new Action<Dictionary<string, object>>(GlobalStateManagerController.Event_OnModStateChanged));
		EventManager.AddEventListener("Event_OnModEnableFailed", new Action<Dictionary<string, object>>(GlobalStateManagerController.Event_OnModEnableFailed));
	}

	// Token: 0x060005AB RID: 1451 RVA: 0x0002E094 File Offset: 0x0002C294
	public static void Dispose()
	{
		EventManager.RemoveEventListener("Event_OnSceneLoaded", new Action<Dictionary<string, object>>(GlobalStateManagerController.Event_OnSceneLoaded));
		EventManager.RemoveEventListener("Event_OnPopupClickOk", new Action<Dictionary<string, object>>(GlobalStateManagerController.Event_OnPopupClickOk));
		EventManager.RemoveEventListener("Event_OnPopupClickClose", new Action<Dictionary<string, object>>(GlobalStateManagerController.Event_OnPopupClickClose));
		EventManager.RemoveEventListener("Event_OnModStateChanged", new Action<Dictionary<string, object>>(GlobalStateManagerController.Event_OnModStateChanged));
		EventManager.RemoveEventListener("Event_OnModEnableFailed", new Action<Dictionary<string, object>>(GlobalStateManagerController.Event_OnModEnableFailed));
	}

	// Token: 0x060005AC RID: 1452 RVA: 0x0002E110 File Offset: 0x0002C310
	private static void Event_OnSceneLoaded(Dictionary<string, object> message)
	{
		if (((Scene)message["scene"]).name == "level_default")
		{
			GlobalStateManager.SetUIState(new Dictionary<string, object>
			{
				{
					"phase",
					UIPhase.Playing
				}
			});
			return;
		}
		GlobalStateManager.SetUIState(new Dictionary<string, object>
		{
			{
				"phase",
				UIPhase.LockerRoom
			}
		});
	}

	// Token: 0x060005AD RID: 1453 RVA: 0x0002E178 File Offset: 0x0002C378
	private static void Event_OnPopupClickOk(Dictionary<string, object> message)
	{
		Popup popup = (Popup)message["popup"];
		string name = popup.Name;
		if (!(name == "missingPassword"))
		{
			if (!(name == "missingMods"))
			{
				return;
			}
			if (GlobalStateManager.ReconnectionState.Phase == ReconnectionPhase.AwaitingMods)
			{
				string[] clientRequiredModIds = GlobalStateManager.ReconnectionState.ClientRequiredModIds;
				string[] second = (from mod in ModManager.EnabledMods
				select mod.Id).ToArray<string>();
				string[] second2 = (from mod in ModManager.ReadyMods
				select mod.Id).ToArray<string>();
				string[] array = clientRequiredModIds.Except(second2).ToArray<string>();
				string[] value = clientRequiredModIds.Except(array).Except(second).ToArray<string>();
				GlobalStateManager.SetReconnectionState(new Dictionary<string, object>
				{
					{
						"pendingReadinessModIds",
						array
					},
					{
						"pendingEnablingModIds",
						value
					}
				});
			}
		}
		else if (GlobalStateManager.ReconnectionState.Phase == ReconnectionPhase.AwaitingPassword)
		{
			PopupMissingPasswordContent popupMissingPasswordContent = (PopupMissingPasswordContent)popup.Content;
			if (string.IsNullOrEmpty(popupMissingPasswordContent.Password))
			{
				GlobalStateManager.ClearReconnectionState();
				return;
			}
			GlobalStateManager.SetReconnectionState(new Dictionary<string, object>
			{
				{
					"password",
					popupMissingPasswordContent.Password
				}
			});
			return;
		}
	}

	// Token: 0x060005AE RID: 1454 RVA: 0x0002E2C4 File Offset: 0x0002C4C4
	private static void Event_OnPopupClickClose(Dictionary<string, object> message)
	{
		string name = ((Popup)message["popup"]).Name;
		if ((name == "missingPassword" || name == "missingMods") && (GlobalStateManager.ReconnectionState.Phase == ReconnectionPhase.AwaitingPassword || GlobalStateManager.ReconnectionState.Phase == ReconnectionPhase.AwaitingMods))
		{
			GlobalStateManager.ClearReconnectionState();
		}
	}

	// Token: 0x060005AF RID: 1455 RVA: 0x0002E320 File Offset: 0x0002C520
	private static void Event_OnModStateChanged(Dictionary<string, object> message)
	{
		Mod mod = (Mod)message["mod"];
		BasePluginState basePluginState = (BasePluginState)message["oldState"];
		BasePluginState basePluginState2 = (BasePluginState)message["newState"];
		if (GlobalStateManager.ReconnectionState.Phase == ReconnectionPhase.AwaitingMods)
		{
			bool flag = GlobalStateManager.ReconnectionState.PendingReadinessModIds.Contains(mod.Id);
			bool flag2 = GlobalStateManager.ReconnectionState.PendingEnablingModIds.Contains(mod.Id);
			bool flag3 = basePluginState.IsEnabled != basePluginState2.IsEnabled;
			bool flag4 = basePluginState.IsReady != basePluginState2.IsReady;
			if (flag && flag4 && basePluginState2.IsReady)
			{
				GlobalStateManager.SetReconnectionState(new Dictionary<string, object>
				{
					{
						"pendingReadinessModIds",
						(from id in GlobalStateManager.ReconnectionState.PendingReadinessModIds
						where id != mod.Id
						select id).ToArray<string>()
					},
					{
						"pendingEnablingModIds",
						GlobalStateManager.ReconnectionState.PendingEnablingModIds.Append(mod.Id).ToArray<string>()
					}
				});
				return;
			}
			if (flag2 && flag3 && basePluginState2.IsEnabled)
			{
				GlobalStateManager.SetReconnectionState(new Dictionary<string, object>
				{
					{
						"pendingEnablingModIds",
						(from id in GlobalStateManager.ReconnectionState.PendingEnablingModIds
						where id != mod.Id
						select id).ToArray<string>()
					}
				});
			}
		}
	}

	// Token: 0x060005B0 RID: 1456 RVA: 0x0000C900 File Offset: 0x0000AB00
	private static void Event_OnModEnableFailed(Dictionary<string, object> message)
	{
		if (GlobalStateManager.ReconnectionState.Phase == ReconnectionPhase.AwaitingMods)
		{
			GlobalStateManager.ClearReconnectionState();
		}
	}

	// Token: 0x0400037F RID: 895
	private static readonly Logger Logger = new Logger("GlobalStateManagerController");
}
