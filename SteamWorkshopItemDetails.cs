using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000142 RID: 322
public class SteamWorkshopItemDetails
{
	// Token: 0x170000F3 RID: 243
	// (get) Token: 0x0600098F RID: 2447 RVA: 0x000100C2 File Offset: 0x0000E2C2
	// (set) Token: 0x06000990 RID: 2448 RVA: 0x00039904 File Offset: 0x00037B04
	public SteamWorkshopItemDetailsState State
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
			SteamWorkshopItemDetailsState oldState = this.state;
			this.state = value;
			this.OnStateChanged(oldState, this.state);
		}
	}

	// Token: 0x170000F4 RID: 244
	// (get) Token: 0x06000991 RID: 2449 RVA: 0x000100CA File Offset: 0x0000E2CA
	public string Title
	{
		get
		{
			return this.State.Title;
		}
	}

	// Token: 0x170000F5 RID: 245
	// (get) Token: 0x06000992 RID: 2450 RVA: 0x000100D7 File Offset: 0x0000E2D7
	public string Description
	{
		get
		{
			return this.State.Description;
		}
	}

	// Token: 0x170000F6 RID: 246
	// (get) Token: 0x06000993 RID: 2451 RVA: 0x000100E4 File Offset: 0x0000E2E4
	public string PreviewUrl
	{
		get
		{
			return this.State.PreviewUrl;
		}
	}

	// Token: 0x170000F7 RID: 247
	// (get) Token: 0x06000994 RID: 2452 RVA: 0x000100F1 File Offset: 0x0000E2F1
	public int Subscriptions
	{
		get
		{
			return this.State.Subscriptions;
		}
	}

	// Token: 0x170000F8 RID: 248
	// (get) Token: 0x06000995 RID: 2453 RVA: 0x000100FE File Offset: 0x0000E2FE
	public int Upvotes
	{
		get
		{
			return this.State.Upvotes;
		}
	}

	// Token: 0x170000F9 RID: 249
	// (get) Token: 0x06000996 RID: 2454 RVA: 0x0001010B File Offset: 0x0000E30B
	public int Downvotes
	{
		get
		{
			return this.State.Downvotes;
		}
	}

	// Token: 0x170000FA RID: 250
	// (get) Token: 0x06000997 RID: 2455 RVA: 0x00010118 File Offset: 0x0000E318
	public string Metadata
	{
		get
		{
			return this.State.Metadata;
		}
	}

	// Token: 0x170000FB RID: 251
	// (get) Token: 0x06000998 RID: 2456 RVA: 0x00010125 File Offset: 0x0000E325
	public Texture2D PreviewTexture
	{
		get
		{
			return this.State.PreviewTexture;
		}
	}

	// Token: 0x06000999 RID: 2457 RVA: 0x0003993C File Offset: 0x00037B3C
	public SteamWorkshopItemDetails(string title, string description, string previewUrl, int subscriptions, int upvotes, int downvotes, string metadata)
	{
		this.state = new SteamWorkshopItemDetailsState
		{
			Title = title,
			Description = description,
			PreviewUrl = previewUrl,
			Subscriptions = subscriptions,
			Upvotes = upvotes,
			Downvotes = downvotes,
			Metadata = metadata
		};
	}

	// Token: 0x0600099A RID: 2458 RVA: 0x00010132 File Offset: 0x0000E332
	public void Initialize()
	{
		MonoBehaviourSingleton<ThreadManager>.Instance.Enqueue(this.DownloadPreviewTexture());
	}

	// Token: 0x0600099B RID: 2459 RVA: 0x00010144 File Offset: 0x0000E344
	public void Dispose()
	{
		this.StateChanged = null;
	}

	// Token: 0x0600099C RID: 2460 RVA: 0x00039990 File Offset: 0x00037B90
	public void SetState(Dictionary<string, object> updates)
	{
		SteamWorkshopItemDetailsState steamWorkshopItemDetailsState = new SteamWorkshopItemDetailsState
		{
			Title = (updates.ContainsKey("title") ? ((string)updates["title"]) : this.State.Title),
			Description = (updates.ContainsKey("description") ? ((string)updates["description"]) : this.State.Description),
			PreviewUrl = (updates.ContainsKey("previewUrl") ? ((string)updates["previewUrl"]) : this.State.PreviewUrl),
			Subscriptions = (updates.ContainsKey("subscriptions") ? ((int)updates["subscriptions"]) : this.State.Subscriptions),
			Upvotes = (updates.ContainsKey("upvotes") ? ((int)updates["upvotes"]) : this.State.Upvotes),
			Downvotes = (updates.ContainsKey("downvotes") ? ((int)updates["downvotes"]) : this.State.Downvotes),
			Metadata = (updates.ContainsKey("metadata") ? ((string)updates["metadata"]) : this.State.Metadata),
			PreviewTexture = (updates.ContainsKey("previewTexture") ? ((Texture2D)updates["previewTexture"]) : this.State.PreviewTexture)
		};
		this.State = steamWorkshopItemDetailsState;
	}

	// Token: 0x0600099D RID: 2461 RVA: 0x0001014D File Offset: 0x0000E34D
	private IEnumerator DownloadPreviewTexture()
	{
		SteamWorkshopItemDetails.<DownloadPreviewTexture>d__26 <DownloadPreviewTexture>d__ = new SteamWorkshopItemDetails.<DownloadPreviewTexture>d__26(0);
		<DownloadPreviewTexture>d__.<>4__this = this;
		return <DownloadPreviewTexture>d__;
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0001015C File Offset: 0x0000E35C
	private void OnStateChanged(SteamWorkshopItemDetailsState oldState, SteamWorkshopItemDetailsState newState)
	{
		Action<SteamWorkshopItemDetailsState, SteamWorkshopItemDetailsState> stateChanged = this.StateChanged;
		if (stateChanged == null)
		{
			return;
		}
		stateChanged(oldState, newState);
	}

	// Token: 0x040005A0 RID: 1440
	private static readonly global::Logger Logger = new global::Logger("SteamWorkshopItemDetails");

	// Token: 0x040005A1 RID: 1441
	private SteamWorkshopItemDetailsState state;

	// Token: 0x040005A2 RID: 1442
	public Action<SteamWorkshopItemDetailsState, SteamWorkshopItemDetailsState> StateChanged;
}
