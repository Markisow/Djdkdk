using System;

// Token: 0x020001E1 RID: 481
public class Country
{
	// Token: 0x17000122 RID: 290
	// (get) Token: 0x06000E68 RID: 3688 RVA: 0x00013D58 File Offset: 0x00011F58
	// (set) Token: 0x06000E69 RID: 3689 RVA: 0x00013D60 File Offset: 0x00011F60
	public string capital { get; set; }

	// Token: 0x17000123 RID: 291
	// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00013D69 File Offset: 0x00011F69
	// (set) Token: 0x06000E6B RID: 3691 RVA: 0x00013D71 File Offset: 0x00011F71
	public string code { get; set; }

	// Token: 0x17000124 RID: 292
	// (get) Token: 0x06000E6C RID: 3692 RVA: 0x00013D7A File Offset: 0x00011F7A
	// (set) Token: 0x06000E6D RID: 3693 RVA: 0x00013D82 File Offset: 0x00011F82
	public string continent { get; set; }

	// Token: 0x17000125 RID: 293
	// (get) Token: 0x06000E6E RID: 3694 RVA: 0x00013D8B File Offset: 0x00011F8B
	// (set) Token: 0x06000E6F RID: 3695 RVA: 0x00013D93 File Offset: 0x00011F93
	public string name { get; set; }

	// Token: 0x040008DC RID: 2268
	private static readonly Logger Logger = new Logger("Country");
}
