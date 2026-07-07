using System;
using System.Collections.Generic;

// Token: 0x02000076 RID: 118
public class CompetitiveGameModeConfig : StandardGameModeConfig
{
	// Token: 0x17000067 RID: 103
	// (get) Token: 0x06000415 RID: 1045 RVA: 0x0000B723 File Offset: 0x00009923
	// (set) Token: 0x06000416 RID: 1046 RVA: 0x0000B72B File Offset: 0x0000992B
	public Dictionary<PlayerTeam, string[]> teamAssignments { get; set; } = new Dictionary<PlayerTeam, string[]>();
}
