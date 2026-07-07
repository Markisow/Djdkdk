using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D1 RID: 465
public class UISettingsController : UIViewController<UISettings>
{
	// Token: 0x06000DD5 RID: 3541 RVA: 0x00049A28 File Offset: 0x00047C28
	public override void Awake()
	{
		base.Awake();
		this.uiSettings = base.GetComponent<UISettings>();
		EventManager.AddEventListener("Event_OnCameraAngleChanged", new Action<Dictionary<string, object>>(this.Event_OnCameraAngleChanged));
		EventManager.AddEventListener("Event_OnHandednessChanged", new Action<Dictionary<string, object>>(this.Event_OnHandednessChanged));
		EventManager.AddEventListener("Event_OnShowPuckSilhouetteChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckSilhouetteChanged));
		EventManager.AddEventListener("Event_OnShowPuckOutlineChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckOutlineChanged));
		EventManager.AddEventListener("Event_OnShowPuckElevationChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckElevationChanged));
		EventManager.AddEventListener("Event_OnShowPlayerUsernamesChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPlayerUsernamesChanged));
		EventManager.AddEventListener("Event_OnPlayerUsernamesFadeThresholdChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerUsernamesFadeThresholdChanged));
		EventManager.AddEventListener("Event_OnUseNetworkSmoothingChanged", new Action<Dictionary<string, object>>(this.Event_OnUseNetworkSmoothingChanged));
		EventManager.AddEventListener("Event_OnNetworkSmoothingStrengthChanged", new Action<Dictionary<string, object>>(this.Event_OnNetworkSmoothingStrengthChanged));
		EventManager.AddEventListener("Event_OnMaxMatchmakingPingChanged", new Action<Dictionary<string, object>>(this.Event_OnMaxMatchmakingPingChanged));
		EventManager.AddEventListener("Event_OnFilterChatProfanityChanged", new Action<Dictionary<string, object>>(this.Event_OnFilterChatProfanityChanged));
		EventManager.AddEventListener("Event_OnUnitsChanged", new Action<Dictionary<string, object>>(this.Event_OnUnitsChanged));
		EventManager.AddEventListener("Event_OnShowGameUserInterfaceChanged", new Action<Dictionary<string, object>>(this.Event_OnShowGameUserInterfaceChanged));
		EventManager.AddEventListener("Event_OnUserInterfaceScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnUserInterfaceScaleChanged));
		EventManager.AddEventListener("Event_OnChatOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnChatOpacityChanged));
		EventManager.AddEventListener("Event_OnChatScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnChatScaleChanged));
		EventManager.AddEventListener("Event_OnMinimapOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapOpacityChanged));
		EventManager.AddEventListener("Event_OnMinimapBackgroundOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapBackgroundOpacityChanged));
		EventManager.AddEventListener("Event_OnMinimapHorizontalPositionChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapHorizontalPositionChanged));
		EventManager.AddEventListener("Event_OnMinimapVerticalPositionChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapVerticalPositionChanged));
		EventManager.AddEventListener("Event_OnMinimapScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapScaleChanged));
		EventManager.AddEventListener("Event_OnGlobalStickSensitivityChanged", new Action<Dictionary<string, object>>(this.Event_OnGlobalStickSensitivityChanged));
		EventManager.AddEventListener("Event_OnHorizontalStickSensitivityChanged", new Action<Dictionary<string, object>>(this.Event_OnHorizontalStickSensitivityChanged));
		EventManager.AddEventListener("Event_OnVerticalStickSensitivityChanged", new Action<Dictionary<string, object>>(this.Event_OnVerticalStickSensitivityChanged));
		EventManager.AddEventListener("Event_OnLookSensitivityChanged", new Action<Dictionary<string, object>>(this.Event_OnLookSensitivityChanged));
		EventManager.AddEventListener("Event_OnKeyBindsLoaded", new Action<Dictionary<string, object>>(this.Event_OnKeyBindsLoaded));
		EventManager.AddEventListener("Event_OnKeyBindsSaved", new Action<Dictionary<string, object>>(this.Event_OnKeyBindsSaved));
		EventManager.AddEventListener("Event_OnGlobalVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnGlobalVolumeChanged));
		EventManager.AddEventListener("Event_OnAmbientVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnAmbientVolumeChanged));
		EventManager.AddEventListener("Event_OnGameVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnGameVolumeChanged));
		EventManager.AddEventListener("Event_OnVoiceVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnVoiceVolumeChanged));
		EventManager.AddEventListener("Event_OnUIVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnUIVolumeChanged));
		EventManager.AddEventListener("Event_OnFullScreenModeChanged", new Action<Dictionary<string, object>>(this.Event_OnFullScreenModeChanged));
		EventManager.AddEventListener("Event_OnDisplayIndexChanged", new Action<Dictionary<string, object>>(this.Event_OnDisplayIndexChanged));
		EventManager.AddEventListener("Event_OnResolutionIndexChanged", new Action<Dictionary<string, object>>(this.Event_OnResolutionIndexChanged));
		EventManager.AddEventListener("Event_OnVSyncChanged", new Action<Dictionary<string, object>>(this.Event_OnVSyncChanged));
		EventManager.AddEventListener("Event_OnFpsLimitChanged", new Action<Dictionary<string, object>>(this.Event_OnFpsLimitChanged));
		EventManager.AddEventListener("Event_OnFovChanged", new Action<Dictionary<string, object>>(this.Event_OnFovChanged));
		EventManager.AddEventListener("Event_OnQualityChanged", new Action<Dictionary<string, object>>(this.Event_OnQualityChanged));
		EventManager.AddEventListener("Event_OnMotionBlurChanged", new Action<Dictionary<string, object>>(this.Event_OnMotionBlurChanged));
		EventManager.AddEventListener("Event_OnIsDisplayChangeInProgressChanged", new Action<Dictionary<string, object>>(this.Event_OnIsDisplayChangeInProgressChanged));
	}

	// Token: 0x06000DD6 RID: 3542 RVA: 0x00049DD0 File Offset: 0x00047FD0
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnCameraAngleChanged", new Action<Dictionary<string, object>>(this.Event_OnCameraAngleChanged));
		EventManager.RemoveEventListener("Event_OnHandednessChanged", new Action<Dictionary<string, object>>(this.Event_OnHandednessChanged));
		EventManager.RemoveEventListener("Event_OnShowPuckSilhouetteChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckSilhouetteChanged));
		EventManager.RemoveEventListener("Event_OnShowPuckOutlineChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckOutlineChanged));
		EventManager.RemoveEventListener("Event_OnShowPuckElevationChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckElevationChanged));
		EventManager.RemoveEventListener("Event_OnShowPlayerUsernamesChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPlayerUsernamesChanged));
		EventManager.RemoveEventListener("Event_OnPlayerUsernamesFadeThresholdChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerUsernamesFadeThresholdChanged));
		EventManager.RemoveEventListener("Event_OnUseNetworkSmoothingChanged", new Action<Dictionary<string, object>>(this.Event_OnUseNetworkSmoothingChanged));
		EventManager.RemoveEventListener("Event_OnNetworkSmoothingStrengthChanged", new Action<Dictionary<string, object>>(this.Event_OnNetworkSmoothingStrengthChanged));
		EventManager.RemoveEventListener("Event_OnMaxMatchmakingPingChanged", new Action<Dictionary<string, object>>(this.Event_OnMaxMatchmakingPingChanged));
		EventManager.RemoveEventListener("Event_OnFilterChatProfanityChanged", new Action<Dictionary<string, object>>(this.Event_OnFilterChatProfanityChanged));
		EventManager.RemoveEventListener("Event_OnUnitsChanged", new Action<Dictionary<string, object>>(this.Event_OnUnitsChanged));
		EventManager.RemoveEventListener("Event_OnShowGameUserInterfaceChanged", new Action<Dictionary<string, object>>(this.Event_OnShowGameUserInterfaceChanged));
		EventManager.RemoveEventListener("Event_OnUserInterfaceScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnUserInterfaceScaleChanged));
		EventManager.RemoveEventListener("Event_OnChatOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnChatOpacityChanged));
		EventManager.RemoveEventListener("Event_OnChatScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnChatScaleChanged));
		EventManager.RemoveEventListener("Event_OnMinimapOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapOpacityChanged));
		EventManager.RemoveEventListener("Event_OnMinimapBackgroundOpacityChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapBackgroundOpacityChanged));
		EventManager.RemoveEventListener("Event_OnMinimapHorizontalPositionChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapHorizontalPositionChanged));
		EventManager.RemoveEventListener("Event_OnMinimapVerticalPositionChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapVerticalPositionChanged));
		EventManager.RemoveEventListener("Event_OnMinimapScaleChanged", new Action<Dictionary<string, object>>(this.Event_OnMinimapScaleChanged));
		EventManager.RemoveEventListener("Event_OnGlobalStickSensitivityChanged", new Action<Dictionary<string, object>>(this.Event_OnGlobalStickSensitivityChanged));
		EventManager.RemoveEventListener("Event_OnHorizontalStickSensitivityChanged", new Action<Dictionary<string, object>>(this.Event_OnHorizontalStickSensitivityChanged));
		EventManager.RemoveEventListener("Event_OnVerticalStickSensitivityChanged", new Action<Dictionary<string, object>>(this.Event_OnVerticalStickSensitivityChanged));
		EventManager.RemoveEventListener("Event_OnLookSensitivityChanged", new Action<Dictionary<string, object>>(this.Event_OnLookSensitivityChanged));
		EventManager.RemoveEventListener("Event_OnKeyBindsLoaded", new Action<Dictionary<string, object>>(this.Event_OnKeyBindsLoaded));
		EventManager.RemoveEventListener("Event_OnKeyBindsSaved", new Action<Dictionary<string, object>>(this.Event_OnKeyBindsSaved));
		EventManager.RemoveEventListener("Event_OnGlobalVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnGlobalVolumeChanged));
		EventManager.RemoveEventListener("Event_OnAmbientVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnAmbientVolumeChanged));
		EventManager.RemoveEventListener("Event_OnGameVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnGameVolumeChanged));
		EventManager.RemoveEventListener("Event_OnVoiceVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnVoiceVolumeChanged));
		EventManager.RemoveEventListener("Event_OnUIVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnUIVolumeChanged));
		EventManager.RemoveEventListener("Event_OnFullScreenModeChanged", new Action<Dictionary<string, object>>(this.Event_OnFullScreenModeChanged));
		EventManager.RemoveEventListener("Event_OnDisplayIndexChanged", new Action<Dictionary<string, object>>(this.Event_OnDisplayIndexChanged));
		EventManager.RemoveEventListener("Event_OnResolutionIndexChanged", new Action<Dictionary<string, object>>(this.Event_OnResolutionIndexChanged));
		EventManager.RemoveEventListener("Event_OnVSyncChanged", new Action<Dictionary<string, object>>(this.Event_OnVSyncChanged));
		EventManager.RemoveEventListener("Event_OnFpsLimitChanged", new Action<Dictionary<string, object>>(this.Event_OnFpsLimitChanged));
		EventManager.RemoveEventListener("Event_OnFovChanged", new Action<Dictionary<string, object>>(this.Event_OnFovChanged));
		EventManager.RemoveEventListener("Event_OnQualityChanged", new Action<Dictionary<string, object>>(this.Event_OnQualityChanged));
		EventManager.RemoveEventListener("Event_OnMotionBlurChanged", new Action<Dictionary<string, object>>(this.Event_OnMotionBlurChanged));
		EventManager.RemoveEventListener("Event_OnIsDisplayChangeInProgressChanged", new Action<Dictionary<string, object>>(this.Event_OnIsDisplayChangeInProgressChanged));
		base.OnDestroy();
	}

	// Token: 0x06000DD7 RID: 3543 RVA: 0x0004A16C File Offset: 0x0004836C
	private void Event_OnCameraAngleChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateCameraAngle(value);
	}

	// Token: 0x06000DD8 RID: 3544 RVA: 0x0004A198 File Offset: 0x00048398
	private void Event_OnHandednessChanged(Dictionary<string, object> message)
	{
		string nameFromHandedness = Utils.GetNameFromHandedness((PlayerHandedness)message["value"]);
		this.uiSettings.UpdateHandedness(nameFromHandedness);
	}

	// Token: 0x06000DD9 RID: 3545 RVA: 0x0004A1C8 File Offset: 0x000483C8
	private void Event_OnShowPuckSilhouetteChanged(Dictionary<string, object> message)
	{
		bool value = (bool)message["value"];
		this.uiSettings.UpdateShowPuckSilhouette(value);
	}

	// Token: 0x06000DDA RID: 3546 RVA: 0x0004A1F4 File Offset: 0x000483F4
	private void Event_OnShowPuckOutlineChanged(Dictionary<string, object> message)
	{
		bool value = (bool)message["value"];
		this.uiSettings.UpdateShowPuckOutline(value);
	}

	// Token: 0x06000DDB RID: 3547 RVA: 0x0004A220 File Offset: 0x00048420
	private void Event_OnShowPuckElevationChanged(Dictionary<string, object> message)
	{
		bool value = (bool)message["value"];
		this.uiSettings.UpdateShowPuckElevation(value);
	}

	// Token: 0x06000DDC RID: 3548 RVA: 0x0004A24C File Offset: 0x0004844C
	private void Event_OnShowPlayerUsernamesChanged(Dictionary<string, object> message)
	{
		bool value = (bool)message["value"];
		this.uiSettings.UpdateShowPlayerUsernames(value);
	}

	// Token: 0x06000DDD RID: 3549 RVA: 0x0004A278 File Offset: 0x00048478
	private void Event_OnPlayerUsernamesFadeThresholdChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdatePlayerUsernamesFadeThreshold(value);
	}

	// Token: 0x06000DDE RID: 3550 RVA: 0x0004A2A4 File Offset: 0x000484A4
	private void Event_OnUseNetworkSmoothingChanged(Dictionary<string, object> message)
	{
		bool value = (bool)message["value"];
		this.uiSettings.UpdateUseNetworkSmoothing(value);
	}

	// Token: 0x06000DDF RID: 3551 RVA: 0x0004A2D0 File Offset: 0x000484D0
	private void Event_OnNetworkSmoothingStrengthChanged(Dictionary<string, object> message)
	{
		int value = (int)message["value"];
		this.uiSettings.UpdateNetworkSmoothingStrength(value);
	}

	// Token: 0x06000DE0 RID: 3552 RVA: 0x0004A2FC File Offset: 0x000484FC
	private void Event_OnMaxMatchmakingPingChanged(Dictionary<string, object> message)
	{
		int value = (int)message["value"];
		this.uiSettings.UpdateMaxMatchmakingPing(value);
	}

	// Token: 0x06000DE1 RID: 3553 RVA: 0x0004A328 File Offset: 0x00048528
	private void Event_OnFilterChatProfanityChanged(Dictionary<string, object> message)
	{
		bool value = (bool)message["value"];
		this.uiSettings.UpdateFilterChatProfanity(value);
	}

	// Token: 0x06000DE2 RID: 3554 RVA: 0x0004A354 File Offset: 0x00048554
	private void Event_OnUnitsChanged(Dictionary<string, object> message)
	{
		string nameFromUnits = Utils.GetNameFromUnits((Units)message["value"]);
		this.uiSettings.UpdateUnits(nameFromUnits);
	}

	// Token: 0x06000DE3 RID: 3555 RVA: 0x0004A384 File Offset: 0x00048584
	private void Event_OnShowGameUserInterfaceChanged(Dictionary<string, object> message)
	{
		bool value = (bool)message["value"];
		this.uiSettings.UpdateShowGameUserInterface(value);
	}

	// Token: 0x06000DE4 RID: 3556 RVA: 0x0004A3B0 File Offset: 0x000485B0
	private void Event_OnUserInterfaceScaleChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateUserInterfaceScale(value);
	}

	// Token: 0x06000DE5 RID: 3557 RVA: 0x0004A3DC File Offset: 0x000485DC
	private void Event_OnChatOpacityChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateChatOpacity(value);
	}

	// Token: 0x06000DE6 RID: 3558 RVA: 0x0004A408 File Offset: 0x00048608
	private void Event_OnChatScaleChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateChatScale(value);
	}

	// Token: 0x06000DE7 RID: 3559 RVA: 0x0004A434 File Offset: 0x00048634
	private void Event_OnMinimapOpacityChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateMinimapOpacity(value);
	}

	// Token: 0x06000DE8 RID: 3560 RVA: 0x0004A460 File Offset: 0x00048660
	private void Event_OnMinimapBackgroundOpacityChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateMinimapBackgroundOpacity(value);
	}

	// Token: 0x06000DE9 RID: 3561 RVA: 0x0004A48C File Offset: 0x0004868C
	private void Event_OnMinimapHorizontalPositionChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateMinimapHorizontalPosition(value);
	}

	// Token: 0x06000DEA RID: 3562 RVA: 0x0004A4B8 File Offset: 0x000486B8
	private void Event_OnMinimapVerticalPositionChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateMinimapVerticalPosition(value);
	}

	// Token: 0x06000DEB RID: 3563 RVA: 0x0004A4E4 File Offset: 0x000486E4
	private void Event_OnMinimapScaleChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateMinimapScale(value);
	}

	// Token: 0x06000DEC RID: 3564 RVA: 0x0004A510 File Offset: 0x00048710
	private void Event_OnGlobalStickSensitivityChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateGlobalStickSensitivity(value);
	}

	// Token: 0x06000DED RID: 3565 RVA: 0x0004A53C File Offset: 0x0004873C
	private void Event_OnHorizontalStickSensitivityChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateHorizontalStickSensitivity(value);
	}

	// Token: 0x06000DEE RID: 3566 RVA: 0x0004A568 File Offset: 0x00048768
	private void Event_OnVerticalStickSensitivityChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateVerticalStickSensitivity(value);
	}

	// Token: 0x06000DEF RID: 3567 RVA: 0x0004A594 File Offset: 0x00048794
	private void Event_OnLookSensitivityChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateLookSensitivity(value);
	}

	// Token: 0x06000DF0 RID: 3568 RVA: 0x0004A5C0 File Offset: 0x000487C0
	private void Event_OnKeyBindsLoaded(Dictionary<string, object> message)
	{
		Dictionary<string, KeyBind> keyBinds = (Dictionary<string, KeyBind>)message["keyBinds"];
		this.uiSettings.UpdateKeyBindInputs(keyBinds);
	}

	// Token: 0x06000DF1 RID: 3569 RVA: 0x0004A5C0 File Offset: 0x000487C0
	private void Event_OnKeyBindsSaved(Dictionary<string, object> message)
	{
		Dictionary<string, KeyBind> keyBinds = (Dictionary<string, KeyBind>)message["keyBinds"];
		this.uiSettings.UpdateKeyBindInputs(keyBinds);
	}

	// Token: 0x06000DF2 RID: 3570 RVA: 0x0004A5EC File Offset: 0x000487EC
	private void Event_OnGlobalVolumeChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateGlobalVolume(value);
	}

	// Token: 0x06000DF3 RID: 3571 RVA: 0x0004A618 File Offset: 0x00048818
	private void Event_OnAmbientVolumeChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateAmbientVolume(value);
	}

	// Token: 0x06000DF4 RID: 3572 RVA: 0x0004A644 File Offset: 0x00048844
	private void Event_OnGameVolumeChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateGameVolume(value);
	}

	// Token: 0x06000DF5 RID: 3573 RVA: 0x0004A670 File Offset: 0x00048870
	private void Event_OnVoiceVolumeChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateVoiceVolume(value);
	}

	// Token: 0x06000DF6 RID: 3574 RVA: 0x0004A69C File Offset: 0x0004889C
	private void Event_OnUIVolumeChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateUIVolume(value);
	}

	// Token: 0x06000DF7 RID: 3575 RVA: 0x0004A6C8 File Offset: 0x000488C8
	private void Event_OnFullScreenModeChanged(Dictionary<string, object> message)
	{
		string nameFromFullScreenMode = Utils.GetNameFromFullScreenMode((FullScreenMode)message["value"]);
		this.uiSettings.UpdateFullScreenMode(nameFromFullScreenMode);
	}

	// Token: 0x06000DF8 RID: 3576 RVA: 0x0004A6F8 File Offset: 0x000488F8
	private void Event_OnDisplayIndexChanged(Dictionary<string, object> message)
	{
		string displayNameFromIndex = Utils.GetDisplayNameFromIndex((int)message["value"]);
		this.uiSettings.UpdateDisplay(displayNameFromIndex);
	}

	// Token: 0x06000DF9 RID: 3577 RVA: 0x0004A728 File Offset: 0x00048928
	private void Event_OnResolutionIndexChanged(Dictionary<string, object> message)
	{
		string resolutionNameFromIndex = Utils.GetResolutionNameFromIndex((int)message["value"]);
		this.uiSettings.UpdateResolution(resolutionNameFromIndex);
	}

	// Token: 0x06000DFA RID: 3578 RVA: 0x0004A758 File Offset: 0x00048958
	private void Event_OnVSyncChanged(Dictionary<string, object> message)
	{
		bool value = (bool)message["value"];
		this.uiSettings.UpdateVSync(value);
	}

	// Token: 0x06000DFB RID: 3579 RVA: 0x0004A784 File Offset: 0x00048984
	private void Event_OnFpsLimitChanged(Dictionary<string, object> message)
	{
		int value = (int)message["value"];
		this.uiSettings.UpdateFpsLimit(value);
	}

	// Token: 0x06000DFC RID: 3580 RVA: 0x0004A7B0 File Offset: 0x000489B0
	private void Event_OnFovChanged(Dictionary<string, object> message)
	{
		float value = (float)message["value"];
		this.uiSettings.UpdateFov(value);
	}

	// Token: 0x06000DFD RID: 3581 RVA: 0x0004A7DC File Offset: 0x000489DC
	private void Event_OnQualityChanged(Dictionary<string, object> message)
	{
		string nameFromApplicationQuality = Utils.GetNameFromApplicationQuality((ApplicationQuality)message["value"]);
		this.uiSettings.UpdateQuality(nameFromApplicationQuality);
	}

	// Token: 0x06000DFE RID: 3582 RVA: 0x0004A80C File Offset: 0x00048A0C
	private void Event_OnMotionBlurChanged(Dictionary<string, object> message)
	{
		bool value = (bool)message["value"];
		this.uiSettings.UpdateMotionBlur(value);
	}

	// Token: 0x06000DFF RID: 3583 RVA: 0x0004A838 File Offset: 0x00048A38
	private void Event_OnIsDisplayChangeInProgressChanged(Dictionary<string, object> message)
	{
		if ((bool)message["isDisplayChangeInProgress"])
		{
			return;
		}
		List<string> resolutionNames = Utils.GetResolutionNames();
		this.uiSettings.UpdateResolutionChoices(resolutionNames);
	}

	// Token: 0x04000850 RID: 2128
	private UISettings uiSettings;
}
