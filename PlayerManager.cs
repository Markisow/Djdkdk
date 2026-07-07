using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

// Token: 0x020000ED RID: 237
public class PlayerManager : MonoBehaviourSingleton<PlayerManager>
{
	// Token: 0x06000708 RID: 1800 RVA: 0x0000D740 File Offset: 0x0000B940
	public void AddPlayer(Player player)
	{
		this.players.Add(player);
		EventManager.TriggerEvent("Event_Everyone_OnPlayerAdded", new Dictionary<string, object>
		{
			{
				"player",
				player
			}
		});
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0000D769 File Offset: 0x0000B969
	public void RemovePlayer(Player player)
	{
		this.players.Remove(player);
		EventManager.TriggerEvent("Event_Everyone_OnPlayerRemoved", new Dictionary<string, object>
		{
			{
				"player",
				player
			}
		});
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x00031740 File Offset: 0x0002F940
	public List<Player> GetPlayers(bool includeReplay = false)
	{
		this.players.RemoveAll((Player player) => !player || !player.NetworkObject.IsSpawned);
		if (includeReplay)
		{
			return this.players;
		}
		return (from player in this.players
		where !player.IsReplay.Value
		select player).ToList<Player>();
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x000317B4 File Offset: 0x0002F9B4
	public List<Player> GetPlayersByPhase(PlayerPhase phase, bool includeReplay = false)
	{
		return (from player in this.GetPlayers(includeReplay)
		where player.Phase == phase
		select player).ToList<Player>();
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x000317EC File Offset: 0x0002F9EC
	public List<Player> GetPlayersByPhases(PlayerPhase[] phases, bool includeReplay = false)
	{
		return (from player in this.GetPlayers(includeReplay)
		where phases.Contains(player.Phase)
		select player).ToList<Player>();
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x00031824 File Offset: 0x0002FA24
	public List<Player> GetPlayersByTeam(PlayerTeam team, bool includeReplay = false)
	{
		return (from player in this.GetPlayers(includeReplay)
		where player.Team == team
		select player).ToList<Player>();
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x0003185C File Offset: 0x0002FA5C
	public List<Player> GetPlayersByTeams(PlayerTeam[] team, bool includeReplay = false)
	{
		return (from player in this.GetPlayers(includeReplay)
		where team.Contains(player.Team)
		select player).ToList<Player>();
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x00031894 File Offset: 0x0002FA94
	public Player GetPlayerByClientId(ulong clientId)
	{
		return this.GetPlayers(false).Find((Player player) => player.OwnerClientId == clientId);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x000318C8 File Offset: 0x0002FAC8
	public Player GetPlayerByUsername(FixedString32Bytes username, bool caseSensitive = false)
	{
		return this.GetPlayers(false).Find((Player player) => (caseSensitive ? player.Username.Value.ToString() : player.Username.Value.ToString().ToLower()) == (caseSensitive ? username.ToString() : username.ToString().ToLower()));
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00031904 File Offset: 0x0002FB04
	public Player GetPlayerByNumber(int number)
	{
		return this.GetPlayers(false).Find((Player player) => player.Number.Value == number);
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00031938 File Offset: 0x0002FB38
	public Player GetPlayerByNeedle(string needle, bool caseSensitive = true)
	{
		Player player = this.GetPlayerByUsername(needle, caseSensitive);
		int number;
		if (!player && int.TryParse(needle, out number))
		{
			player = this.GetPlayerByNumber(number);
		}
		return player;
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00031970 File Offset: 0x0002FB70
	public Player GetPlayerBySteamId(FixedString32Bytes steamId)
	{
		return this.GetPlayers(false).Find(delegate(Player player)
		{
			FixedString32Bytes value = player.SteamId.Value;
			return value == steamId;
		});
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x0000D793 File Offset: 0x0000B993
	public List<Player> GetReplayPlayers()
	{
		return (from player in this.GetPlayers(true)
		where player.IsReplay.Value
		select player).ToList<Player>();
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x000319A4 File Offset: 0x0002FBA4
	public Player GetReplayPlayerByClientId(ulong clientId)
	{
		return this.GetReplayPlayers().Find((Player player) => player.OwnerClientId == clientId + 1337UL);
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x0000D7C5 File Offset: 0x0000B9C5
	public Player GetLocalPlayer()
	{
		return this.GetPlayers(false).Find((Player player) => player.IsLocalPlayer);
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x0000D7F2 File Offset: 0x0000B9F2
	public List<Player> GetSpawnedPlayers(bool includeReplay = false)
	{
		return this.GetPlayers(includeReplay).FindAll((Player player) => player.IsCharacterSpawned);
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x000319D8 File Offset: 0x0002FBD8
	public List<Player> GetSpawnedPlayersByTeam(PlayerTeam team, bool includeReplay = false)
	{
		return (from player in this.GetSpawnedPlayers(includeReplay)
		where player.Team == team
		select player).ToList<Player>();
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x00031A10 File Offset: 0x0002FC10
	public void Server_SpawnPlayer(ulong clientId, PlayerGameState gameState, PlayerCustomizationState customizationState, PlayerHandedness handedness, string steamID, string username, int number, int patreonLevel, int adminLevel, bool isMuted = false, bool isReplay = false)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		Player player = UnityEngine.Object.Instantiate<Player>(this.playerPrefab);
		Player player2 = player;
		FixedString32Bytes steamID2 = steamID;
		FixedString32Bytes username2 = username;
		player2.InitializeNetworkVariables(gameState, customizationState, handedness, steamID2, username2, number, patreonLevel, adminLevel, 0, 0, 0UL, default(NetworkObjectReference), isMuted, isReplay);
		if (isReplay)
		{
			player.NetworkObject.SpawnWithOwnership(1337UL + clientId, false);
			PlayerManager.Logger.Info(string.Format("Spawned replay player ({0})", clientId));
			return;
		}
		player.NetworkObject.SpawnAsPlayerObject(clientId, false);
		PlayerManager.Logger.Info(string.Format("Spawned player ({0})", clientId));
	}

	// Token: 0x04000448 RID: 1096
	private static readonly global::Logger Logger = new global::Logger("PlayerManager");

	// Token: 0x04000449 RID: 1097
	[Header("Prefabs")]
	[SerializeField]
	private Player playerPrefab;

	// Token: 0x0400044A RID: 1098
	private List<Player> players = new List<Player>();
}
