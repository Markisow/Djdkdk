using System;
using System.Collections.Generic;
using UI;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;

// Token: 0x02000195 RID: 405
public class UIIdentity : UIView
{
	// Token: 0x06000BC3 RID: 3011 RVA: 0x00041EAC File Offset: 0x000400AC
	public void Initialize(VisualElement rootVisualElement)
	{
		base.View = rootVisualElement.Query("IdentityView", null);
		this.identity = base.View.Query("Identity", null);
		this.closeIconButton = this.identity.Query("CloseIconButtonContainer", null).First().Query(null, null);
		this.closeIconButton.clicked += this.OnClickClose;
		this.confirmButton = this.identity.Query("ConfirmButton", null);
		this.confirmButton.clicked += this.OnClickConfirm;
		this.usernameTextField = this.identity.Query("UsernameTextField", null).First().Query(null, null);
		this.usernameTextField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<string>>(this.OnNameChanged));
		this.usernameTextField.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnNameFocusOut), TrickleDown.NoTrickleDown);
		this.numberIntegerField = this.identity.Query("NumberIntegerField", null).First().Query(null, null);
		this.numberIntegerField.RegisterValueChangedCallback(new EventCallback<ChangeEvent<int>>(this.OnNumberChanged));
		this.numberIntegerField.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnNumberFocusOut), TrickleDown.NoTrickleDown);
	}

	// Token: 0x06000BC4 RID: 3012 RVA: 0x000119FB File Offset: 0x0000FBFB
	public override bool Show()
	{
		bool flag = base.Show();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnIdentityShow", null);
		}
		return flag;
	}

	// Token: 0x06000BC5 RID: 3013 RVA: 0x00011A11 File Offset: 0x0000FC11
	public override bool Hide()
	{
		bool flag = base.Hide();
		if (flag)
		{
			EventManager.TriggerEvent("Event_OnIdentityHide", null);
		}
		return flag;
	}

	// Token: 0x06000BC6 RID: 3014 RVA: 0x00011A27 File Offset: 0x0000FC27
	public void SetIdentity(string username, int number)
	{
		this.username = username;
		this.usernameTextField.value = this.username;
		this.number = number;
		this.numberIntegerField.value = this.number;
	}

	// Token: 0x06000BC7 RID: 3015 RVA: 0x00011A59 File Offset: 0x0000FC59
	private void OnNameChanged(ChangeEvent<string> changeEvent)
	{
		this.username = changeEvent.newValue;
		if (!string.IsNullOrEmpty(this.username))
		{
			this.username = StringUtils.FilterStringNotLetters(this.username);
			this.usernameTextField.value = this.username;
		}
	}

	// Token: 0x06000BC8 RID: 3016 RVA: 0x00042018 File Offset: 0x00040218
	private void OnNameFocusOut(FocusOutEvent focusOutEvent)
	{
		this.username = this.usernameTextField.value;
		if (!string.IsNullOrEmpty(this.username))
		{
			this.username = StringUtils.FilterStringNotLetters(this.username);
			this.username = StringUtils.FilterStringProfanity(this.username, false);
			this.usernameTextField.value = this.username;
		}
	}

	// Token: 0x06000BC9 RID: 3017 RVA: 0x00011A96 File Offset: 0x0000FC96
	private void OnNumberChanged(ChangeEvent<int> changeEvent)
	{
		this.number = changeEvent.newValue;
	}

	// Token: 0x06000BCA RID: 3018 RVA: 0x00011AA4 File Offset: 0x0000FCA4
	private void OnNumberFocusOut(FocusOutEvent focusOutEvent)
	{
		this.number = Mathf.Clamp(this.numberIntegerField.value, 1, 99);
		this.numberIntegerField.value = this.number;
	}

	// Token: 0x06000BCB RID: 3019 RVA: 0x00011AD0 File Offset: 0x0000FCD0
	private void OnClickClose()
	{
		EventManager.TriggerEvent("Event_OnIdentityClickClose", null);
	}

	// Token: 0x06000BCC RID: 3020 RVA: 0x00011ADD File Offset: 0x0000FCDD
	private void OnClickConfirm()
	{
		EventManager.TriggerEvent("Event_OnIdentityClickConfirm", new Dictionary<string, object>
		{
			{
				"username",
				this.username
			},
			{
				"number",
				this.number
			}
		});
	}

	// Token: 0x0400070C RID: 1804
	private VisualElement identity;

	// Token: 0x0400070D RID: 1805
	private TextField usernameTextField;

	// Token: 0x0400070E RID: 1806
	private IntegerField numberIntegerField;

	// Token: 0x0400070F RID: 1807
	private IconButton closeIconButton;

	// Token: 0x04000710 RID: 1808
	private Button confirmButton;

	// Token: 0x04000711 RID: 1809
	private string username;

	// Token: 0x04000712 RID: 1810
	private int number;
}
