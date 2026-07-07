using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x020001D6 RID: 470
public class UIToastManager : UIView
{
	// Token: 0x06000E14 RID: 3604 RVA: 0x000139BD File Offset: 0x00011BBD
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("ToastsView", null);
		this.toasts = base.View.Query("Toasts", null);
		this.toasts.Clear();
	}

	// Token: 0x06000E15 RID: 3605 RVA: 0x0004AA08 File Offset: 0x00048C08
	public void ShowToast(string name, string content, float hideDelay = 3f)
	{
		if (this.nameToastMap.ContainsKey(name))
		{
			this.HideToast(name);
		}
		VisualElement visualElement = this.toastAsset.Instantiate();
		Toast toast = new Toast(this, visualElement, name, content.ToUpper(), hideDelay);
		this.toasts.Add(toast.VisualElement);
		this.nameToastMap.Add(name, toast);
	}

	// Token: 0x06000E16 RID: 3606 RVA: 0x0004AA64 File Offset: 0x00048C64
	public void HideToast(string name)
	{
		if (!this.nameToastMap.ContainsKey(name))
		{
			return;
		}
		this.toasts.Remove(this.nameToastMap[name].VisualElement);
		this.nameToastMap[name].Dispose();
		this.nameToastMap.Remove(name);
	}

	// Token: 0x04000860 RID: 2144
	[Header("References")]
	[SerializeField]
	public VisualTreeAsset toastAsset;

	// Token: 0x04000861 RID: 2145
	private Dictionary<string, Toast> nameToastMap = new Dictionary<string, Toast>();

	// Token: 0x04000862 RID: 2146
	private VisualElement toasts;
}
