using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;

// Token: 0x0200008F RID: 143
public static class BackendManager
{
	// Token: 0x17000079 RID: 121
	// (get) Token: 0x060004C3 RID: 1219 RVA: 0x0000BE41 File Offset: 0x0000A041
	// (set) Token: 0x060004C4 RID: 1220 RVA: 0x0000BE48 File Offset: 0x0000A048
	public static PlayerState PlayerState
	{
		get
		{
			return BackendManager.playerState;
		}
		set
		{
			if (BackendManager.playerState.Equals(value))
			{
				return;
			}
			PlayerState oldPlayerState = BackendManager.playerState;
			BackendManager.playerState = value;
			BackendManager.OnPlayerStateChanged(oldPlayerState, BackendManager.playerState);
		}
	}

	// Token: 0x1700007A RID: 122
	// (get) Token: 0x060004C5 RID: 1221 RVA: 0x0000BE6D File Offset: 0x0000A06D
	// (set) Token: 0x060004C6 RID: 1222 RVA: 0x0000BE74 File Offset: 0x0000A074
	public static ServerState ServerState
	{
		get
		{
			return BackendManager.serverState;
		}
		set
		{
			if (BackendManager.serverState.Equals(value))
			{
				return;
			}
			ServerState oldServerState = BackendManager.serverState;
			BackendManager.serverState = value;
			BackendManager.OnServerStateChanged(oldServerState, BackendManager.serverState);
		}
	}

