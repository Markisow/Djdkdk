using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000074 RID: 116
public abstract class BaseGameMode<TConfig> : IGameMode where TConfig : BaseGameModeConfig, new()
{
	// Token: 0x17000064 RID: 100
	// (get) Token: 0x060003BF RID: 959 RVA: 0x0000B62A File Offset: 0x0000982A
	protected global::Logger Logger
	{
		get
		{
			return new global::Logger(base.GetType().Name);
		}
	}

	// Token: 0x17000065 RID: 101
	// (get) Token: 0x060003C0 RID: 960 RVA: 0x0000B63C File Offset: 0x0000983C
	// (set) Token: 0x060003C1 RID: 961 RVA: 0x0000B644 File Offset: 0x00009844
	public bool IsInitialized { get; set; }

	// Token: 0x17000066 RID: 102
	// (get) Token: 0x060003C2 RID: 962 RVA: 0x0000B64D File Offset: 0x0000984D
	// (set) Token: 0x060003C3 RID: 963 RVA: 0x0000B655 File Offset: 0x00009855
	public TConfig Config { get; private set; }

	// Token: 0x060003C4 RID: 964 RVA: 0x0000B65E File Offset: 0x0000985E
	public BaseGameMode(string defaultConfigFilePath, string configFilePathCliArgument = null, string configCliArgument = null, string configEnvVariable = null)
	{
		this.defaultConfigFilePath = defaultConfigFilePath;
		this.configFilePathCliArgument = configFilePathCliArgument;
		this.configCliArgument = configCliArgument;
		this.configEnvVariable = configEnvVariable;
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00026084 File Offset: 0x00024284
	public virtual bool Initialize(Level level, ServerManager serverManager, GameManager gameManager, PlayerManager playerManager, PuckManager puckManager, ChatManager chatManager, ReplayManager replayManager, VoteManager voteManager)
	{
		if (this.IsInitialized)
		{
			return false;
		}
		this.IsInitialized = true;
		this.Level = level;
		this.ServerManager = serverManager;
		this.GameManager = gameManager;
		this.PlayerManager = playerManager;
		this.PuckManager = puckManager;
		this.ChatManager = chatManager;
		this.ReplayManager = replayManager;
		this.VoteManager = voteManager;
		this.LoadConfig(this.defaultConfigFilePath, this.configFilePathCliArgument, this.configCliArgument, this.configEnvVariable);
		this.SubscribeEvents();
		return true;
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00026104 File Offset: 0x00024304
	public virtual bool Dispose()
	{
		if (!this.IsInitialized)
		{
			return false;
		}
		this.IsInitialized = false;
		this.Level = null;
		this.GameManager = null;
		this.PlayerManager = null;
		this.PuckManager = null;
		this.ChatManager = null;
		this.ReplayManager = null;
		this.VoteManager = null;
		this.UnsubscribeEvents();
		return true;
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x0002615C File Offset: 0x0002435C
	protected virtual void SubscribeEvents()
	{
		EventManager.AddEventListener("Event_Everyone_OnGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGameStateChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerAdded", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerAdded));
		EventManager.AddEventListener("Event_Everyone_OnPlayerRemoved", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerRemoved));
		EventManager.AddEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.AddEventListener("Event_Everyone_OnPlayerPositionChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionChanged));
		EventManager.AddEventListener("Event_Everyone_OnGoalScored", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGoalScored));
		EventManager.AddEventListener("Event_Server_OnPuckEnterGoal", new Action<Dictionary<string, object>>(this.Event_Server_OnPuckEnterGoal));
		EventManager.AddEventListener("Event_Server_OnPlayerRequestTeamSelect", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerRequestTeamSelect));
		EventManager.AddEventListener("Event_Server_OnPlayerRequestTeam", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerRequestTeam));
		EventManager.AddEventListener("Event_Server_OnPlayerRequestPositionSelect", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerRequestPositionSelect));
		EventManager.AddEventListener("Event_Server_OnPlayerRequestPosition", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerRequestPosition));
		EventManager.AddEventListener("Event_Server_OnPlayerRequestHandedness", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerRequestHandedness));
		EventManager.AddEventListener("Event_Server_OnVoteAdded", new Action<Dictionary<string, object>>(this.Event_Server_OnVoteAdded));
		EventManager.AddEventListener("Event_Server_OnVoteProgressed", new Action<Dictionary<string, object>>(this.Event_Server_OnVoteProgressed));
		EventManager.AddEventListener("Event_Server_OnVoteRemoved", new Action<Dictionary<string, object>>(this.Event_Server_OnVoteRemoved));
		EventManager.AddEventListener("Event_Server_OnChatCommand", new Action<Dictionary<string, object>>(this.Event_Server_OnChatCommand));
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x000262CC File Offset: 0x000244CC
	protected virtual void UnsubscribeEvents()
	{
		EventManager.RemoveEventListener("Event_Everyone_OnGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGameStateChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerAdded", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerAdded));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerRemoved", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerRemoved));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerGameStateChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerGameStateChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnPlayerPositionChanged", new Action<Dictionary<string, object>>(this.Event_Everyone_OnPlayerPositionChanged));
		EventManager.RemoveEventListener("Event_Everyone_OnGoalScored", new Action<Dictionary<string, object>>(this.Event_Everyone_OnGoalScored));
		EventManager.RemoveEventListener("Event_Server_OnPuckEnterGoal", new Action<Dictionary<string, object>>(this.Event_Server_OnPuckEnterGoal));
		EventManager.RemoveEventListener("Event_Server_OnPlayerRequestTeamSelect", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerRequestTeamSelect));
		EventManager.RemoveEventListener("Event_Server_OnPlayerRequestTeam", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerRequestTeam));
		EventManager.RemoveEventListener("Event_Server_OnPlayerRequestPositionSelect", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerRequestPositionSelect));
		EventManager.RemoveEventListener("Event_Server_OnPlayerRequestPosition", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerRequestPosition));
		EventManager.RemoveEventListener("Event_Server_OnPlayerRequestHandedness", new Action<Dictionary<string, object>>(this.Event_Server_OnPlayerRequestHandedness));
		EventManager.RemoveEventListener("Event_Server_OnVoteAdded", new Action<Dictionary<string, object>>(this.Event_Server_OnVoteAdded));
		EventManager.RemoveEventListener("Event_Server_OnVoteProgressed", new Action<Dictionary<string, object>>(this.Event_Server_OnVoteProgressed));
		EventManager.RemoveEventListener("Event_Server_OnVoteRemoved", new Action<Dictionary<string, object>>(this.Event_Server_OnVoteRemoved));
		EventManager.RemoveEventListener("Event_Server_OnChatCommand", new Action<Dictionary<string, object>>(this.Event_Server_OnChatCommand));
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x0002643C File Offset: 0x0002463C
	private void LoadConfig(string defaultFilePath, string filePathCliArgument = null, string cliArgument = null, string envVariable = null)
	{
		string environmentVariable = Environment.GetEnvironmentVariable(envVariable);
		string commandLineArgument = Utils.GetCommandLineArgument(cliArgument, null);
		if (!string.IsNullOrEmpty(commandLineArgument))
		{
			this.Logger.Info("Deserializing config from CLI argument (" + cliArgument + ")");
			this.Config = ConfigUtils.LoadConfigFromSerializedString<TConfig>(commandLineArgument);
		}
		else if (!string.IsNullOrEmpty(environmentVariable))
		{
			this.Logger.Info("Deserializing config from environment variable (" + envVariable + ")");
			this.Config = ConfigUtils.LoadConfigFromSerializedString<TConfig>(environmentVariable);
		}
		else
		{
			string text = Utils.GetCommandLineArgument(filePathCliArgument, null) ?? defaultFilePath;
			this.Logger.Info("Deserializing config from file (" + text + ")");
			this.Config = ConfigUtils.LoadConfigFromFile<TConfig>(text, true);
		}
		this.OnConfigLoaded();
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00026500 File Offset: 0x00024700
	protected virtual void ScoreGoal(PlayerTeam byTeam, Player goalPlayer, Player assistPlayer, Player secondAssistPlayer, Puck puck)
	{
		NetworkObjectReference goalPlayerNetworkObjectReference = new NetworkObjectReference((goalPlayer != null) ? goalPlayer.NetworkObject : null);
		NetworkObjectReference assistPlayerNetworkObjectReference = new NetworkObjectReference((assistPlayer != null) ? assistPlayer.NetworkObject : null);
		NetworkObjectReference secondAssistPlayerNetworkObjectReference = new NetworkObjectReference((secondAssistPlayer != null) ? secondAssistPlayer.NetworkObject : null);
		NetworkObjectReference puckNetworkObjectReference = new NetworkObjectReference((puck != null) ? puck.NetworkObject : null);
		this.GameManager.Server_NotifyGoalScoredRpc(byTeam, goalPlayerNetworkObjectReference, assistPlayerNetworkObjectReference, secondAssistPlayerNetworkObjectReference, puckNetworkObjectReference);
	}

	// Token: 0x060003CB RID: 971 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnConfigLoaded()
	{
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00026570 File Offset: 0x00024770
	protected virtual void OnGameStateChanged(GameState oldGameState, GameState newGameState)
	{
		bool flag = newGameState.Tick <= 0;
		bool flag2 = oldGameState.Phase != newGameState.Phase;
		if (flag)
		{
			this.OnGamePhaseTimedOut(newGameState.Phase);
			return;
		}
		if (flag2)
		{
			this.OnGamePhaseEnded(oldGameState.Phase);
			this.OnGamePhaseStarted(newGameState.Phase);
		}
	}

	// Token: 0x060003CD RID: 973 RVA: 0x000265C8 File Offset: 0x000247C8
	protected virtual void OnGamePhaseTimedOut(GamePhase gamePhase)
	{
		switch (gamePhase)
		{
		case GamePhase.Warmup:
			this.OnWarmupTimedOut();
			return;
		case GamePhase.PreGame:
			this.OnPreGameTimedOut();
			return;
		case GamePhase.FaceOff:
			this.OnFaceOffTimedOut();
			return;
		case GamePhase.Play:
			this.OnPlayTimedOut();
			return;
		case GamePhase.BlueScore:
			this.OnBlueScoreTimedOut();
			return;
		case GamePhase.RedScore:
			this.OnRedScoreTimedOut();
			return;
		case GamePhase.Replay:
			this.OnReplayTimedOut();
			return;
		case GamePhase.Intermission:
			this.OnIntermissionTimedOut();
			return;
		case GamePhase.GameOver:
			this.OnGameOverTimedOut();
			return;
		case GamePhase.PostGame:
			this.OnPostGameTimedOut();
			return;
		default:
			return;
		}
	}

	// Token: 0x060003CE RID: 974 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnWarmupTimedOut()
	{
	}

	// Token: 0x060003CF RID: 975 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPreGameTimedOut()
	{
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnFaceOffTimedOut()
	{
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnBlueScoreTimedOut()
	{
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnRedScoreTimedOut()
	{
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnReplayTimedOut()
	{
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnIntermissionTimedOut()
	{
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPlayTimedOut()
	{
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnGameOverTimedOut()
	{
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPostGameTimedOut()
	{
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x0002664C File Offset: 0x0002484C
	protected virtual void OnGamePhaseStarted(GamePhase gamePhase)
	{
		switch (gamePhase)
		{
		case GamePhase.Warmup:
			this.OnWarmupStarted();
			return;
		case GamePhase.PreGame:
			this.OnPreGameStarted();
			return;
		case GamePhase.FaceOff:
			this.OnFaceOffStarted();
			return;
		case GamePhase.Play:
			this.OnPlayStarted();
			return;
		case GamePhase.BlueScore:
			this.OnBlueScoreStarted();
			return;
		case GamePhase.RedScore:
			this.OnRedScoreStarted();
			return;
		case GamePhase.Replay:
			this.OnReplayStarted();
			return;
		case GamePhase.Intermission:
			this.OnIntermissionStarted();
			return;
		case GamePhase.GameOver:
			this.OnGameOverStarted();
			return;
		case GamePhase.PostGame:
			this.OnPostGameStarted();
			return;
		default:
			return;
		}
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnWarmupStarted()
	{
	}

	// Token: 0x060003DA RID: 986 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPreGameStarted()
	{
	}

	// Token: 0x060003DB RID: 987 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnFaceOffStarted()
	{
	}

	// Token: 0x060003DC RID: 988 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnBlueScoreStarted()
	{
	}

	// Token: 0x060003DD RID: 989 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnRedScoreStarted()
	{
	}

	// Token: 0x060003DE RID: 990 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnReplayStarted()
	{
	}

	// Token: 0x060003DF RID: 991 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnIntermissionStarted()
	{
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPlayStarted()
	{
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnGameOverStarted()
	{
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPostGameStarted()
	{
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x000266D0 File Offset: 0x000248D0
	protected virtual void OnGamePhaseEnded(GamePhase gamePhase)
	{
		switch (gamePhase)
		{
		case GamePhase.Warmup:
			this.OnWarmupEnded();
			return;
		case GamePhase.PreGame:
			this.OnPreGameEnded();
			return;
		case GamePhase.FaceOff:
			this.OnFaceOffEnded();
			return;
		case GamePhase.Play:
			this.OnPlayEnded();
			return;
		case GamePhase.BlueScore:
			this.OnBlueScoreEnded();
			return;
		case GamePhase.RedScore:
			this.OnRedScoreEnded();
			return;
		case GamePhase.Replay:
			this.OnReplayEnded();
			return;
		case GamePhase.Intermission:
			this.OnIntermissionEnded();
			return;
		case GamePhase.GameOver:
			this.OnGameOverEnded();
			return;
		case GamePhase.PostGame:
			this.OnPostGameEnded();
			return;
		default:
			return;
		}
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnWarmupEnded()
	{
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPreGameEnded()
	{
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnFaceOffEnded()
	{
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnBlueScoreEnded()
	{
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnRedScoreEnded()
	{
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnReplayEnded()
	{
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnIntermissionEnded()
	{
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPlayEnded()
	{
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnGameOverEnded()
	{
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPostGameEnded()
	{
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x0000B683 File Offset: 0x00009883
	protected virtual void OnPlayerJoined(Player player)
	{
		this.ChatManager.Server_BroadcastChatMessage(string.Format("{0} has joined the server", player.Username.Value), "#b8b8b8");
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x0000B6AF File Offset: 0x000098AF
	protected virtual void OnPlayerLeft(Player player)
	{
		this.ChatManager.Server_BroadcastChatMessage(string.Format("{0} has left the server", player.Username.Value), "#b8b8b8");
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00026754 File Offset: 0x00024954
	protected virtual void OnPlayerGameStateChanged(Player player, PlayerGameState oldGameState, PlayerGameState newGameState)
	{
		if (oldGameState.Phase != newGameState.Phase)
		{
			this.OnPlayerPhaseChanged(player, oldGameState.Phase, newGameState.Phase);
		}
		if (oldGameState.Team != newGameState.Team)
		{
			this.OnPlayerTeamChanged(player, oldGameState.Team, newGameState.Team);
		}
		if (oldGameState.Role != newGameState.Role)
		{
			this.OnPlayerRoleChanged(player, oldGameState.Role, newGameState.Role);
		}
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPlayerPhaseChanged(Player player, PlayerPhase oldPlayerPhase, PlayerPhase newPlayerPhase)
	{
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x000267C4 File Offset: 0x000249C4
	protected virtual void OnPlayerTeamChanged(Player player, PlayerTeam oldPlayerTeam, PlayerTeam newPlayerTeam)
	{
		if (newPlayerTeam == PlayerTeam.None || newPlayerTeam == PlayerTeam.Spectator)
		{
			return;
		}
		string arg = StringUtils.WrapInTeamColor(newPlayerTeam.ToString(), newPlayerTeam);
		this.ChatManager.Server_BroadcastChatMessage(string.Format("{0} joined team {1}", player.Username.Value, arg), "#b8b8b8");
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPlayerRoleChanged(Player player, PlayerRole oldPlayerRole, PlayerRole newPlayerRole)
	{
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPlayerPositionChanged(Player player, PlayerPosition oldPlayerPosition, PlayerPosition newPlayerPosition)
	{
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnGoalScored(PlayerTeam byTeam, Player goalPlayer, Player assistPlayer, Player secondAssistPlayer, Puck puck)
	{
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPuckEnterGoal(PlayerTeam team, Puck puck)
	{
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPlayerRequestTeamSelect(Player player)
	{
	}

	// Token: 0x060003F8 RID: 1016 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPlayerRequestTeam(Player player, PlayerTeam team)
	{
	}

	// Token: 0x060003F9 RID: 1017 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPlayerRequestPositionSelect(Player player)
	{
	}

	// Token: 0x060003FA RID: 1018 RVA: 0x0000895D File Offset: 0x00006B5D
	protected virtual void OnPlayerRequestPosition(Player player, PlayerPosition position)
	{
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x0000B6DB File Offset: 0x000098DB
	protected virtual void OnPlayerRequestHandedness(Player player, PlayerHandedness handedness)
	{
		player.Handedness.Value = handedness;
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00026818 File Offset: 0x00024A18
	protected virtual void OnVoteAdded(Vote vote)
	{
		Player playerBySteamId = this.PlayerManager.GetPlayerBySteamId(vote.SteamId);
		List<Player> playersByTeams = this.PlayerManager.GetPlayersByTeams(vote.Teams, false);
		if (playerBySteamId && playersByTeams.Count > 0)
		{
			string text = StringUtils.WrapInTeamColor(playerBySteamId.Username.Value.ToString(), playerBySteamId.Team);
			this.ChatManager.Server_SendChatMessage(string.Concat(new string[]
			{
				vote.Title,
				" vote started by ",
				text,
				" (",
				vote.Description,
				")"
			}), "#e67e22", playersByTeams.ConvertAll<ulong>((Player p) => p.OwnerClientId).ToArray());
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00026900 File Offset: 0x00024B00
	protected virtual void OnVoteProgressed(Vote vote, string steamId, bool inFavour)
	{
		if (inFavour)
		{
			List<Player> playersByTeams = this.PlayerManager.GetPlayersByTeams(vote.Teams, false);
			if (playersByTeams.Count > 0)
			{
				this.ChatManager.Server_SendChatMessage(string.Format("{0} vote progressed {1}/{2}", vote.Title, vote.InFavourVotes, vote.RequiredVotes), "#e67e22", playersByTeams.ConvertAll<ulong>((Player p) => p.OwnerClientId).ToArray());
			}
		}
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x0002698C File Offset: 0x00024B8C
	protected virtual void OnVoteRemoved(Vote vote)
	{
		UnityEngine.Object playerBySteamId = this.PlayerManager.GetPlayerBySteamId(vote.SteamId);
		List<Player> playersByTeams = this.PlayerManager.GetPlayersByTeams(vote.Teams, false);
		if (playerBySteamId && playersByTeams.Count > 0)
		{
			string str = vote.Passed ? "passed" : "failed";
			this.ChatManager.Server_SendChatMessage(vote.Title + " vote " + str, "#e67e22", playersByTeams.ConvertAll<ulong>((Player p) => p.OwnerClientId).ToArray());
		}
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x0000B6E9 File Offset: 0x000098E9
	protected virtual void OnChatCommand(Player player, string command, string[] args)
	{
		this.ChatManager.Server_SendChatMessage("Unknown command", "#e74c3c", new ulong[]
		{
			player.OwnerClientId
		});
	}

	// Token: 0x06000400 RID: 1024 RVA: 0x00026A34 File Offset: 0x00024C34
	private void Event_Everyone_OnGameStateChanged(Dictionary<string, object> message)
	{
		GameState oldGameState = (GameState)message["oldGameState"];
		GameState newGameState = (GameState)message["newGameState"];
		this.OnGameStateChanged(oldGameState, newGameState);
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x00026A6C File Offset: 0x00024C6C
	private void Event_Everyone_OnPlayerAdded(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.OnPlayerJoined(player);
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x00026AA0 File Offset: 0x00024CA0
	private void Event_Everyone_OnPlayerRemoved(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.OnPlayerLeft(player);
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x00026AD4 File Offset: 0x00024CD4
	private void Event_Everyone_OnPlayerGameStateChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		PlayerGameState oldGameState = (PlayerGameState)message["oldGameState"];
		PlayerGameState newGameState = (PlayerGameState)message["newGameState"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.OnPlayerGameStateChanged(player, oldGameState, newGameState);
	}

	// Token: 0x06000404 RID: 1028 RVA: 0x00026B2C File Offset: 0x00024D2C
	private void Event_Everyone_OnPlayerPositionChanged(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		PlayerPosition oldPlayerPosition = (PlayerPosition)message["oldPlayerPosition"];
		PlayerPosition newPlayerPosition = (PlayerPosition)message["newPlayerPosition"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.OnPlayerPositionChanged(player, oldPlayerPosition, newPlayerPosition);
	}

	// Token: 0x06000405 RID: 1029 RVA: 0x00026B84 File Offset: 0x00024D84
	private void Event_Everyone_OnGoalScored(Dictionary<string, object> message)
	{
		PlayerTeam byTeam = (PlayerTeam)message["byTeam"];
		Player goalPlayer = (Player)message["goalPlayer"];
		Player assistPlayer = (Player)message["assistPlayer"];
		Player secondAssistPlayer = (Player)message["secondAssistPlayer"];
		Puck puck = (Puck)message["puck"];
		this.OnGoalScored(byTeam, goalPlayer, assistPlayer, secondAssistPlayer, puck);
	}

	// Token: 0x06000406 RID: 1030 RVA: 0x00026BF4 File Offset: 0x00024DF4
	private void Event_Server_OnPuckEnterGoal(Dictionary<string, object> message)
	{
		PlayerTeam team = (PlayerTeam)message["team"];
		Puck puck = (Puck)message["puck"];
		this.OnPuckEnterGoal(team, puck);
	}

	// Token: 0x06000407 RID: 1031 RVA: 0x00026C2C File Offset: 0x00024E2C
	private void Event_Server_OnPlayerRequestTeamSelect(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.OnPlayerRequestTeamSelect(player);
	}

	// Token: 0x06000408 RID: 1032 RVA: 0x00026C60 File Offset: 0x00024E60
	private void Event_Server_OnPlayerRequestTeam(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		PlayerTeam team = (PlayerTeam)message["team"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.OnPlayerRequestTeam(player, team);
	}

	// Token: 0x06000409 RID: 1033 RVA: 0x00026CA8 File Offset: 0x00024EA8
	private void Event_Server_OnPlayerRequestPositionSelect(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.OnPlayerRequestPositionSelect(player);
	}

	// Token: 0x0600040A RID: 1034 RVA: 0x00026CDC File Offset: 0x00024EDC
	private void Event_Server_OnPlayerRequestPosition(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		PlayerPosition position = (PlayerPosition)message["playerPosition"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.OnPlayerRequestPosition(player, position);
	}

	// Token: 0x0600040B RID: 1035 RVA: 0x00026D24 File Offset: 0x00024F24
	private void Event_Server_OnPlayerRequestHandedness(Dictionary<string, object> message)
	{
		Player player = (Player)message["player"];
		PlayerHandedness handedness = (PlayerHandedness)message["handedness"];
		if (player.IsReplay.Value)
		{
			return;
		}
		this.OnPlayerRequestHandedness(player, handedness);
	}

	// Token: 0x0600040C RID: 1036 RVA: 0x00026D6C File Offset: 0x00024F6C
	private void Event_Server_OnVoteAdded(Dictionary<string, object> message)
	{
		Vote vote = (Vote)message["vote"];
		this.OnVoteAdded(vote);
	}

	// Token: 0x0600040D RID: 1037 RVA: 0x00026D94 File Offset: 0x00024F94
	private void Event_Server_OnVoteProgressed(Dictionary<string, object> message)
	{
		Vote vote = (Vote)message["vote"];
		string steamId = (string)message["steamId"];
		bool inFavour = (bool)message["inFavour"];
		this.OnVoteProgressed(vote, steamId, inFavour);
	}

	// Token: 0x0600040E RID: 1038 RVA: 0x00026DE0 File Offset: 0x00024FE0
	private void Event_Server_OnVoteRemoved(Dictionary<string, object> message)
	{
		Vote vote = (Vote)message["vote"];
		this.OnVoteRemoved(vote);
	}

	// Token: 0x0600040F RID: 1039 RVA: 0x00026E08 File Offset: 0x00025008
	private void Event_Server_OnChatCommand(Dictionary<string, object> message)
	{
		ulong clientId = (ulong)message["clientId"];
		string command = (string)message["command"];
		string[] args = (string[])message["args"];
		Player playerByClientId = this.PlayerManager.GetPlayerByClientId(clientId);
		if (playerByClientId != null)
		{
			this.OnChatCommand(playerByClientId, command, args);
		}
	}

	// Token: 0x040002A5 RID: 677
	public Level Level;

	// Token: 0x040002A6 RID: 678
	public ServerManager ServerManager;

	// Token: 0x040002A7 RID: 679
	public GameManager GameManager;

	// Token: 0x040002A8 RID: 680
	public PlayerManager PlayerManager;

	// Token: 0x040002A9 RID: 681
	public PuckManager PuckManager;

	// Token: 0x040002AA RID: 682
	public ChatManager ChatManager;

	// Token: 0x040002AB RID: 683
	public ReplayManager ReplayManager;

	// Token: 0x040002AC RID: 684
	public VoteManager VoteManager;

	// Token: 0x040002AE RID: 686
	private string defaultConfigFilePath;

	// Token: 0x040002AF RID: 687
	private string configFilePathCliArgument;

	// Token: 0x040002B0 RID: 688
	private string configCliArgument;

	// Token: 0x040002B1 RID: 689
	private string configEnvVariable;
}
