using System;
using System.Collections.Generic;

// Token: 0x0200007E RID: 126
public class GameResult
{
	// Token: 0x1700006D RID: 109
	// (get) Token: 0x06000447 RID: 1095 RVA: 0x0000B9AE File Offset: 0x00009BAE
	// (set) Token: 0x06000448 RID: 1096 RVA: 0x0000B9B6 File Offset: 0x00009BB6
	public PlayerTeam winningTeam { get; set; }

	// Token: 0x1700006E RID: 110
	// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000B9BF File Offset: 0x00009BBF
	// (set) Token: 0x0600044A RID: 1098 RVA: 0x0000B9C7 File Offset: 0x00009BC7
	public int blueScore { get; set; }

	// Token: 0x1700006F RID: 111
	// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000B9D0 File Offset: 0x00009BD0
	// (set) Token: 0x0600044C RID: 1100 RVA: 0x0000B9D8 File Offset: 0x00009BD8
	public int redScore { get; set; }

	// Token: 0x17000070 RID: 112
	// (get) Token: 0x0600044D RID: 1101 RVA: 0x0000B9E1 File Offset: 0x00009BE1
	// (set) Token: 0x0600044E RID: 1102 RVA: 0x0000B9E9 File Offset: 0x00009BE9
	public bool forefeit { get; set; }

	// Token: 0x17000071 RID: 113
	// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000B9F2 File Offset: 0x00009BF2
	// (set) Token: 0x06000450 RID: 1104 RVA: 0x0000B9FA File Offset: 0x00009BFA
	public Dictionary<string, PlayerResult> playerResults { get; set; } = new Dictionary<string, PlayerResult>();
}
