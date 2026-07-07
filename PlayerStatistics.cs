using System;

// Token: 0x0200022F RID: 559
public class PlayerStatistics
{
	// Token: 0x1700015C RID: 348
	// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00014A2D File Offset: 0x00012C2D
	// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x00014A35 File Offset: 0x00012C35
	public PlayerManagerStatistics playerManager { get; set; }

	// Token: 0x1700015D RID: 349
	// (get) Token: 0x06000FF8 RID: 4088 RVA: 0x00014A3E File Offset: 0x00012C3E
	// (set) Token: 0x06000FF9 RID: 4089 RVA: 0x00014A46 File Offset: 0x00012C46
	public ServerManagerStatistics serverManager { get; set; }

	// Token: 0x1700015E RID: 350
	// (get) Token: 0x06000FFA RID: 4090 RVA: 0x00014A4F File Offset: 0x00012C4F
	// (set) Token: 0x06000FFB RID: 4091 RVA: 0x00014A57 File Offset: 0x00012C57
	public MatchmakingManagerStatistics matchmakingManager { get; set; }
}
