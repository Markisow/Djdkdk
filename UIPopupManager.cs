using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x020001BA RID: 442
public class UIPopupManager : UIView
{
	// Token: 0x06000CF0 RID: 3312 RVA: 0x00045C10 File Offset: 0x00043E10
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("PopupsView", null);
		this.popups = base.View.Query("Popups", null);
		this.popups.Clear();
		this.UpdateFocus();
		this.UpdateVisibility();
	}

	// Token: 0x06000CF1 RID: 3313 RVA: 0x00045C68 File Offset: 0x00043E68
	public void ShowPopup(string name, string title, BasePopupContent content, bool showOkButton, bool showCloseButton, object data = null)
	{
		if (this.GetPopupByName(name) != null)
		{
			return;
		}
		Popup popup = new Popup(this.popupAsset.Instantiate(), name, title, content, showOkButton, showCloseButton, data);
		this.popups.Add(popup.VisualElement);
		this.namePopupMap.Add(name, popup);
		popup.Initialize();
		popup.VisualElement.BringToFront();
		this.UpdateFocus();
		this.UpdateVisibility();
		EventManager.TriggerEvent("Event_OnPopupShow", new Dictionary<string, object>
		{
			{
				"name",
				name
			}
		});
	}

	// Token: 0x06000CF2 RID: 3314 RVA: 0x00045CF0 File Offset: 0x00043EF0
	public void HidePopup(string name)
	{
		Popup popupByName = this.GetPopupByName(name);
		if (popupByName == null)
		{
			return;
		}
		this.popups.Remove(popupByName.VisualElement);
		this.namePopupMap.Remove(name);
		popupByName.Dispose();
		this.UpdateFocus();
		this.UpdateVisibility();
		EventManager.TriggerEvent("Event_OnPopupHide", new Dictionary<string, object>
		{
			{
				"name",
				name
			}
		});
	}

	// Token: 0x06000CF3 RID: 3315 RVA: 0x00012B5D File Offset: 0x00010D5D
	public Popup GetPopupByName(string name)
	{
		if (this.namePopupMap.ContainsKey(name))
		{
			return this.namePopupMap[name];
		}
		return null;
	}

	// Token: 0x06000CF4 RID: 3316 RVA: 0x00012B7B File Offset: 0x00010D7B
	private void UpdateFocus()
	{
		base.IsFocused = (this.namePopupMap.Count > 0);
	}

	// Token: 0x06000CF5 RID: 3317 RVA: 0x00012B91 File Offset: 0x00010D91
	private void UpdateVisibility()
	{
		this.popups.style.display = ((this.namePopupMap.Count > 0) ? DisplayStyle.Flex : DisplayStyle.None);
	}

	// Token: 0x06000CF6 RID: 3318 RVA: 0x00012BBA File Offset: 0x00010DBA
	public PopupNotificationContent CreateNotificationContent(string text)
	{
		return new PopupNotificationContent(this.notificationContentAsset, text);
	}

	// Token: 0x06000CF7 RID: 3319 RVA: 0x00012BC8 File Offset: 0x00010DC8
	public PopupMissingPasswordContent CreateMissingPasswordContent()
	{
		return new PopupMissingPasswordContent(this.missingPasswordContentAsset);
	}

	// Token: 0x06000CF8 RID: 3320 RVA: 0x00012BD5 File Offset: 0x00010DD5
	public PopupMissingModsPopupContent CreateMissingModsContent(string text, string notice, string[] missingModIds)
	{
		return new PopupMissingModsPopupContent(this.missingModsContentAsset, this.modPreviewAsset, text, notice, missingModIds);
	}

	// Token: 0x040007C9 RID: 1993
	[Header("References")]
	[SerializeField]
	private VisualTreeAsset popupAsset;

	// Token: 0x040007CA RID: 1994
	[SerializeField]
	private VisualTreeAsset notificationContentAsset;

	// Token: 0x040007CB RID: 1995
	[SerializeField]
	private VisualTreeAsset missingPasswordContentAsset;

	// Token: 0x040007CC RID: 1996
	[SerializeField]
	private VisualTreeAsset missingModsContentAsset;

	// Token: 0x040007CD RID: 1997
	[SerializeField]
	private VisualTreeAsset modPreviewAsset;

	// Token: 0x040007CE RID: 1998
	private VisualElement popups;

	// Token: 0x040007CF RID: 1999
	private Dictionary<string, Popup> namePopupMap = new Dictionary<string, Popup>();
}
