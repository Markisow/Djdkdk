using System;
using System.Collections.Generic;
using UI;
using UnityEngine.UIElements;

// Token: 0x020001B9 RID: 441
public class Popup
{
	// Token: 0x06000CEB RID: 3307 RVA: 0x00012AE6 File Offset: 0x00010CE6
	public Popup(VisualElement visualElement, string name, string title, BasePopupContent content, bool showOkButton, bool showCloseButton, object data = null)
	{
		this.VisualElement = visualElement;
		this.Name = name;
		this.Title = title;
		this.Content = content;
		this.ShowOkButton = showOkButton;
		this.ShowCloseButton = showCloseButton;
		this.Data = data;
	}

	// Token: 0x06000CEC RID: 3308 RVA: 0x00045A58 File Offset: 0x00043C58
	public void Initialize()
	{
		this.header = this.VisualElement.Query("Header", null);
		this.content = this.VisualElement.Query("Content", null);
		this.footer = this.VisualElement.Query("Footer", null);
		this.titleLabel = this.header.Query(null, null);
		this.titleLabel.text = this.Title;
		this.closeIconButton = this.header.Query("CloseIconButtonContainer", null).First().Query(null, null);
		this.closeIconButton.clicked += this.OnClickClose;
		this.okButton = this.footer.Query("OkButton", null);
		this.okButton.clicked += this.OnClickOk;
		if (!this.ShowOkButton)
		{
			this.okButton.style.display = DisplayStyle.None;
		}
		if (!this.ShowCloseButton)
		{
			this.closeIconButton.style.display = DisplayStyle.None;
		}
		this.Content.Initialize();
		this.content.Add(this.Content.VisualElement);
	}

	// Token: 0x06000CED RID: 3309 RVA: 0x00045BB4 File Offset: 0x00043DB4
	public void Dispose()
	{
		this.closeIconButton.clicked -= this.OnClickClose;
		this.okButton.clicked -= this.OnClickOk;
		this.content.Remove(this.Content.VisualElement);
		this.Content.Dispose();
	}

	// Token: 0x06000CEE RID: 3310 RVA: 0x00012B23 File Offset: 0x00010D23
	private void OnClickClose()
	{
		EventManager.TriggerEvent("Event_OnPopupClickClose", new Dictionary<string, object>
		{
			{
				"popup",
				this
			}
		});
	}

	// Token: 0x06000CEF RID: 3311 RVA: 0x00012B40 File Offset: 0x00010D40
	private void OnClickOk()
	{
		EventManager.TriggerEvent("Event_OnPopupClickOk", new Dictionary<string, object>
		{
			{
				"popup",
				this
			}
		});
	}

	// Token: 0x040007BB RID: 1979
	public TemplateContainer TemplateContainer;

	// Token: 0x040007BC RID: 1980
	public VisualElement VisualElement;

	// Token: 0x040007BD RID: 1981
	public string Name;

	// Token: 0x040007BE RID: 1982
	public string Title;

	// Token: 0x040007BF RID: 1983
	public BasePopupContent Content;

	// Token: 0x040007C0 RID: 1984
	public bool ShowOkButton;

	// Token: 0x040007C1 RID: 1985
	public bool ShowCloseButton;

	// Token: 0x040007C2 RID: 1986
	public object Data;

	// Token: 0x040007C3 RID: 1987
	private VisualElement header;

	// Token: 0x040007C4 RID: 1988
	private VisualElement content;

	// Token: 0x040007C5 RID: 1989
	private VisualElement footer;

	// Token: 0x040007C6 RID: 1990
	private Label titleLabel;

	// Token: 0x040007C7 RID: 1991
	private Button okButton;

	// Token: 0x040007C8 RID: 1992
	private IconButton closeIconButton;
}
