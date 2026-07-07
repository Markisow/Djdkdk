using System;
using Linework.SoftOutline;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

// Token: 0x02000057 RID: 87
public class PostProcessing : MonoBehaviour
{
	// Token: 0x060002F1 RID: 753 RVA: 0x0000ABEB File Offset: 0x00008DEB
	private void Awake()
	{
		this.volume = base.GetComponent<Volume>();
	}

	// Token: 0x060002F2 RID: 754 RVA: 0x0000ABF9 File Offset: 0x00008DF9
	public void SetPuckSilhouette(bool enabled)
	{
		this.universalRendererData.rendererFeatures.Find((ScriptableRendererFeature x) => x.name == "Puck Silhouette").SetActive(enabled);
	}

	// Token: 0x060002F3 RID: 755 RVA: 0x0000AC30 File Offset: 0x00008E30
	public void SetPuckOutline(bool enabled)
	{
		this.puckOutlineSettings.SetActive(enabled);
	}

	// Token: 0x060002F4 RID: 756 RVA: 0x00023AF8 File Offset: 0x00021CF8
	public void SetQuality(ApplicationQuality quality)
	{
		switch (quality)
		{
		case ApplicationQuality.Low:
			this.renderPipelineAsset.msaaSampleCount = 1;
			return;
		case ApplicationQuality.Medium:
			this.renderPipelineAsset.msaaSampleCount = 2;
			return;
		case ApplicationQuality.High:
			this.renderPipelineAsset.msaaSampleCount = 4;
			return;
		case ApplicationQuality.Ultra:
			this.renderPipelineAsset.msaaSampleCount = 8;
			return;
		default:
			return;
		}
	}

	// Token: 0x060002F5 RID: 757 RVA: 0x00023B50 File Offset: 0x00021D50
	public void SetMotionBlur(bool enabled)
	{
		MotionBlur motionBlur;
		if (this.volume.profile.TryGet<MotionBlur>(out motionBlur))
		{
			motionBlur.active = enabled;
		}
	}

	// Token: 0x04000216 RID: 534
	[Header("References")]
	[SerializeField]
	private UniversalRenderPipelineAsset renderPipelineAsset;

	// Token: 0x04000217 RID: 535
	[SerializeField]
	private UniversalRendererData universalRendererData;

	// Token: 0x04000218 RID: 536
	[SerializeField]
	private SoftOutlineSettings puckOutlineSettings;

	// Token: 0x04000219 RID: 537
	private Volume volume;
}
