using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x020001A6 RID: 422
public class UIOverlayManager : UIView
{
	// Token: 0x06000C7A RID: 3194 RVA: 0x00012529 File Offset: 0x00010729
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("OverlaysView", null);
		this.overlays = base.View.Query("Overlays", null);
		this.overlays.Clear();
	}

	// Token: 0x06000C7B RID: 3195 RVA: 0x00044A78 File Offset: 0x00042C78
	private void OnDestroy()
	{
		foreach (Overlay overlay in this.identifierOverlaysMap.Values)
		{
			overlay.Dispose();
		}
	}

	// Token: 0x06000C7C RID: 3196 RVA: 0x00044AD0 File Offset: 0x00042CD0
	public void ShowOverlay(string identifier, bool requiresSpinner = false, bool fadeIn = false, bool fadeOut = false, float fadeTime = 0.25f, bool autoHide = false, float hideTimeout = 0.25f)
	{
		Overlay overlay;
		if (!this.identifierOverlaysMap.ContainsKey(identifier))
		{
			overlay = new Overlay(this.overlayAsset.Instantiate(), identifier, requiresSpinner);
			Overlay overlay2 = overlay;
			overlay2.Hidden = (Action)Delegate.Combine(overlay2.Hidden, new Action(delegate()
			{
				overlay.Dispose();
				this.identifierOverlaysMap.Remove(identifier);
				this.overlays.Remove(overlay.VisualElement);
			}));
			this.identifierOverlaysMap.Add(identifier, overlay);
			this.overlays.Add(overlay.VisualElement);
		}
		else
		{
			overlay = this.identifierOverlaysMap[identifier];
		}
		overlay.FadeIn = fadeIn;
		overlay.FadeOut = fadeOut;
		overlay.FadeTime = fadeTime;
		overlay.AutoHide = autoHide;
		overlay.HideTimeout = hideTimeout;
		overlay.Show();
	}

	// Token: 0x06000C7D RID: 3197 RVA: 0x00012569 File Offset: 0x00010769
	public void HideOverlay(string identifier)
	{
		if (!this.identifierOverlaysMap.ContainsKey(identifier))
		{
			return;
		}
		this.identifierOverlaysMap[identifier].Hide();
	}

	// Token: 0x04000780 RID: 1920
	[Header("References")]
	public VisualTreeAsset overlayAsset;

	// Token: 0x04000781 RID: 1921
	private VisualElement overlays;

	// Token: 0x04000782 RID: 1922
	private Dictionary<string, Overlay> identifierOverlaysMap = new Dictionary<string, Overlay>();
}
