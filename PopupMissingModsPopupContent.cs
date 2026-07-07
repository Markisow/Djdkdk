using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UI;
using UnityEngine.UIElements;

// Token: 0x020001B3 RID: 435
public class PopupMissingModsPopupContent : BasePopupContent
{
	// Token: 0x17000119 RID: 281
	// (get) Token: 0x06000CCE RID: 3278 RVA: 0x000129AB File Offset: 0x00010BAB
	// (set) Token: 0x06000CCF RID: 3279 RVA: 0x000129B3 File Offset: 0x00010BB3
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

	// Token: 0x1700011A RID: 282
	// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x000129D1 File Offset: 0x00010BD1
	// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x000129D9 File Offset: 0x00010BD9
	public string Notice
	{
		get
		{
			return this.notice;
		}
		set
		{
			if (this.notice == value)
			{
				return;
			}
			this.notice = value;
			this.Update();
		}
	}

	// Token: 0x06000CD2 RID: 3282 RVA: 0x0004563C File Offset: 0x0004383C
	public PopupMissingModsPopupContent(VisualTreeAsset asset, VisualTreeAsset modPreviewAsset, string text, string notice, string[] missingModIds) : base(asset)
	{
		this.modPreviewAsset = modPreviewAsset;
		this.text = text;
		this.notice = notice;
		this.steamWorkshopItems = (from modId in missingModIds
		select new SteamWorkshopItem(modId, null)).ToArray<SteamWorkshopItem>();
	}

	// Token: 0x06000CD3 RID: 3283 RVA: 0x000456A4 File Offset: 0x000438A4
	public override void Initialize()
	{
		base.Initialize();
		this.textLabel = base.VisualElement.Query("TextLabel", null);
		this.noticeLabel = base.VisualElement.Query("NoticeLabel", null);
		this.missingModsList = base.VisualElement.Query("MissingModsList", null);
		SteamWorkshopItem[] array = this.steamWorkshopItems;
		for (int i = 0; i < array.Length; i++)
		{
			SteamWorkshopItem steamWorkshopItem = array[i];
			this.AddModPreview(steamWorkshopItem);
			SteamWorkshopItem steamWorkshopItem3 = steamWorkshopItem;
			steamWorkshopItem3.StateChanged = (Action<SteamWorkshopItemState, SteamWorkshopItemState>)Delegate.Combine(steamWorkshopItem3.StateChanged, new Action<SteamWorkshopItemState, SteamWorkshopItemState>(delegate(SteamWorkshopItemState oldState, SteamWorkshopItemState newState)
			{
				this.OnSteamWorkshopItemStateChanged(steamWorkshopItem.Id, oldState, newState);
			}));
			SteamWorkshopItem steamWorkshopItem2 = steamWorkshopItem;
			steamWorkshopItem2.DetailsStateChanged = (Action<SteamWorkshopItemDetailsState, SteamWorkshopItemDetailsState>)Delegate.Combine(steamWorkshopItem2.DetailsStateChanged, new Action<SteamWorkshopItemDetailsState, SteamWorkshopItemDetailsState>(delegate(SteamWorkshopItemDetailsState oldDetailsState, SteamWorkshopItemDetailsState newDetailsState)
			{
				this.OnSteamWorkshopItemDetailsStateChanged(steamWorkshopItem.Id, oldDetailsState, newDetailsState);
			}));
			steamWorkshopItem.Initialize();
		}
		this.Update();
	}

	// Token: 0x06000CD4 RID: 3284 RVA: 0x000129F7 File Offset: 0x00010BF7
	internal override void Update()
	{
		base.Update();
		if (this.textLabel != null)
		{
			this.textLabel.text = this.Text;
		}
		if (this.noticeLabel != null)
		{
			this.noticeLabel.text = this.Notice;
		}
	}

	// Token: 0x06000CD5 RID: 3285 RVA: 0x000457A4 File Offset: 0x000439A4
	public override void Dispose()
	{
		base.Dispose();
		foreach (SteamWorkshopItem steamWorkshopItem in this.steamWorkshopItems)
		{
			steamWorkshopItem.Dispose();
			this.RemoveMod(steamWorkshopItem);
		}
	}

	// Token: 0x06000CD6 RID: 3286 RVA: 0x000457E0 File Offset: 0x000439E0
	private void AddModPreview(SteamWorkshopItem steamWorkshopItem)
	{
		TemplateContainer templateContainer = this.modPreviewAsset.Instantiate();
		ModPreview uiModPreview = templateContainer.Query(null, null);
		ModPreview uiModPreview2 = uiModPreview;
		Action <>9__1;
		uiModPreview2.Ready = (Action)Delegate.Combine(uiModPreview2.Ready, new Action(delegate()
		{
			Link link = uiModPreview.Link;
			Delegate clicked = link.Clicked;
			Action b;
			if ((b = <>9__1) == null)
			{
				b = (<>9__1 = delegate()
				{
					this.OnModLinkClicked(steamWorkshopItem);
				});
			}
			link.Clicked = (Action)Delegate.Combine(clicked, b);
			this.UpdateMod(steamWorkshopItem);
		}));
		this.steamWorkshopItemTemplateContainerMap.Add(steamWorkshopItem, templateContainer);
		this.missingModsList.Add(templateContainer);
	}

