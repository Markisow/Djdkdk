using System;
using UnityEngine.UIElements;

// Token: 0x020001AE RID: 430
public class UIPlayerMenu : UIView
{
	// Token: 0x06000CB2 RID: 3250 RVA: 0x000454F0 File Offset: 0x000436F0
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("PlayerMenuView", null);
		this.playerMenu = base.View.Query("PlayerMenu", null);
		this.identityButton = this.playerMenu.Query("IdentityButton", null);
		this.identityButton.clicked += this.OnClickIdentity;
		this.appearanceButton = this.playerMenu.Query("AppearanceButton", null);
		this.appearanceButton.clicked += this.OnClickAppearance;
		this.backButton = this.playerMenu.Query("BackButton", null);
		this.backButton.clicked += this.OnClickBack;
	}

	// Token: 0x06000CB3 RID: 3251 RVA: 0x000127DD File Offset: 0x000109DD
	public override bool Show()
	{
		bool flag = base.Show();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnPlayerMenuShow", null);
		}
		return flag;
	}

	// Token: 0x06000CB4 RID: 3252 RVA: 0x000127F3 File Offset: 0x000109F3
	public override bool Hide()
	{
		bool flag = base.Hide();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnPlayerMenuHide", null);
		}
		return flag;
	}

	// Token: 0x06000CB5 RID: 3253 RVA: 0x00012809 File Offset: 0x00010A09
	private void OnClickIdentity()
	{
		EventManager.TriggerEvent("Event_OnPlayerMenuClickIdentity", null);
	}

	// Token: 0x06000CB6 RID: 3254 RVA: 0x00012816 File Offset: 0x00010A16
	private void OnClickAppearance()
	{
		EventManager.TriggerEvent("Event_OnPlayerMenuClickAppearance", null);
	}

	// Token: 0x06000CB7 RID: 3255 RVA: 0x00012823 File Offset: 0x00010A23
	private void OnClickBack()
	{
		EventManager.TriggerEvent("Event_OnPlayerMenuClickBack", null);
	}

	// Token: 0x0400079B RID: 1947
	private VisualElement playerMenu;

	// Token: 0x0400079C RID: 1948
	private Button identityButton;

	// Token: 0x0400079D RID: 1949
	private Button appearanceButton;

	// Token: 0x0400079E RID: 1950
	private Button backButton;
}
