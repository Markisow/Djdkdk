using System;
using System.Collections.Generic;
using UnityEngine.InputSystem;

// Token: 0x02000135 RID: 309
public static class SettingsManagerController
{
	// Token: 0x060008EC RID: 2284 RVA: 0x00037350 File Offset: 0x00035550
	public static void Initialize()
	{
		EventManager.AddEventListener("Event_OnDisplayIndexChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnDisplayIndexChanged));
		EventManager.AddEventListener("Event_OnIsDisplayChangeInProgressChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnIsDisplayChangeInProgressChanged));
		EventManager.AddEventListener("Event_OnBaseCameraEnabled", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnBaseCameraEnabled));
		EventManager.AddEventListener("Event_OnPopupClickOk", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnPopupClickOk));
		SettingsManagerController.AddSettingsEventListeners();
		SettingsManagerController.AddAppearanceEventListeners();
		InputManager.Debug1Action.performed += SettingsManagerController.OnDebug1ActionPerformed;
	}

	// Token: 0x060008ED RID: 2285 RVA: 0x000373D8 File Offset: 0x000355D8
	public static void Dispose()
	{
		EventManager.RemoveEventListener("Event_OnDisplayIndexChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnDisplayIndexChanged));
		EventManager.RemoveEventListener("Event_OnIsDisplayChangeInProgressChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnIsDisplayChangeInProgressChanged));
		EventManager.RemoveEventListener("Event_OnBaseCameraEnabled", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnBaseCameraEnabled));
		EventManager.RemoveEventListener("Event_OnPopupClickOk", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnPopupClickOk));
		SettingsManagerController.RemoveSettingsEventListeners();
		SettingsManagerController.RemoveAppearanceEventListeners();
		InputManager.Debug1Action.performed -= SettingsManagerController.OnDebug1ActionPerformed;
	}

