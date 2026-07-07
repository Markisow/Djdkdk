using System;

// Token: 0x02000237 RID: 567
public class Beacon
{
	// Token: 0x17000171 RID: 369
	// (get) Token: 0x06001028 RID: 4136 RVA: 0x00014B92 File Offset: 0x00012D92
	// (set) Token: 0x06001029 RID: 4137 RVA: 0x00014B9A File Offset: 0x00012D9A
	public string id { get; set; }

	// Token: 0x17000172 RID: 370
	// (get) Token: 0x0600102A RID: 4138 RVA: 0x00014BA3 File Offset: 0x00012DA3
	// (set) Token: 0x0600102B RID: 4139 RVA: 0x00014BAB File Offset: 0x00012DAB
	public string host { get; set; }

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x0600102C RID: 4140 RVA: 0x00014BB4 File Offset: 0x00012DB4
	// (set) Token: 0x0600102D RID: 4141 RVA: 0x00014BBC File Offset: 0x00012DBC
	public string fqdn { get; set; }

	// Token: 0x17000174 RID: 372
	// (get) Token: 0x0600102E RID: 4142 RVA: 0x00014BC5 File Offset: 0x00012DC5
	// (set) Token: 0x0600102F RID: 4143 RVA: 0x00014BCD File Offset: 0x00012DCD
	public ushort udp_port { get; set; }

	// Token: 0x17000175 RID: 373
	// (get) Token: 0x06001030 RID: 4144 RVA: 0x00014BD6 File Offset: 0x00012DD6
	// (set) Token: 0x06001031 RID: 4145 RVA: 0x00014BDE File Offset: 0x00012DDE
	public ushort tcp_port { get; set; }

	// Token: 0x17000176 RID: 374
	// (get) Token: 0x06001032 RID: 4146 RVA: 0x00014BE7 File Offset: 0x00012DE7
	// (set) Token: 0x06001033 RID: 4147 RVA: 0x00014BEF File Offset: 0x00012DEF
	public Location location { get; set; }
}
