using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

// Token: 0x02000078 RID: 120
public class MatchableGameMode<TConfig> : StandardGameMode<!0> where TConfig : StandardGameModeConfig, new()
{
	// Token: 0x17000068 RID: 104
	// (get) Token: 0x0600041D RID: 1053 RVA: 0x0000B7B4 File Offset: 0x000099B4
	protected MatchData matchData
	{
		get
		{
			return BackendManager.ServerState.MatchData;
		}
	}

	// Token: 0x17000069 RID: 105
	// (get) Token: 0x0600041E RID: 1054 RVA: 0x0000B7C0 File Offset: 0x000099C0
	protected bool isMatch
	{
		get
		{
			return this.matchData != null;
		}
	}

	// Token: 0x0600041F RID: 1055 RVA: 0x0000B7CB File Offset: 0x000099CB
	public MatchableGameMode(string defaultConfigFilePath, string configFilePathCliArgument = null, string configCliArgument = null, string configEnvVariable = null) : base(defaultConfigFilePath, configFilePathCliArgument, configCliArgument, configEnvVariable)
	{
	}

	// Token: 0x06000420 RID: 1056 RVA: 0x00026FE0 File Offset: 0x000251E0
	public override bool Initialize(Level level, ServerManager serverManager, GameManager gameManager, PlayerManager playerManager, PuckManager puckManager, ChatManager chatManager, ReplayManager replayManager, VoteManager voteManager)
	{
		if (!base.Initialize(level, serverManager, gameManager, playerManager, puckManager, chatManager, replayManager, voteManager))
		{
			return false;
		}
		if (this.isMatch)
		{
			this.StartMatch();
		}
		return true;
	}

	// Token: 0x06000421 RID: 1057 RVA: 0x0000B7EE File Offset: 0x000099EE
	protected override void SubscribeEvents()
	{
		base.SubscribeEvents();
		EventManager.AddEventListener("Event_OnServerMatchDataChanged", new Action<Dictionary<string, object>>(this.Event_OnServerMatchDataChanged));
	}

	// Token: 0x06000422 RID: 1058 RVA: 0x0000B80C File Offset: 0x00009A0C
	protected override void UnsubscribeEvents()
	{
		base.UnsubscribeEvents();
		EventManager.RemoveEventListener("Event_OnServerMatchDataChanged", new Action<Dictionary<string, object>>(this.Event_OnServerMatchDataChanged));
	}

	// Token: 0x06000423 RID: 1059 RVA: 0x0000B82A File Offset: 0x00009A2A
	private void StartMatch()
	{
		if (this.isMatchStarted)
		{
			return;
		}
		this.isMatchStarted = true;
		this.OnMatchStarted();
	}

	// Token: 0x06000424 RID: 1060 RVA: 0x0000B842 File Offset: 0x00009A42
	private void EndMatch()
	{
		if (!this.isMatchStarted)
		{
			return;
		}
		this.isMatchStarted = false;
		this.OnMatchEnded(this.gameResult);
	}

	// Token: 0x06000425 RID: 1061 RVA: 0x0000B860 File Offset: 0x00009A60
	private void CancelMatch()
	{
		if (!this.isMatchStarted)
		{
			return;
		}
		this.isMatchStarted = false;
		this.OnMatchCancelled();
	}

	// Token: 0x06000426 RID: 1062 RVA: 0x00027014 File Offset: 0x00025214
	private void IssueCooldown(string steamId)
	{
		if (!this.isMatchStarted)
		{
			return;
		}
		WebSocketManager.Emit("serverMatchIssueCooldown", new Dictionary<string, object>
		{
			{
				"steamId",
				steamId
			}
		}, null);
		MatchPlayer matchPlayerBySteamId = this.matchData.GetMatchPlayerBySteamId(steamId);
		if (matchPlayerBySteamId != null)
		{
			this.ChatManager.Server_BroadcastChatMessage(matchPlayerBySteamId.username + " has been issued a matchmaking cooldown", "#b8b8b8");
		}
	}

	// Token: 0x06000427 RID: 1063 RVA: 0x00027078 File Offset: 0x00025278
	private void StartAbandonmentTimeout(string steamId, float timeout)
	{
		if (this.steamIdAbandonmentTweenMap.ContainsKey(steamId))
		{
			return;
		}
		Tween value = DOVirtual.DelayedCall(timeout, delegate
		{
			this.steamIdAbandonmentTweenMap.Remove(steamId);
			this.OnSteamIdAbandonedMatch(steamId);
		}, true);
		this.steamIdAbandonmentTweenMap.Add(steamId, value);
	}

	// Token: 0x06000428 RID: 1064 RVA: 0x0000B878 File Offset: 0x00009A78
	private void StopAbandonmentTimeout(string steamId)
	{
		if (!this.steamIdAbandonmentTweenMap.ContainsKey(steamId))
		{
			return;
		}
		this.steamIdAbandonmentTweenMap[steamId].Kill(false);
		this.steamIdAbandonmentTweenMap.Remove(steamId);
	}

