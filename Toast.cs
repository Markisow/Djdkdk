using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x020001D4 RID: 468
public class Toast
{
	// Token: 0x06000E08 RID: 3592 RVA: 0x000138F7 File Offset: 0x00011AF7
	public Toast(UIToastManager uiToastManager, VisualElement visualElement, string name, string content, float hideDelay)
	{
		this.UIToastManager = uiToastManager;
		this.VisualElement = visualElement;
		this.Name = name;
		this.Content = content;
		this.HideDelay = hideDelay;
		this.Initialize();
	}

	// Token: 0x06000E09 RID: 3593 RVA: 0x0004A948 File Offset: 0x00048B48
	public void Initialize()
	{
		this.VisualElement.RegisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnClick), TrickleDown.NoTrickleDown);
		this.contentLabel = this.VisualElement.Query("ContentLabel", null);
		this.contentLabel.text = this.Content;
		this.Hide();
	}

	// Token: 0x06000E0A RID: 3594 RVA: 0x0001392A File Offset: 0x00011B2A
	public void Hide()
	{
		this.hideCoroutine = this.IHide();
		this.UIToastManager.StartCoroutine(this.hideCoroutine);
	}

	// Token: 0x06000E0B RID: 3595 RVA: 0x0001394A File Offset: 0x00011B4A
	private IEnumerator IHide()
	{
		yield return new WaitForSeconds(this.HideDelay);
		this.UIToastManager.HideToast(this.Name);
		this.hideCoroutine = null;
		yield break;
	}

	// Token: 0x06000E0C RID: 3596 RVA: 0x00013959 File Offset: 0x00011B59
	public void Dispose()
	{
		this.VisualElement.UnregisterCallback<ClickEvent>(new EventCallback<ClickEvent>(this.OnClick), TrickleDown.NoTrickleDown);
		if (this.hideCoroutine != null)
		{
			this.UIToastManager.StopCoroutine(this.hideCoroutine);
			this.hideCoroutine = null;
		}
	}

	// Token: 0x06000E0D RID: 3597 RVA: 0x00013993 File Offset: 0x00011B93
	private void OnClick(ClickEvent clickEvent)
	{
		this.UIToastManager.HideToast(this.Name);
	}

	// Token: 0x04000856 RID: 2134
	public UIToastManager UIToastManager;

	// Token: 0x04000857 RID: 2135
	public VisualElement VisualElement;

	// Token: 0x04000858 RID: 2136
	public string Name;

	// Token: 0x04000859 RID: 2137
	public string Content;

	// Token: 0x0400085A RID: 2138
	public float HideDelay;

	// Token: 0x0400085B RID: 2139
	private IEnumerator hideCoroutine;

	// Token: 0x0400085C RID: 2140
	private Label contentLabel;
}
