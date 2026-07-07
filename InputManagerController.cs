using System;
using System.Collections.Generic;

// Token: 0x020000BF RID: 191
public static class InputManagerController
{
	// Token: 0x060005E1 RID: 1505 RVA: 0x0000CA78 File Offset: 0x0000AC78
	public static void Initialize()
	{
		if (!ApplicationManager.IsDedicatedGameServer)
		{
			InputManager.LoadKeyBinds();
		}
		InputManagerController.AddSettingsEventListeners();
	}

	// Token: 0x060005E2 RID: 1506 RVA: 0x0000CA8B File Offset: 0x0000AC8B
	public static void Dispose()
	{
		InputManagerController.RemoveSettingsEventListeners();
		if (!ApplicationManager.IsDedicatedGameServer)
		{
			InputManager.SaveKeyBinds();
		}
	}

	// Token: 0x060005E3 RID: 1507 RVA: 0x0002F568 File Offset: 0x0002D768
	private static void AddSettingsEventListeners()
	{
		EventManager.AddEventListener("Event_OnSettingsKeyBindInputClicked", new Action<Dictionary<string, object>>(InputManagerController.Event_OnSettingsKeyBindInputClicked));
		EventManager.AddEventListener("Event_OnSettingsKeyBindInputInteractionChanged", new Action<Dictionary<string, object>>(InputManagerController.Event_OnSettingsKeyBindInputInteractionChanged));
		EventManager.AddEventListener("Event_OnPopupClickOk", new Action<Dictionary<string, object>>(InputManagerController.Event_OnPopupClickOk));
	}

	// Token: 0x060005E4 RID: 1508 RVA: 0x0002F5B8 File Offset: 0x0002D7B8
	private static void RemoveSettingsEventListeners()
	{
		EventManager.RemoveEventListener("Event_OnSettingsKeyBindInputClicked", new Action<Dictionary<string, object>>(InputManagerController.Event_OnSettingsKeyBindInputClicked));
		EventManager.RemoveEventListener("Event_OnSettingsKeyBindInputInteractionChanged", new Action<Dictionary<string, object>>(InputManagerController.Event_OnSettingsKeyBindInputInteractionChanged));
		EventManager.RemoveEventListener("Event_OnPopupClickOk", new Action<Dictionary<string, object>>(InputManagerController.Event_OnPopupClickOk));
	}

	// Token: 0x060005E5 RID: 1509 RVA: 0x0000CA9E File Offset: 0x0000AC9E
	private static void Event_OnSettingsKeyBindInputClicked(Dictionary<string, object> message)
	{
		InputManager.RebindButtonInteractively((string)message["actionName"]);
	}

	// Token: 0x060005E6 RID: 1510 RVA: 0x0002F608 File Offset: 0x0002D808
	private static void Event_OnSettingsKeyBindInputInteractionChanged(Dictionary<string, object> message)
	{
		string actionName = (string)message["actionName"];
		KeyBindInteraction keyBindInteraction = (KeyBindInteraction)message["interaction"];
		InputManager.SetActionInteractions(actionName, Utils.GetInteractionFromKeyBindInteraction(keyBindInteraction));
	}

	// Token: 0x060005E7 RID: 1511 RVA: 0x0000CAB5 File Offset: 0x0000ACB5
	private static void Event_OnPopupClickOk(Dictionary<string, object> message)
	{
		if (((Popup)message["popup"]).Name == "settingsResetToDefault")
		{
			InputManager.ResetToDefault();
		}
	}
}
