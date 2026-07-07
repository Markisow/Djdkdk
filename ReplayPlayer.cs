using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Unity.Netcode;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class ReplayPlayer : MonoBehaviour
{
	// Token: 0x170000D4 RID: 212
	// (get) Token: 0x0600076D RID: 1901 RVA: 0x0000DBDC File Offset: 0x0000BDDC
	[HideInInspector]
	public float TickInterval
	{
		get
		{
			return 1f / (float)this.TickRate;
		}
	}

	// Token: 0x0600076E RID: 1902 RVA: 0x00032268 File Offset: 0x00030468
	private void Update()
	{
		if (NetworkManager.Singleton == null || !NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!this.IsReplaying)
		{
			return;
		}
		this.tickAccumulator += Time.deltaTime * (float)this.TickRate;
		if (this.tickAccumulator >= 1f)
		{
			while (this.tickAccumulator >= 1f)
			{
				this.tickAccumulator -= 1f;
			}
			this.Server_Tick(this.Tick, false);
			this.Tick++;
		}
	}

	// Token: 0x0600076F RID: 1903 RVA: 0x000322FC File Offset: 0x000304FC
	public void Server_StartReplay(SortedList<int, List<ValueTuple<string, object>>> tickEventMap, int tickRate, int fromTick = 0)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (this.IsReplaying)
		{
			return;
		}
		if (tickEventMap.Keys.Count == 0)
		{
			return;
		}
		int num = tickEventMap.Keys.Min();
		int max = tickEventMap.Keys.Max();
		fromTick = Mathf.Clamp(fromTick, num, max);
		this.EventMap = tickEventMap;
		this.TickRate = tickRate;
		int i;
		for (i = num; i < fromTick; i++)
		{
			this.Server_Tick(i, true);
			this.EventMap.Remove(i);
		}
		this.Tick = i;
		List<ValueTuple<string, object>> list = this.Server_GetSkippedEvents();
		if (list.Count > 0)
		{
			this.Tick--;
			this.EventMap.Add(this.Tick, list);
		}
		this.IsReplaying = true;
	}

	// Token: 0x06000770 RID: 1904 RVA: 0x0000DBEB File Offset: 0x0000BDEB
	public void Server_StopReplay()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!this.IsReplaying)
		{
			return;
		}
		this.IsReplaying = false;
		this.Server_Dispose();
	}

	// Token: 0x06000771 RID: 1905 RVA: 0x000323BC File Offset: 0x000305BC
	private void Server_Tick(int tick, bool skip = false)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (tick > this.EventMap.Keys.Max())
		{
			this.Server_StopReplay();
			return;
		}
		if (!this.EventMap.ContainsKey(tick))
		{
			return;
		}
		foreach (ValueTuple<string, object> valueTuple in this.EventMap[tick])
		{
			string item = valueTuple.Item1;
			object item2 = valueTuple.Item2;
			if (skip)
			{
				this.Server_SkipEvent(item, item2);
			}
			else
			{
				this.Server_ReplayEvent(item, item2);
			}
		}
	}

	// Token: 0x06000772 RID: 1906 RVA: 0x00032468 File Offset: 0x00030668
	private List<ValueTuple<string, object>> Server_GetSkippedEvents()
	{
		List<ValueTuple<string, object>> list = new List<ValueTuple<string, object>>();
		foreach (ReplayPlayerSpawned replayPlayerSpawned in this.replayPlayerSpawnedList)
		{
			list.Add(new ValueTuple<string, object>("PlayerSpawned", replayPlayerSpawned));
		}
		foreach (ReplayPlayerBodySpawned replayPlayerBodySpawned in this.replayPlayerBodySpawnedList)
		{
			list.Add(new ValueTuple<string, object>("PlayerBodySpawned", replayPlayerBodySpawned));
		}
		foreach (ReplayStickSpawned replayStickSpawned in this.replayStickSpawnedList)
		{
			list.Add(new ValueTuple<string, object>("StickSpawned", replayStickSpawned));
		}
		foreach (ReplayPuckSpawned replayPuckSpawned in this.replayPuckSpawnedList)
		{
			list.Add(new ValueTuple<string, object>("PuckSpawned", replayPuckSpawned));
		}
		return list;
	}

	// Token: 0x06000773 RID: 1907 RVA: 0x000325CC File Offset: 0x000307CC
	private void Server_SkipEvent(string eventName, object eventData)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		uint num = <PrivateImplementationDetails>.ComputeStringHash(eventName);
		if (num <= 1238133331U)
		{
			if (num <= 541464130U)
			{
				if (num != 151785130U)
				{
					if (num != 541464130U)
					{
						return;
					}
					if (!(eventName == "PlayerBodySpawned"))
					{
						return;
					}
					ReplayPlayerBodySpawned item = (ReplayPlayerBodySpawned)eventData;
					this.replayPlayerBodySpawnedList.Add(item);
					return;
				}
				else
				{
					if (!(eventName == "PuckSpawned"))
					{
						return;
					}
					ReplayPuckSpawned item2 = (ReplayPuckSpawned)eventData;
					this.replayPuckSpawnedList.Add(item2);
					return;
				}
			}
			else if (num != 921486523U)
			{
				if (num != 1238133331U)
				{
					return;
				}
				if (!(eventName == "StickSpawned"))
				{
					return;
				}
				ReplayStickSpawned item3 = (ReplayStickSpawned)eventData;
				this.replayStickSpawnedList.Add(item3);
				return;
			}
			else
			{
				if (!(eventName == "PuckDespawned"))
				{
					return;
				}
				ReplayPuckDespawned puckDespawned = (ReplayPuckDespawned)eventData;
				this.replayPuckSpawnedList.RemoveAll((ReplayPuckSpawned x) => x.NetworkObjectId == puckDespawned.NetworkObjectId);
				return;
			}
		}
		else if (num <= 1946187694U)
		{
			if (num != 1769383370U)
			{
				if (num != 1946187694U)
				{
					return;
				}
				if (!(eventName == "PlayerSpawned"))
				{
					return;
				}
				ReplayPlayerSpawned item4 = (ReplayPlayerSpawned)eventData;
				this.replayPlayerSpawnedList.Add(item4);
				return;
			}
			else
			{
				if (!(eventName == "StickDespawned"))
				{
					return;
				}
				ReplayStickDespawned stickDespawned = (ReplayStickDespawned)eventData;
				this.replayStickSpawnedList.RemoveAll((ReplayStickSpawned x) => x.OwnerClientId == stickDespawned.OwnerClientId);
				return;
			}
		}
		else if (num != 3342882255U)
		{
			if (num != 3490707091U)
			{
				return;
			}
			if (!(eventName == "PlayerBodyDespawned"))
			{
				return;
			}
			ReplayPlayerBodyDespawned playerBodyDespawned = (ReplayPlayerBodyDespawned)eventData;
			this.replayPlayerBodySpawnedList.RemoveAll((ReplayPlayerBodySpawned x) => x.OwnerClientId == playerBodyDespawned.OwnerClientId);
			return;
		}
		else
		{
			if (!(eventName == "PlayerDespawned"))
			{
				return;
			}
			ReplayPlayerDespawned playerDespawned = (ReplayPlayerDespawned)eventData;
			this.replayPlayerSpawnedList.RemoveAll((ReplayPlayerSpawned x) => x.OwnerClientId == playerDespawned.OwnerClientId);
			return;
		}
	}

	// Token: 0x06000774 RID: 1908 RVA: 0x000327DC File Offset: 0x000309DC
	private void Server_ReplayEvent(string eventName, object eventData)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		uint num = <PrivateImplementationDetails>.ComputeStringHash(eventName);
		if (num <= 1238133331U)
		{
			if (num <= 541464130U)
			{
				if (num != 151785130U)
				{
					if (num != 468072804U)
					{
						if (num != 541464130U)
						{
							return;
						}
						if (!(eventName == "PlayerBodySpawned"))
						{
							return;
						}
						ReplayPlayerBodySpawned replayPlayerBodySpawned = (ReplayPlayerBodySpawned)eventData;
						Player replayPlayerByClientId = MonoBehaviourSingleton<PlayerManager>.Instance.GetReplayPlayerByClientId(replayPlayerBodySpawned.OwnerClientId);
						if (!replayPlayerByClientId)
						{
							return;
						}
						replayPlayerByClientId.GameState.Value = replayPlayerBodySpawned.GameState;
						replayPlayerByClientId.CustomizationState.Value = replayPlayerBodySpawned.CustomizationState;
						replayPlayerByClientId.Username.Value = replayPlayerBodySpawned.Username;
						replayPlayerByClientId.Number.Value = replayPlayerBodySpawned.Number;
						replayPlayerByClientId.Server_SpawnPlayerBody(replayPlayerBodySpawned.Position, replayPlayerBodySpawned.Rotation, replayPlayerByClientId.Role);
						return;
					}
					else
					{
						if (!(eventName == "PlayerInput"))
						{
							return;
						}
						ReplayPlayerInput replayPlayerInput = (ReplayPlayerInput)eventData;
						Player replayPlayerByClientId2 = MonoBehaviourSingleton<PlayerManager>.Instance.GetReplayPlayerByClientId(replayPlayerInput.OwnerClientId);
						if (!replayPlayerByClientId2)
						{
							return;
						}
						replayPlayerByClientId2.PlayerInput.LookAngleInput.ClientValue = replayPlayerInput.LookAngleInput;
						replayPlayerByClientId2.PlayerInput.BladeAngleInput.ClientValue = replayPlayerInput.BladeAngleInput;
						replayPlayerByClientId2.PlayerInput.TrackInput.ClientValue = replayPlayerInput.TrackInput;
						replayPlayerByClientId2.PlayerInput.LookInput.ClientValue = replayPlayerInput.LookInput;
						return;
					}
				}
				else
				{
					if (!(eventName == "PuckSpawned"))
					{
						return;
					}
					ReplayPuckSpawned replayPuckSpawned = (ReplayPuckSpawned)eventData;
					Puck puck = MonoBehaviourSingleton<PuckManager>.Instance.Server_SpawnPuck(replayPuckSpawned.Position, replayPuckSpawned.Rotation, true);
					this.replayPuckNetworkObjectIdMap.Add(replayPuckSpawned.NetworkObjectId, puck.NetworkObjectId);
					puck.transform.position = replayPuckSpawned.Position;
					puck.transform.rotation = replayPuckSpawned.Rotation;
					return;
				}
			}
			else if (num != 551687134U)
			{
				if (num != 921486523U)
				{
					if (num != 1238133331U)
					{
						return;
					}
					if (!(eventName == "StickSpawned"))
					{
						return;
					}
					ReplayStickSpawned replayStickSpawned = (ReplayStickSpawned)eventData;
					Player replayPlayerByClientId3 = MonoBehaviourSingleton<PlayerManager>.Instance.GetReplayPlayerByClientId(replayStickSpawned.OwnerClientId);
					if (!replayPlayerByClientId3)
					{
						return;
					}
					replayPlayerByClientId3.Server_SpawnStick(replayStickSpawned.Position, replayStickSpawned.Rotation, replayPlayerByClientId3.Role);
					return;
				}
				else
				{
					if (!(eventName == "PuckDespawned"))
					{
						return;
					}
					ReplayPuckDespawned replayPuckDespawned = (ReplayPuckDespawned)eventData;
					if (!this.replayPuckNetworkObjectIdMap.ContainsKey(replayPuckDespawned.NetworkObjectId))
					{
						return;
					}
					Puck replayPuckByNetworkObjectId = MonoBehaviourSingleton<PuckManager>.Instance.GetReplayPuckByNetworkObjectId(this.replayPuckNetworkObjectIdMap[replayPuckDespawned.NetworkObjectId]);
					if (!replayPuckByNetworkObjectId)
					{
						return;
					}
					MonoBehaviourSingleton<PuckManager>.Instance.Server_DespawnPuck(replayPuckByNetworkObjectId);
					return;
				}
			}
			else
			{
				if (!(eventName == "StickMove"))
				{
					return;
				}
				ReplayStickMove replayStickMove = (ReplayStickMove)eventData;
				Player replayPlayerByClientId4 = MonoBehaviourSingleton<PlayerManager>.Instance.GetReplayPlayerByClientId(replayStickMove.OwnerClientId);
				if (!replayPlayerByClientId4)
				{
					return;
				}
				if (!replayPlayerByClientId4.Stick)
				{
					return;
				}
				replayPlayerByClientId4.Stick.transform.DOKill(true);
				replayPlayerByClientId4.Stick.transform.DOMove(replayStickMove.Position, this.TickInterval, false).SetEase(Ease.Linear);
				replayPlayerByClientId4.Stick.transform.DORotateQuaternion(replayStickMove.Rotation, this.TickInterval).SetEase(Ease.Linear);
				return;
			}
		}
		else if (num <= 2284572537U)
		{
			if (num != 1769383370U)
			{
				if (num != 1946187694U)
				{
					if (num != 2284572537U)
					{
						return;
					}
					if (!(eventName == "PlayerBodyMove"))
					{
						return;
					}
					ReplayPlayerBodyMove replayPlayerBodyMove = (ReplayPlayerBodyMove)eventData;
					Player replayPlayerByClientId5 = MonoBehaviourSingleton<PlayerManager>.Instance.GetReplayPlayerByClientId(replayPlayerBodyMove.OwnerClientId);
					if (!replayPlayerByClientId5)
					{
						return;
					}
					if (!replayPlayerByClientId5.PlayerBody)
					{
						return;
					}
					replayPlayerByClientId5.PlayerBody.transform.DOKill(true);
					replayPlayerByClientId5.PlayerBody.transform.DOMove(replayPlayerBodyMove.Position, this.TickInterval, false).SetEase(Ease.Linear);
					replayPlayerByClientId5.PlayerBody.transform.DORotateQuaternion(replayPlayerBodyMove.Rotation, this.TickInterval).SetEase(Ease.Linear);
					replayPlayerByClientId5.PlayerBody.Stamina.Value = replayPlayerBodyMove.Stamina;
					replayPlayerByClientId5.PlayerBody.Speed.Value = replayPlayerBodyMove.Speed;
					replayPlayerByClientId5.PlayerBody.IsSprinting.Value = replayPlayerBodyMove.IsSprinting;
					replayPlayerByClientId5.PlayerBody.IsSliding.Value = replayPlayerBodyMove.IsSliding;
					replayPlayerByClientId5.PlayerBody.IsStopping.Value = replayPlayerBodyMove.IsStopping;
					replayPlayerByClientId5.PlayerBody.IsExtendedLeft.Value = replayPlayerBodyMove.IsExtendedLeft;
					replayPlayerByClientId5.PlayerBody.IsExtendedRight.Value = replayPlayerBodyMove.IsExtendedRight;
					return;
				}
				else
				{
					if (!(eventName == "PlayerSpawned"))
					{
						return;
					}
					ReplayPlayerSpawned replayPlayerSpawned = (ReplayPlayerSpawned)eventData;
					MonoBehaviourSingleton<PlayerManager>.Instance.Server_SpawnPlayer(replayPlayerSpawned.OwnerClientId, replayPlayerSpawned.GameState, replayPlayerSpawned.CustomizationState, replayPlayerSpawned.Handedness, replayPlayerSpawned.SteamId.ToString(), replayPlayerSpawned.Username.ToString(), replayPlayerSpawned.Number, replayPlayerSpawned.PatreonLevel, replayPlayerSpawned.AdminLevel, replayPlayerSpawned.IsMuted, true);
					return;
				}
			}
			else
			{
				if (!(eventName == "StickDespawned"))
				{
					return;
				}
				ReplayStickDespawned replayStickDespawned = (ReplayStickDespawned)eventData;
				Player replayPlayerByClientId6 = MonoBehaviourSingleton<PlayerManager>.Instance.GetReplayPlayerByClientId(replayStickDespawned.OwnerClientId);
				if (!replayPlayerByClientId6)
				{
					return;
				}
				if (!replayPlayerByClientId6.Stick)
				{
					return;
				}
				replayPlayerByClientId6.Server_DespawnStick();
				return;
			}
		}
		else if (num != 3342882255U)
		{
			if (num != 3490707091U)
			{
				if (num != 4197733569U)
				{
					return;
				}
				if (!(eventName == "PuckMove"))
				{
					return;
				}
				ReplayPuckMove replayPuckMove = (ReplayPuckMove)eventData;
				if (!this.replayPuckNetworkObjectIdMap.ContainsKey(replayPuckMove.NetworkObjectId))
				{
					return;
				}
				Puck replayPuckByNetworkObjectId2 = MonoBehaviourSingleton<PuckManager>.Instance.GetReplayPuckByNetworkObjectId(this.replayPuckNetworkObjectIdMap[replayPuckMove.NetworkObjectId]);
				if (!replayPuckByNetworkObjectId2)
				{
					return;
				}
				replayPuckByNetworkObjectId2.transform.DOKill(true);
				replayPuckByNetworkObjectId2.transform.DOMove(replayPuckMove.Position, this.TickInterval, false).SetEase(Ease.Linear);
				replayPuckByNetworkObjectId2.transform.DORotateQuaternion(replayPuckMove.Rotation, this.TickInterval).SetEase(Ease.Linear);
				return;
			}
			else
			{
				if (!(eventName == "PlayerBodyDespawned"))
				{
					return;
				}
				ReplayPlayerBodyDespawned replayPlayerBodyDespawned = (ReplayPlayerBodyDespawned)eventData;
				Player replayPlayerByClientId7 = MonoBehaviourSingleton<PlayerManager>.Instance.GetReplayPlayerByClientId(replayPlayerBodyDespawned.OwnerClientId);
				if (!replayPlayerByClientId7)
				{
					return;
				}
				if (!replayPlayerByClientId7.PlayerBody)
				{
					return;
				}
				replayPlayerByClientId7.Server_DespawnPlayerBody();
				return;
			}
		}
		else
		{
			if (!(eventName == "PlayerDespawned"))
			{
				return;
			}
			ReplayPlayerDespawned replayPlayerDespawned = (ReplayPlayerDespawned)eventData;
			Player replayPlayerByClientId8 = MonoBehaviourSingleton<PlayerManager>.Instance.GetReplayPlayerByClientId(replayPlayerDespawned.OwnerClientId);
			if (!replayPlayerByClientId8)
			{
				return;
			}
			replayPlayerByClientId8.NetworkObject.Despawn(true);
			ReplayPlayer.Logger.Info(string.Format("Despawned replay player {0}", replayPlayerByClientId8.OwnerClientId));
			return;
		}
	}

	// Token: 0x06000775 RID: 1909 RVA: 0x00032EF0 File Offset: 0x000310F0
	private void Server_DisposeReplayObjects()
	{
		foreach (Player player in MonoBehaviourSingleton<PlayerManager>.Instance.GetReplayPlayers())
		{
			if (player.PlayerBody)
			{
				player.PlayerBody.transform.DOKill(false);
				player.Server_DespawnPlayerBody();
			}
			if (player.Stick)
			{
				player.Stick.transform.DOKill(false);
				player.Server_DespawnStick();
			}
			player.NetworkObject.Despawn(true);
			ReplayPlayer.Logger.Info(string.Format("Despawned replay player {0}", player.OwnerClientId));
		}
		foreach (Puck puck in MonoBehaviourSingleton<PuckManager>.Instance.GetReplayPucks())
		{
			puck.transform.DOKill(false);
			MonoBehaviourSingleton<PuckManager>.Instance.Server_DespawnPuck(puck);
			ReplayPlayer.Logger.Info(string.Format("Despawned replay puck {0}", puck.OwnerClientId));
		}
	}

	// Token: 0x06000776 RID: 1910 RVA: 0x00033030 File Offset: 0x00031230
	private void Server_Dispose()
	{
		this.Server_DisposeReplayObjects();
		this.Tick = 0;
		this.EventMap.Clear();
		this.replayPuckNetworkObjectIdMap.Clear();
		this.replayPlayerSpawnedList.Clear();
		this.replayPlayerBodySpawnedList.Clear();
		this.replayStickSpawnedList.Clear();
		this.replayPuckSpawnedList.Clear();
	}

	// Token: 0x040004A0 RID: 1184
	private static readonly global::Logger Logger = new global::Logger("ReplayPlayer");

	// Token: 0x040004A1 RID: 1185
	[HideInInspector]
	public bool IsReplaying;

	// Token: 0x040004A2 RID: 1186
	[HideInInspector]
	public int TickRate = 15;

	// Token: 0x040004A3 RID: 1187
	[HideInInspector]
	public int Tick;

	// Token: 0x040004A4 RID: 1188
	[HideInInspector]
	public SortedList<int, List<ValueTuple<string, object>>> EventMap = new SortedList<int, List<ValueTuple<string, object>>>();

	// Token: 0x040004A5 RID: 1189
	private float tickAccumulator;

	// Token: 0x040004A6 RID: 1190
	private Dictionary<ulong, ulong> replayPuckNetworkObjectIdMap = new Dictionary<ulong, ulong>();

	// Token: 0x040004A7 RID: 1191
	private List<ReplayPlayerSpawned> replayPlayerSpawnedList = new List<ReplayPlayerSpawned>();

	// Token: 0x040004A8 RID: 1192
	private List<ReplayPlayerBodySpawned> replayPlayerBodySpawnedList = new List<ReplayPlayerBodySpawned>();

	// Token: 0x040004A9 RID: 1193
	private List<ReplayStickSpawned> replayStickSpawnedList = new List<ReplayStickSpawned>();

	// Token: 0x040004AA RID: 1194
	private List<ReplayPuckSpawned> replayPuckSpawnedList = new List<ReplayPuckSpawned>();
}