	// Token: 0x1700007B RID: 123
	// (get) Token: 0x060004C7 RID: 1223 RVA: 0x0000BE99 File Offset: 0x0000A099
	// (set) Token: 0x060004C8 RID: 1224 RVA: 0x00029FDC File Offset: 0x000281DC
	public static TransactionState TransactionState
	{
		get
		{
			return BackendManager.transactionState;
		}
		set
		{
			if (BackendManager.transactionState.Equals(value))
			{
				return;
			}
			TransactionState transactionState = BackendManager.transactionState;
			BackendManager.transactionState = value;
			EventManager.TriggerEvent("Event_OnTransactionStateChanged", new Dictionary<string, object>
			{
				{
					"oldTransactionState",
					transactionState
				},
				{
					"newTransactionState",
					BackendManager.transactionState
				}
			});
		}
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0000BEA0 File Offset: 0x0000A0A0
	public static void Initialize()
	{
		BackendManagerController.Initialize();
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0000BEA7 File Offset: 0x0000A0A7
	public static void Dispose()
	{
		BackendManagerController.Dispose();
	}

	// Token: 0x060004CB RID: 1227 RVA: 0x0002A038 File Offset: 0x00028238
	public static void SetPlayerState(Dictionary<string, object> updates)
	{
		BackendManager.PlayerState = new PlayerState
		{
			AuthenticationPhase = (updates.ContainsKey("authenticationPhase") ? ((AuthenticationPhase)updates["authenticationPhase"]) : BackendManager.PlayerState.AuthenticationPhase),
			PlayerData = (updates.ContainsKey("playerData") ? ((PlayerData)updates["playerData"]) : BackendManager.PlayerState.PlayerData),
			PartyData = (updates.ContainsKey("partyData") ? ((PlayerPartyData)updates["partyData"]) : BackendManager.PlayerState.PartyData),
			GroupData = (updates.ContainsKey("groupData") ? ((PlayerGroupData)updates["groupData"]) : BackendManager.PlayerState.GroupData),
			MatchData = (updates.ContainsKey("matchData") ? ((PlayerMatchData)updates["matchData"]) : BackendManager.PlayerState.MatchData),
			PlayerStatistics = (updates.ContainsKey("playerStatistics") ? ((PlayerStatistics)updates["playerStatistics"]) : BackendManager.PlayerState.PlayerStatistics),
			Key = (updates.ContainsKey("key") ? ((string)updates["key"]) : BackendManager.PlayerState.Key)
		};
	}

	// Token: 0x060004CC RID: 1228 RVA: 0x0002A1A4 File Offset: 0x000283A4
	public static void SetServerState(Dictionary<string, object> updates)
	{
		BackendManager.ServerState = new ServerState
		{
			AuthenticationPhase = (updates.ContainsKey("authenticationPhase") ? ((AuthenticationPhase)updates["authenticationPhase"]) : BackendManager.ServerState.AuthenticationPhase),
			ServerData = (updates.ContainsKey("serverData") ? ((ServerData)updates["serverData"]) : BackendManager.ServerState.ServerData),
			MatchData = (updates.ContainsKey("matchData") ? ((ServerMatchData)updates["matchData"]) : BackendManager.ServerState.MatchData)
		};
	}

	// Token: 0x060004CD RID: 1229 RVA: 0x0002A250 File Offset: 0x00028450
	public static void SetTransactionState(Dictionary<string, object> updates)
	{
		BackendManager.TransactionState = new TransactionState
		{
			Phase = (updates.ContainsKey("phase") ? ((TransactionPhase)updates["phase"]) : BackendManager.TransactionState.Phase)
		};
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x0002A29C File Offset: 0x0002849C
	private static void OnPlayerStateChanged(PlayerState oldPlayerState, PlayerState newPlayerState)
	{
		EventManager.TriggerEvent("Event_OnPlayerStateChanged", new Dictionary<string, object>
		{
			{
				"oldPlayerState",
				oldPlayerState
			},
			{
				"newPlayerState",
				newPlayerState
			}
		});
		if (oldPlayerState.PlayerData != newPlayerState.PlayerData)
		{
			EventManager.TriggerEvent("Event_OnPlayerDataChanged", new Dictionary<string, object>
			{
				{
					"oldPlayerData",
					oldPlayerState.PlayerData
				},
				{
					"newPlayerData",
					newPlayerState.PlayerData
				}
			});
			if (newPlayerState.PlayerData != null)
			{
				bool flag = BackendUtils.GetActivePlayerDataBan(oldPlayerState.PlayerData) != null;
				bool flag2 = BackendUtils.GetActivePlayerDataMute(oldPlayerState.PlayerData) != null;
				bool flag3 = BackendUtils.GetActivePlayerDataCooldown(oldPlayerState.PlayerData) != null;
				PlayerBan activePlayerDataBan = BackendUtils.GetActivePlayerDataBan(newPlayerState.PlayerData);
				PlayerMute activePlayerDataMute = BackendUtils.GetActivePlayerDataMute(newPlayerState.PlayerData);
				PlayerCooldown activePlayerDataCooldown = BackendUtils.GetActivePlayerDataCooldown(newPlayerState.PlayerData);
				bool flag4 = activePlayerDataBan != null;
				bool flag5 = activePlayerDataMute != null;
				bool flag6 = activePlayerDataCooldown != null;
				if (flag4 && !flag)
				{
					EventManager.TriggerEvent("Event_OnPlayerBanned", new Dictionary<string, object>
					{
						{
							"reason",
							activePlayerDataBan.reason
						},
						{
							"expiresAt",
							activePlayerDataBan.expiresAt
						}
					});
				}
				else if (!flag4 && flag)
				{
					EventManager.TriggerEvent("Event_OnPlayerUnbanned", null);
				}
				if (flag5 && !flag2)
				{
					EventManager.TriggerEvent("Event_OnPlayerMuted", new Dictionary<string, object>
					{
						{
							"reason",
							activePlayerDataMute.reason
						},
						{
							"expiresAt",
							activePlayerDataMute.expiresAt
						}
					});
				}
				else if (!flag5 && flag2)
				{
					EventManager.TriggerEvent("Event_OnPlayerUnmuted", null);
				}
				if (flag6 && !flag3)
				{
					EventManager.TriggerEvent("Event_OnPlayerCooldown", new Dictionary<string, object>
					{
						{
							"expiresAt",
							activePlayerDataCooldown.expiresAt
						}
					});
				}
				else if (!flag6 && flag3)
				{
					EventManager.TriggerEvent("Event_OnPlayerCooldownExpired", null);
				}
			}
		}
		if (oldPlayerState.PartyData != newPlayerState.PartyData)
		{
			EventManager.TriggerEvent("Event_OnPlayerPartyDataChanged", new Dictionary<string, object>
			{
				{
					"oldPlayerPartyData",
					oldPlayerState.PartyData
				},
				{
					"newPlayerPartyData",
					newPlayerState.PartyData
				}
			});
		}
		if (oldPlayerState.GroupData != newPlayerState.GroupData)
		{
			EventManager.TriggerEvent("Event_OnPlayerGroupDataChanged", new Dictionary<string, object>
			{
				{
					"oldPlayerGroupData",
					oldPlayerState.GroupData
				},
				{
					"newPlayerGroupData",
					newPlayerState.GroupData
				}
			});
			bool flag7 = oldPlayerState.GroupData == null && newPlayerState.GroupData != null;
			bool flag8 = oldPlayerState.GroupData != null && newPlayerState.GroupData == null;
			if (flag7)
			{
				BackendManager.StartMatchingTicker(0);
			}
			else if (flag8)
			{
				BackendManager.StopMatchingTicker();
			}
		}
		if (oldPlayerState.MatchData != newPlayerState.MatchData)
		{
			EventManager.TriggerEvent("Event_OnPlayerMatchDataChanged", new Dictionary<string, object>
			{
				{
					"oldPlayerMatchData",
					oldPlayerState.MatchData
				},
				{
					"newPlayerMatchData",
					newPlayerState.MatchData
				}
			});
			PlayerMatchData matchData = oldPlayerState.MatchData;
			bool flag9;
			if (matchData == null || matchData.JoinTimeoutRemainingSeconds == null)
			{
				PlayerMatchData matchData2 = newPlayerState.MatchData;
				flag9 = (matchData2 != null && matchData2.JoinTimeoutRemainingSeconds != null);
			}
			else
			{
				flag9 = false;
			}
			PlayerMatchData matchData3 = oldPlayerState.MatchData;
			bool flag10;
			if (matchData3 != null && matchData3.JoinTimeoutRemainingSeconds != null)
			{
				PlayerMatchData matchData4 = newPlayerState.MatchData;
				flag10 = (matchData4 == null || matchData4.JoinTimeoutRemainingSeconds == null);
			}
			else
			{
				flag10 = false;
			}
			bool flag11 = flag10;
			if (flag9)
			{
				BackendManager.StartMatchJoinTimeoutTicker(newPlayerState.MatchData.JoinTimeoutRemainingSeconds.Value);
			}
			else if (flag11)
			{
				BackendManager.StopMatchJoinTimeoutTicker();
			}
		}
		if (oldPlayerState.PlayerStatistics != newPlayerState.PlayerStatistics)
		{
			EventManager.TriggerEvent("Event_OnPlayerStatisticsChanged", new Dictionary<string, object>
			{
				{
					"oldPlayerStatistics",
					oldPlayerState.PlayerStatistics
				},
				{
					"newPlayerStatistics",
					newPlayerState.PlayerStatistics
				}
			});
		}
		if (oldPlayerState.Key != newPlayerState.Key)
		{
			EventManager.TriggerEvent("Event_OnPlayerKeyChanged", new Dictionary<string, object>
			{
				{
					"oldKey",
					oldPlayerState.Key
				},
				{
					"newKey",
					newPlayerState.Key
				}
			});
		}
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0002A6A0 File Offset: 0x000288A0
	private static void OnServerStateChanged(ServerState oldServerState, ServerState newServerState)
	{
		EventManager.TriggerEvent("Event_OnServerStateChanged", new Dictionary<string, object>
		{
			{
				"oldServerState",
				oldServerState
			},
			{
				"newServerState",
				newServerState
			}
		});
		if (oldServerState.ServerData != newServerState.ServerData)
		{
			EventManager.TriggerEvent("Event_OnServerDataChanged", new Dictionary<string, object>
			{
				{
					"oldServerData",
					oldServerState.ServerData
				},
				{
					"newServerData",
					newServerState.ServerData
				}
			});
		}
		if (oldServerState.MatchData != newServerState.MatchData)
		{
			EventManager.TriggerEvent("Event_OnServerMatchDataChanged", new Dictionary<string, object>
			{
				{
					"oldServerMatchData",
					oldServerState.MatchData
				},
				{
					"newServerMatchData",
					newServerState.MatchData
				}
			});
		}
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0002A75C File Offset: 0x0002895C
	private static void StartMatchingTicker(int startingTick)
	{
		EventManager.TriggerEvent("Event_OnMatchingTickerStarted", new Dictionary<string, object>
		{
			{
				"startingTick",
				startingTick
			}
		});
		BackendManager.matchingTick = startingTick;
		Tween tween = BackendManager.matchingTickerTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		BackendManager.matchingTickerTween = DOVirtual.DelayedCall(1f, new TweenCallback(BackendManager.<StartMatchingTicker>g__Tick|23_0), true).SetLoops(-1);
	}

	// Token: 0x060004D1 RID: 1233 RVA: 0x0000BEAE File Offset: 0x0000A0AE
	private static void StopMatchingTicker()
	{
		BackendManager.matchingTick = 0;
		Tween tween = BackendManager.matchingTickerTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		BackendManager.matchingTickerTween = null;
		EventManager.TriggerEvent("Event_OnMatchingTickerStopped", null);
	}

	// Token: 0x060004D2 RID: 1234 RVA: 0x0002A7C4 File Offset: 0x000289C4
	private static void StartMatchJoinTimeoutTicker(int startingTick)
	{
		EventManager.TriggerEvent("Event_OnMatchJoinTimeoutTickerStarted", new Dictionary<string, object>
		{
			{
				"startingTick",
				startingTick
			}
		});
		BackendManager.joinTimeoutTick = startingTick;
		Tween tween = BackendManager.matchJoinTimeoutTickerTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		BackendManager.matchJoinTimeoutTickerTween = DOVirtual.DelayedCall(1f, new TweenCallback(BackendManager.<StartMatchJoinTimeoutTicker>g__Tick|25_0), true).SetLoops(-1);
	}

	// Token: 0x060004D3 RID: 1235 RVA: 0x0000BED8 File Offset: 0x0000A0D8
	private static void StopMatchJoinTimeoutTicker()
	{
		BackendManager.joinTimeoutTick = 0;
		Tween tween = BackendManager.matchJoinTimeoutTickerTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		BackendManager.matchJoinTimeoutTickerTween = null;
		EventManager.TriggerEvent("Event_OnMatchJoinTimeoutTickerStopped", null);
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0000BF02 File Offset: 0x0000A102
	[CompilerGenerated]
	internal static void <StartMatchingTicker>g__Tick|23_0()
	{
		BackendManager.matchingTick++;
		EventManager.TriggerEvent("Event_OnMatchingTickerTick", new Dictionary<string, object>
		{
			{
				"tick",
				BackendManager.matchingTick
			}
		});
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0000BF34 File Offset: 0x0000A134
	[CompilerGenerated]
	internal static void <StartMatchJoinTimeoutTicker>g__Tick|25_0()
	{
		if (BackendManager.joinTimeoutTick <= 0)
		{
			BackendManager.StopMatchJoinTimeoutTicker();
			return;
		}
		BackendManager.joinTimeoutTick--;
		EventManager.TriggerEvent("Event_OnMatchJoinTimeoutTickerTick", new Dictionary<string, object>
		{
			{
				"tick",
				BackendManager.joinTimeoutTick
			}
		});
	}

	// Token: 0x040002F4 RID: 756
	private static PlayerState playerState;

	// Token: 0x040002F5 RID: 757
	private static ServerState serverState;

	// Token: 0x040002F6 RID: 758
	private static TransactionState transactionState;

	// Token: 0x040002F7 RID: 759
	private static Tween matchingTickerTween;

	// Token: 0x040002F8 RID: 760
	private static int matchingTick;

	// Token: 0x040002F9 RID: 761
	private static Tween matchJoinTimeoutTickerTween;

	// Token: 0x040002FA RID: 762
	private static int joinTimeoutTick;
}
