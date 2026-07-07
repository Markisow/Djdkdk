using System;

// Token: 0x02000230 RID: 560
public class PlayerMute
{
	// Token: 0x1700015F RID: 351
	// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00014A60 File Offset: 0x00012C60
	// (set) Token: 0x06000FFE RID: 4094 RVA: 0x00014A68 File Offset: 0x00012C68
	public int id { get; set; }

	// Token: 0x17000160 RID: 352
	// (get) Token: 0x06000FFF RID: 4095 RVA: 0x00014A71 File Offset: 0x00012C71
	// (set) Token: 0x06001000 RID: 4096 RVA: 0x00014A79 File Offset: 0x00012C79
	public double issuedAt { get; set; }

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x06001001 RID: 4097 RVA: 0x00014A82 File Offset: 0x00012C82
	// (set) Token: 0x06001002 RID: 4098 RVA: 0x00014A8A File Offset: 0x00012C8A
	public double expiresAt { get; set; }

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06001003 RID: 4099 RVA: 0x00014A93 File Offset: 0x00012C93
	// (set) Token: 0x06001004 RID: 4100 RVA: 0x00014A9B File Offset: 0x00012C9B
	public string reason { get; set; }
}
