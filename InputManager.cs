using System;
using System.Collections.Generic;
using System.Text.Json;
using UnityEngine.InputSystem;

// Token: 0x020000B9 RID: 185
public static class InputManager
{
	// Token: 0x060005C6 RID: 1478 RVA: 0x0002E5CC File Offset: 0x0002C7CC
	public static void Initialize()
	{
		InputSystem.RegisterInteraction<DoublePressInteraction>(null);
		InputSystem.RegisterInteraction<ToggleInteraction>(null);
		InputActionAsset actions = InputSystem.actions;
		InputManager.MoveForwardAction = actions.FindAction("Move Forward", false);
		InputManager.MoveBackwardAction = actions.FindAction("Move Backward", false);
		InputManager.TurnLeftAction = actions.FindAction("Turn Left", false);
		InputManager.TurnRightAction = actions.FindAction("Turn Right", false);
		InputManager.StickAction = actions.FindAction("Stick", false);
		InputManager.BladeAngleUpAction = actions.FindAction("Blade Angle Up", false);
		InputManager.BladeAngleDownAction = actions.FindAction("Blade Angle Down", false);
		InputManager.SlideAction = actions.FindAction("Slide", false);
		InputManager.SprintAction = actions.FindAction("Sprint", false);
		InputManager.TrackAction = actions.FindAction("Track", false);
		InputManager.LookAction = actions.FindAction("Look", false);
		InputManager.JumpAction = actions.FindAction("Jump", false);
		InputManager.StopAction = actions.FindAction("Stop", false);
		InputManager.TwistLeftAction = actions.FindAction("Twist Left", false);
		InputManager.TwistRightAction = actions.FindAction("Twist Right", false);
		InputManager.DashLeftAction = actions.FindAction("Dash Left", false);
		InputManager.DashRightAction = actions.FindAction("Dash Right", false);
		InputManager.ExtendLeftAction = actions.FindAction("Extend Left", false);
		InputManager.ExtendRightAction = actions.FindAction("Extend Right", false);
		InputManager.LateralLeftAction = actions.FindAction("Lateral Left", false);
		InputManager.LateralRightAction = actions.FindAction("Lateral Right", false);
		InputManager.TalkAction = actions.FindAction("Talk", false);
		InputManager.AllChatAction = actions.FindAction("All Chat", false);
		InputManager.TeamChatAction = actions.FindAction("Team Chat", false);
		InputManager.PauseAction = actions.FindAction("Pause", false);
		InputManager.PositionSelectAction = actions.FindAction("Position Select", false);
		InputManager.ScoreboardAction = actions.FindAction("Scoreboard", false);
		InputManager.QuickChat1Action = actions.FindAction("Quick Chat 1", false);
		InputManager.QuickChat2Action = actions.FindAction("Quick Chat 2", false);
		InputManager.QuickChat3Action = actions.FindAction("Quick Chat 3", false);
		InputManager.QuickChat4Action = actions.FindAction("Quick Chat 4", false);
		InputManager.QuickChat5Action = actions.FindAction("Quick Chat 5", false);
		InputManager.Debug1Action = actions.FindAction("Debug 1", false);
		InputManager.Debug2Action = actions.FindAction("Debug 2", false);
		InputManager.Debug3Action = actions.FindAction("Debug 3", false);
		InputManager.Debug4Action = actions.FindAction("Debug 4", false);
		InputManager.PointAction = actions.FindAction("Point", false);
		InputManager.ClickAction = actions.FindAction("Click", false);
		InputManager.InputActions = new List<InputAction>
		{
			InputManager.MoveForwardAction,
			InputManager.MoveBackwardAction,
			InputManager.TurnLeftAction,
			InputManager.TurnRightAction,
			InputManager.StickAction,
			InputManager.BladeAngleUpAction,
			InputManager.BladeAngleDownAction,
			InputManager.SlideAction,
			InputManager.SprintAction,
			InputManager.TrackAction,
			InputManager.LookAction,
			InputManager.JumpAction,
			InputManager.StopAction,
			InputManager.TwistLeftAction,
			InputManager.TwistRightAction,
			InputManager.DashLeftAction,
			InputManager.DashRightAction,
			InputManager.ExtendLeftAction,
			InputManager.ExtendRightAction,
			InputManager.LateralLeftAction,
			InputManager.LateralRightAction,
			InputManager.TalkAction,
			InputManager.AllChatAction,
			InputManager.TeamChatAction,
			InputManager.PauseAction,
			InputManager.PositionSelectAction,
			InputManager.ScoreboardAction,
			InputManager.QuickChat1Action,
			InputManager.QuickChat2Action,
			InputManager.QuickChat3Action,
			InputManager.QuickChat4Action,
			InputManager.QuickChat5Action,
			InputManager.Debug1Action,
			InputManager.Debug2Action,
			InputManager.Debug3Action,
			InputManager.Debug4Action,
			InputManager.PointAction,
			InputManager.ClickAction
		};
		InputManager.RebindableInputActions = new List<InputAction>
		{
			InputManager.MoveForwardAction,
			InputManager.MoveBackwardAction,
			InputManager.TurnLeftAction,
			InputManager.TurnRightAction,
			InputManager.BladeAngleUpAction,
			InputManager.BladeAngleDownAction,
			InputManager.SlideAction,
			InputManager.SprintAction,
			InputManager.TrackAction,
			InputManager.LookAction,
			InputManager.JumpAction,
			InputManager.StopAction,
			InputManager.TwistLeftAction,
			InputManager.TwistRightAction,
			InputManager.DashLeftAction,
			InputManager.DashRightAction,
			InputManager.ExtendLeftAction,
			InputManager.ExtendRightAction,
			InputManager.LateralLeftAction,
			InputManager.LateralRightAction,
			InputManager.TalkAction,
			InputManager.AllChatAction,
			InputManager.TeamChatAction,
			InputManager.PositionSelectAction,
			InputManager.ScoreboardAction
		};
		InputManagerController.Initialize();
	}

