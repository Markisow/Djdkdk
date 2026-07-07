using System;
using UnityEngine;
using UnityEngine.Audio;

// Token: 0x02000088 RID: 136
public class AudioManager : MonoBehaviourSingleton<AudioManager>
{
	// Token: 0x060004A8 RID: 1192 RVA: 0x0000BCB1 File Offset: 0x00009EB1
	public void SetGlobalVolume(float volume)
	{
		this.mixer.SetFloat("globalVolume", Mathf.Log(volume + 0.001f) * 20f);
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0000BCD6 File Offset: 0x00009ED6
	public void SetAmbientVolume(float volume)
	{
		this.mixer.SetFloat("ambientVolume", Mathf.Log(volume + 0.001f) * 20f);
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0000BCFB File Offset: 0x00009EFB
	public void SetGameVolume(float volume)
	{
		this.mixer.SetFloat("gameVolume", Mathf.Log(volume + 0.001f) * 20f);
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0000BD20 File Offset: 0x00009F20
	public void SetVoiceVolume(float volume)
	{
		this.mixer.SetFloat("voiceVolume", Mathf.Log(volume + 0.001f) * 20f);
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0000BD45 File Offset: 0x00009F45
	public void SetUIVolume(float volume)
	{
		this.mixer.SetFloat("uiVolume", Mathf.Log(volume + 0.001f) * 20f);
	}

	// Token: 0x040002DF RID: 735
	[Header("References")]
	[SerializeField]
	private AudioMixer mixer;
}