	// Token: 0x060008EE RID: 2286 RVA: 0x00037460 File Offset: 0x00035660
	private static void AddSettingsEventListeners()
	{
		EventManager.AddEventListener("Event_OnSettingsCameraAngleChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsCameraAngleChanged));
		EventManager.AddEventListener("Event_OnSettingsHandednessChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsHandednessChanged));
		EventManager.AddEventListener("Event_OnSettingsShowPuckSilhouetteChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsShowPuckSilhouetteChanged));
		EventManager.AddEventListener("Event_OnSettingsShowPuckOutlineChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsShowPuckOutlineChanged));
		EventManager.AddEventListener("Event_OnSettingsShowPuckElevationChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsShowPuckElevationChanged));
		EventManager.AddEventListener("Event_OnSettingsShowPlayerUsernamesChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsShowPlayerUsernamesChanged));
		EventManager.AddEventListener("Event_OnSettingsPlayerUsernamesFadeThresholdChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsPlayerUsernamesFadeThresholdChanged));
		EventManager.AddEventListener("Event_OnSettingsUseNetworkSmoothingChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsUseNetworkSmoothingChanged));
		EventManager.AddEventListener("Event_OnSettingsNetworkSmoothingStrengthChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsNetworkSmoothingStrengthChanged));
		EventManager.AddEventListener("Event_OnSettingsMaxMatchmakingPingChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMaxMatchmakingPingChanged));
		EventManager.AddEventListener("Event_OnSettingsFilterChatProfanityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsFilterChatProfanityChanged));
		EventManager.AddEventListener("Event_OnSettingsUnitsChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsUnitsChanged));
		EventManager.AddEventListener("Event_OnSettingsShowGameUserInterfaceChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsShowGameUserInterfaceChanged));
		EventManager.AddEventListener("Event_OnSettingsUserInterfaceScaleChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsUserInterfaceScaleChanged));
		EventManager.AddEventListener("Event_OnSettingsChatOpacityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsChatOpacityChanged));
		EventManager.AddEventListener("Event_OnSettingsChatScaleChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsChatScaleChanged));
		EventManager.AddEventListener("Event_OnSettingsMinimapOpacityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMinimapOpacityChanged));
		EventManager.AddEventListener("Event_OnSettingsMinimapBackgroundOpacityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMinimapBackgroundOpacityChanged));
		EventManager.AddEventListener("Event_OnSettingsMinimapHorizontalPositionChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMinimapHorizontalPositionChanged));
		EventManager.AddEventListener("Event_OnSettingsMinimapVerticalPositionChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMinimapVerticalPositionChanged));
		EventManager.AddEventListener("Event_OnSettingsMinimapScaleChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMinimapScaleChanged));
		EventManager.AddEventListener("Event_OnSettingsGlobalStickSensitivityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsGlobalStickSensitivityChanged));
		EventManager.AddEventListener("Event_OnSettingsHorizontalStickSensitivityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsHorizontalStickSensitivityChanged));
		EventManager.AddEventListener("Event_OnSettingsVerticalStickSensitivityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsVerticalStickSensitivityChanged));
		EventManager.AddEventListener("Event_OnSettingsLookSensitivityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsLookSensitivityChanged));
		EventManager.AddEventListener("Event_OnSettingsGlobalVolumeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsGlobalVolumeChanged));
		EventManager.AddEventListener("Event_OnSettingsAmbientVolumeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsAmbientVolumeChanged));
		EventManager.AddEventListener("Event_OnSettingsGameVolumeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsGameVolumeChanged));
		EventManager.AddEventListener("Event_OnSettingsVoiceVolumeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsVoiceVolumeChanged));
		EventManager.AddEventListener("Event_OnSettingsUIVolumeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsUIVolumeChanged));
		EventManager.AddEventListener("Event_OnSettingsFullScreenModeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsFullScreenModeChanged));
		EventManager.AddEventListener("Event_OnSettingsDisplayChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsDisplayChanged));
		EventManager.AddEventListener("Event_OnSettingsResolutionChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsResolutionChanged));
		EventManager.AddEventListener("Event_OnSettingsVSyncChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsVSyncChanged));
		EventManager.AddEventListener("Event_OnSettingsFpsLimitChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsFpsLimitChanged));
		EventManager.AddEventListener("Event_OnSettingsFovChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsFovChanged));
		EventManager.AddEventListener("Event_OnSettingsQualityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsQualityChanged));
		EventManager.AddEventListener("Event_OnSettingsMotionBlurChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMotionBlurChanged));
	}

	// Token: 0x060008EF RID: 2287 RVA: 0x000377B4 File Offset: 0x000359B4
	private static void AddAppearanceEventListeners()
	{
		EventManager.AddEventListener("Event_OnAppearanceTeamChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnAppearanceTeamChanged));
		EventManager.AddEventListener("Event_OnAppearanceRoleChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnAppearanceRoleChanged));
		EventManager.AddEventListener("Event_OnAppearanceApplyForBothTeamsChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnAppearanceApplyForBothTeamsChanged));
		EventManager.AddEventListener("Event_OnAppearanceClickItem", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnAppearanceClickItem));
	}

	// Token: 0x060008F0 RID: 2288 RVA: 0x0003781C File Offset: 0x00035A1C
	private static void RemoveSettingsEventListeners()
	{
		EventManager.RemoveEventListener("Event_OnSettingsCameraAngleChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsCameraAngleChanged));
		EventManager.RemoveEventListener("Event_OnSettingsHandednessChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsHandednessChanged));
		EventManager.RemoveEventListener("Event_OnSettingsShowPuckSilhouetteChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsShowPuckSilhouetteChanged));
		EventManager.RemoveEventListener("Event_OnSettingsShowPuckOutlineChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsShowPuckOutlineChanged));
		EventManager.RemoveEventListener("Event_OnSettingsShowPuckElevationChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsShowPuckElevationChanged));
		EventManager.RemoveEventListener("Event_OnSettingsShowPlayerUsernamesChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsShowPlayerUsernamesChanged));
		EventManager.RemoveEventListener("Event_OnSettingsPlayerUsernamesFadeThresholdChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsPlayerUsernamesFadeThresholdChanged));
		EventManager.RemoveEventListener("Event_OnSettingsUseNetworkSmoothingChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsUseNetworkSmoothingChanged));
		EventManager.RemoveEventListener("Event_OnSettingsNetworkSmoothingStrengthChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsNetworkSmoothingStrengthChanged));
		EventManager.RemoveEventListener("Event_OnSettingsMaxMatchmakingPingChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMaxMatchmakingPingChanged));
		EventManager.RemoveEventListener("Event_OnSettingsFilterChatProfanityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsFilterChatProfanityChanged));
		EventManager.RemoveEventListener("Event_OnSettingsUnitsChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsUnitsChanged));
		EventManager.RemoveEventListener("Event_OnSettingsShowGameUserInterfaceChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsShowGameUserInterfaceChanged));
		EventManager.RemoveEventListener("Event_OnSettingsUserInterfaceScaleChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsUserInterfaceScaleChanged));
		EventManager.RemoveEventListener("Event_OnSettingsChatOpacityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsChatOpacityChanged));
		EventManager.RemoveEventListener("Event_OnSettingsChatScaleChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsChatScaleChanged));
		EventManager.RemoveEventListener("Event_OnSettingsMinimapOpacityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMinimapOpacityChanged));
		EventManager.RemoveEventListener("Event_OnSettingsMinimapBackgroundOpacityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMinimapBackgroundOpacityChanged));
		EventManager.RemoveEventListener("Event_OnSettingsMinimapHorizontalPositionChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMinimapHorizontalPositionChanged));
		EventManager.RemoveEventListener("Event_OnSettingsMinimapVerticalPositionChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMinimapVerticalPositionChanged));
		EventManager.RemoveEventListener("Event_OnSettingsMinimapScaleChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMinimapScaleChanged));
		EventManager.RemoveEventListener("Event_OnSettingsGlobalStickSensitivityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsGlobalStickSensitivityChanged));
		EventManager.RemoveEventListener("Event_OnSettingsHorizontalStickSensitivityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsHorizontalStickSensitivityChanged));
		EventManager.RemoveEventListener("Event_OnSettingsVerticalStickSensitivityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsVerticalStickSensitivityChanged));
		EventManager.RemoveEventListener("Event_OnSettingsLookSensitivityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsLookSensitivityChanged));
		EventManager.RemoveEventListener("Event_OnSettingsGlobalVolumeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsGlobalVolumeChanged));
		EventManager.RemoveEventListener("Event_OnSettingsAmbientVolumeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsAmbientVolumeChanged));
		EventManager.RemoveEventListener("Event_OnSettingsGameVolumeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsGameVolumeChanged));
		EventManager.RemoveEventListener("Event_OnSettingsVoiceVolumeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsVoiceVolumeChanged));
		EventManager.RemoveEventListener("Event_OnSettingsUIVolumeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsUIVolumeChanged));
		EventManager.RemoveEventListener("Event_OnSettingsFullScreenModeChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsFullScreenModeChanged));
		EventManager.RemoveEventListener("Event_OnSettingsDisplayChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsDisplayChanged));
		EventManager.RemoveEventListener("Event_OnSettingsResolutionChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsResolutionChanged));
		EventManager.RemoveEventListener("Event_OnSettingsVSyncChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsVSyncChanged));
		EventManager.RemoveEventListener("Event_OnSettingsFpsLimitChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsFpsLimitChanged));
		EventManager.RemoveEventListener("Event_OnSettingsFovChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsFovChanged));
		EventManager.RemoveEventListener("Event_OnSettingsQualityChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsQualityChanged));
		EventManager.RemoveEventListener("Event_OnSettingsMotionBlurChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnSettingsMotionBlurChanged));
	}

	// Token: 0x060008F1 RID: 2289 RVA: 0x00037B70 File Offset: 0x00035D70
	private static void RemoveAppearanceEventListeners()
	{
		EventManager.RemoveEventListener("Event_OnAppearanceTeamChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnAppearanceTeamChanged));
		EventManager.RemoveEventListener("Event_OnAppearanceRoleChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnAppearanceRoleChanged));
		EventManager.RemoveEventListener("Event_OnAppearanceApplyForBothTeamsChanged", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnAppearanceApplyForBothTeamsChanged));
		EventManager.RemoveEventListener("Event_OnAppearanceClickItem", new Action<Dictionary<string, object>>(SettingsManagerController.Event_OnAppearanceClickItem));
	}

	// Token: 0x060008F2 RID: 2290 RVA: 0x0000F740 File Offset: 0x0000D940
	private static void OnDebug1ActionPerformed(InputAction.CallbackContext context)
	{
		SettingsManager.UpdateDebug(!SettingsManager.Debug);
	}

	// Token: 0x060008F3 RID: 2291 RVA: 0x0000F74F File Offset: 0x0000D94F
	private static void Event_OnSettingsCameraAngleChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateCameraAngle((float)message["value"]);
	}

	// Token: 0x060008F4 RID: 2292 RVA: 0x0000F766 File Offset: 0x0000D966
	private static void Event_OnSettingsHandednessChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateHandedness(Utils.GetHandednessFromName((string)message["value"]));
	}

	// Token: 0x060008F5 RID: 2293 RVA: 0x0000F782 File Offset: 0x0000D982
	private static void Event_OnSettingsShowPuckSilhouetteChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateShowPuckSilhouette((bool)message["value"]);
	}

	// Token: 0x060008F6 RID: 2294 RVA: 0x0000F799 File Offset: 0x0000D999
	private static void Event_OnSettingsShowPuckOutlineChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateShowPuckOutline((bool)message["value"]);
	}

	// Token: 0x060008F7 RID: 2295 RVA: 0x0000F7B0 File Offset: 0x0000D9B0
	private static void Event_OnSettingsShowPuckElevationChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateShowPuckElevation((bool)message["value"]);
	}

	// Token: 0x060008F8 RID: 2296 RVA: 0x0000F7C7 File Offset: 0x0000D9C7
	private static void Event_OnSettingsShowPlayerUsernamesChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateShowPlayerUsernames((bool)message["value"]);
	}

	// Token: 0x060008F9 RID: 2297 RVA: 0x0000F7DE File Offset: 0x0000D9DE
	private static void Event_OnSettingsPlayerUsernamesFadeThresholdChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdatePlayerUsernamesFadeThreshold((float)message["value"]);
	}

	// Token: 0x060008FA RID: 2298 RVA: 0x0000F7F5 File Offset: 0x0000D9F5
	private static void Event_OnSettingsUseNetworkSmoothingChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateUseNetworkSmoothing((bool)message["value"]);
	}

	// Token: 0x060008FB RID: 2299 RVA: 0x0000F80C File Offset: 0x0000DA0C
	private static void Event_OnSettingsNetworkSmoothingStrengthChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateNetworkSmoothingStrength((int)message["value"]);
	}

	// Token: 0x060008FC RID: 2300 RVA: 0x0000F823 File Offset: 0x0000DA23
	private static void Event_OnSettingsMaxMatchmakingPingChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateMaxMatchmakingPing((int)message["value"]);
	}

	// Token: 0x060008FD RID: 2301 RVA: 0x0000F83A File Offset: 0x0000DA3A
	private static void Event_OnSettingsFilterChatProfanityChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateFilterChatProfanity((bool)message["value"]);
	}

	// Token: 0x060008FE RID: 2302 RVA: 0x0000F851 File Offset: 0x0000DA51
	private static void Event_OnSettingsUnitsChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateUnits(Utils.GetUnitsFromName((string)message["value"]));
	}

	// Token: 0x060008FF RID: 2303 RVA: 0x0000F86D File Offset: 0x0000DA6D
	private static void Event_OnSettingsShowGameUserInterfaceChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateShowGameUserInterface((bool)message["value"]);
	}

	// Token: 0x06000900 RID: 2304 RVA: 0x0000F884 File Offset: 0x0000DA84
	private static void Event_OnSettingsUserInterfaceScaleChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateUserInterfaceScale((float)message["value"]);
	}

	// Token: 0x06000901 RID: 2305 RVA: 0x0000F89B File Offset: 0x0000DA9B
	private static void Event_OnSettingsChatOpacityChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateChatOpacity((float)message["value"]);
	}

	// Token: 0x06000902 RID: 2306 RVA: 0x0000F8B2 File Offset: 0x0000DAB2
	private static void Event_OnSettingsChatScaleChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateChatScale((float)message["value"]);
	}

	// Token: 0x06000903 RID: 2307 RVA: 0x0000F8C9 File Offset: 0x0000DAC9
	private static void Event_OnSettingsMinimapOpacityChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateMinimapOpacity((float)message["value"]);
	}

	// Token: 0x06000904 RID: 2308 RVA: 0x0000F8E0 File Offset: 0x0000DAE0
	private static void Event_OnSettingsMinimapBackgroundOpacityChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateMinimapBackgroundOpacity((float)message["value"]);
	}

	// Token: 0x06000905 RID: 2309 RVA: 0x0000F8F7 File Offset: 0x0000DAF7
	private static void Event_OnSettingsMinimapHorizontalPositionChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateMinimapHorizontalPosition((float)message["value"]);
	}

	// Token: 0x06000906 RID: 2310 RVA: 0x0000F90E File Offset: 0x0000DB0E
	private static void Event_OnSettingsMinimapVerticalPositionChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateMinimapVerticalPosition((float)message["value"]);
	}

	// Token: 0x06000907 RID: 2311 RVA: 0x0000F925 File Offset: 0x0000DB25
	private static void Event_OnSettingsMinimapScaleChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateMinimapScale((float)message["value"]);
	}

	// Token: 0x06000908 RID: 2312 RVA: 0x0000F93C File Offset: 0x0000DB3C
	private static void Event_OnSettingsGlobalStickSensitivityChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateGlobalStickSensitivity((float)message["value"]);
	}

	// Token: 0x06000909 RID: 2313 RVA: 0x0000F953 File Offset: 0x0000DB53
	private static void Event_OnSettingsHorizontalStickSensitivityChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateHorizontalStickSensitivity((float)message["value"]);
	}

	// Token: 0x0600090A RID: 2314 RVA: 0x0000F96A File Offset: 0x0000DB6A
	private static void Event_OnSettingsVerticalStickSensitivityChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateVerticalStickSensitivity((float)message["value"]);
	}

	// Token: 0x0600090B RID: 2315 RVA: 0x0000F981 File Offset: 0x0000DB81
	private static void Event_OnSettingsLookSensitivityChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateLookSensitivity((float)message["value"]);
	}

	// Token: 0x0600090C RID: 2316 RVA: 0x0000F998 File Offset: 0x0000DB98
	private static void Event_OnSettingsGlobalVolumeChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateGlobalVolume((float)message["value"]);
	}

	// Token: 0x0600090D RID: 2317 RVA: 0x0000F9AF File Offset: 0x0000DBAF
	private static void Event_OnSettingsAmbientVolumeChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateAmbientVolume((float)message["value"]);
	}

	// Token: 0x0600090E RID: 2318 RVA: 0x0000F9C6 File Offset: 0x0000DBC6
	private static void Event_OnSettingsGameVolumeChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateGameVolume((float)message["value"]);
	}

	// Token: 0x0600090F RID: 2319 RVA: 0x0000F9DD File Offset: 0x0000DBDD
	private static void Event_OnSettingsVoiceVolumeChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateVoiceVolume((float)message["value"]);
	}

	// Token: 0x06000910 RID: 2320 RVA: 0x0000F9F4 File Offset: 0x0000DBF4
	private static void Event_OnSettingsUIVolumeChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateUIVolume((float)message["value"]);
	}

	// Token: 0x06000911 RID: 2321 RVA: 0x0000FA0B File Offset: 0x0000DC0B
	private static void Event_OnSettingsFullScreenModeChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateFullScreenMode(Utils.GetFullScreenModeFromName((string)message["value"]));
	}

	// Token: 0x06000912 RID: 2322 RVA: 0x00037BD8 File Offset: 0x00035DD8
	private static void Event_OnSettingsDisplayChanged(Dictionary<string, object> message)
	{
		string text = (string)message["value"];
		int displayIndexFromName = Utils.GetDisplayIndexFromName(text);
		if (displayIndexFromName == -1)
		{
			SettingsManagerController.Logger.Warning("Could not find display index for display name " + text + ", skipping update");
			return;
		}
		SettingsManager.UpdateDisplayIndex(displayIndexFromName);
	}

	// Token: 0x06000913 RID: 2323 RVA: 0x00037C24 File Offset: 0x00035E24
	private static void Event_OnSettingsResolutionChanged(Dictionary<string, object> message)
	{
		string text = (string)message["value"];
		int resolutionIndexFromName = Utils.GetResolutionIndexFromName(text);
		if (resolutionIndexFromName == -1)
		{
			SettingsManagerController.Logger.Warning("Could not find resolution index for resolution name " + text + ", skipping update");
			return;
		}
		SettingsManager.UpdateResolutionIndex(resolutionIndexFromName);
	}

	// Token: 0x06000914 RID: 2324 RVA: 0x0000FA27 File Offset: 0x0000DC27
	private static void Event_OnSettingsVSyncChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateVSync((bool)message["value"]);
	}

	// Token: 0x06000915 RID: 2325 RVA: 0x0000FA3E File Offset: 0x0000DC3E
	private static void Event_OnSettingsFpsLimitChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateFpsLimit((int)message["value"]);
	}

	// Token: 0x06000916 RID: 2326 RVA: 0x0000FA55 File Offset: 0x0000DC55
	private static void Event_OnSettingsFovChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateFov((float)message["value"]);
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x0000FA6C File Offset: 0x0000DC6C
	private static void Event_OnSettingsQualityChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateQuality(Utils.GetApplicationQualityFromName((string)message["value"]));
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x0000FA88 File Offset: 0x0000DC88
	private static void Event_OnSettingsMotionBlurChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateMotionBlur((bool)message["value"]);
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x0000FA9F File Offset: 0x0000DC9F
	private static void Event_OnAppearanceTeamChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateTeam(Utils.GetTeamFromName((string)message["value"]));
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x0000FABB File Offset: 0x0000DCBB
	private static void Event_OnAppearanceRoleChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateRole(Utils.GetRoleFromName((string)message["value"]));
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x0000FAD7 File Offset: 0x0000DCD7
	private static void Event_OnAppearanceApplyForBothTeamsChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateApplyForBothTeams((bool)message["value"]);
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00037C70 File Offset: 0x00035E70
	private static void Event_OnAppearanceClickItem(Dictionary<string, object> message)
	{
		Item item = (Item)message["item"];
		AppearanceCategory appearanceCategory = (AppearanceCategory)message["category"];
		AppearanceSubcategory appearanceSubcategory = (AppearanceSubcategory)message["subcategory"];
		PlayerTeam playerTeam = (PlayerTeam)message["team"];
		PlayerRole role = (PlayerRole)message["role"];
		if (!item.IsOwned)
		{
			return;
		}
		PlayerTeam team = (playerTeam == PlayerTeam.Blue) ? PlayerTeam.Red : PlayerTeam.Blue;
		switch (appearanceSubcategory)
		{
		case AppearanceSubcategory.Headgear:
			SettingsManager.UpdateHeadgearID(playerTeam, role, item.id);
			if (SettingsManager.ApplyForBothTeams)
			{
				SettingsManager.UpdateHeadgearID(team, role, item.id);
				return;
			}
			break;
		case AppearanceSubcategory.Flags:
			SettingsManager.UpdateFlagID(item.id);
			return;
		case AppearanceSubcategory.Mustaches:
			SettingsManager.UpdateMustacheID(item.id);
			return;
		case AppearanceSubcategory.Beards:
			SettingsManager.UpdateBeardID(item.id);
			return;
		case AppearanceSubcategory.Jerseys:
			SettingsManager.UpdateJerseyID(playerTeam, role, item.id);
			if (SettingsManager.ApplyForBothTeams)
			{
				SettingsManager.UpdateJerseyID(team, role, item.id);
				return;
			}
			break;
		case AppearanceSubcategory.StickSkins:
			SettingsManager.UpdateStickSkinID(playerTeam, role, item.id);
			if (SettingsManager.ApplyForBothTeams)
			{
				SettingsManager.UpdateStickSkinID(team, role, item.id);
				return;
			}
			break;
		case AppearanceSubcategory.StickShaftTapes:
			SettingsManager.UpdateStickShaftTapeID(playerTeam, role, item.id);
			if (SettingsManager.ApplyForBothTeams)
			{
				SettingsManager.UpdateStickShaftTapeID(team, role, item.id);
				return;
			}
			break;
		case AppearanceSubcategory.StickBladeTapes:
			SettingsManager.UpdateStickBladeTapeID(playerTeam, role, item.id);
			if (SettingsManager.ApplyForBothTeams)
			{
				SettingsManager.UpdateStickBladeTapeID(team, role, item.id);
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x0000FAEE File Offset: 0x0000DCEE
	private static void Event_OnDisplayIndexChanged(Dictionary<string, object> message)
	{
		SettingsManager.UpdateResolutionIndex(-1);
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x0000FAF6 File Offset: 0x0000DCF6
	private static void Event_OnIsDisplayChangeInProgressChanged(Dictionary<string, object> message)
	{
		if ((bool)message["isDisplayChangeInProgress"])
		{
			return;
		}
		if (SettingsManager.ResolutionIndex == -1)
		{
			SettingsManager.UpdateResolutionIndex(Utils.GetResolutions().Count - 1);
		}
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x0000FB24 File Offset: 0x0000DD24
	private static void Event_OnBaseCameraEnabled(Dictionary<string, object> message)
	{
		((BaseCamera)message["baseCamera"]).SetFieldOfView(SettingsManager.Fov);
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x0000FB40 File Offset: 0x0000DD40
	private static void Event_OnPopupClickOk(Dictionary<string, object> message)
	{
		if (((Popup)message["popup"]).Name == "settingsResetToDefault")
		{
			SettingsManager.ResetToDefault();
		}
		EventManager.TriggerEvent("Event_OnSettingsResetToDefault", null);
	}

	// Token: 0x04000568 RID: 1384
	private static readonly Logger Logger = new Logger("SettingsManagerController");
}
