using System;
using System.Linq;
using System.Text.Json.Serialization;

// Token: 0x02000128 RID: 296
public class ServerConfig
{
	// Token: 0x170000E1 RID: 225
	// (get) Token: 0x0600083B RID: 2107 RVA: 0x0000E70D File Offset: 0x0000C90D
	// (set) Token: 0x0600083C RID: 2108 RVA: 0x0000E715 File Offset: 0x0000C915
	public ushort port { get; set; } = 30609;

	// Token: 0x170000E2 RID: 226
	// (get) Token: 0x0600083D RID: 2109 RVA: 0x0000E71E File Offset: 0x0000C91E
	// (set) Token: 0x0600083E RID: 2110 RVA: 0x0000E726 File Offset: 0x0000C926
	public string name { get; set; } = "MY PUCK SERVER";

	// Token: 0x170000E3 RID: 227
	// (get) Token: 0x0600083F RID: 2111 RVA: 0x0000E72F File Offset: 0x0000C92F
	// (set) Token: 0x06000840 RID: 2112 RVA: 0x0000E737 File Offset: 0x0000C937
	public int maxPlayers { get; set; } = 12;

	// Token: 0x170000E4 RID: 228
	// (get) Token: 0x06000841 RID: 2113 RVA: 0x0000E740 File Offset: 0x0000C940
	// (set) Token: 0x06000842 RID: 2114 RVA: 0x0000E748 File Offset: 0x0000C948
	public string password { get; set; }

	// Token: 0x170000E5 RID: 229
	// (get) Token: 0x06000843 RID: 2115 RVA: 0x0000E751 File Offset: 0x0000C951
	// (set) Token: 0x06000844 RID: 2116 RVA: 0x0000E759 File Offset: 0x0000C959
	public int tickRate { get; set; } = 200;

	// Token: 0x170000E6 RID: 230
	// (get) Token: 0x06000845 RID: 2117 RVA: 0x0000E762 File Offset: 0x0000C962
	// (set) Token: 0x06000846 RID: 2118 RVA: 0x0000E76A File Offset: 0x0000C96A
	public bool isPublic { get; set; } = true;

	// Token: 0x170000E7 RID: 231
	// (get) Token: 0x06000847 RID: 2119 RVA: 0x0000E773 File Offset: 0x0000C973
	// (set) Token: 0x06000848 RID: 2120 RVA: 0x0000E77B File Offset: 0x0000C97B
	public bool useVoip { get; set; }

	// Token: 0x170000E8 RID: 232
	// (get) Token: 0x06000849 RID: 2121 RVA: 0x0000E784 File Offset: 0x0000C984
	// (set) Token: 0x0600084A RID: 2122 RVA: 0x0000E78C File Offset: 0x0000C98C
	public bool useWhitelist { get; set; }

	// Token: 0x170000E9 RID: 233
	// (get) Token: 0x0600084B RID: 2123 RVA: 0x0000E795 File Offset: 0x0000C995
	// (set) Token: 0x0600084C RID: 2124 RVA: 0x0000E79D File Offset: 0x0000C99D
	public ModConfig[] mods { get; set; } = Constants.DEFAULT_SERVER_MODS;

	// Token: 0x170000EA RID: 234
	// (get) Token: 0x0600084D RID: 2125 RVA: 0x0000E7A6 File Offset: 0x0000C9A6
	// (set) Token: 0x0600084E RID: 2126 RVA: 0x0000E7AE File Offset: 0x0000C9AE
	public string gameMode { get; set; } = "public";

	// Token: 0x170000EB RID: 235
	// (get) Token: 0x0600084F RID: 2127 RVA: 0x0000E7B7 File Offset: 0x0000C9B7
	// (set) Token: 0x06000850 RID: 2128 RVA: 0x0000E7BF File Offset: 0x0000C9BF
	public string level { get; set; } = "default";

	// Token: 0x170000EC RID: 236
	// (get) Token: 0x06000851 RID: 2129 RVA: 0x00035204 File Offset: 0x00033404
	[JsonIgnore]
	public string[] EnabledModIds
	{
		get
		{
			return (from mod in this.mods
			where mod.isEnabled
			select mod.id).ToArray<string>();
		}
	}

	// Token: 0x170000ED RID: 237
	// (get) Token: 0x06000852 RID: 2130 RVA: 0x00035264 File Offset: 0x00033464
	[JsonIgnore]
	public string[] ClientRequiredModIds
	{
		get
		{
			return (from mod in this.mods
			where mod.isClientRequired
			select mod.id).ToArray<string>();
		}
	}
}
