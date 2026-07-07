using System;
using System.Collections.Generic;

// Token: 0x02000140 RID: 320
public class SteamWorkshopItem
{
	// Token: 0x170000EF RID: 239
	// (get) Token: 0x0600097E RID: 2430 RVA: 0x0000FFE7 File Offset: 0x0000E1E7
	// (set) Token: 0x0600097F RID: 2431 RVA: 0x00039520 File Offset: 0x00037720
	public SteamWorkshopItemState State
	{
		get
		{
			return this.state;
		}
		set
		{
			if (this.state.Equals(value))
			{
				return;
			}
			SteamWorkshopItemState oldState = this.state;
			this.state = value;
			this.OnStateChanged(oldState, this.state);
		}
	}

	// Token: 0x170000F0 RID: 240
	// (get) Token: 0x06000980 RID: 2432 RVA: 0x0000FFEF File Offset: 0x0000E1EF
	public string Path
	{
		get
		{
			return this.State.Path;
		}
	}

	// Token: 0x170000F1 RID: 241
	// (get) Token: 0x06000981 RID: 2433 RVA: 0x0000FFFC File Offset: 0x0000E1FC
	public SteamWorkshopItemDetails Details
	{
		get
		{
			return this.State.Details;
		}
	}

	// Token: 0x170000F2 RID: 242
	// (get) Token: 0x06000982 RID: 2434 RVA: 0x00010009 File Offset: 0x0000E209
	public SteamWorkshopItemPhase Phase
	{
		get
		{
			return this.State.Phase;
		}
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x00010016 File Offset: 0x0000E216
	public SteamWorkshopItem(string id, string path = null)
	{
		this.Id = id;
		this.state = new SteamWorkshopItemState
		{
			Path = path
		};
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x00010037 File Offset: 0x0000E237
	public virtual void Initialize()
	{
		EventManager.AddEventListener("Event_OnSteamWorkshopItemDetails", new Action<Dictionary<string, object>>(this.Event_OnSteamWorkshopItemDetails));
		SteamWorkshopManager.GetItemDetails(new string[]
		{
			this.Id
		});
	}

	// Token: 0x06000985 RID: 2437 RVA: 0x00039558 File Offset: 0x00037758
	public virtual void Dispose()
	{
		EventManager.RemoveEventListener("Event_OnSteamWorkshopItemDetails", new Action<Dictionary<string, object>>(this.Event_OnSteamWorkshopItemDetails));
		if (this.State.Details != null)
		{
			this.State.Details.Dispose();
		}
		this.StateChanged = null;
		this.DetailsStateChanged = null;
	}

	// Token: 0x06000986 RID: 2438 RVA: 0x000395A8 File Offset: 0x000377A8
	public void SetState(Dictionary<string, object> updates)
	{
		SteamWorkshopItemState steamWorkshopItemState = new SteamWorkshopItemState
		{
			Path = (updates.ContainsKey("path") ? ((string)updates["path"]) : this.State.Path),
			Details = (updates.ContainsKey("details") ? ((SteamWorkshopItemDetails)updates["details"]) : this.State.Details),
			Phase = (updates.ContainsKey("phase") ? ((SteamWorkshopItemPhase)updates["phase"]) : this.State.Phase)
		};
		this.State = steamWorkshopItemState;
	}

	// Token: 0x06000987 RID: 2439 RVA: 0x00039654 File Offset: 0x00037854
	private void Event_OnSteamWorkshopItemDetails(Dictionary<string, object> message)
	{
		string b = (string)message["id"];
		string text = (string)message["title"];
		string text2 = (string)message["description"];
		string text3 = (string)message["previewUrl"];
		int num = (int)message["subscriptions"];
		int num2 = (int)message["upvotes"];
		int num3 = (int)message["downvotes"];
		string text4 = (string)message["metadata"];
		if (this.Id != b)
		{
			return;
		}
		if (this.state.Details == null)
		{
			SteamWorkshopItemDetails steamWorkshopItemDetails = new SteamWorkshopItemDetails(text, text2, text3, num, num2, num3, text4);
			this.SetState(new Dictionary<string, object>
			{
				{
					"details",
					steamWorkshopItemDetails
				}
			});
			SteamWorkshopItemDetails steamWorkshopItemDetails2 = steamWorkshopItemDetails;
			steamWorkshopItemDetails2.StateChanged = (Action<SteamWorkshopItemDetailsState, SteamWorkshopItemDetailsState>)Delegate.Combine(steamWorkshopItemDetails2.StateChanged, new Action<SteamWorkshopItemDetailsState, SteamWorkshopItemDetailsState>(this.OnDetailsStateChanged));
			steamWorkshopItemDetails.Initialize();
			return;
		}
		this.Details.SetState(new Dictionary<string, object>
		{
			{
				"title",
				text
			},
			{
				"description",
				text2
			},
			{
				"previewUrl",
				text3
			},
			{
				"subscriptions",
				num
			},
			{
				"upvotes",
				num2
			},
			{
				"downvotes",
				num3
			},
			{
				"metadata",
				text4
			}
		});
	}

	// Token: 0x06000988 RID: 2440 RVA: 0x00010063 File Offset: 0x0000E263
	private void OnStateChanged(SteamWorkshopItemState oldState, SteamWorkshopItemState newState)
	{
		Action<SteamWorkshopItemState, SteamWorkshopItemState> stateChanged = this.StateChanged;
		if (stateChanged == null)
		{
			return;
		}
		stateChanged(oldState, newState);
	}

	// Token: 0x06000989 RID: 2441 RVA: 0x00010077 File Offset: 0x0000E277
	private void OnDetailsStateChanged(SteamWorkshopItemDetailsState oldDetailsState, SteamWorkshopItemDetailsState newDetailsState)
	{
		Action<SteamWorkshopItemDetailsState, SteamWorkshopItemDetailsState> detailsStateChanged = this.DetailsStateChanged;
		if (detailsStateChanged == null)
		{
			return;
		}
		detailsStateChanged(oldDetailsState, newDetailsState);
	}

	// Token: 0x04000594 RID: 1428
	public readonly string Id;

	// Token: 0x04000595 RID: 1429
	private SteamWorkshopItemState state;

	// Token: 0x04000596 RID: 1430
	public Action<SteamWorkshopItemState, SteamWorkshopItemState> StateChanged;

	// Token: 0x04000597 RID: 1431
	public Action<SteamWorkshopItemDetailsState, SteamWorkshopItemDetailsState> DetailsStateChanged;
}
