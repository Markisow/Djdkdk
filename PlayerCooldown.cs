using System;

// Token: 0x02000232 RID: 562
public class PlayerCooldown
{
	// Token: 0x17000167 RID: 359
	// (get) Token: 0x0600100F RID: 4111 RVA: 0x00014AE8 File Offset: 0x00012CE8
	// (set) Token: 0x06001010 RID: 4112 RVA: 0x00014AF0 File Offset: 0x00012CF0
	public int id { get; set; }

	// Token: 0x17000168 RID: 360
	// (get) Token: 0x06001011 RID: 4113 RVA: 0x00014AF9 File Offset: 0x00012CF9
	// (set) Token: 0x06001012 RID: 4114 RVA: 0x00014B01 File Offset: 0x00012D01
	public string matchId { get; set; }

	// Token: 0x17000169 RID: 361
	// (get) Token: 0x06001013 RID: 4115 RVA: 0x00014B0A File Offset: 0x00012D0A
	// (set) Token: 0x06001014 RID: 4116 RVA: 0x00014B12 File Offset: 0x00012D12
	public double issuedAt { get; set; }

	// Token: 0x1700016A RID: 362
	// (get) Token: 0x06001015 RID: 4117 RVA: 0x00014B1B File Offset: 0x00012D1B
	// (set) Token: 0x06001016 RID: 4118 RVA: 0x00014B23 File Offset: 0x00012D23
	public double expiresAt { get; set; }
}
