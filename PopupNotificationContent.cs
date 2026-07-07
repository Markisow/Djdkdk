using System;
using UnityEngine.UIElements;

// Token: 0x020001B1 RID: 433
public class PopupNotificationContent : BasePopupContent
{
	// Token: 0x17000117 RID: 279
	// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x000128A9 File Offset: 0x00010AA9
	// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x000128B1 File Offset: 0x00010AB1
	public string Text
	{
		get
		{
			return this.text;
		}
		set
		{
			if (this.text == value)
			{
				return;
			}
			this.text = value;
			this.Update();
		}
	}

	// Token: 0x06000CC4 RID: 3268 RVA: 0x000128CF File Offset: 0x00010ACF
	public PopupNotificationContent(VisualTreeAsset asset, string text) : base(asset)
	{
		this.text = text;
	}

	// Token: 0x06000CC5 RID: 3269 RVA: 0x000128DF File Offset: 0x00010ADF
	public override void Initialize()
	{
		base.Initialize();
		this.textLabel = base.VisualElement.Query("TextLabel", null);
		this.Update();
	}

	// Token: 0x06000CC6 RID: 3270 RVA: 0x00012909 File Offset: 0x00010B09
	internal override void Update()
	{
		base.Update();
		if (this.textLabel != null)
		{
			this.textLabel.text = this.Text;
		}
	}

	// Token: 0x040007A4 RID: 1956
	private string text;

	// Token: 0x040007A5 RID: 1957
	private Label textLabel;
}
