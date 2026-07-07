using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000059 RID: 89
public class PostProcessingController : MonoBehaviour
{
	// Token: 0x060002FA RID: 762 RVA: 0x00023B78 File Offset: 0x00021D78
	private void Awake()
	{
		this.postProcessing = base.GetComponent<PostProcessing>();
		EventManager.AddEventListener("Event_OnShowPuckSilhouetteChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckSilhouetteChanged));
		EventManager.AddEventListener("Event_OnShowPuckOutlineChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckOutlineChanged));
		EventManager.AddEventListener("Event_OnQualityChanged", new Action<Dictionary<string, object>>(this.Event_OnQualityChanged));
		EventManager.AddEventListener("Event_OnMotionBlurChanged", new Action<Dictionary<string, object>>(this.Event_OnMotionBlurChanged));
	}

	// Token: 0x060002FB RID: 763 RVA: 0x00023BEC File Offset: 0x00021DEC
	private void Start()
	{
		this.postProcessing.SetPuckSilhouette(SettingsManager.ShowPuckSilhouette);
		this.postProcessing.SetPuckOutline(SettingsManager.ShowPuckOutline);
		this.postProcessing.SetQuality(SettingsManager.Quality);
		this.postProcessing.SetMotionBlur(SettingsManager.MotionBlur);
	}

	// Token: 0x060002FC RID: 764 RVA: 0x00023C3C File Offset: 0x00021E3C
	private void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnShowPuckSilhouetteChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckSilhouetteChanged));
		EventManager.RemoveEventListener("Event_OnShowPuckOutlineChanged", new Action<Dictionary<string, object>>(this.Event_OnShowPuckOutlineChanged));
		EventManager.RemoveEventListener("Event_OnQualityChanged", new Action<Dictionary<string, object>>(this.Event_OnQualityChanged));
		EventManager.RemoveEventListener("Event_OnMotionBlurChanged", new Action<Dictionary<string, object>>(this.Event_OnMotionBlurChanged));
	}

	// Token: 0x060002FD RID: 765 RVA: 0x00023CA4 File Offset: 0x00021EA4
	private void Event_OnShowPuckSilhouetteChanged(Dictionary<string, object> message)
	{
		bool puckSilhouette = (bool)message["value"];
		this.postProcessing.SetPuckSilhouette(puckSilhouette);
	}

	// Token: 0x060002FE RID: 766 RVA: 0x00023CD0 File Offset: 0x00021ED0
	private void Event_OnShowPuckOutlineChanged(Dictionary<string, object> message)
	{
		bool puckOutline = (bool)message["value"];
		this.postProcessing.SetPuckOutline(puckOutline);
	}

	// Token: 0x060002FF RID: 767 RVA: 0x00023CFC File Offset: 0x00021EFC
	private void Event_OnQualityChanged(Dictionary<string, object> message)
	{
		ApplicationQuality quality = (ApplicationQuality)message["value"];
		this.postProcessing.SetQuality(quality);
	}

	// Token: 0x06000300 RID: 768 RVA: 0x00023D28 File Offset: 0x00021F28
	private void Event_OnMotionBlurChanged(Dictionary<string, object> message)
	{
		bool motionBlur = (bool)message["value"];
		this.postProcessing.SetMotionBlur(motionBlur);
	}

	// Token: 0x0400021C RID: 540
	private PostProcessing postProcessing;
}
