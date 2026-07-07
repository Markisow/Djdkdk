using System;

// Token: 0x0200013F RID: 319
public class SteamWorkshopItemState
{
	// Token: 0x0600097A RID: 2426 RVA: 0x0000FF78 File Offset: 0x0000E178
	public bool Equals(SteamWorkshopItemState other)
	{
		return this.Path == other.Path && this.Details == other.Details && this.Phase == other.Phase;
	}

	// Token: 0x0600097B RID: 2427 RVA: 0x00039500 File Offset: 0x00037700
	public override bool Equals(object obj)
	{
		SteamWorkshopItemState steamWorkshopItemState = obj as SteamWorkshopItemState;
		return steamWorkshopItemState != null && this.Equals(steamWorkshopItemState);
	}

	// Token: 0x0600097C RID: 2428 RVA: 0x0000FFAB File Offset: 0x0000E1AB
	public override int GetHashCode()
	{
		return HashCode.Combine<string, SteamWorkshopItemDetails, SteamWorkshopItemPhase>(this.Path, this.Details, this.Phase);
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0000FFC4 File Offset: 0x0000E1C4
	public override string ToString()
	{
		return string.Format("Path={0}, Details={1}, Phase={2}", this.Path, this.Details, this.Phase);
	}

	// Token: 0x04000591 RID: 1425
	public string Path;

	// Token: 0x04000592 RID: 1426
	public SteamWorkshopItemDetails Details;

	// Token: 0x04000593 RID: 1427
	public SteamWorkshopItemPhase Phase;
}
