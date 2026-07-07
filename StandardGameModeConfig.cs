using System;
using System.Collections.Generic;

// Token: 0x0200007D RID: 125
public class StandardGameModeConfig : BaseGameModeConfig
{
	// Token: 0x1700006A RID: 106
	// (get) Token: 0x06000440 RID: 1088 RVA: 0x0000B97B File Offset: 0x00009B7B
	// (set) Token: 0x06000441 RID: 1089 RVA: 0x0000B983 File Offset: 0x00009B83
	public Dictionary<GamePhase, int> phaseDurationMap { get; set; } = new Dictionary<GamePhase, int>
	{
		{
			GamePhase.None,
			0
		},
		{
			GamePhase.Warmup,
			60
		},
		{
			GamePhase.PreGame,
			10
		},
		{
			GamePhase.FaceOff,
			5
		},
		{
			GamePhase.Play,
			300
		},
		{
			GamePhase.BlueScore,
			5
		},
		{
			GamePhase.RedScore,
			5
		},
		{
			GamePhase.Replay,
			10
		},
		{
			GamePhase.Intermission,
			10
		},
		{
			GamePhase.GameOver,
			30
		},
		{
			GamePhase.PostGame,
			10
		}
	};

	// Token: 0x1700006B RID: 107
	// (get) Token: 0x06000442 RID: 1090 RVA: 0x0000B98C File Offset: 0x00009B8C
	// (set) Token: 0x06000443 RID: 1091 RVA: 0x0000B994 File Offset: 0x00009B94
	public float spawnDelay { get; set; } = 5f;

	// Token: 0x1700006C RID: 108
	// (get) Token: 0x06000444 RID: 1092 RVA: 0x0000B99D File Offset: 0x00009B9D
	// (set) Token: 0x06000445 RID: 1093 RVA: 0x0000B9A5 File Offset: 0x00009BA5
	public int maxPeriods { get; set; } = 3;
}
