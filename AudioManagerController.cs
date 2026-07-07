using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000089 RID: 137
public class AudioManagerController : MonoBehaviour
{
	// Token: 0x060004AE RID: 1198 RVA: 0x00029C4C File Offset: 0x00027E4C
	private void Awake()
	{
		this.audioManager = base.GetComponent<AudioManager>();
		EventManager.AddEventListener("Event_OnGlobalVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnGlobalVolumeChanged));
		EventManager.AddEventListener("Event_OnAmbientVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnAmbientVolumeChanged));
		EventManager.AddEventListener("Event_OnGameVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnGameVolumeChanged));
		EventManager.AddEventListener("Event_OnVoiceVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnVoiceVolumeChanged));
		EventManager.AddEventListener("Event_OnUIVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnUIVolumeChanged));
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00029CD4 File Offset: 0x00027ED4
	private void Start()
	{
		this.audioManager.SetGlobalVolume(SettingsManager.GlobalVolume);
		this.audioManager.SetAmbientVolume(SettingsManager.AmbientVolume);
		this.audioManager.SetGameVolume(SettingsManager.GameVolume);
		this.audioManager.SetVoiceVolume(SettingsManager.VoiceVolume);
		this.audioManager.SetUIVolume(SettingsManager.UIVolume);
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x00029D34 File Offset: 0x00027F34
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnGlobalVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnGlobalVolumeChanged));
		EventManager.RemoveEventListener("Event_OnAmbientVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnAmbientVolumeChanged));
		EventManager.RemoveEventListener("Event_OnGameVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnGameVolumeChanged));
		EventManager.RemoveEventListener("Event_OnVoiceVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnVoiceVolumeChanged));
		EventManager.RemoveEventListener("Event_OnUIVolumeChanged", new Action<Dictionary<string, object>>(this.Event_OnUIVolumeChanged));
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x00029DB0 File Offset: 0x00027FB0
	private void Event_OnGlobalVolumeChanged(Dictionary<string, object> eventParams)
	{
		float globalVolume = (float)eventParams["value"];
		this.audioManager.SetGlobalVolume(globalVolume);
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x00029DDC File Offset: 0x00027FDC
	private void Event_OnAmbientVolumeChanged(Dictionary<string, object> eventParams)
	{
		float ambientVolume = (float)eventParams["value"];
		this.audioManager.SetAmbientVolume(ambientVolume);
	}

	// Token: 0x060004B3 RID: 1203 RVA: 0x00029E08 File Offset: 0x00028008
	private void Event_OnGameVolumeChanged(Dictionary<string, object> eventParams)
	{
		float gameVolume = (float)eventParams["value"];
		this.audioManager.SetGameVolume(gameVolume);
	}

	// Token: 0x060004B4 RID: 1204 RVA: 0x00029E34 File Offset: 0x00028034
	private void Event_OnVoiceVolumeChanged(Dictionary<string, object> eventParams)
	{
		float voiceVolume = (float)eventParams["value"];
		this.audioManager.SetVoiceVolume(voiceVolume);
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x00029E60 File Offset: 0x00028060
	private void Event_OnUIVolumeChanged(Dictionary<string, object> eventParams)
	{
		float uivolume = (float)eventParams["value"];
		this.audioManager.SetUIVolume(uivolume);
	}

	// Token: 0x040002E0 RID: 736
	private AudioManager audioManager;
}