	// Token: 0x060005C7 RID: 1479 RVA: 0x0000C9CC File Offset: 0x0000ABCC
	public static void Dispose()
	{
		InputManagerController.Dispose();
	}

	// Token: 0x060005C8 RID: 1480 RVA: 0x0002EB40 File Offset: 0x0002CD40
	public static void LoadKeyBinds()
	{
		try
		{
			string @string = SaveManager.GetString("keyBinds", null);
			if (string.IsNullOrEmpty(@string))
			{
				throw new Exception("No saved key binds found");
			}
			Dictionary<string, KeyBind> dictionary = JsonSerializer.Deserialize<Dictionary<string, KeyBind>>(@string, null);
			List<string> list = new List<string>();
			foreach (InputAction inputAction in InputManager.RebindableInputActions)
			{
				if (!dictionary.ContainsKey(inputAction.name))
				{
					list.Add(inputAction.name);
				}
			}
			if (list.Count > 0)
			{
				throw new Exception("Missing keys in loaded key binds (" + string.Join(", ", list) + ")");
			}
			InputManager.KeyBinds.Clear();
			foreach (KeyValuePair<string, KeyBind> keyValuePair in dictionary)
			{
				string actionName = keyValuePair.Key;
				KeyBind value = keyValuePair.Value;
				InputAction inputAction2 = InputManager.RebindableInputActions.Find((InputAction action) => action.name == actionName);
				value.InputAction = inputAction2;
				if (value.InputAction == null)
				{
					InputManager.Logger.Warning("Cannot load key bind for " + actionName + " because it is not rebindable");
				}
				else
				{
					InputManager.KeyBinds.Add(actionName, value);
					InputManager.ApplyKeyBind(value);
				}
			}
			InputManager.Logger.Info(string.Format("Loaded {0} key binds: {1}", InputManager.KeyBinds.Count, @string));
			EventManager.TriggerEvent("Event_OnKeyBindsLoaded", new Dictionary<string, object>
			{
				{
					"keyBinds",
					InputManager.KeyBinds
				}
			});
		}
		catch (Exception ex)
		{
			InputManager.Logger.Warning("Failed to load key binds: " + ex.Message);
			InputManager.SaveKeyBinds();
			InputManager.LoadKeyBinds();
		}
	}

	// Token: 0x060005C9 RID: 1481 RVA: 0x0002ED6C File Offset: 0x0002CF6C
	public static void ApplyKeyBinds()
	{
		foreach (KeyBind keyBind in InputManager.KeyBinds.Values)
		{
			InputManager.ApplyKeyBind(keyBind);
		}
	}

