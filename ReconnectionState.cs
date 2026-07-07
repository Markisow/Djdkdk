using System;
using System.Linq;

// Token: 0x020000B0 RID: 176
public struct ReconnectionState
{
	// Token: 0x17000084 RID: 132
	// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000C726 File Offset: 0x0000A926
	public string[] PendingModIds
	{
		get
		{
			return this.PendingReadinessModIds.Union(this.PendingEnablingModIds).ToArray<string>();
		}
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0000C73E File Offset: 0x0000A93E
	public ReconnectionState()
	{
		this.Phase = ReconnectionPhase.None;
		this.Password = null;
		this.ClientRequiredModIds = new string[0];
		this.PendingReadinessModIds = new string[0];
		this.PendingEnablingModIds = new string[0];
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x0000C772 File Offset: 0x0000A972
	public bool IsPendingModId(string modId)
	{
		return this.PendingReadinessModIds.Contains(modId) || this.PendingEnablingModIds.Contains(modId);
	}

	// Token: 0x06000594 RID: 1428 RVA: 0x0002DB8C File Offset: 0x0002BD8C
	public bool Equals(ReconnectionState other)
	{
		return this.Phase == other.Phase && this.Password == other.Password && this.ClientRequiredModIds.SequenceEqual(other.ClientRequiredModIds) && this.PendingReadinessModIds.SequenceEqual(other.PendingReadinessModIds) && this.PendingEnablingModIds.SequenceEqual(other.PendingEnablingModIds);
	}

	// Token: 0x06000595 RID: 1429 RVA: 0x0002DBF4 File Offset: 0x0002BDF4
	public override bool Equals(object obj)
	{
		if (obj is ReconnectionState)
		{
			ReconnectionState other = (ReconnectionState)obj;
			return this.Equals(other);
		}
		return false;
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x0000C790 File Offset: 0x0000A990
	public override int GetHashCode()
	{
		return HashCode.Combine<ReconnectionPhase, string, string[], string[], string[]>(this.Phase, this.Password, this.ClientRequiredModIds, this.PendingReadinessModIds, this.PendingEnablingModIds);
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x0002DC1C File Offset: 0x0002BE1C
	public override string ToString()
	{
		return string.Format("Phase: {0}, Password: {1}, ClientRequiredModIds: [{2}], PendingReadinessModIds: [{3}], PendingEnablingModIds: [{4}]", new object[]
		{
			this.Phase,
			this.Password ?? "null",
			string.Join(", ", this.ClientRequiredModIds),
			string.Join(", ", this.PendingReadinessModIds),
			string.Join(", ", this.PendingEnablingModIds)
		});
	}

	// Token: 0x04000376 RID: 886
	public ReconnectionPhase Phase;

	// Token: 0x04000377 RID: 887
	public string Password;

	// Token: 0x04000378 RID: 888
	public string[] ClientRequiredModIds;

	// Token: 0x04000379 RID: 889
	public string[] PendingReadinessModIds;

	// Token: 0x0400037A RID: 890
	public string[] PendingEnablingModIds;
}
