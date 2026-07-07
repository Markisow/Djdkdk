using System;
using UnityEngine.UIElements;

// Token: 0x02000188 RID: 392
public class UIDebug : UIView
{
	// Token: 0x06000B73 RID: 2931 RVA: 0x0001153E File Offset: 0x0000F73E
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("DebugView", null);
		this.buildLabel = base.View.Query("BuildLabel", null);
	}

	// Token: 0x06000B74 RID: 2932 RVA: 0x00011573 File Offset: 0x0000F773
	public override bool Show()
	{
		return SettingsManager.Debug && base.Show();
	}

	// Token: 0x06000B75 RID: 2933 RVA: 0x00011589 File Offset: 0x0000F789
	public override bool Hide()
	{
		return !SettingsManager.Debug && base.Hide();
	}

	// Token: 0x06000B76 RID: 2934 RVA: 0x0001159F File Offset: 0x0000F79F
	public void SetBuild(string text)
	{
		this.buildLabel.text = text;
	}

	// Token: 0x040006E0 RID: 1760
	private Label buildLabel;
}
