using System;
using UnityEngine.UIElements;

// Token: 0x020001B2 RID: 434
public class PopupMissingPasswordContent : BasePopupContent
{
	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0001292A File Offset: 0x00010B2A
	// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x00012932 File Offset: 0x00010B32
	public string Password
	{
		get
		{
			return this.password;
		}
		set
		{
			if (this.password == value)
			{
				return;
			}
			this.password = value;
			this.Update();
		}
	}

	// Token: 0x06000CC9 RID: 3273 RVA: 0x00012950 File Offset: 0x00010B50
	public PopupMissingPasswordContent(VisualTreeAsset asset) : base(asset)
	{
	}

	// Token: 0x06000CCA RID: 3274 RVA: 0x000455CC File Offset: 0x000437CC
	public override void Initialize()
	{
		base.Initialize();
		this.textField = base.VisualElement.Query("PasswordTextField", null).First().Query(null, null);
		this.textField.value = this.Password;
		this.textField.RegisterCallback<ChangeEvent<string>>(new EventCallback<ChangeEvent<string>>(this.OnPasswordChanged), TrickleDown.NoTrickleDown);
		this.Update();
	}

	// Token: 0x06000CCB RID: 3275 RVA: 0x00012964 File Offset: 0x00010B64
	public override void Dispose()
	{
		base.Dispose();
		this.textField.UnregisterCallback<ChangeEvent<string>>(new EventCallback<ChangeEvent<string>>(this.OnPasswordChanged), TrickleDown.NoTrickleDown);
	}

	// Token: 0x06000CCC RID: 3276 RVA: 0x00012984 File Offset: 0x00010B84
	internal override void Update()
	{
		base.Update();
		this.textField.value = this.Password;
	}

	// Token: 0x06000CCD RID: 3277 RVA: 0x0001299D File Offset: 0x00010B9D
	private void OnPasswordChanged(ChangeEvent<string> changeEvent)
	{
		this.Password = changeEvent.newValue;
	}

	// Token: 0x040007A6 RID: 1958
	private string password = string.Empty;

	// Token: 0x040007A7 RID: 1959
	private TextField textField;
}
