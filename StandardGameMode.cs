using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class StandardGameMode<TConfig> : BaseGameMode<!0> where TConfig : StandardGameModeConfig, new()
{
	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000457 RID: 1111 RVA: 0x00027870 File Offset: 0x00025A70
	protected bool isGameInProgress
	{
		get
		{
			return this.GameManager.Phase == GamePhase.FaceOff || this.GameManager.Phase == GamePhase.Play || this.GameManager.Phase == GamePhase.BlueScore || this.GameManager.Phase == GamePhase.RedScore || this.GameManager.Phase == GamePhase.Replay || this.GameManager.Phase == GamePhase.Intermission;
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000458 RID: 1112 RVA: 0x0000BA38 File Offset: 0x00009C38
	protected bool isReplayable
	{
		get
		{
			return this.GameManager.Phase == GamePhase.FaceOff || this.GameManager.Phase == GamePhase.Play || this.GameManager.Phase == GamePhase.BlueScore || this.GameManager.Phase == GamePhase.RedScore;
		}
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x0000BA74 File Offset: 0x00009C74
	public StandardGameMode(string defaultConfigFilePath, string configFilePathCliArgument = null, string configCliArgument = null, string configEnvVariable = null) : base(defaultConfigFilePath, configFilePathCliArgument, configCliArgument, configEnvVariable)
	{
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x000278D4 File Offset: 0x00025AD4
	public override bool Initialize(Level level, ServerManager serverManager, GameManager gameManager, PlayerManager playerManager, PuckManager puckManager, ChatManager chatManager, ReplayManager replayManager, VoteManager voteManager)
	{
		if (!base.Initialize(level, serverManager, gameManager, playerManager, puckManager, chatManager, replayManager, voteManager))
		{
			return false;
		}
		this.StartGame(GamePhase.Warmup);
		this.GameManager.Server_StartTicking();
		return true;
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x0002790C File Offset: 0x00025B0C
	protected bool CanPlayerEnterPhase(Player player, PlayerPhase phase)
	{
		switch (phase)
		{
		case PlayerPhase.TeamSelect:
			return true;
		case PlayerPhase.PositionSelect:
			return player.Team == PlayerTeam.Blue || player.Team == PlayerTeam.Red;
		case PlayerPhase.Play:
		case PlayerPhase.Replay:
			return (player.Team == PlayerTeam.Blue || player.Team == PlayerTeam.Red) && (player.Role == PlayerRole.Attacker || player.Role == PlayerRole.Goalie) && player.PlayerPosition != null;
		case PlayerPhase.Spectate:
			return player.Team == PlayerTeam.Spectator;
		default:
			return false;
		}
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x0002798C File Offset: 0x00025B8C
	protected virtual void PreparePlayersForGamePhase(GamePhase gamePhase)
	{
		foreach (Player player in this.PlayerManager.GetPlayers(false))
		{
			this.PreparePlayerForGamePhase(player, gamePhase);
		}
	}

	// Token: 0x0600045D RID: 1117 RVA: 0x000279E8 File Offset: 0x00025BE8
	protected virtual void PreparePlayerForGamePhase(Player player, GamePhase gamePhase)
	{
		player.Server_CancelDelayedGameState();
		switch (gamePhase)
		{
		case GamePhase.Warmup:
			if (this.CanPlayerEnterPhase(player, PlayerPhase.Play))
			{
				PlayerPhase? phase = new PlayerPhase?(PlayerPhase.Play);
				PlayerTeam? team = null;
				PlayerRole? role = null;
				float? delay = null;
				player.Server_SetGameState(phase, team, role, delay);
			}
			break;
		case GamePhase.PreGame:
			if (this.CanPlayerEnterPhase(player, PlayerPhase.PositionSelect))
			{
				PlayerPhase? phase2 = new PlayerPhase?(PlayerPhase.PositionSelect);
				PlayerTeam? team2 = null;
				PlayerRole? role2 = null;
				float? delay = null;
				player.Server_SetGameState(phase2, team2, role2, delay);
			}
			break;
		case GamePhase.FaceOff:
			if (player.Phase == PlayerPhase.Play)
			{
				player.Server_SpawnCharacter(player.PlayerPosition.transform.position, player.PlayerPosition.transform.rotation, player.Role);
			}
			else if (this.CanPlayerEnterPhase(player, PlayerPhase.Play))
			{
				PlayerPhase? phase3 = new PlayerPhase?(PlayerPhase.Play);
				PlayerTeam? team3 = null;
				PlayerRole? role3 = null;
				float? delay = null;
				player.Server_SetGameState(phase3, team3, role3, delay);
			}
			break;
		case GamePhase.Play:
			if (player.Phase != PlayerPhase.Play && this.CanPlayerEnterPhase(player, PlayerPhase.Play))
			{
				PlayerPhase? phase4 = new PlayerPhase?(PlayerPhase.Play);
				float? delay = new float?(base.Config.spawnDelay);
				player.Server_SetGameState(phase4, null, null, delay);
				this.ChatManager.Server_SendChatMessage(string.Format("Spawning in {0} seconds...", base.Config.spawnDelay), "#ffe97f", new ulong[]
				{
					player.OwnerClientId
				});
			}
			break;
		case GamePhase.Replay:
			if (this.CanPlayerEnterPhase(player, PlayerPhase.Replay))
			{
				PlayerPhase? phase5 = new PlayerPhase?(PlayerPhase.Replay);
				PlayerTeam? team4 = null;
				PlayerRole? role4 = null;
				float? delay = null;
				player.Server_SetGameState(phase5, team4, role4, delay);
			}
			break;
		}
		if (player.IsCharacterSpawned)
		{
			if (gamePhase == GamePhase.FaceOff)
			{
				player.PlayerBody.Server_Freeze((RigidbodyConstraints)10);
				return;
			}
			player.PlayerBody.Server_Unfreeze();
		}
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x0000BA8C File Offset: 0x00009C8C
	protected virtual void ClearGameResult()
	{
		this.PlayerManager.GetPlayers(false).ForEach(delegate(Player player)
		{
			player.Server_ResetPoints();
			if (this.CanPlayerEnterPhase(player, PlayerPhase.PositionSelect))
			{
				player.Server_SetGameState(new PlayerPhase?(PlayerPhase.PositionSelect), null, null, null);
				if (player.PlayerPosition != null)
				{
					player.PlayerPosition.Server_Unclaim();
				}
			}
		});
		this.gameResult = new GameResult();
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x00027BF0 File Offset: 0x00025DF0
	protected virtual void UpdateGameResult(PlayerTeam? forfeitingTeam = null)
	{
		PlayerTeam winningTeam = (forfeitingTeam == null) ? ((this.GameManager.BlueScore > this.GameManager.RedScore) ? PlayerTeam.Blue : ((this.GameManager.BlueScore < this.GameManager.RedScore) ? PlayerTeam.Red : PlayerTeam.None)) : Utils.GetOpposingTeam(forfeitingTeam.Value).GetValueOrDefault();
		this.gameResult.winningTeam = winningTeam;
		this.gameResult.blueScore = this.GameManager.BlueScore;
		this.gameResult.redScore = this.GameManager.RedScore;
		this.gameResult.forefeit = (forfeitingTeam != null);
		foreach (Player player in this.PlayerManager.GetPlayers(false))
		{
			string key = player.SteamId.Value.ToString();
			if (!this.gameResult.playerResults.ContainsKey(key))
			{
				this.gameResult.playerResults[key] = new PlayerResult();
			}
			this.gameResult.playerResults[key].goals = player.Goals.Value;
			this.gameResult.playerResults[key].assists = player.Assists.Value;
		}
		this.UpdatePlayerResult(Array.Empty<string>());
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x00027D80 File Offset: 0x00025F80
	protected virtual void UpdatePlayerResult(params string[] steamIds)
	{
		foreach (string key in steamIds)
		{
			if (!this.gameResult.playerResults.ContainsKey(key))
			{
				this.gameResult.playerResults[key] = new PlayerResult();
			}
		}
		foreach (Player player in this.PlayerManager.GetPlayers(false))
		{
			string key2 = player.SteamId.Value.ToString();
			if (!this.gameResult.playerResults.ContainsKey(key2))
			{
				this.gameResult.playerResults[key2] = new PlayerResult();
			}
			this.gameResult.playerResults[key2].goals = player.Goals.Value;
			this.gameResult.playerResults[key2].assists = player.Assists.Value;
		}
	}

	// Token: 0x06000461 RID: 1121 RVA: 0x00027EA4 File Offset: 0x000260A4
	protected virtual void StartGame(GamePhase phase = GamePhase.Warmup)
	{
		this.ClearGameResult();
		this.UpdateGameResult(null);
		this.GameManager.Server_SetGameState(new GamePhase?(phase), new int?(base.Config.phaseDurationMap[phase]), new int?(1), new int?(0), new int?(0), new bool?(false));
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x00027F0C File Offset: 0x0002610C
	protected virtual void EndGame()
	{
		this.UpdateGameResult(null);
		this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.GameOver), new int?(base.Config.phaseDurationMap[GamePhase.GameOver]), null, null, null, null);
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x00027F7C File Offset: 0x0002617C
	protected virtual void ForfeitGame(PlayerTeam forfeitingTeam)
	{
		this.UpdateGameResult(new PlayerTeam?(forfeitingTeam));
		this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.GameOver), new int?(base.Config.phaseDurationMap[GamePhase.GameOver]), null, null, null, null);
	}

	// Token: 0x06000464 RID: 1124 RVA: 0x00027FE8 File Offset: 0x000261E8
	protected override void OnWarmupTimedOut()
	{
		base.OnWarmupTimedOut();
		this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.PreGame), new int?(base.Config.phaseDurationMap[GamePhase.PreGame]), null, null, null, null);
	}

	// Token: 0x06000465 RID: 1125 RVA: 0x0002804C File Offset: 0x0002624C
	protected override void OnPreGameTimedOut()
	{
		base.OnPreGameTimedOut();
		this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.FaceOff), new int?(base.Config.phaseDurationMap[GamePhase.FaceOff]), null, null, null, null);
	}

	// Token: 0x06000466 RID: 1126 RVA: 0x000280B0 File Offset: 0x000262B0
	protected override void OnFaceOffTimedOut()
	{
		base.OnFaceOffTimedOut();
		this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.Play), new int?(this.tickRemainder), null, null, null, null);
	}

	// Token: 0x06000467 RID: 1127 RVA: 0x00028104 File Offset: 0x00026304
	protected override void OnPlayTimedOut()
	{
		base.OnPlayTimedOut();
		if (this.GameManager.Period < base.Config.maxPeriods)
		{
			this.tickRemainder = base.Config.phaseDurationMap[GamePhase.Play];
			GameManager gameManager = this.GameManager;
			GamePhase? phase = new GamePhase?(GamePhase.Intermission);
			int? tick = new int?(base.Config.phaseDurationMap[GamePhase.Intermission]);
			int? period = new int?(this.GameManager.Period + 1);
			int? blueScore = null;
			int? redScore = null;
			bool? isOvertime = null;
			gameManager.Server_SetGameState(phase, tick, period, blueScore, redScore, isOvertime);
			return;
		}
		if (this.GameManager.BlueScore == this.GameManager.RedScore)
		{
			this.tickRemainder = base.Config.phaseDurationMap[GamePhase.Play];
			GameManager gameManager2 = this.GameManager;
			GamePhase? phase2 = new GamePhase?(GamePhase.Intermission);
			int? tick2 = new int?(base.Config.phaseDurationMap[GamePhase.Intermission]);
			int? period2 = new int?(this.GameManager.Period + 1);
			bool? isOvertime = new bool?(true);
			gameManager2.Server_SetGameState(phase2, tick2, period2, null, null, isOvertime);
			return;
		}
		this.EndGame();
	}

	// Token: 0x06000468 RID: 1128 RVA: 0x00028240 File Offset: 0x00026440
	protected override void OnBlueScoreTimedOut()
	{
		base.OnBlueScoreTimedOut();
		this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.Replay), new int?(base.Config.phaseDurationMap[GamePhase.Replay]), null, null, null, null);
	}

	// Token: 0x06000469 RID: 1129 RVA: 0x000282A4 File Offset: 0x000264A4
	protected override void OnRedScoreTimedOut()
	{
		base.OnRedScoreTimedOut();
		this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.Replay), new int?(base.Config.phaseDurationMap[GamePhase.Replay]), null, null, null, null);
	}

	// Token: 0x0600046A RID: 1130 RVA: 0x00028308 File Offset: 0x00026508
	protected override void OnReplayTimedOut()
	{
		base.OnReplayTimedOut();
		if (this.GameManager.IsOvertime)
		{
			this.EndGame();
			return;
		}
		this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.FaceOff), new int?(base.Config.phaseDurationMap[GamePhase.FaceOff]), null, null, null, null);
	}

	// Token: 0x0600046B RID: 1131 RVA: 0x00028380 File Offset: 0x00026580
	protected override void OnIntermissionTimedOut()
	{
		base.OnIntermissionTimedOut();
		this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.FaceOff), new int?(base.Config.phaseDurationMap[GamePhase.FaceOff]), null, null, null, null);
	}

	// Token: 0x0600046C RID: 1132 RVA: 0x000283E4 File Offset: 0x000265E4
	protected override void OnGameOverTimedOut()
	{
		base.OnGameOverTimedOut();
		this.GameManager.Server_SetGameState(new GamePhase?(GamePhase.PostGame), new int?(base.Config.phaseDurationMap[GamePhase.PostGame]), null, null, null, null);
	}

	// Token: 0x0600046D RID: 1133 RVA: 0x0000BAB6 File Offset: 0x00009CB6
	protected override void OnPostGameTimedOut()
	{
		base.OnPostGameTimedOut();
		this.StartGame(GamePhase.Warmup);
	}

	// Token: 0x0600046E RID: 1134 RVA: 0x0002844C File Offset: 0x0002664C
	protected override void OnGamePhaseStarted(GamePhase gamePhase)
	{
		base.OnGamePhaseStarted(gamePhase);
		if (this.isReplayable)
		{
			this.ReplayManager.Server_StartRecording();
		}
		else
		{
			this.ReplayManager.Server_StopRecording();
		}
		if (!this.isGameInProgress)
		{
			foreach (Vote vote in this.VoteManager.Server_GetVotesByName("forfeit"))
			{
				this.VoteManager.Server_RemoveVote(vote);
			}
		}
		this.PreparePlayersForGamePhase(gamePhase);
	}

	// Token: 0x0600046F RID: 1135 RVA: 0x0000BAC5 File Offset: 0x00009CC5
	protected override void OnWarmupStarted()
	{
		base.OnWarmupStarted();
		this.PuckManager.Server_DespawnPucks(false);
		this.PuckManager.Server_SpawnPucksForPhase(GamePhase.Warmup);
	}

	// Token: 0x06000470 RID: 1136 RVA: 0x0000BAE5 File Offset: 0x00009CE5
	protected override void OnPreGameStarted()
	{
		base.OnPreGameStarted();
		this.PuckManager.Server_DespawnPucks(false);
		this.tickRemainder = base.Config.phaseDurationMap[GamePhase.Play];
	}

	// Token: 0x06000471 RID: 1137 RVA: 0x0000BB15 File Offset: 0x00009D15
	protected override void OnFaceOffStarted()
	{
		base.OnFaceOffStarted();
		this.PuckManager.Server_DespawnPucks(false);
	}

	// Token: 0x06000472 RID: 1138 RVA: 0x0000BB29 File Offset: 0x00009D29
	protected override void OnPlayStarted()
	{
		base.OnPlayStarted();
		this.PuckManager.Server_SpawnPucksForPhase(GamePhase.Play);
	}

	// Token: 0x06000473 RID: 1139 RVA: 0x000284C0 File Offset: 0x000266C0
	protected override void OnBlueScoreStarted()
	{
		base.OnBlueScoreStarted();
		this.Level.Server_PlayerCheerSound((float)(base.Config.phaseDurationMap[GamePhase.BlueScore] + base.Config.phaseDurationMap[GamePhase.Replay]));
		this.Level.Server_PlayRedGoalSound();
	}

	// Token: 0x06000474 RID: 1140 RVA: 0x00028518 File Offset: 0x00026718
	protected override void OnRedScoreStarted()
	{
		base.OnRedScoreStarted();
		this.Level.Server_PlayerCheerSound((float)(base.Config.phaseDurationMap[GamePhase.RedScore] + base.Config.phaseDurationMap[GamePhase.Replay]));
		this.Level.Server_PlayBlueGoalSound();
	}

	// Token: 0x06000475 RID: 1141 RVA: 0x0000BB3D File Offset: 0x00009D3D
	protected override void OnIntermissionStarted()
	{
		base.OnIntermissionStarted();
		this.Level.Server_PlayHornSound();
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x0000BB50 File Offset: 0x00009D50
	protected override void OnReplayStarted()
	{
		base.OnReplayStarted();
		this.PuckManager.Server_DespawnPucks(false);
		this.ReplayManager.Server_StartReplaying((float)base.Config.phaseDurationMap[GamePhase.Replay]);
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00028570 File Offset: 0x00026770
	protected override void OnGameOverStarted()
	{
		base.OnGameOverStarted();
		PlayerTeam winningTeam = this.gameResult.winningTeam;
		if (this.gameResult.forefeit)
		{
			string str = StringUtils.WrapInTeamColor(winningTeam.ToString(), winningTeam);
			this.ChatManager.Server_BroadcastChatMessage("Game Over! " + str + " team wins by forfeit!", "#ffe97f");
		}
		else
		{
			PlayerTeam team = (winningTeam == PlayerTeam.Blue) ? PlayerTeam.Red : PlayerTeam.Blue;
			int num = Mathf.Max(this.gameResult.blueScore, this.gameResult.redScore);
			int num2 = Mathf.Min(this.gameResult.blueScore, this.gameResult.redScore);
			string content = string.Concat(new string[]
			{
				"Game Over! ",
				StringUtils.WrapInTeamColor(winningTeam.ToString(), winningTeam),
				" team wins with a score of ",
				StringUtils.WrapInTeamColor(num.ToString(), winningTeam),
				" to ",
				StringUtils.WrapInTeamColor(num2.ToString(), team),
				"!"
			});
			this.ChatManager.Server_BroadcastChatMessage(content, "#ffe97f");
		}
		this.Level.Server_PlayBlueGoalSound();
		this.Level.Server_PlayRedGoalSound();
		this.Level.Server_PlayHornSound();
	}

	// Token: 0x06000478 RID: 1144 RVA: 0x0000BB86 File Offset: 0x00009D86
	protected override void OnReplayEnded()
	{
		base.OnReplayEnded();
		this.ReplayManager.Server_StopReplaying();
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x000286B0 File Offset: 0x000268B0
	protected override void OnPlayerJoined(Player player)
	{
		base.OnPlayerJoined(player);
		string text = player.SteamId.Value.ToString();
		this.UpdatePlayerResult(new string[]
		{
			text
		});
		if (this.gameResult.playerResults.ContainsKey(text))
		{
			PlayerResult playerResult = this.gameResult.playerResults[text];
			player.Goals.Value = playerResult.goals;
			player.Assists.Value = playerResult.assists;
		}
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00028738 File Offset: 0x00026938
	protected override void OnPlayerLeft(Player player)
	{
		base.OnPlayerLeft(player);
		string text = player.SteamId.Value.ToString();
		this.UpdatePlayerResult(new string[]
		{
			text
		});
		if (player.PlayerPosition != null)
		{
			player.PlayerPosition.Server_Unclaim();
		}
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x00028790 File Offset: 0x00026990
	protected override void OnPlayerPhaseChanged(Player player, PlayerPhase oldPhase, PlayerPhase newPhase)
	{
		base.OnPlayerPhaseChanged(player, oldPhase, newPhase);
		switch (newPhase)
		{
		case PlayerPhase.PositionSelect:
			if (player.IsCharacterSpawned)
			{
				player.Server_DespawnCharacter();
				return;
			}
			break;
		case PlayerPhase.Play:
			player.Server_SpawnCharacter(player.PlayerPosition.transform.position, player.PlayerPosition.transform.rotation, player.Role);
			return;
		case PlayerPhase.Replay:
			if (player.IsCharacterSpawned)
			{
				player.Server_DespawnCharacter();
				return;
			}
			break;
		case PlayerPhase.Spectate:
			player.Server_SpawnSpectatorCamera(Vector3.zero, Quaternion.identity);
			return;
		default:
			if (player.IsCharacterSpawned)
			{
				player.Server_DespawnCharacter();
			}
			if (player.PlayerPosition != null)
			{
				player.PlayerPosition.Server_Unclaim();
			}
			break;
		}
	}

	// Token: 0x0600047C RID: 1148 RVA: 0x00028844 File Offset: 0x00026A44
	protected override void OnPlayerPositionChanged(Player player, PlayerPosition oldPlayerPosition, PlayerPosition newPlayerPosition)
	{
		base.OnPlayerPositionChanged(player, oldPlayerPosition, newPlayerPosition);
		PlayerRole? role;
		if (newPlayerPosition == null)
		{
			role = new PlayerRole?(PlayerRole.None);
			player.Server_SetGameState(null, null, role, null);
			return;
		}
		role = new PlayerRole?(newPlayerPosition.Role);
		player.Server_SetGameState(null, null, role, null);
		this.PreparePlayerForGamePhase(player, this.GameManager.Phase);
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x000288D0 File Offset: 0x00026AD0
	protected override void OnGoalScored(PlayerTeam byTeam, Player goalPlayer, Player assistPlayer, Player secondAssistPlayer, Puck puck)
	{
		base.OnGoalScored(byTeam, goalPlayer, assistPlayer, secondAssistPlayer, puck);
		this.tickRemainder = this.GameManager.Tick;
		if (goalPlayer)
		{
			goalPlayer.Server_GoalScored();
		}
		if (assistPlayer)
		{
			assistPlayer.Server_AssistScored();
		}
		if (secondAssistPlayer)
		{
			secondAssistPlayer.Server_AssistScored();
		}
		if (byTeam != PlayerTeam.Blue)
		{
			if (byTeam == PlayerTeam.Red)
			{
				GameManager gameManager = this.GameManager;
				GamePhase? phase = new GamePhase?(GamePhase.RedScore);
				int? tick = new int?(base.Config.phaseDurationMap[GamePhase.RedScore]);
				int? num = new int?(this.GameManager.GameState.Value.RedScore + 1);
				gameManager.Server_SetGameState(phase, tick, null, null, num, null);
			}
		}
		else
		{
			GameManager gameManager2 = this.GameManager;
			GamePhase? phase2 = new GamePhase?(GamePhase.BlueScore);
			int? tick2 = new int?(base.Config.phaseDurationMap[GamePhase.BlueScore]);
			int? num = new int?(this.GameManager.GameState.Value.BlueScore + 1);
			gameManager2.Server_SetGameState(phase2, tick2, null, num, null, null);
		}
		if (puck)
		{
			string text = puck.Speed.ToString(CultureInfo.InvariantCulture);
			string text2 = puck.ShotSpeed.ToString(CultureInfo.InvariantCulture);
			this.ChatManager.Server_BroadcastChatMessage(string.Concat(new string[]
			{
				"Goal scored! <united>",
				text,
				"</united> &units across the line, <united>",
				text2,
				"</united> &units from the stick."
			}), "#ffe97f");
		}
		else
		{
			this.ChatManager.Server_BroadcastChatMessage("Goal scored!", "#ffe97f");
		}
		this.UpdateGameResult(null);
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x00028A98 File Offset: 0x00026C98
	protected override void OnPuckEnterGoal(PlayerTeam team, Puck puck)
	{
		base.OnPuckEnterGoal(team, puck);
		if (this.GameManager.Phase != GamePhase.Play)
		{
			return;
		}
		PlayerTeam? opposingTeam = Utils.GetOpposingTeam(team);
		if (opposingTeam == null)
		{
			return;
		}
		List<KeyValuePair<Player, float>> playerCollisionsByTeam = puck.GetPlayerCollisionsByTeam(opposingTeam.Value);
		Player goalPlayer = null;
		Player assistPlayer = null;
		Player secondAssistPlayer = null;
		if (playerCollisionsByTeam.Count >= 1)
		{
			List<KeyValuePair<Player, float>> list = playerCollisionsByTeam;
			goalPlayer = list[list.Count - 1].Key;
			if (playerCollisionsByTeam.Count >= 2)
			{
				List<KeyValuePair<Player, float>> list2 = playerCollisionsByTeam;
				assistPlayer = list2[list2.Count - 2].Key;
			}
			if (playerCollisionsByTeam.Count >= 3)
			{
				List<KeyValuePair<Player, float>> list3 = playerCollisionsByTeam;
				secondAssistPlayer = list3[list3.Count - 3].Key;
			}
		}
		this.ScoreGoal(opposingTeam.Value, goalPlayer, assistPlayer, secondAssistPlayer, puck);
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x00028B58 File Offset: 0x00026D58
	protected override void OnPlayerRequestPositionSelect(Player player)
	{
		base.OnPlayerRequestPositionSelect(player);
		if (this.CanPlayerEnterPhase(player, PlayerPhase.PositionSelect))
		{
			player.Server_SetGameState(new PlayerPhase?(PlayerPhase.PositionSelect), null, null, null);
			if (player.PlayerPosition != null)
			{
				player.PlayerPosition.Server_Unclaim();
			}
		}
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00028BB8 File Offset: 0x00026DB8
	protected override void OnPlayerRequestPosition(Player player, PlayerPosition position)
	{
		base.OnPlayerRequestPosition(player, position);
		if (position.IsClaimed)
		{
			if (player == position.ClaimedByPlayer)
			{
				position.Server_Unclaim();
				return;
			}
		}
		else if (position.Team == player.Team)
		{
			PlayerPosition playerPosition = player.PlayerPosition;
			position.Server_Claim(player);
			if (playerPosition != null)
			{
				playerPosition.Server_Unclaim();
			}
		}
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x00028C18 File Offset: 0x00026E18
	protected override void OnVoteRemoved(Vote vote)
	{
		base.OnVoteRemoved(vote);
		if (!vote.Passed)
		{
			return;
		}
		string name = vote.Name;
		if (!(name == "kick"))
		{
			if (!(name == "forfeit"))
			{
				return;
			}
			PlayerTeam forfeitingTeam = (PlayerTeam)vote.Data;
			this.ForfeitGame(forfeitingTeam);
			return;
		}
		else
		{
			string text = (string)vote.Data;
			Player playerBySteamId = this.PlayerManager.GetPlayerBySteamId(text);
			if (playerBySteamId)
			{
				this.ServerManager.Server_KickPlayer(playerBySteamId, DisconnectionCode.Kicked, null, true);
				return;
			}
			this.ServerManager.TimeoutManager.AddSteamIdTimeout(text, 60f);
			return;
		}
	}

	// Token: 0x06000482 RID: 1154 RVA: 0x00028CB8 File Offset: 0x00026EB8
	protected override void OnChatCommand(Player player, string command, string[] args)
	{
		bool flag = player.AdminLevel.Value > 0 || this.ServerManager.AdminManager.IsSteamIdAdmin(player.SteamId.Value.ToString());
		uint num = <PrivateImplementationDetails>.ComputeStringHash(command);
		if (num <= 2919672153U)
		{
			if (num <= 580386680U)
			{
				if (num != 261473465U)
				{
					if (num != 580386680U)
					{
						goto IL_5E1;
					}
					if (!(command == "/kick"))
					{
						goto IL_5E1;
					}
					if (!flag)
					{
						this.ChatManager.Server_SendChatMessage("You do not have permissions to use this command", "#e74c3c", new ulong[]
						{
							player.OwnerClientId
						});
						return;
					}
					string needle = (args.Length != 0) ? args[0] : string.Empty;
					Player playerByNeedle = this.PlayerManager.GetPlayerByNeedle(needle, true);
					if (!playerByNeedle)
					{
						this.ChatManager.Server_SendChatMessage("Player not found", "#e74c3c", new ulong[]
						{
							player.OwnerClientId
						});
						return;
					}
					this.ServerManager.Server_KickPlayer(playerByNeedle, DisconnectionCode.Kicked, null, true);
					return;
				}
				else
				{
					if (!(command == "/resume"))
					{
						goto IL_5E1;
					}
					if (!flag)
					{
						this.ChatManager.Server_SendChatMessage("You do not have permissions to use this command", "#e74c3c", new ulong[]
						{
							player.OwnerClientId
						});
						return;
					}
					this.GameManager.Server_StartTicking();
					return;
				}
			}
			else if (num != 2459559413U)
			{
				if (num != 2919672153U)
				{
					goto IL_5E1;
				}
				if (!(command == "/forfeit"))
				{
					goto IL_5E1;
				}
			}
			else
			{
				if (!(command == "/skip"))
				{
					goto IL_5E1;
				}
				if (!flag)
				{
					this.ChatManager.Server_SendChatMessage("You do not have permissions to use this command", "#e74c3c", new ulong[]
					{
						player.OwnerClientId
					});
					return;
				}
				GameManager gameManager = this.GameManager;
				int? tick = new int?(0);
				gameManager.Server_SetGameState(null, tick, null, null, null, null);
				return;
			}
		}
		else
		{
			if (num <= 3277915388U)
			{
				if (num != 3194771123U)
				{
					if (num != 3277915388U)
					{
						goto IL_5E1;
					}
					if (!(command == "/votekick"))
					{
						goto IL_5E1;
					}
				}
				else
				{
					if (!(command == "/ban"))
					{
						goto IL_5E1;
					}
					if (!flag)
					{
						this.ChatManager.Server_SendChatMessage("You do not have permissions to use this command", "#e74c3c", new ulong[]
						{
							player.OwnerClientId
						});
						return;
					}
					string needle2 = (args.Length != 0) ? args[0] : string.Empty;
					Player playerByNeedle2 = this.PlayerManager.GetPlayerByNeedle(needle2, true);
					if (!playerByNeedle2)
					{
						this.ChatManager.Server_SendChatMessage("Player not found", "#e74c3c", new ulong[]
						{
							player.OwnerClientId
						});
						return;
					}
					this.ServerManager.Server_BanPlayer(playerByNeedle2);
					return;
				}
			}
			else if (num != 3607949496U)
			{
				if (num != 3819631050U)
				{
					if (num != 4169298025U)
					{
						goto IL_5E1;
					}
					if (!(command == "/vk"))
					{
						goto IL_5E1;
					}
				}
				else
				{
					if (!(command == "/ff"))
					{
						goto IL_5E1;
					}
					goto IL_2ED;
				}
			}
			else
			{
				if (!(command == "/pause"))
				{
					goto IL_5E1;
				}
				if (!flag)
				{
					this.ChatManager.Server_SendChatMessage("You do not have permissions to use this command", "#e74c3c", new ulong[]
					{
						player.OwnerClientId
					});
					return;
				}
				this.GameManager.Server_StopTicking();
				return;
			}
			Vote vote = this.VoteManager.Server_GetTeamVoteByName("kick", player.Team);
			if (vote != null)
			{
				vote.CastVote(player.SteamId.Value.ToString(), true);
				return;
			}
			string needle3 = (args.Length != 0) ? args[0] : string.Empty;
			Player playerByNeedle3 = this.PlayerManager.GetPlayerByNeedle(needle3, true);
			if (!playerByNeedle3)
			{
				this.ChatManager.Server_SendChatMessage("Player not found", "#e74c3c", new ulong[]
				{
					player.OwnerClientId
				});
				return;
			}
			if (playerByNeedle3.Team != player.Team)
			{
				this.ChatManager.Server_SendChatMessage("You can only kick players on your team", "#e74c3c", new ulong[]
				{
					player.OwnerClientId
				});
				return;
			}
			string str = StringUtils.WrapInTeamColor(playerByNeedle3.Username.Value.ToString(), playerByNeedle3.Team);
			this.VoteManager.Server_AddVote("kick", "Kick " + str, "use /vk or /votekick", new PlayerTeam[]
			{
				player.Team
			}, 30f, player.SteamId.Value.ToString(), Utils.GetVoteMajority(this.PlayerManager.GetPlayersByTeam(player.Team, false).Count), playerByNeedle3.SteamId.Value.ToString());
			return;
		}
		IL_2ED:
		if (player.Team != PlayerTeam.Blue && player.Team != PlayerTeam.Red)
		{
			this.ChatManager.Server_SendChatMessage("You must be on a team to start this vote", "#e74c3c", new ulong[]
			{
				player.OwnerClientId
			});
			return;
		}
		if (!this.isGameInProgress)
		{
			this.ChatManager.Server_SendChatMessage("You can not forfeit right now", "#e74c3c", new ulong[]
			{
				player.OwnerClientId
			});
			return;
		}
		Vote vote2 = this.VoteManager.Server_GetTeamVoteByName("forfeit", player.Team);
		if (vote2 != null)
		{
			vote2.CastVote(player.SteamId.Value.ToString(), true);
			return;
		}
		this.VoteManager.Server_AddVote("forfeit", "Forfeit", "use /ff or /forfeit", new PlayerTeam[]
		{
			player.Team
		}, 30f, player.SteamId.Value.ToString(), Utils.GetVoteMajority(this.PlayerManager.GetPlayersByTeam(player.Team, false).Count), player.Team);
		return;
		IL_5E1:
		base.OnChatCommand(player, command, args);
	}

	// Token: 0x040002C9 RID: 713
	protected int tickRemainder;

	// Token: 0x040002CA RID: 714
	protected GameResult gameResult = new GameResult();
}
