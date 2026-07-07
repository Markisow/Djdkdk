using System;
using System.Collections.Generic;
using DG.Tweening;
using UI;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

// Token: 0x020001CE RID: 462
public class UISettings : UIView
{
	// Token: 0x06000D7B RID: 3451 RVA: 0x00048614 File Offset: 0x00046814
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("SettingsView", null);
		this.settings = base.View.Query("Settings", null);
		this.closeIconButton = this.settings.Query("CloseIconButtonContainer", null).First().Query(null, null);
		this.closeIconButton.clicked += this.OnClickClose;
		this.cameraAngleSlider = this.settings.Query("CameraAngleSlider", null).First().Query(null, null);
		this.cameraAngleSlider.value = SettingsManager.CameraAngle;
		this.cameraAngleSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnCameraAngleChanged));
		this.handednessDropdown = this.settings.Query("HandednessDropdown", null).First().Query(null, null);
		this.handednessDropdown.value = Utils.GetNameFromHandedness(SettingsManager.Handedness);
		this.handednessDropdown.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnHandednessChanged));
		this.showPuckSilhouetteToggle = this.settings.Query("ShowPuckSilhouetteToggle", null).First().Query(null, null);
		this.showPuckSilhouetteToggle.value = SettingsManager.ShowPuckSilhouette;
		this.showPuckSilhouetteToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnShowPuckSilhouetteChanged));
		this.showPuckOutlineToggle = this.settings.Query("ShowPuckOutlineToggle", null).First().Query(null, null);
		this.showPuckOutlineToggle.value = SettingsManager.ShowPuckOutline;
		this.showPuckOutlineToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnShowPuckOutlineChanged));
		this.showPuckElevationToggle = this.settings.Query("ShowPuckElevationToggle", null).First().Query(null, null);
		this.showPuckElevationToggle.value = SettingsManager.ShowPuckElevation;
		this.showPuckElevationToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnShowPuckEleveationChanged));
		this.showPlayerUsernamesToggle = this.settings.Query("ShowPlayerUsernamesToggle", null).First().Query(null, null);
		this.showPlayerUsernamesToggle.value = SettingsManager.ShowPlayerUsernames;
		this.showPlayerUsernamesToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnShowPlayerUsernamesChanged));
		this.playerUsernamesFadeThresholdSlider = this.settings.Query("PlayerUsernamesFadeThresholdSlider", null).First().Query(null, null);
		this.playerUsernamesFadeThresholdSlider.value = SettingsManager.PlayerUsernamesFadeThreshold;
		this.playerUsernamesFadeThresholdSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnPlayerUsernamesFadeThresholdChanged));
		this.useNetworkSmoothingToggle = this.settings.Query("UseNetworkSmoothingToggle", null).First().Query(null, null);
		this.useNetworkSmoothingToggle.value = SettingsManager.UseNetworkSmoothing;
		this.useNetworkSmoothingToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnUseNetworkSmoothingChanged));
		this.networkSmoothingStrengthSliderInt = this.settings.Query("NetworkSmoothingStrengthSliderInt", null).First().Query(null, null);
		this.networkSmoothingStrengthSliderInt.value = SettingsManager.NetworkSmoothingStrength;
		this.networkSmoothingStrengthSliderInt.RegisterValueChangedCallback(new EventCallback<ChangeEvent<int>>(this.OnNetworkSmoothingStrengthChanged));
		this.maxMatchmakingPingSliderInt = this.settings.Query("MaxMatchmakingPingSliderInt", null).First().Query(null, null);
		this.maxMatchmakingPingSliderInt.value = SettingsManager.MaxMatchmakingPing;
		this.maxMatchmakingPingSliderInt.RegisterValueChangedCallback(new EventCallback<ChangeEvent<int>>(this.OnMaxMatchmakingPingChanged));
		this.filterChatProfanityToggle = this.settings.Query("FilterChatProfanityToggle", null).First().Query(null, null);
		this.filterChatProfanityToggle.value = SettingsManager.FilterChatProfanity;
		this.filterChatProfanityToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnFilterChatProfanityChanged));
		this.unitsDropdown = this.settings.Query("UnitsDropdown", null).First().Query(null, null);
		this.unitsDropdown.value = Utils.GetNameFromUnits(SettingsManager.Units);
		this.unitsDropdown.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnUnitsChanged));
		this.showGameUserInterfaceToggle = this.settings.Query("ShowGameUserInterfaceToggle", null).First().Query(null, null);
		this.showGameUserInterfaceToggle.value = SettingsManager.ShowGameUserInterface;
		this.showGameUserInterfaceToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnShowGameUserInterfaceChanged));
		this.userInterfaceScaleSlider = this.settings.Query("UserInterfaceScaleSlider", null).First().Query(null, null);
		this.userInterfaceScaleSlider.value = SettingsManager.UserInterfaceScale;
		this.userInterfaceScaleSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnUserInterfaceScaleChanged));
		this.chatOpacitySlider = this.settings.Query("ChatOpacitySlider", null).First().Query(null, null);
		this.chatOpacitySlider.value = SettingsManager.ChatOpacity;
		this.chatOpacitySlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnChatOpacityChanged));
		this.chatScaleSlider = this.settings.Query("ChatScaleSlider", null).First().Query(null, null);
		this.chatScaleSlider.value = SettingsManager.ChatScale;
		this.chatScaleSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnChatScaleChanged));
		this.minimapOpacitySlider = this.settings.Query("MinimapOpacitySlider", null).First().Query(null, null);
		this.minimapOpacitySlider.value = SettingsManager.MinimapOpacity;
		this.minimapOpacitySlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnMinimapOpacityChanged));
		this.minimapBackgroundOpacitySlider = this.settings.Query("MinimapBackgroundOpacitySlider", null).First().Query(null, null);
		this.minimapBackgroundOpacitySlider.value = SettingsManager.MinimapBackgroundOpacity;
		this.minimapBackgroundOpacitySlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnMinimapBackgroundOpacityChanged));
		this.minimapHorizontalPositionSlider = this.settings.Query("MinimapHorizontalPositionSlider", null).First().Query(null, null);
		this.minimapHorizontalPositionSlider.value = SettingsManager.MinimapHorizontalPosition;
		this.minimapHorizontalPositionSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnMinimapHorizontalPositionChanged));
		this.minimapVerticalPositionSlider = this.settings.Query("MinimapVerticalPositionSlider", null).First().Query(null, null);
		this.minimapVerticalPositionSlider.value = SettingsManager.MinimapVerticalPosition;
		this.minimapVerticalPositionSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnMinimapVerticalPositionChanged));
		this.minimapScaleSlider = this.settings.Query("MinimapScaleSlider", null).First().Query(null, null);
		this.minimapScaleSlider.value = SettingsManager.MinimapScale;
		this.minimapScaleSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnMinimapScaleChanged));
		this.globalStickSensitivitySlider = this.settings.Query("GlobalStickSensitivitySlider", null).First().Query(null, null);
		this.globalStickSensitivitySlider.value = SettingsManager.GlobalStickSensitivity;
		this.globalStickSensitivitySlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnGlobalStickSensitivityChanged));
		this.horizontalStickSensitivitySlider = this.settings.Query("HorizontalStickSensitivitySlider", null).First().Query(null, null);
		this.horizontalStickSensitivitySlider.value = SettingsManager.HorizontalStickSensitivity;
		this.horizontalStickSensitivitySlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnHorizontalStickSensitivityChanged));
		this.verticalStickSensitivitySlider = this.settings.Query("VerticalStickSensitivitySlider", null).First().Query(null, null);
		this.verticalStickSensitivitySlider.value = SettingsManager.VerticalStickSensitivity;
		this.verticalStickSensitivitySlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnVerticalStickSensitivityChanged));
		this.lookSensitivitySlider = this.settings.Query("LookSensitivitySlider", null).First().Query(null, null);
		this.lookSensitivitySlider.value = SettingsManager.LookSensitivity;
		this.lookSensitivitySlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnLookSensitivityChanged));
		this.actionNameKeyBindFieldMap = new Dictionary<string, KeyBindField>
		{
			{
				"Move Forward",
				this.settings.Query("MoveForwardKeyBindInput", null).First().Query(null, null)
			},
			{
				"Move Backward",
				this.settings.Query("MoveBackwardKeyBindInput", null).First().Query(null, null)
			},
			{
				"Turn Left",
				this.settings.Query("TurnLeftKeyBindInput", null).First().Query(null, null)
			},
			{
				"Turn Right",
				this.settings.Query("TurnRightKeyBindInput", null).First().Query(null, null)
			},
			{
				"Blade Angle Up",
				this.settings.Query("BladeAngleUpKeyBindInput", null).First().Query(null, null)
			},
			{
				"Blade Angle Down",
				this.settings.Query("BladeAngleDownKeyBindInput", null).First().Query(null, null)
			},
			{
				"Slide",
				this.settings.Query("SlideKeyBindInput", null).First().Query(null, null)
			},
			{
				"Sprint",
				this.settings.Query("SprintKeyBindInput", null).First().Query(null, null)
			},
			{
				"Track",
				this.settings.Query("TrackKeyBindInput", null).First().Query(null, null)
			},
			{
				"Look",
				this.settings.Query("LookKeyBindInput", null).First().Query(null, null)
			},
			{
				"Jump",
				this.settings.Query("JumpKeyBindInput", null).First().Query(null, null)
			},
			{
				"Stop",
				this.settings.Query("StopKeyBindInput", null).First().Query(null, null)
			},
			{
				"Twist Left",
				this.settings.Query("TwistLeftKeyBindInput", null).First().Query(null, null)
			},
			{
				"Twist Right",
				this.settings.Query("TwistRightKeyBindInput", null).First().Query(null, null)
			},
			{
				"Dash Left",
				this.settings.Query("DashLeftKeyBindInput", null).First().Query(null, null)
			},
			{
				"Dash Right",
				this.settings.Query("DashRightKeyBindInput", null).First().Query(null, null)
			},
			{
				"Extend Left",
				this.settings.Query("ExtendLeftKeyBindInput", null).First().Query(null, null)
			},
			{
				"Extend Right",
				this.settings.Query("ExtendRightKeyBindInput", null).First().Query(null, null)
			},
			{
				"Lateral Left",
				this.settings.Query("LateralLeftKeyBindInput", null).First().Query(null, null)
			},
			{
				"Lateral Right",
				this.settings.Query("LateralRightKeyBindInput", null).First().Query(null, null)
			},
			{
				"Talk",
				this.settings.Query("TalkKeyBindInput", null).First().Query(null, null)
			},
			{
				"All Chat",
				this.settings.Query("AllChatKeyBindInput", null).First().Query(null, null)
			},
			{
				"Team Chat",
				this.settings.Query("TeamChatKeyBindInput", null).First().Query(null, null)
			},
			{
				"Position Select",
				this.settings.Query("PositionSelectKeyBindInput", null).First().Query(null, null)
			},
			{
				"Scoreboard",
				this.settings.Query("ScoreboardKeyBindInput", null).First().Query(null, null)
			}
		};
		this.UpdateKeyBindInputs(InputManager.KeyBinds);
		foreach (KeyValuePair<string, KeyBindField> keyValuePair in this.actionNameKeyBindFieldMap)
		{
			string actionName = keyValuePair.Key;
			KeyBindField value = keyValuePair.Value;
			value.Click = delegate()
			{
				this.OnKeyBindInputClicked(actionName);
			};
			value.InteractionChange = delegate(KeyBindInteraction interaction)
			{
				this.OnKeyBindInputInteractionChanged(actionName, interaction);
			};
		}
		this.globalVolumeSlider = this.settings.Query("GlobalVolumeSlider", null).First().Query(null, null);
		this.globalVolumeSlider.value = SettingsManager.GlobalVolume;
		this.globalVolumeSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnGlobalVolumeChanged));
		this.ambientVolumeSlider = this.settings.Query("AmbientVolumeSlider", null).First().Query(null, null);
		this.ambientVolumeSlider.value = SettingsManager.AmbientVolume;
		this.ambientVolumeSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnAmbientVolumeChanged));
		this.gameVolumeSlider = this.settings.Query("GameVolumeSlider", null).First().Query(null, null);
		this.gameVolumeSlider.value = SettingsManager.GameVolume;
		this.gameVolumeSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnGameVolumeChanged));
		this.voiceVolumeSlider = this.settings.Query("VoiceVolumeSlider", null).First().Query(null, null);
		this.voiceVolumeSlider.value = SettingsManager.VoiceVolume;
		this.voiceVolumeSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnVoiceVolumeChanged));
		this.uiVolumeSlider = this.settings.Query("UIVolumeSlider", null).First().Query(null, null);
		this.uiVolumeSlider.value = SettingsManager.UIVolume;
		this.uiVolumeSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnUIVolumeChanged));
		this.fullScreenModeDropdown = this.settings.Query("FullScreenModeDropdown", null).First().Query(null, null);
		this.fullScreenModeDropdown.choices = Utils.GetFullScreenModeNames();
		this.fullScreenModeDropdown.value = Utils.GetNameFromFullScreenMode(SettingsManager.FullScreenMode);
		this.fullScreenModeDropdown.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnFullScreenModeChanged));
		this.displayDropdown = this.settings.Query("DisplayDropdown", null).First().Query(null, null);
		this.displayDropdown.choices = Utils.GetDisplayNames();
		this.displayDropdown.value = Utils.GetDisplayNameFromIndex(SettingsManager.DisplayIndex);
		this.displayDropdown.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnDisplayChanged));
		this.resolutionDropdown = this.settings.Query("ResolutionDropdown", null).First().Query(null, null);
		this.resolutionDropdown.choices = Utils.GetResolutionNames();
		this.resolutionDropdown.value = Utils.GetResolutionNameFromIndex(SettingsManager.ResolutionIndex);
		this.resolutionDropdown.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnResolutionChanged));
		this.vSyncToggle = this.settings.Query("VSyncToggle", null).First().Query(null, null);
		this.vSyncToggle.value = SettingsManager.VSync;
		this.vSyncToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnVSyncChanged));
		this.fpsLimitSlider = this.settings.Query("FPSLimitSlider", null).First().Query(null, null);
		this.fpsLimitSlider.value = (float)SettingsManager.FpsLimit;
		this.fpsLimitSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnFpsLimitChanged));
		this.fovSlider = this.settings.Query("FOVSlider", null).First().Query(null, null);
		this.fovSlider.value = SettingsManager.Fov;
		this.fovSlider.RegisterValueChangedCallback(new EventCallback<ChangeEvent<float>>(this.OnFovChanged));
		this.qualityDropdown = this.settings.Query("QualityDropdown", null).First().Query(null, null);
		this.qualityDropdown.choices = Utils.GetApplicationQualityNames();
		this.qualityDropdown.value = Utils.GetNameFromApplicationQuality(SettingsManager.Quality);
		this.qualityDropdown.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnQualityChanged));
		this.motionBlurToggle = this.settings.Query("MotionBlurToggle", null).First().Query(null, null);
		this.motionBlurToggle.value = SettingsManager.MotionBlur;
		this.motionBlurToggle.RegisterValueChangedCallback(new EventCallback<ChangeEvent<bool>>(this.OnMotionBlurChanged));
		this.resetToDefaultButton = this.settings.Query("ResetToDefaultButton", null);
		this.resetToDefaultButton.clicked += this.OnClickResetToDefault;
	}

	// Token: 0x06000D7C RID: 3452 RVA: 0x00012FF4 File Offset: 0x000111F4
	private void OnClickClose()
	{
		EventManager.TriggerEvent("Event_OnSettingsClickClose", null);
	}

	// Token: 0x06000D7D RID: 3453 RVA: 0x00013001 File Offset: 0x00011201
	private void OnClickResetToDefault()
	{
		EventManager.TriggerEvent("Event_OnSettingsClickResetToDefault", null);
	}

	// Token: 0x06000D7E RID: 3454 RVA: 0x000498C8 File Offset: 0x00047AC8
	public void UpdateKeyBindInputs(Dictionary<string, KeyBind> keyBinds)
	{
		foreach (KeyValuePair<string, KeyBind> keyValuePair in keyBinds)
		{
			string key = keyValuePair.Key;
			KeyBind value = keyValuePair.Value;
			if (this.actionNameKeyBindFieldMap.ContainsKey(key))
			{
				KeyBindField keyBindField = this.actionNameKeyBindFieldMap[key];
				if (value.IsComposite)
				{
					string text = null;
					if (!string.IsNullOrEmpty(value.ModifierPath))
					{
						text = text + value.InputAction.GetBindingDisplayString(1, InputBinding.DisplayStringOptions.DontIncludeInteractions).ToUpper() + "+";
					}
					text += value.InputAction.GetBindingDisplayString(2, InputBinding.DisplayStringOptions.DontIncludeInteractions).ToUpper();
					keyBindField.Path = text;
				}
				else
				{
					keyBindField.Path = value.InputAction.GetBindingDisplayString(0, InputBinding.DisplayStringOptions.DontIncludeInteractions).ToUpper();
				}
				KeyBindInteraction keyBindInteractionFromInteraction = Utils.GetKeyBindInteractionFromInteraction(value.Interactions, keyBindField.InteractionType);
				keyBindField.Interaction = keyBindInteractionFromInteraction;
			}
		}
	}

	// Token: 0x06000D7F RID: 3455 RVA: 0x0001300E File Offset: 0x0001120E
	private void OnCameraAngleChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsCameraAngleChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D80 RID: 3456 RVA: 0x00013035 File Offset: 0x00011235
	public void UpdateCameraAngle(float value)
	{
		this.cameraAngleSlider.value = value;
	}

	// Token: 0x06000D81 RID: 3457 RVA: 0x00013043 File Offset: 0x00011243
	private void OnHandednessChanged(ChangeEvent<string> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsHandednessChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D82 RID: 3458 RVA: 0x00013065 File Offset: 0x00011265
	public void UpdateHandedness(string value)
	{
		this.handednessDropdown.value = value;
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x00013073 File Offset: 0x00011273
	private void OnShowPuckSilhouetteChanged(ChangeEvent<bool> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsShowPuckSilhouetteChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0001309A File Offset: 0x0001129A
	public void UpdateShowPuckSilhouette(bool value)
	{
		this.showPuckSilhouetteToggle.value = value;
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x000130A8 File Offset: 0x000112A8
	private void OnShowPuckOutlineChanged(ChangeEvent<bool> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsShowPuckOutlineChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x000130CF File Offset: 0x000112CF
	public void UpdateShowPuckOutline(bool value)
	{
		this.showPuckOutlineToggle.value = value;
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x000130DD File Offset: 0x000112DD
	private void OnShowPuckEleveationChanged(ChangeEvent<bool> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsShowPuckElevationChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x00013104 File Offset: 0x00011304
	public void UpdateShowPuckElevation(bool value)
	{
		this.showPuckElevationToggle.value = value;
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x00013112 File Offset: 0x00011312
	private void OnShowPlayerUsernamesChanged(ChangeEvent<bool> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsShowPlayerUsernamesChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x00013139 File Offset: 0x00011339
	public void UpdateShowPlayerUsernames(bool value)
	{
		this.showPlayerUsernamesToggle.value = value;
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x00013147 File Offset: 0x00011347
	private void OnPlayerUsernamesFadeThresholdChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsPlayerUsernamesFadeThresholdChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0001316E File Offset: 0x0001136E
	public void UpdatePlayerUsernamesFadeThreshold(float value)
	{
		this.playerUsernamesFadeThresholdSlider.value = value;
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x0001317C File Offset: 0x0001137C
	private void OnUseNetworkSmoothingChanged(ChangeEvent<bool> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsUseNetworkSmoothingChanged", new Dictionary<string, object>
		{
			{
				"value",
				this.useNetworkSmoothingToggle.value
			}
		});
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x000131A8 File Offset: 0x000113A8
	public void UpdateUseNetworkSmoothing(bool value)
	{
		this.useNetworkSmoothingToggle.value = value;
	}

	// Token: 0x06000D8F RID: 3471 RVA: 0x000131B6 File Offset: 0x000113B6
	private void OnNetworkSmoothingStrengthChanged(ChangeEvent<int> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsNetworkSmoothingStrengthChanged", new Dictionary<string, object>
		{
			{
				"value",
				this.networkSmoothingStrengthSliderInt.value
			}
		});
	}

	// Token: 0x06000D90 RID: 3472 RVA: 0x000131E2 File Offset: 0x000113E2
	public void UpdateNetworkSmoothingStrength(int value)
	{
		this.networkSmoothingStrengthSliderInt.value = value;
	}

	// Token: 0x06000D91 RID: 3473 RVA: 0x000131F0 File Offset: 0x000113F0
	private void OnMaxMatchmakingPingChanged(ChangeEvent<int> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsMaxMatchmakingPingChanged", new Dictionary<string, object>
		{
			{
				"value",
				this.maxMatchmakingPingSliderInt.value
			}
		});
	}

	// Token: 0x06000D92 RID: 3474 RVA: 0x0001321C File Offset: 0x0001141C
	public void UpdateMaxMatchmakingPing(int value)
	{
		this.maxMatchmakingPingSliderInt.value = value;
	}

	// Token: 0x06000D93 RID: 3475 RVA: 0x0001322A File Offset: 0x0001142A
	private void OnFilterChatProfanityChanged(ChangeEvent<bool> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsFilterChatProfanityChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D94 RID: 3476 RVA: 0x00013251 File Offset: 0x00011451
	public void UpdateFilterChatProfanity(bool value)
	{
		this.filterChatProfanityToggle.value = value;
	}

	// Token: 0x06000D95 RID: 3477 RVA: 0x0001325F File Offset: 0x0001145F
	private void OnUnitsChanged(ChangeEvent<string> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsUnitsChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D96 RID: 3478 RVA: 0x00013281 File Offset: 0x00011481
	public void UpdateUnits(string value)
	{
		this.unitsDropdown.value = value;
	}

	// Token: 0x06000D97 RID: 3479 RVA: 0x0001328F File Offset: 0x0001148F
	private void OnShowGameUserInterfaceChanged(ChangeEvent<bool> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsShowGameUserInterfaceChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D98 RID: 3480 RVA: 0x000132B6 File Offset: 0x000114B6
	public void UpdateShowGameUserInterface(bool value)
	{
		this.showGameUserInterfaceToggle.value = value;
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x000499D8 File Offset: 0x00047BD8
	private void OnUserInterfaceScaleChanged(ChangeEvent<float> changeEvent)
	{
		float newValue = changeEvent.newValue;
		Tween tween = this.debounceTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		this.debounceTween = DOVirtual.DelayedCall(1f, delegate
		{
			EventManager.TriggerEvent("Event_OnSettingsUserInterfaceScaleChanged", new Dictionary<string, object>
			{
				{
					"value",
					newValue
				}
			});
		}, true);
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x000132C4 File Offset: 0x000114C4
	public void UpdateUserInterfaceScale(float value)
	{
		this.userInterfaceScaleSlider.value = value;
	}

	// Token: 0x06000D9B RID: 3483 RVA: 0x000132D2 File Offset: 0x000114D2
	private void OnChatOpacityChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsChatOpacityChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x000132F9 File Offset: 0x000114F9
	public void UpdateChatOpacity(float value)
	{
		this.chatOpacitySlider.value = value;
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x00013307 File Offset: 0x00011507
	private void OnChatScaleChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsChatScaleChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0001332E File Offset: 0x0001152E
	public void UpdateChatScale(float value)
	{
		this.chatScaleSlider.value = value;
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x0001333C File Offset: 0x0001153C
	private void OnMinimapOpacityChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsMinimapOpacityChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x00013363 File Offset: 0x00011563
	public void UpdateMinimapOpacity(float value)
	{
		this.minimapOpacitySlider.value = value;
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x00013371 File Offset: 0x00011571
	private void OnMinimapBackgroundOpacityChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsMinimapBackgroundOpacityChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x00013398 File Offset: 0x00011598
	public void UpdateMinimapBackgroundOpacity(float value)
	{
		this.minimapBackgroundOpacitySlider.value = value;
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x000133A6 File Offset: 0x000115A6
	private void OnMinimapHorizontalPositionChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsMinimapHorizontalPositionChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DA4 RID: 3492 RVA: 0x000133CD File Offset: 0x000115CD
	public void UpdateMinimapHorizontalPosition(float value)
	{
		this.minimapHorizontalPositionSlider.value = value;
	}

	// Token: 0x06000DA5 RID: 3493 RVA: 0x000133DB File Offset: 0x000115DB
	private void OnMinimapVerticalPositionChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsMinimapVerticalPositionChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DA6 RID: 3494 RVA: 0x00013402 File Offset: 0x00011602
	public void UpdateMinimapVerticalPosition(float value)
	{
		this.minimapVerticalPositionSlider.value = value;
	}

	// Token: 0x06000DA7 RID: 3495 RVA: 0x00013410 File Offset: 0x00011610
	private void OnMinimapScaleChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsMinimapScaleChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DA8 RID: 3496 RVA: 0x00013437 File Offset: 0x00011637
	public void UpdateMinimapScale(float value)
	{
		this.minimapScaleSlider.value = value;
	}

	// Token: 0x06000DA9 RID: 3497 RVA: 0x00013445 File Offset: 0x00011645
	private void OnGlobalStickSensitivityChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsGlobalStickSensitivityChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DAA RID: 3498 RVA: 0x0001346C File Offset: 0x0001166C
	public void UpdateGlobalStickSensitivity(float value)
	{
		this.globalStickSensitivitySlider.value = value;
	}

	// Token: 0x06000DAB RID: 3499 RVA: 0x0001347A File Offset: 0x0001167A
	private void OnHorizontalStickSensitivityChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsHorizontalStickSensitivityChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DAC RID: 3500 RVA: 0x000134A1 File Offset: 0x000116A1
	public void UpdateHorizontalStickSensitivity(float value)
	{
		this.horizontalStickSensitivitySlider.value = value;
	}

	// Token: 0x06000DAD RID: 3501 RVA: 0x000134AF File Offset: 0x000116AF
	private void OnVerticalStickSensitivityChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsVerticalStickSensitivityChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DAE RID: 3502 RVA: 0x000134D6 File Offset: 0x000116D6
	public void UpdateVerticalStickSensitivity(float value)
	{
		this.verticalStickSensitivitySlider.value = value;
	}

	// Token: 0x06000DAF RID: 3503 RVA: 0x000134E4 File Offset: 0x000116E4
	private void OnKeyBindInputClicked(string actionName)
	{
		EventManager.TriggerEvent("Event_OnSettingsKeyBindInputClicked", new Dictionary<string, object>
		{
			{
				"actionName",
				actionName
			}
		});
	}

	// Token: 0x06000DB0 RID: 3504 RVA: 0x00013501 File Offset: 0x00011701
	private void OnKeyBindInputInteractionChanged(string actionName, KeyBindInteraction interaction)
	{
		EventManager.TriggerEvent("Event_OnSettingsKeyBindInputInteractionChanged", new Dictionary<string, object>
		{
			{
				"actionName",
				actionName
			},
			{
				"interaction",
				interaction
			}
		});
	}

	// Token: 0x06000DB1 RID: 3505 RVA: 0x0001352F File Offset: 0x0001172F
	private void OnLookSensitivityChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsLookSensitivityChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DB2 RID: 3506 RVA: 0x00013556 File Offset: 0x00011756
	public void UpdateLookSensitivity(float value)
	{
		this.lookSensitivitySlider.value = value;
	}

	// Token: 0x06000DB3 RID: 3507 RVA: 0x00013564 File Offset: 0x00011764
	private void OnGlobalVolumeChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsGlobalVolumeChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DB4 RID: 3508 RVA: 0x0001358B File Offset: 0x0001178B
	public void UpdateGlobalVolume(float value)
	{
		this.globalVolumeSlider.value = value;
	}

	// Token: 0x06000DB5 RID: 3509 RVA: 0x00013599 File Offset: 0x00011799
	private void OnAmbientVolumeChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsAmbientVolumeChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DB6 RID: 3510 RVA: 0x000135C0 File Offset: 0x000117C0
	public void UpdateAmbientVolume(float value)
	{
		this.ambientVolumeSlider.value = value;
	}

	// Token: 0x06000DB7 RID: 3511 RVA: 0x000135CE File Offset: 0x000117CE
	private void OnGameVolumeChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsGameVolumeChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DB8 RID: 3512 RVA: 0x000135F5 File Offset: 0x000117F5
	public void UpdateGameVolume(float value)
	{
		this.gameVolumeSlider.value = value;
	}

	// Token: 0x06000DB9 RID: 3513 RVA: 0x00013603 File Offset: 0x00011803
	private void OnVoiceVolumeChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsVoiceVolumeChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DBA RID: 3514 RVA: 0x0001362A File Offset: 0x0001182A
	public void UpdateVoiceVolume(float value)
	{
		this.voiceVolumeSlider.value = value;
	}

	// Token: 0x06000DBB RID: 3515 RVA: 0x00013638 File Offset: 0x00011838
	private void OnUIVolumeChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsUIVolumeChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DBC RID: 3516 RVA: 0x0001365F File Offset: 0x0001185F
	public void UpdateUIVolume(float value)
	{
		this.uiVolumeSlider.value = value;
	}

	// Token: 0x06000DBD RID: 3517 RVA: 0x0001366D File Offset: 0x0001186D
	private void OnFullScreenModeChanged(ChangeEvent<string> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsFullScreenModeChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DBE RID: 3518 RVA: 0x0001368F File Offset: 0x0001188F
	public void UpdateFullScreenMode(string value)
	{
		this.fullScreenModeDropdown.value = value;
	}

	// Token: 0x06000DBF RID: 3519 RVA: 0x0001369D File Offset: 0x0001189D
	private void OnDisplayChanged(ChangeEvent<string> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsDisplayChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DC0 RID: 3520 RVA: 0x000136BF File Offset: 0x000118BF
	public void UpdateDisplay(string value)
	{
		this.displayDropdown.value = value;
	}

	// Token: 0x06000DC1 RID: 3521 RVA: 0x000136CD File Offset: 0x000118CD
	public void UpdateDisplayChoices(List<string> choices)
	{
		this.displayDropdown.choices = choices;
	}

	// Token: 0x06000DC2 RID: 3522 RVA: 0x000136DB File Offset: 0x000118DB
	private void OnResolutionChanged(ChangeEvent<string> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsResolutionChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DC3 RID: 3523 RVA: 0x000136FD File Offset: 0x000118FD
	public void UpdateResolution(string value)
	{
		this.resolutionDropdown.value = value;
	}

	// Token: 0x06000DC4 RID: 3524 RVA: 0x0001370B File Offset: 0x0001190B
	public void UpdateResolutionChoices(List<string> choices)
	{
		this.resolutionDropdown.choices = choices;
	}

	// Token: 0x06000DC5 RID: 3525 RVA: 0x00013719 File Offset: 0x00011919
	private void OnVSyncChanged(ChangeEvent<bool> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsVSyncChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DC6 RID: 3526 RVA: 0x00013740 File Offset: 0x00011940
	public void UpdateVSync(bool value)
	{
		this.vSyncToggle.value = value;
	}

	// Token: 0x06000DC7 RID: 3527 RVA: 0x0001374E File Offset: 0x0001194E
	private void OnFpsLimitChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsFpsLimitChanged", new Dictionary<string, object>
		{
			{
				"value",
				(int)changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DC8 RID: 3528 RVA: 0x00013776 File Offset: 0x00011976
	public void UpdateFpsLimit(int value)
	{
		this.fpsLimitSlider.value = (float)value;
	}

	// Token: 0x06000DC9 RID: 3529 RVA: 0x00013785 File Offset: 0x00011985
	private void OnFovChanged(ChangeEvent<float> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsFovChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DCA RID: 3530 RVA: 0x000137AC File Offset: 0x000119AC
	public void UpdateFov(float value)
	{
		this.fovSlider.value = value;
	}

	// Token: 0x06000DCB RID: 3531 RVA: 0x000137BA File Offset: 0x000119BA
	private void OnQualityChanged(ChangeEvent<string> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsQualityChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DCC RID: 3532 RVA: 0x000137DC File Offset: 0x000119DC
	public void UpdateQuality(string value)
	{
		this.qualityDropdown.value = value;
	}

	// Token: 0x06000DCD RID: 3533 RVA: 0x000137EA File Offset: 0x000119EA
	private void OnMotionBlurChanged(ChangeEvent<bool> changeEvent)
	{
		EventManager.TriggerEvent("Event_OnSettingsMotionBlurChanged", new Dictionary<string, object>
		{
			{
				"value",
				changeEvent.newValue
			}
		});
	}

	// Token: 0x06000DCE RID: 3534 RVA: 0x00013811 File Offset: 0x00011A11
	public void UpdateMotionBlur(bool value)
	{
		this.motionBlurToggle.value = value;
	}

	// Token: 0x04000822 RID: 2082
	private VisualElement settings;

	// Token: 0x04000823 RID: 2083
	private IconButton closeIconButton;

	// Token: 0x04000824 RID: 2084
	private Slider cameraAngleSlider;

	// Token: 0x04000825 RID: 2085
	private DropdownField handednessDropdown;

	// Token: 0x04000826 RID: 2086
	private Toggle showPuckSilhouetteToggle;

	// Token: 0x04000827 RID: 2087
	private Toggle showPuckOutlineToggle;

	// Token: 0x04000828 RID: 2088
	private Toggle showPuckElevationToggle;

	// Token: 0x04000829 RID: 2089
	private Toggle showPlayerUsernamesToggle;

	// Token: 0x0400082A RID: 2090
	private Slider playerUsernamesFadeThresholdSlider;

	// Token: 0x0400082B RID: 2091
	private Toggle useNetworkSmoothingToggle;

	// Token: 0x0400082C RID: 2092
	private SliderInt networkSmoothingStrengthSliderInt;

	// Token: 0x0400082D RID: 2093
	private SliderInt maxMatchmakingPingSliderInt;

	// Token: 0x0400082E RID: 2094
	private Toggle filterChatProfanityToggle;

	// Token: 0x0400082F RID: 2095
	private DropdownField unitsDropdown;

	// Token: 0x04000830 RID: 2096
	private Toggle showGameUserInterfaceToggle;

	// Token: 0x04000831 RID: 2097
	private Slider userInterfaceScaleSlider;

	// Token: 0x04000832 RID: 2098
	private Slider chatOpacitySlider;

	// Token: 0x04000833 RID: 2099
	private Slider chatScaleSlider;

	// Token: 0x04000834 RID: 2100
	private Slider minimapOpacitySlider;

	// Token: 0x04000835 RID: 2101
	private Slider minimapBackgroundOpacitySlider;

	// Token: 0x04000836 RID: 2102
	private Slider minimapHorizontalPositionSlider;

	// Token: 0x04000837 RID: 2103
	private Slider minimapVerticalPositionSlider;

	// Token: 0x04000838 RID: 2104
	private Slider minimapScaleSlider;

	// Token: 0x04000839 RID: 2105
	private Slider globalStickSensitivitySlider;

	// Token: 0x0400083A RID: 2106
	private Slider horizontalStickSensitivitySlider;

	// Token: 0x0400083B RID: 2107
	private Slider verticalStickSensitivitySlider;

	// Token: 0x0400083C RID: 2108
	private Slider lookSensitivitySlider;

	// Token: 0x0400083D RID: 2109
	private Dictionary<string, KeyBindField> actionNameKeyBindFieldMap;

	// Token: 0x0400083E RID: 2110
	private Slider globalVolumeSlider;

	// Token: 0x0400083F RID: 2111
	private Slider ambientVolumeSlider;

	// Token: 0x04000840 RID: 2112
	private Slider gameVolumeSlider;

	// Token: 0x04000841 RID: 2113
	private Slider voiceVolumeSlider;

	// Token: 0x04000842 RID: 2114
	private Slider uiVolumeSlider;

	// Token: 0x04000843 RID: 2115
	private DropdownField fullScreenModeDropdown;

	// Token: 0x04000844 RID: 2116
	private DropdownField displayDropdown;

	// Token: 0x04000845 RID: 2117
	private DropdownField resolutionDropdown;

	// Token: 0x04000846 RID: 2118
	private Toggle vSyncToggle;

	// Token: 0x04000847 RID: 2119
	private Slider fpsLimitSlider;

	// Token: 0x04000848 RID: 2120
	private Slider fovSlider;

	// Token: 0x04000849 RID: 2121
	private DropdownField qualityDropdown;

	// Token: 0x0400084A RID: 2122
	private Toggle motionBlurToggle;

	// Token: 0x0400084B RID: 2123
	private Button resetToDefaultButton;

	// Token: 0x0400084C RID: 2124
	private Tween debounceTween;
}