	// Token: 0x06000CD7 RID: 3287 RVA: 0x00045868 File Offset: 0x00043A68
	private void UpdateMod(SteamWorkshopItem steamWorkshopItem)
	{
		if (!this.steamWorkshopItemTemplateContainerMap.ContainsKey(steamWorkshopItem))
		{
			return;
		}
		ModPreview modPreview = this.steamWorkshopItemTemplateContainerMap[steamWorkshopItem].Query(null, null);
		modPreview.IsStatisticsVisible = (steamWorkshopItem.Details != null);
		SteamWorkshopItemDetails details = steamWorkshopItem.Details;
		modPreview.Subscriptions = ((details != null) ? details.Subscriptions : 0);
		SteamWorkshopItemDetails details2 = steamWorkshopItem.Details;
		modPreview.Upvotes = ((details2 != null) ? details2.Upvotes : 0);
		SteamWorkshopItemDetails details3 = steamWorkshopItem.Details;
		modPreview.Downvotes = ((details3 != null) ? details3.Downvotes : 0);
		Link link = modPreview.Link;
		SteamWorkshopItemDetails details4 = steamWorkshopItem.Details;
		link.Text = (((details4 != null) ? details4.Title : null) ?? steamWorkshopItem.Id.ToString());
	}

	// Token: 0x06000CD8 RID: 3288 RVA: 0x00045920 File Offset: 0x00043B20
	private void RemoveMod(SteamWorkshopItem steamWorkshopItem)
	{
		if (!this.steamWorkshopItemTemplateContainerMap.ContainsKey(steamWorkshopItem))
		{
			return;
		}
		TemplateContainer element = this.steamWorkshopItemTemplateContainerMap[steamWorkshopItem];
		this.missingModsList.Remove(element);
		this.steamWorkshopItemTemplateContainerMap.Remove(steamWorkshopItem);
	}

	// Token: 0x06000CD9 RID: 3289 RVA: 0x00045964 File Offset: 0x00043B64
	private void OnSteamWorkshopItemStateChanged(string id, SteamWorkshopItemState oldState, SteamWorkshopItemState newState)
	{
		SteamWorkshopItem steamWorkshopItem = this.steamWorkshopItems.FirstOrDefault((SteamWorkshopItem item) => item.Id == id);
		if (steamWorkshopItem != null)
		{
			this.UpdateMod(steamWorkshopItem);
		}
	}

	// Token: 0x06000CDA RID: 3290 RVA: 0x000459A0 File Offset: 0x00043BA0
	private void OnSteamWorkshopItemDetailsStateChanged(string id, SteamWorkshopItemDetailsState oldDetailsState, SteamWorkshopItemDetailsState newDetailsState)
	{
		SteamWorkshopItem steamWorkshopItem = this.steamWorkshopItems.FirstOrDefault((SteamWorkshopItem item) => item.Id == id);
		if (steamWorkshopItem != null)
		{
			this.UpdateMod(steamWorkshopItem);
		}
	}

	// Token: 0x06000CDB RID: 3291 RVA: 0x000459DC File Offset: 0x00043BDC
	private void OnSteamWorkshopItemPropertyChanged(object sender, PropertyChangedEventArgs e)
	{
		SteamWorkshopItem steamWorkshopItem = (SteamWorkshopItem)sender;
		this.UpdateMod(steamWorkshopItem);
	}

	// Token: 0x06000CDC RID: 3292 RVA: 0x00012A31 File Offset: 0x00010C31
	private void OnModLinkClicked(SteamWorkshopItem steamWorkshopItem)
	{
		EventManager.TriggerEvent("Event_OnModPreviewLinkClicked", new Dictionary<string, object>
		{
			{
				"id",
				steamWorkshopItem.Id
			}
		});
	}

	// Token: 0x040007A8 RID: 1960
	private static readonly Logger Logger = new Logger("PopupMissingModsPopupContent");

	// Token: 0x040007A9 RID: 1961
	private string text;

	// Token: 0x040007AA RID: 1962
	private string notice;

	// Token: 0x040007AB RID: 1963
	private VisualTreeAsset modPreviewAsset;

	// Token: 0x040007AC RID: 1964
	private Label textLabel;

	// Token: 0x040007AD RID: 1965
	private Label noticeLabel;

	// Token: 0x040007AE RID: 1966
	private VisualElement missingModsList;

	// Token: 0x040007AF RID: 1967
	private SteamWorkshopItem[] steamWorkshopItems;

	// Token: 0x040007B0 RID: 1968
	private Dictionary<SteamWorkshopItem, TemplateContainer> steamWorkshopItemTemplateContainerMap = new Dictionary<SteamWorkshopItem, TemplateContainer>();
}
