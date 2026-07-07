using System;
using UnityEngine;

// Token: 0x02000141 RID: 321
public class SteamWorkshopItemDetailsState
{
	// Token: 0x0600098B RID: 2443 RVA: 0x000397D8 File Offset: 0x000379D8
	public bool Equals(SteamWorkshopItemDetailsState other)
	{
		return this.Title == other.Title && this.Description == other.Description && this.PreviewUrl == other.PreviewUrl && this.Subscriptions == other.Subscriptions && this.Upvotes == other.Upvotes && this.Downvotes == other.Downvotes && this.Metadata == other.Metadata && this.PreviewTexture == other.PreviewTexture;
	}

	// Token: 0x0600098C RID: 2444 RVA: 0x00039870 File Offset: 0x00037A70
	public override bool Equals(object obj)
	{
		SteamWorkshopItemDetailsState steamWorkshopItemDetailsState = obj as SteamWorkshopItemDetailsState;
		return steamWorkshopItemDetailsState != null && this.Equals(steamWorkshopItemDetailsState);
	}

	// Token: 0x0600098D RID: 2445 RVA: 0x0001008B File Offset: 0x0000E28B
	public override int GetHashCode()
	{
		return HashCode.Combine<string, string, string, int, int, int, string, Texture2D>(this.Title, this.Description, this.PreviewUrl, this.Subscriptions, this.Upvotes, this.Downvotes, this.Metadata, this.PreviewTexture);
	}

	// Token: 0x0600098E RID: 2446 RVA: 0x00039890 File Offset: 0x00037A90
	public override string ToString()
	{
		return string.Format("Title={0}, Description={1}, PreviewUrl={2}, Subscriptions={3}, Upvotes={4}, Downvotes={5}, Metadata={6}, PreviewTexture={7}", new object[]
		{
			this.Title,
			this.Description,
			this.PreviewUrl,
			this.Subscriptions,
			this.Upvotes,
			this.Downvotes,
			this.Metadata,
			this.PreviewTexture
		});
	}

	// Token: 0x04000598 RID: 1432
	public string Title;

	// Token: 0x04000599 RID: 1433
	public string Description;

	// Token: 0x0400059A RID: 1434
	public string PreviewUrl;

	// Token: 0x0400059B RID: 1435
	public int Subscriptions;

	// Token: 0x0400059C RID: 1436
	public int Upvotes;

	// Token: 0x0400059D RID: 1437
	public int Downvotes;

	// Token: 0x0400059E RID: 1438
	public string Metadata;

	// Token: 0x0400059F RID: 1439
	public Texture2D PreviewTexture;
}
