using System;

// Token: 0x020001F6 RID: 502
public interface Snapshot
{
	// Token: 0x1700012B RID: 299
	// (get) Token: 0x06000ECC RID: 3788
	// (set) Token: 0x06000ECD RID: 3789
	double remoteTime { get; set; }

	// Token: 0x1700012C RID: 300
	// (get) Token: 0x06000ECE RID: 3790
	// (set) Token: 0x06000ECF RID: 3791
	double localTime { get; set; }
}
