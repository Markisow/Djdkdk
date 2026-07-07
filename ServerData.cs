using System;

// Token: 0x02000234 RID: 564
public class ServerData
{
	// Token: 0x1700016D RID: 365
	// (get) Token: 0x0600101D RID: 4125 RVA: 0x00014B4E File Offset: 0x00012D4E
	// (set) Token: 0x0600101E RID: 4126 RVA: 0x00014B56 File Offset: 0x00012D56
	public string id { get; set; }

	// Token: 0x1700016E RID: 366
	// (get) Token: 0x0600101F RID: 4127 RVA: 0x00014B5F File Offset: 0x00012D5F
	// (set) Token: 0x06001020 RID: 4128 RVA: 0x00014B67 File Offset: 0x00012D67
	public string ipAddress { get; set; }

	// Token: 0x1700016F RID: 367
	// (get) Token: 0x06001021 RID: 4129 RVA: 0x00014B70 File Offset: 0x00012D70
	// (set) Token: 0x06001022 RID: 4130 RVA: 0x00014B78 File Offset: 0x00012D78
	public ushort port { get; set; }
}