	// Token: 0x06000429 RID: 1065 RVA: 0x000270D4 File Offset: 0x000252D4
	private void ClearAbandonmentTimeouts()
	{
		foreach (Tween t in this.steamIdAbandonmentTweenMap.Values)
		{
			t.Kill(false);
		}
		this.steamIdAbandonmentTweenMap.Clear();
	}

	// Token: 0x0600042A RID: 1066 RVA: 0x0000B8A8 File Offset: 0x00009AA8
	protected virtual void OnMatchStarted()
	{
		WebSocketManager.Emit("serverMatchStart", null, null);
		this.UpdatePlayerResult(this.matchData.SteamIds);
		this.ServerManager.WhitelistManager.AddWhitelistedSteamIds(this.matchData.SteamIds);
	}

	// Token: 0x0600042B RID: 1067 RVA: 0x0000B8E2 File Offset: 0x00009AE2
	protected virtual void OnMatchEnded(GameResult gameResult)
	{
		WebSocketManager.Emit("serverMatchEnd", new Dictionary<string, object>
		{
			{
				"gameResult",
				gameResult
			}
		}, null);
		this.ClearAbandonmentTimeouts();
	}

	// Token: 0x0600042C RID: 1068 RVA: 0x0000B906 File Offset: 0x00009B06
	protected virtual void OnMatchCancelled()
	{
		WebSocketManager.Emit("serverMatchCancel", null, null);
		this.ClearAbandonmentTimeouts();
	}

	// Token: 0x0600042D RID: 1069 RVA: 0x00027138 File Offset: 0x00025338
	protected virtual void OnSteamIdAbandonedMatch(string steamId)
	{
		this.abandonedSteamIds.Add(steamId);
		this.IssueCooldown(steamId);
		string[] array = this.matchData.HomeSteamIds.Except(this.abandonedSteamIds).ToArray<string>();
		string[] array2 = this.matchData.AwaySteamIds.Except(this.abandonedSteamIds).ToArray<string>();
		if (array.Length == 0)
		{
			this.ForfeitGame(PlayerTeam.Blue);
			return;
		}
		if (array2.Length == 0)
		{
			this.ForfeitGame(PlayerTeam.Red);
		}
	}

	// Token: 0x0600042E RID: 1070 RVA: 0x000271A8 File Offset: 0x000253A8
	protected override void OnWarmupTimedOut()
	{
		if (this.isMatchStarted)
		{
			string[] second = (from p in this.PlayerManager.GetPlayers(false)
			select p.SteamId.Value.ToString()).ToArray<string>();
			string[] array = this.matchData.SteamIds.Except(second).ToArray<string>();
			if (array.Length != 0)
			{
				this.ChatManager.Server_BroadcastChatMessage("Cancelling match due to players failing to join in time", "#b8b8b8");
				foreach (string steamId in array)
				{
					this.IssueCooldown(steamId);
				}
				this.CancelMatch();
				return;
			}
		}
		base.OnWarmupTimedOut();
	}

	// Token: 0x0600042F RID: 1071 RVA: 0x0000B91A File Offset: 0x00009B1A
	protected override void OnGameOverEnded()
	{
		base.OnGameOverEnded();
		if (this.isMatch)
		{
			this.EndMatch();
		}
	}

	// Token: 0x06000430 RID: 1072 RVA: 0x00027250 File Offset: 0x00025450
	protected override void OnPlayerJoined(Player player)
	{
		base.OnPlayerJoined(player);
		if (this.isMatchStarted)
		{
			this.StopAbandonmentTimeout(player.SteamId.Value.ToString());
		}
	}

	// Token: 0x06000431 RID: 1073 RVA: 0x0002728C File Offset: 0x0002548C
	protected override void OnPlayerLeft(Player player)
	{
		base.OnPlayerLeft(player);
		if (this.isMatchStarted && base.isGameInProgress)
		{
			this.StartAbandonmentTimeout(player.SteamId.Value.ToString(), 120f);
			this.ChatManager.Server_BroadcastChatMessage(string.Format("{0} has {1} seconds to reconnect before receiving a matchmaking cooldown", player.Username.Value, 120), "#b8b8b8");
		}
	}

	// Token: 0x06000432 RID: 1074 RVA: 0x00027308 File Offset: 0x00025508
	private void Event_OnServerMatchDataChanged(Dictionary<string, object> message)
	{
		bool flag = (MatchData)message["oldServerMatchData"] != null;
		MatchData matchData = (MatchData)message["newServerMatchData"];
		if (!flag && matchData != null)
		{
			this.StartMatch();
		}
	}

	// Token: 0x040002B8 RID: 696
	protected bool isMatchStarted;

	// Token: 0x040002B9 RID: 697
	private Dictionary<string, Tween> steamIdAbandonmentTweenMap = new Dictionary<string, Tween>();

	// Token: 0x040002BA RID: 698
	private List<string> abandonedSteamIds = new List<string>();
}
