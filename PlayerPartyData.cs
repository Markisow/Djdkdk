using System;

// Token: 0x02000228 RID: 552
public class PlayerPartyData
{
	// Token: 0x1700014E RID: 334
	// (get) Token: 0x06000FD4 RID: 4052 RVA: 0x00014948 File Offset: 0x00012B48
	// (set) Token: 0x06000FD5 RID: 4053 RVA: 0x00014950 File Offset: 0x00012B50
	public string id { get; set; }

	// Token: 0x1700014F RID: 335
	// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x00014959 File Offset: 0x00012B59
	// (set) Token: 0x06000FD7 RID: 4055 RVA: 0x00014961 File Offset: 0x00012B61
	public string steamLobbyId { get; set; }

	// Token: 0x17000150 RID: 336
	// (get) Token: 0x06000FD8 RID: 4056 RVA: 0x0001496A File Offset: 0x00012B6A
	// (set) Token: 0x06000FD9 RID: 4057 RVA: 0x00014972 File Offset: 0x00012B72
	public string ownerSteamId { get; set; }

	// Token: 0x17000151 RID: 337
	// (get) Token: 0x06000FDA RID: 4058 RVA: 0x0001497B File Offset: 0x00012B7B
	// (set) Token: 0x06000FDB RID: 4059 RVA: 0x00014983 File Offset: 0x00012B83
	public string[] memberSteamIds { get; set; }
}
