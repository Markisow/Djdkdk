using System;
using System.Collections.Generic;
using System.Linq;

// Token: 0x02000077 RID: 119
public class CompetitiveGameMode<TConfig> : MatchableGameMode<!0> where TConfig : CompetitiveGameModeConfig, new()
{
	// Token: 0x06000418 RID: 1048 RVA: 0x0000B747 File Offset: 0x00009947
	public CompetitiveGameMode(string defaultConfigFilePath, string configFilePathCliArgument = null, string configCliArgument = null, string configEnvVariable = null) : base(defaultConfigFilePath, configFilePathCliArgument, configCliArgument, configEnvVariable)
	{
	}

	// Token: 0x06000419 RID: 1049 RVA: 0x00026E68 File Offset: 0x00025068
	private PlayerTeam GetAssignedPlayerTeam(Player player)
	{
		foreach (PlayerTeam playerTeam in this.teamAssignments.Keys)
		{
			if (this.teamAssignments[playerTeam].Contains(player.SteamId.Value.ToString()))
			{
				return playerTeam;
			}
		}
		return PlayerTeam.None;
	}

	// Token: 0x0600041A RID: 1050 RVA: 0x0000B75F File Offset: 0x0000995F
	protected override void OnConfigLoaded()
	{
		base.OnConfigLoaded();
		this.teamAssignments = base.Config.teamAssignments;
	}

	// Token: 0x0600041B RID: 1051 RVA: 0x00026EEC File Offset: 0x000250EC
	protected override void OnPlayerJoined(Player player)
	{
		base.OnPlayerJoined(player);
		PlayerTeam assignedPlayerTeam = this.GetAssignedPlayerTeam(player);
		switch (assignedPlayerTeam)
		{
		case PlayerTeam.None:
		case PlayerTeam.Spectator:
		{
			PlayerTeam? team = new PlayerTeam?(PlayerTeam.Spectator);
			player.Server_SetGameState(null, team, null, null);
			if (base.CanPlayerEnterPhase(player, PlayerPhase.Spectate))
			{
				PlayerPhase? phase = new PlayerPhase?(PlayerPhase.Spectate);
				team = null;
				player.Server_SetGameState(phase, team, null, null);
			}
			break;
		}
		case PlayerTeam.Blue:
		case PlayerTeam.Red:
		{
			PlayerTeam? team = new PlayerTeam?(assignedPlayerTeam);
			player.Server_SetGameState(null, team, null, null);
			if (base.CanPlayerEnterPhase(player, PlayerPhase.PositionSelect))
			{
				PlayerPhase? phase2 = new PlayerPhase?(PlayerPhase.PositionSelect);
				team = null;
				player.Server_SetGameState(phase2, team, null, null);
				return;
			}
			break;
		}
		default:
			return;
		}
	}

	// Token: 0x0600041C RID: 1052 RVA: 0x0000B77D File Offset: 0x0000997D
	protected override void OnMatchStarted()
	{
		base.OnMatchStarted();
		this.teamAssignments = new Dictionary<PlayerTeam, string[]>
		{
			{
				PlayerTeam.Blue,
				base.matchData.HomeSteamIds
			},
			{
				PlayerTeam.Red,
				base.matchData.AwaySteamIds
			}
		};
	}

	// Token: 0x040002B7 RID: 695
	private Dictionary<PlayerTeam, string[]> teamAssignments = new Dictionary<PlayerTeam, string[]>();
}