	// Token: 0x060005CA RID: 1482 RVA: 0x0000C9D3 File Offset: 0x0000ABD3
	public static void ApplyKeyBind(KeyBind keyBind)
	{
		string name = keyBind.InputAction.name;
		InputManager.RebindAction(name, keyBind.ModifierPath, keyBind.Path);
		InputManager.SetActionInteractions(name, keyBind.Interactions);
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x0002EDC0 File Offset: 0x0002CFC0
	public static void SaveKeyBinds()
	{
		try
		{
			foreach (InputAction inputAction in InputManager.RebindableInputActions)
			{
				if (!InputManager.KeyBinds.ContainsKey(inputAction.name))
				{
					KeyBind value = new KeyBind(inputAction);
					InputManager.KeyBinds.Add(inputAction.name, value);
				}
				else
				{
					InputManager.KeyBinds[inputAction.name].Update(inputAction);
				}
			}
			string text = JsonSerializer.Serialize<Dictionary<string, KeyBind>>(InputManager.KeyBinds, new JsonSerializerOptions
			{
				WriteIndented = true
			});
			SaveManager.SetString("keyBinds", text);
			InputManager.Logger.Info(string.Format("Saved {0} key binds: {1}", InputManager.KeyBinds.Count, text));
			EventManager.TriggerEvent("Event_OnKeyBindsSaved", new Dictionary<string, object>
			{
				{
					"keyBinds",
					InputManager.KeyBinds
				}
			});
		}
		catch (Exception ex)
		{
			InputManager.Logger.Error("Failed to save key binds: " + ex.Message);
		}
	}

	// Token: 0x060005CC RID: 1484 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
	public static void ResetToDefault()
	{
		InputManager.Logger.Info("Resetting key binds to default");
		foreach (InputAction action in InputManager.RebindableInputActions)
		{
			action.RemoveAllBindingOverrides();
		}
		InputManager.SaveKeyBinds();
	}

	// Token: 0x060005CD RID: 1485 RVA: 0x0002EF44 File Offset: 0x0002D144
	public static void RebindButtonInteractively(string actionName)
	{
		InputManager.<>c__DisplayClass49_0 CS$<>8__locals1 = new InputManager.<>c__DisplayClass49_0();
		CS$<>8__locals1.actionName = actionName;
		CS$<>8__locals1.inputAction = InputManager.RebindableInputActions.Find((InputAction action) => action.name == CS$<>8__locals1.actionName);
		if (CS$<>8__locals1.inputAction == null)
		{
			InputManager.Logger.Warning("Cannot rebind action " + CS$<>8__locals1.actionName + " because it is not rebindable");
			return;
		}
		CS$<>8__locals1.inputAction.Disable();
		InputActionRebindingExtensions.RebindingOperation rebindingOperation = CS$<>8__locals1.<RebindButtonInteractively>g__GenerateRebindingOperation|1();
		CS$<>8__locals1.rebindingOperation = CS$<>8__locals1.<RebindButtonInteractively>g__GenerateRebindingOperation|1();
		bool isComposite = CS$<>8__locals1.inputAction.bindings[0].isComposite;
		CS$<>8__locals1.interactions = CS$<>8__locals1.inputAction.bindings[0].effectiveInteractions;
		InputManager.Logger.Info("Rebinding " + CS$<>8__locals1.actionName);
		if (isComposite)
		{
			InputManager.Logger.Info("Rebinding " + CS$<>8__locals1.actionName + " as composite");
			rebindingOperation.WithTargetBinding(1).OnComplete(delegate(InputActionRebindingExtensions.RebindingOperation modifierOperation)
			{
				string modifierPath = modifierOperation.action.bindings[1].effectivePath;
				InputManager.Logger.Info("Rebound " + CS$<>8__locals1.actionName + " modifierPath to " + modifierPath);
				CS$<>8__locals1.rebindingOperation.WithControlsExcluding(modifierPath).WithTargetBinding(2).WithTimeout(0.5f).OnComplete(delegate(InputActionRebindingExtensions.RebindingOperation operation)
				{
					CS$<>8__locals1.inputAction.Enable();
					string effectivePath = operation.action.bindings[2].effectivePath;
					InputManager.RebindAction(CS$<>8__locals1.actionName, modifierPath, effectivePath);
					InputManager.SetActionInteractions(CS$<>8__locals1.actionName, CS$<>8__locals1.interactions);
					InputManager.Logger.Info(string.Concat(new string[]
					{
						"Rebound ",
						CS$<>8__locals1.actionName,
						" to ",
						modifierPath,
						" + ",
						effectivePath
					}));
					EventManager.TriggerEvent("Event_OnKeyBindRebindComplete", new Dictionary<string, object>
					{
						{
							"actionName",
							CS$<>8__locals1.actionName
						}
					});
					InputManager.SaveKeyBinds();
				}).OnCancel(delegate(InputActionRebindingExtensions.RebindingOperation operation)
				{
					CS$<>8__locals1.inputAction.Enable();
					InputManager.RebindAction(CS$<>8__locals1.actionName, null, modifierPath);
					InputManager.SetActionInteractions(CS$<>8__locals1.actionName, CS$<>8__locals1.interactions);
					InputManager.Logger.Info("Rebinding " + CS$<>8__locals1.actionName + " path was cancelled, using modifier path as path " + modifierPath);
					EventManager.TriggerEvent("Event_OnKeyBindRebindComplete", new Dictionary<string, object>
					{
						{
							"actionName",
							CS$<>8__locals1.actionName
						}
					});
					InputManager.SaveKeyBinds();
				}).Start();
			}).OnCancel(delegate(InputActionRebindingExtensions.RebindingOperation operation)
			{
				CS$<>8__locals1.inputAction.Enable();
				InputManager.Logger.Info("Rebinding " + CS$<>8__locals1.actionName + " was cancelled");
				EventManager.TriggerEvent("Event_OnKeyBindRebindCancel", new Dictionary<string, object>
				{
					{
						"actionName",
						CS$<>8__locals1.actionName
					}
				});
			});
		}
		else
		{
			rebindingOperation.OnComplete(delegate(InputActionRebindingExtensions.RebindingOperation operation)
			{
				CS$<>8__locals1.inputAction.Enable();
				string effectivePath = operation.action.bindings[0].effectivePath;
				InputManager.RebindAction(CS$<>8__locals1.actionName, effectivePath, null);
				InputManager.SetActionInteractions(CS$<>8__locals1.actionName, CS$<>8__locals1.interactions);
				InputManager.Logger.Info("Rebound " + CS$<>8__locals1.actionName + " to " + effectivePath);
				EventManager.TriggerEvent("Event_OnKeyBindRebindComplete", new Dictionary<string, object>
				{
					{
						"actionName",
						CS$<>8__locals1.actionName
					}
				});
				InputManager.SaveKeyBinds();
			}).OnCancel(delegate(InputActionRebindingExtensions.RebindingOperation operation)
			{
				CS$<>8__locals1.inputAction.Enable();
				InputManager.Logger.Info("Rebinding " + CS$<>8__locals1.actionName + " was cancelled");
				EventManager.TriggerEvent("Event_OnKeyBindRebindCancel", new Dictionary<string, object>
				{
					{
						"actionName",
						CS$<>8__locals1.actionName
					}
				});
			});
		}
		rebindingOperation.Start();
		EventManager.TriggerEvent("Event_OnKeyBindRebindStart", new Dictionary<string, object>
		{
			{
				"actionName",
				CS$<>8__locals1.actionName
			},
			{
				"isComposite",
				isComposite
			}
		});
	}

	// Token: 0x060005CE RID: 1486 RVA: 0x0002F0C8 File Offset: 0x0002D2C8
	public static void RebindAction(string actionName, string modifierPath = null, string path = null)
	{
		InputAction inputAction = InputManager.RebindableInputActions.Find((InputAction action) => action.name == actionName);
		if (inputAction == null)
		{
			InputManager.Logger.Warning("Cannot rebind action " + actionName + " because it is not rebindable");
			return;
		}
		if (inputAction.bindings[0].isComposite)
		{
			inputAction.ApplyBindingOverride(1, new InputBinding
			{
				overridePath = modifierPath
			});
			inputAction.ApplyBindingOverride(2, new InputBinding
			{
				overridePath = path
			});
			return;
		}
		inputAction.ApplyBindingOverride(0, new InputBinding
		{
			overridePath = path
		});
	}

	// Token: 0x060005CF RID: 1487 RVA: 0x0002F180 File Offset: 0x0002D380
	public static void SetActionInteractions(string actionName, string interactions)
	{
		InputAction inputAction = InputManager.RebindableInputActions.Find((InputAction action) => action.name == actionName);
		if (inputAction == null)
		{
			InputManager.Logger.Warning("Cannot set interactions for action " + actionName + " because it is not rebindable");
			return;
		}
		inputAction.ApplyBindingOverride(0, new InputBinding
		{
			overrideInteractions = interactions
		});
	}

	// Token: 0x04000396 RID: 918
	private static readonly Logger Logger = new Logger("InputManager");

	// Token: 0x04000397 RID: 919
	public static InputAction MoveForwardAction;

	// Token: 0x04000398 RID: 920
	public static InputAction MoveBackwardAction;

	// Token: 0x04000399 RID: 921
	public static InputAction TurnLeftAction;

	// Token: 0x0400039A RID: 922
	public static InputAction TurnRightAction;

	// Token: 0x0400039B RID: 923
	public static InputAction StickAction;

	// Token: 0x0400039C RID: 924
	public static InputAction BladeAngleUpAction;

	// Token: 0x0400039D RID: 925
	public static InputAction BladeAngleDownAction;

	// Token: 0x0400039E RID: 926
	public static InputAction SlideAction;

	// Token: 0x0400039F RID: 927
	public static InputAction SprintAction;

	// Token: 0x040003A0 RID: 928
	public static InputAction TrackAction;

	// Token: 0x040003A1 RID: 929
	public static InputAction LookAction;

	// Token: 0x040003A2 RID: 930
	public static InputAction JumpAction;

	// Token: 0x040003A3 RID: 931
	public static InputAction StopAction;

	// Token: 0x040003A4 RID: 932
	public static InputAction TwistLeftAction;

	// Token: 0x040003A5 RID: 933
	public static InputAction TwistRightAction;

	// Token: 0x040003A6 RID: 934
	public static InputAction DashLeftAction;

	// Token: 0x040003A7 RID: 935
	public static InputAction DashRightAction;

	// Token: 0x040003A8 RID: 936
	public static InputAction ExtendLeftAction;

	// Token: 0x040003A9 RID: 937
	public static InputAction ExtendRightAction;

	// Token: 0x040003AA RID: 938
	public static InputAction LateralLeftAction;

	// Token: 0x040003AB RID: 939
	public static InputAction LateralRightAction;

	// Token: 0x040003AC RID: 940
	public static InputAction TalkAction;

	// Token: 0x040003AD RID: 941
	public static InputAction AllChatAction;

	// Token: 0x040003AE RID: 942
	public static InputAction TeamChatAction;

	// Token: 0x040003AF RID: 943
	public static InputAction PauseAction;

	// Token: 0x040003B0 RID: 944
	public static InputAction PositionSelectAction;

	// Token: 0x040003B1 RID: 945
	public static InputAction ScoreboardAction;

	// Token: 0x040003B2 RID: 946
	public static InputAction QuickChat1Action;

	// Token: 0x040003B3 RID: 947
	public static InputAction QuickChat2Action;

	// Token: 0x040003B4 RID: 948
	public static InputAction QuickChat3Action;

	// Token: 0x040003B5 RID: 949
	public static InputAction QuickChat4Action;

	// Token: 0x040003B6 RID: 950
	public static InputAction QuickChat5Action;

	// Token: 0x040003B7 RID: 951
	public static InputAction Debug1Action;

	// Token: 0x040003B8 RID: 952
	public static InputAction Debug2Action;

	// Token: 0x040003B9 RID: 953
	public static InputAction Debug3Action;

	// Token: 0x040003BA RID: 954
	public static InputAction Debug4Action;

	// Token: 0x040003BB RID: 955
	public static InputAction PointAction;

	// Token: 0x040003BC RID: 956
	public static InputAction ClickAction;

	// Token: 0x040003BD RID: 957
	public static Dictionary<string, KeyBind> KeyBinds = new Dictionary<string, KeyBind>();

	// Token: 0x040003BE RID: 958
	public static List<InputAction> InputActions = new List<InputAction>();

	// Token: 0x040003BF RID: 959
	public static List<InputAction> RebindableInputActions = new List<InputAction>();
}
