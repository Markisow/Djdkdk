using System;

// Token: 0x02000229 RID: 553
public class PlayerGroupData
{
	// Token: 0x17000152 RID: 338
	// (get) Token: 0x06000FDD RID: 4061 RVA: 0x0001498C File Offset: 0x00012B8C
	// (set) Token: 0x06000FDE RID: 4062 RVA: 0x00014994 File Offset: 0x00012B94
	public string id { get; set; }

	// Token: 0x17000153 RID: 339
	// (get) Token: 0x06000FDF RID: 4063 RVA: 0x0001499D File Offset: 0x00012B9D
	// (set) Token: 0x06000FE0 RID: 4064 RVA: 0x000149A5 File Offset: 0x00012BA5
	public string ownerSteamId { get; set; }

	// Token: 0x17000154 RID: 340
	// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x000149AE File Offset: 0x00012BAE
	// (set) Token: 0x06000FE2 RID: 4066 RVA: 0x000149B6 File Offset: 0x00012BB6
	public string[] memberSteamIds { get; set; }
}
