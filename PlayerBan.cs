using System;

// Token: 0x02000231 RID: 561
public class PlayerBan
{
	// Token: 0x17000163 RID: 355
	// (get) Token: 0x06001006 RID: 4102 RVA: 0x00014AA4 File Offset: 0x00012CA4
	// (set) Token: 0x06001007 RID: 4103 RVA: 0x00014AAC File Offset: 0x00012CAC
	public int id { get; set; }

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x06001008 RID: 4104 RVA: 0x00014AB5 File Offset: 0x00012CB5
	// (set) Token: 0x06001009 RID: 4105 RVA: 0x00014ABD File Offset: 0x00012CBD
	public double issuedAt { get; set; }

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x0600100A RID: 4106 RVA: 0x00014AC6 File Offset: 0x00012CC6
	// (set) Token: 0x0600100B RID: 4107 RVA: 0x00014ACE File Offset: 0x00012CCE
	public double expiresAt { get; set; }

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x0600100C RID: 4108 RVA: 0x00014AD7 File Offset: 0x00012CD7
	// (set) Token: 0x0600100D RID: 4109 RVA: 0x00014ADF File Offset: 0x00012CDF
	public string reason { get; set; }
}
