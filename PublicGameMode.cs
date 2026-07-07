using System;

// Token: 0x0200007C RID: 124
public class PublicGameMode<TConfig> : StandardGameMode<!0> where TConfig : PublicGameModeConfig, new()
{
	// Token: 0x06000439 RID: 1081 RVA: 0x0000B96E File Offset: 0x00009B6E
	public PublicGameMode(string defaultConfigFilePath, string configFilePathCliArgument = null, string configCliArgument = null, string configEnvVariable = null) : base(defaultConfigFilePath, configFilePathCliArgument, configCliArgument, configEnvVariable)
	{
	}

	// Token: 0x0600043A RID: 1082 RVA: 0x0002736C File Offset: 0x0002556C
	protected override void OnWarmupTimedOut()
	{
		if (this.PlayerManager.GetPlayersByTeam(PlayerTeam.Blue, false).Count == 0 || this.PlayerManager.GetPlayersByTeam(PlayerTeam.Red, false).Count == 0)
		{
			this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.Warmup), new int?(base.Config.phaseDurationMap[GamePhase.Warmup]), null, null, null, null);
			this.ChatManager.Server_BroadcastChatMessage("Not enough players to start the game. Extending warmup...", "#ffe97f");
			return;
		}
		base.OnWarmupTimedOut();
	}

	// Token: 0x0600043B RID: 1083 RVA: 0x00027410 File Offset: 0x00025610
	protected override void OnPlayerJoined(Player player)
	{
		base.OnPlayerJoined(player);
		if (base.CanPlayerEnterPhase(player, PlayerPhase.TeamSelect))
		{
			player.Server_SetGameState(new PlayerPhase?(PlayerPhase.TeamSelect), null, null, null);
		}
	}

	// Token: 0x0600043C RID: 1084 RVA: 0x00027458 File Offset: 0x00025658
	protected override void OnPlayerRequestTeamSelect(Player player)
	{
		base.OnPlayerRequestTeamSelect(player);
		if (base.CanPlayerEnterPhase(player, PlayerPhase.TeamSelect))
		{
			player.Server_SetGameState(new PlayerPhase?(PlayerPhase.TeamSelect), new PlayerTeam?(PlayerTeam.None), null, null);
		}
	}

	// Token: 0x0600043D RID: 1085 RVA: 0x0002749C File Offset: 0x0002569C
	protected override void OnPlayerRequestTeam(Player player, PlayerTeam team)
	{
		base.OnPlayerRequestTeam(player, team);
		if (team - PlayerTeam.Blue > 1)
		{
			if (team != PlayerTeam.Spectator)
			{
				return;
			}
			PlayerTeam? team2 = new PlayerTeam?(team);
			player.Server_SetGameState(null, team2, null, null);
			if (base.CanPlayerEnterPhase(player, PlayerPhase.Spectate))
			{
				PlayerPhase? phase = new PlayerPhase?(PlayerPhase.Spectate);
				team2 = null;
				player.Server_SetGameState(phase, team2, null, null);
			}
		}
		else
		{
			PlayerTeam? team2 = new PlayerTeam?(team);
			player.Server_SetGameState(null, team2, null, null);
			if (base.CanPlayerEnterPhase(player, PlayerPhase.PositionSelect))
			{
				PlayerPhase? phase2 = new PlayerPhase?(PlayerPhase.PositionSelect);
				team2 = null;
				player.Server_SetGameState(phase2, team2, null, null);
				return;
			}
		}
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00027578 File Offset: 0x00025778
	protected override void OnVoteRemoved(Vote vote)
	{
		base.OnVoteRemoved(vote);
		if (!vote.Passed)
		{
			return;
		}
		string name = vote.Name;
		if (name == "start")
		{
			this.StartGame(GamePhase.PreGame);
			return;
		}
		if (!(name == "warmup"))
		{
			return;
		}
		this.StartGame(GamePhase.Warmup);
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x000275C8 File Offset: 0x000257C8
	protected override void OnChatCommand(Player player, string command, string[] args)
	{
		if (!(command == "/vs") && !(command == "/votestart"))
		{
			if (!(command == "/vw") && !(command == "/votewarmup"))
			{
				base.OnChatCommand(player, command, args);
				return;
			}
			if (player.Team != PlayerTeam.Blue && player.Team != PlayerTeam.Red)
			{
				this.ChatManager.Server_SendChatMessage("You must be on a team to start this vote", "#e74c3c", new ulong[]
				{
					player.OwnerClientId
				});
				return;
			}
			Vote vote = this.VoteManager.Server_GetTeamVoteByName("warmup", player.Team);
			if (vote != null)
			{
				vote.CastVote(player.SteamId.Value.ToString(), true);
				return;
			}
			this.VoteManager.Server_AddVote("warmup", "Warmup", "use /vw or /votewarmup", new PlayerTeam[]
			{
				PlayerTeam.Blue,
				PlayerTeam.Red
			}, 30f, player.SteamId.Value.ToString(), Utils.GetVoteMajority(this.PlayerManager.GetPlayersByTeams(new PlayerTeam[]
			{
				PlayerTeam.Blue,
				PlayerTeam.Red
			}, false).Count), null);
			return;
		}
		else
		{
			if (player.Team != PlayerTeam.Blue && player.Team != PlayerTeam.Red)
			{
				this.ChatManager.Server_SendChatMessage("You must be on a team to start this vote", "#e74c3c", new ulong[]
				{
					player.OwnerClientId
				});
				return;
			}
			Vote vote2 = this.VoteManager.Server_GetTeamVoteByName("start", player.Team);
			if (vote2 != null)
			{
				vote2.CastVote(player.SteamId.Value.ToString(), true);
				return;
			}
			this.VoteManager.Server_AddVote("start", "Start", "use /vs or /votestart", new PlayerTeam[]
			{
				PlayerTeam.Blue,
				PlayerTeam.Red
			}, 30f, player.SteamId.Value.ToString(), Utils.GetVoteMajority(this.PlayerManager.GetPlayersByTeams(new PlayerTeam[]
			{
				PlayerTeam.Blue,
				PlayerTeam.Red
			}, false).Count), null);
			return;
		}
	}
}
