using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Humanizer;

// Token: 0x020001AC RID: 428
public class UIPlayController : UIViewController<UIPlay>
{
	// Token: 0x06000CA4 RID: 3236 RVA: 0x00045200 File Offset: 0x00043400
	public override void Awake()
	{
		base.Awake();
		this.uiPlay = base.GetComponent<UIPlay>();
		EventManager.AddEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
		EventManager.AddEventListener("Event_OnPlayerPartyDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerPartyDataChanged));
		EventManager.AddEventListener("Event_OnPlayerGroupDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerGroupDataChanged));
		EventManager.AddEventListener("Event_OnPlayerMatchDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerMatchDataChanged));
		EventManager.AddEventListener("Event_OnPlayerStatisticsChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerStatisticsChanged));
	}

	// Token: 0x06000CA5 RID: 3237 RVA: 0x00012783 File Offset: 0x00010983
	private void Start()
	{
		this.uiPlay.SetThreeVsThreeButtonEnabled(false);
		this.uiPlay.SetFiveVsFiveButtonEnabled(false);
	}

	// Token: 0x06000CA6 RID: 3238 RVA: 0x00045290 File Offset: 0x00043490
	public override void OnDestroy()
	{
		EventManager.RemoveEventListener("Event_OnPlayerDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerDataChanged));
		EventManager.RemoveEventListener("Event_OnPlayerPartyDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerPartyDataChanged));
		EventManager.RemoveEventListener("Event_OnPlayerGroupDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerGroupDataChanged));
		EventManager.RemoveEventListener("Event_OnPlayerMatchDataChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerMatchDataChanged));
		EventManager.RemoveEventListener("Event_OnPlayerStatisticsChanged", new Action<Dictionary<string, object>>(this.Event_OnPlayerStatisticsChanged));
		base.OnDestroy();
	}

	// Token: 0x06000CA7 RID: 3239 RVA: 0x00045314 File Offset: 0x00043514
	private void UpdateMatchmakingPlayerButtonState()
	{
		PlayerData playerData = BackendManager.PlayerState.PlayerData;
		PlayerGroupData groupData = BackendManager.PlayerState.GroupData;
		PlayerMatchData matchData = BackendManager.PlayerState.MatchData;
		PlayerPartyData partyData = BackendManager.PlayerState.PartyData;
		bool flag = playerData != null && (partyData == null || (partyData != null && partyData.ownerSteamId == playerData.steamId)) && groupData == null && matchData == null;
		this.uiPlay.SetThreeVsThreeButtonEnabled(flag);
		this.uiPlay.SetFiveVsFiveButtonEnabled(flag);
	}

	// Token: 0x06000CA8 RID: 3240 RVA: 0x00045390 File Offset: 0x00043590
	private void Event_OnPlayerStatisticsChanged(Dictionary<string, object> message)
	{
		PlayerStatistics playerStatistics = (PlayerStatistics)message["newPlayerStatistics"];
		PoolStatistics poolStatistics = playerStatistics.matchmakingManager.pools.FirstOrDefault((PoolStatistics pool) => pool.id == "3v3");
		PoolStatistics poolStatistics2 = playerStatistics.matchmakingManager.pools.FirstOrDefault((PoolStatistics pool) => pool.id == "5v5");
		if (poolStatistics != null)
		{
			string arg = "NO DATA";
			if (poolStatistics.averageMatchingDuration != null)
			{
				arg = TimeSpan.FromMilliseconds(poolStatistics.averageMatchingDuration.Value).Humanize(2, CultureInfo.InvariantCulture, TimeUnit.Week, TimeUnit.Second, ", ", false);
			}
			this.uiPlay.SetThreeVsThreeButtonDescription(string.Format("IN QUEUE: {0}<br>EST. MATCHING TIME: {1}", poolStatistics.groupPlayers, arg));
		}
		if (poolStatistics2 != null)
		{
			string arg2 = "NO DATA";
			if (poolStatistics2.averageMatchingDuration != null)
			{
				arg2 = TimeSpan.FromMilliseconds(poolStatistics2.averageMatchingDuration.Value).Humanize(2, CultureInfo.InvariantCulture, TimeUnit.Week, TimeUnit.Second, ", ", false);
			}
			this.uiPlay.SetFiveVsFiveButtonDescription(string.Format("IN QUEUE: {0}<br>EST. MATCHING TIME: {1}", poolStatistics2.groupPlayers, arg2));
		}
		this.uiPlay.SetStatistics(playerStatistics.playerManager.players);
	}

	// Token: 0x06000CA9 RID: 3241 RVA: 0x0001279D File Offset: 0x0001099D
	private void Event_OnPlayerDataChanged(Dictionary<string, object> message)
	{
		this.UpdateMatchmakingPlayerButtonState();
	}

	// Token: 0x06000CAA RID: 3242 RVA: 0x0001279D File Offset: 0x0001099D
	private void Event_OnPlayerPartyDataChanged(Dictionary<string, object> message)
	{
		this.UpdateMatchmakingPlayerButtonState();
	}

	// Token: 0x06000CAB RID: 3243 RVA: 0x0001279D File Offset: 0x0001099D
	private void Event_OnPlayerGroupDataChanged(Dictionary<string, object> message)
	{
		this.UpdateMatchmakingPlayerButtonState();
	}

	// Token: 0x06000CAC RID: 3244 RVA: 0x0001279D File Offset: 0x0001099D
	private void Event_OnPlayerMatchDataChanged(Dictionary<string, object> message)
	{
		this.UpdateMatchmakingPlayerButtonState();
	}

	// Token: 0x04000797 RID: 1943
	private UIPlay uiPlay;
}
