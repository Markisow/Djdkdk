using System;

// Token: 0x020000D9 RID: 217
public class ConnectionRejection
{
	// Token: 0x170000CB RID: 203
	// (get) Token: 0x060006BC RID: 1724 RVA: 0x0000D467 File Offset: 0x0000B667
	// (set) Token: 0x060006BD RID: 1725 RVA: 0x0000D46F File Offset: 0x0000B66F
	public ConnectionRejectionCode code { get; set; }

	// Token: 0x170000CC RID: 204
	// (get) Token: 0x060006BE RID: 1726 RVA: 0x0000D478 File Offset: 0x0000B678
	// (set) Token: 0x060006BF RID: 1727 RVA: 0x0000D480 File Offset: 0x0000B680
	public string message { get; set; }

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x060006C0 RID: 1728 RVA: 0x0000D489 File Offset: 0x0000B689
	// (set) Token: 0x060006C1 RID: 1729 RVA: 0x0000D491 File Offset: 0x0000B691
	public ConnectionRejectionData data { get; set; }
}
