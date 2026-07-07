using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

// Token: 0x02000113 RID: 275
public class ReplayRecorder : MonoBehaviour
{
	// Token: 0x06000781 RID: 1921 RVA: 0x000330EC File Offset: 0x000312EC
	private void Update()
	{
		if (NetworkManager.Singleton == null || !NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!this.IsRecording)
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
			this.Server_Tick();
			this.Tick++;
		}
	}

	// Token: 0x06000782 RID: 1922 RVA: 0x00033178 File Offset: 0x00031378
	public void Server_StartRecording(int tickRate)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (this.IsRecording)
		{
			return;
		}
		ReplayRecorder.Logger.Info(string.Format("Replay recording started at {0} ticks per second", this.TickRate));
		this.EventMap.Clear();
		this.TickRate = tickRate;
		this.Tick = 0;
		this.IsRecording = true;
		foreach (Player player in MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayers(false))
		{
			this.Server_AddPlayerSpawnedEvent(player);
			if (player.PlayerBody)
			{
				this.Server_AddPlayerBodySpawnedEvent(player.PlayerBody);
			}
			if (player.Stick)
			{
				this.Server_AddStickSpawnedEvent(player.Stick);
			}
		}
		foreach (Puck puck in MonoBehaviourSingleton<PuckManager>.Instance.GetPucks(false))
		{
			this.Server_AddPuckSpawnedEvent(puck);
		}
	}

	// Token: 0x06000783 RID: 1923 RVA: 0x0000DC75 File Offset: 0x0000BE75
	public void Server_StopRecording()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!this.IsRecording)
		{
			return;
		}
		ReplayRecorder.Logger.Info("Replay recording stopped");
		this.Tick = 0;
		this.IsRecording = false;
	}

	// Token: 0x06000784 RID: 1924 RVA: 0x000332A0 File Offset: 0x000314A0
	private void Server_Tick()
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		foreach (Player player in MonoBehaviourSingleton<PlayerManager>.Instance.GetSpawnedPlayers(false))
		{
			this.Server_AddReplayEvent("PlayerBodyMove", new ReplayPlayerBodyMove
			{
				OwnerClientId = player.OwnerClientId,
				Position = player.PlayerBody.transform.position,
				Rotation = player.PlayerBody.transform.rotation,
				Stamina = player.PlayerBody.Stamina.Value,
				Speed = player.PlayerBody.Speed.Value,
				IsSprinting = player.PlayerBody.IsSprinting.Value,
				IsSliding = player.PlayerBody.IsSliding.Value,
				IsStopping = player.PlayerBody.IsStopping.Value,
				IsExtendedLeft = player.PlayerBody.IsExtendedLeft.Value,
				IsExtendedRight = player.PlayerBody.IsExtendedRight.Value
			});
			this.Server_AddReplayEvent("StickMove", new ReplayStickMove
			{
				OwnerClientId = player.OwnerClientId,
				Position = player.Stick.transform.position,
				Rotation = player.Stick.transform.rotation
			});
			this.Server_AddReplayEvent("PlayerInput", new ReplayPlayerInput
			{
				OwnerClientId = player.OwnerClientId,
				LookAngleInput = player.PlayerInput.LookAngleInput.ServerValue,
				BladeAngleInput = player.PlayerInput.BladeAngleInput.ServerValue,
				TrackInput = player.PlayerInput.TrackInput.ServerValue,
				LookInput = player.PlayerInput.LookInput.ServerValue
			});
		}
		foreach (Puck puck in MonoBehaviourSingleton<PuckManager>.Instance.GetPucks(false))
		{
			this.Server_AddReplayEvent("PuckMove", new ReplayPuckMove
			{
				NetworkObjectId = puck.NetworkObjectId,
				Position = puck.transform.position,
				Rotation = puck.transform.rotation
			});
		}
	}

	// Token: 0x06000785 RID: 1925 RVA: 0x00033578 File Offset: 0x00031778
	public void Server_AddReplayEvent(string eventName, object eventData)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		if (!this.IsRecording)
		{
			return;
		}
		if (this.EventMap.ContainsKey(this.Tick))
		{
			this.EventMap[this.Tick].Add(new ValueTuple<string, object>(eventName, eventData));
			return;
		}
		List<ValueTuple<string, object>> value = new List<ValueTuple<string, object>>
		{
			new ValueTuple<string, object>(eventName, eventData)
		};
		this.EventMap.Add(this.Tick, value);
	}

	// Token: 0x06000786 RID: 1926 RVA: 0x000335F4 File Offset: 0x000317F4
	public void Server_AddPlayerSpawnedEvent(Player player)
	{
		this.Server_AddReplayEvent("PlayerSpawned", new ReplayPlayerSpawned
		{
			OwnerClientId = player.OwnerClientId,
			GameState = player.GameState.Value,
			CustomizationState = player.CustomizationState.Value,
			Handedness = player.Handedness.Value,
			SteamId = player.SteamId.Value,
			Username = player.Username.Value,
			Number = player.Number.Value,
			PatreonLevel = player.PatreonLevel.Value,
			AdminLevel = player.AdminLevel.Value,
			IsMuted = player.IsMuted.Value
		});
	}

	// Token: 0x06000787 RID: 1927 RVA: 0x000336CC File Offset: 0x000318CC
	public void Server_AddPlayerDespawnedEvent(Player player)
	{
		this.Server_AddReplayEvent("PlayerDespawned", new ReplayPlayerDespawned
		{
			OwnerClientId = player.OwnerClientId
		});
	}

	// Token: 0x06000788 RID: 1928 RVA: 0x00033700 File Offset: 0x00031900
	public void Server_AddPlayerBodySpawnedEvent(PlayerBody playerBody)
	{
		this.Server_AddReplayEvent("PlayerBodySpawned", new ReplayPlayerBodySpawned
		{
			OwnerClientId = playerBody.OwnerClientId,
			Position = playerBody.transform.position,
			Rotation = playerBody.transform.rotation,
			GameState = playerBody.Player.GameState.Value,
			CustomizationState = playerBody.Player.CustomizationState.Value,
			Username = playerBody.Player.Username.Value,
			Number = playerBody.Player.Number.Value
		});
	}

	// Token: 0x06000789 RID: 1929 RVA: 0x000337B4 File Offset: 0x000319B4
	public void Server_AddPlayerBodyDespawnedEvent(PlayerBody playerBody)
	{
		this.Server_AddReplayEvent("PlayerBodyDespawned", new ReplayPlayerBodyDespawned
		{
			OwnerClientId = playerBody.OwnerClientId
		});
	}

	// Token: 0x0600078A RID: 1930 RVA: 0x000337E8 File Offset: 0x000319E8
	public void Server_AddStickSpawnedEvent(Stick stick)
	{
		this.Server_AddReplayEvent("StickSpawned", new ReplayStickSpawned
		{
			OwnerClientId = stick.OwnerClientId,
			Position = stick.transform.position,
			Rotation = stick.transform.rotation
		});
	}

	// Token: 0x0600078B RID: 1931 RVA: 0x00033840 File Offset: 0x00031A40
	public void Server_AddStickDespawnedEvent(Stick stick)
	{
		this.Server_AddReplayEvent("StickDespawned", new ReplayStickDespawned
		{
			OwnerClientId = stick.OwnerClientId
		});
	}

	// Token: 0x0600078C RID: 1932 RVA: 0x00033874 File Offset: 0x00031A74
	public void Server_AddPuckSpawnedEvent(Puck puck)
	{
		this.Server_AddReplayEvent("PuckSpawned", new ReplayPuckSpawned
		{
			NetworkObjectId = puck.NetworkObjectId,
			Position = puck.transform.position,
			Rotation = puck.transform.rotation
		});
	}

	// Token: 0x0600078D RID: 1933 RVA: 0x000338CC File Offset: 0x00031ACC
	public void Server_AddPuckDespawnedEvent(Puck puck)
	{
		this.Server_AddReplayEvent("PuckDespawned", new ReplayPuckDespawned
		{
			NetworkObjectId = puck.NetworkObjectId
		});
	}

	// Token: 0x040004AF RID: 1199
	private static readonly global::Logger Logger = new global::Logger("ReplayRecorder");

	// Token: 0x040004B0 RID: 1200
	[HideInInspector]
	public bool IsRecording;

	// Token: 0x040004B1 RID: 1201
	[HideInInspector]
	public int TickRate = 15;

	// Token: 0x040004B2 RID: 1202
	[HideInInspector]
	public int Tick;

	// Token: 0x040004B3 RID: 1203
	[HideInInspector]
	public SortedList<int, List<ValueTuple<string, object>>> EventMap = new SortedList<int, List<ValueTuple<string, object>>>();

	// Token: 0x040004B4 RID: 1204
	private float tickAccumulator;
}
