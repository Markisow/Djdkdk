using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

// Token: 0x02000239 RID: 569
public class MatchData
{
	// Token: 0x17000180 RID: 384
	// (get) Token: 0x06001048 RID: 4168 RVA: 0x00014C91 File Offset: 0x00012E91
	// (set) Token: 0x06001049 RID: 4169 RVA: 0x00014C99 File Offset: 0x00012E99
	public MatchPlayer[] homePlayers { get; set; }

	// Token: 0x17000181 RID: 385
	// (get) Token: 0x0600104A RID: 4170 RVA: 0x00014CA2 File Offset: 0x00012EA2
	// (set) Token: 0x0600104B RID: 4171 RVA: 0x00014CAA File Offset: 0x00012EAA
	public MatchPlayer[] awayPlayers { get; set; }

	// Token: 0x17000182 RID: 386
	// (get) Token: 0x0600104C RID: 4172 RVA: 0x00014CB3 File Offset: 0x00012EB3
	// (set) Token: 0x0600104D RID: 4173 RVA: 0x00014CBB File Offset: 0x00012EBB
	public double? startedAt { get; set; }

	// Token: 0x17000183 RID: 387
	// (get) Token: 0x0600104E RID: 4174 RVA: 0x00014CC4 File Offset: 0x00012EC4
	// (set) Token: 0x0600104F RID: 4175 RVA: 0x00014CCC File Offset: 0x00012ECC
	public EndPoint endPoint { get; set; }

	// Token: 0x17000184 RID: 388
	// (get) Token: 0x06001050 RID: 4176 RVA: 0x00014CD5 File Offset: 0x00012ED5
	[JsonIgnore]
	public Dictionary<PlayerTeam, string[]> TeamAssignments
	{
		get
		{
			return new Dictionary<PlayerTeam, string[]>
			{
				{
					PlayerTeam.Blue,
					this.HomeSteamIds
				},
				{
					PlayerTeam.Red,
					this.AwaySteamIds
				}
			};
		}
	}

	// Token: 0x17000185 RID: 389
	// (get) Token: 0x06001051 RID: 4177 RVA: 0x00014CF6 File Offset: 0x00012EF6
	[JsonIgnore]
	public MatchPlayer[] Players
	{
		get
		{
			return this.homePlayers.Concat(this.awayPlayers).ToArray<MatchPlayer>();
		}
	}

	// Token: 0x17000186 RID: 390
	// (get) Token: 0x06001052 RID: 4178 RVA: 0x00014D0E File Offset: 0x00012F0E
	[JsonIgnore]
	public string[] SteamIds
	{
		get
		{
			return (from p in this.Players
			select p.steamId).ToArray<string>();
		}
	}

	// Token: 0x17000187 RID: 391
	// (get) Token: 0x06001053 RID: 4179 RVA: 0x00014D3F File Offset: 0x00012F3F
	[JsonIgnore]
	public string[] HomeSteamIds
	{
		get
		{
			return (from p in this.homePlayers
			select p.steamId).ToArray<string>();
		}
	}

	// Token: 0x17000188 RID: 392
	// (get) Token: 0x06001054 RID: 4180 RVA: 0x00014D70 File Offset: 0x00012F70
	[JsonIgnore]
	public string[] AwaySteamIds
	{
		get
		{
			return (from p in this.awayPlayers
			select p.steamId).ToArray<string>();
		}
	}

	// Token: 0x06001055 RID: 4181 RVA: 0x0004ECC0 File Offset: 0x0004CEC0
	public MatchPlayer GetMatchPlayerBySteamId(string steamId)
	{
		return this.Players.FirstOrDefault((MatchPlayer p) => p.steamId == steamId);
	}
}
