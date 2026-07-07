using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class PuckManager : MonoBehaviourSingleton<PuckManager>
{
	// Token: 0x0600073F RID: 1855 RVA: 0x0000D939 File Offset: 0x0000BB39
	public void AddPuckPosition(PuckPosition puckPosition)
	{
		PuckManager.Logger.Info(string.Format("Added puck position for phase {0}", puckPosition.Phase));
		this.puckPositions.Add(puckPosition);
	}

	// Token: 0x06000740 RID: 1856 RVA: 0x0000D966 File Offset: 0x0000BB66
	public void RemovePuckPosition(PuckPosition puckPosition)
	{
		this.puckPositions.Remove(puckPosition);
	}

	// Token: 0x06000741 RID: 1857 RVA: 0x0000D975 File Offset: 0x0000BB75
	public void AddPuck(Puck puck)
	{
		this.pucks.Add(puck);
	}

	// Token: 0x06000742 RID: 1858 RVA: 0x0000D983 File Offset: 0x0000BB83
	public void RemovePuck(Puck puck)
	{
		this.pucks.Remove(puck);
	}

	// Token: 0x06000743 RID: 1859 RVA: 0x0000D992 File Offset: 0x0000BB92
	public List<Puck> GetPucks(bool includeReplay = false)
	{
		if (includeReplay)
		{
			return this.pucks;
		}
		return (from puck in this.pucks
		where !puck.IsReplay.Value
		select puck).ToList<Puck>();
	}

	// Token: 0x06000744 RID: 1860 RVA: 0x0000D9CD File Offset: 0x0000BBCD
	public List<Puck> GetReplayPucks()
	{
		return (from puck in this.pucks
		where puck.IsReplay.Value
		select puck).ToList<Puck>();
	}

	// Token: 0x06000745 RID: 1861 RVA: 0x0000D9FE File Offset: 0x0000BBFE
	public Puck GetPuck(bool includeReplay = false)
	{
		return this.GetPucks(includeReplay).FirstOrDefault((Puck puck) => puck);
	}

	// Token: 0x06000746 RID: 1862 RVA: 0x00031E58 File Offset: 0x00030058
	public Puck GetPlayerPuck(ulong clientId)
	{
		Player playerByClientId = MonoBehaviourSingleton<PlayerManager>.Instance.GetPlayerByClientId(clientId);
		if (!playerByClientId)
		{
			return null;
		}
		if (!playerByClientId.Stick)
		{
			return null;
		}
		return NetworkingUtils.GetPuckFromNetworkObjectReference(playerByClientId.Stick.NetworkObjectCollisionRecorder.NetworkObjectCollisions.LastOrDefault<NetworkObjectCollision>().NetworkObjectReference);
	}

	// Token: 0x06000747 RID: 1863 RVA: 0x00031EAC File Offset: 0x000300AC
	public Puck GetPuckByNetworkObjectId(ulong networkObjectId)
	{
		return this.GetPucks(false).FirstOrDefault((Puck puck) => puck.NetworkObjectId == networkObjectId);
	}

	// Token: 0x06000748 RID: 1864 RVA: 0x00031EE0 File Offset: 0x000300E0
	public Puck GetReplayPuckByNetworkObjectId(ulong networkObjectId)
	{
		return this.GetReplayPucks().FirstOrDefault((Puck puck) => puck.NetworkObjectId == networkObjectId);
	}

	// Token: 0x06000749 RID: 1865 RVA: 0x00031F14 File Offset: 0x00030114
	public Puck Server_SpawnPuck(Vector3 position, Quaternion rotation, bool isReplay = false)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return null;
		}
		Puck puck = UnityEngine.Object.Instantiate<Puck>(this.puckPrefab, position, rotation);
		puck.InitializeNetworkVariables(isReplay);
		puck.NetworkObject.Spawn(false);
		PuckManager.Logger.Info(string.Format("Spawned puck {0}", puck.NetworkObjectId));
		return puck;
	}

	// Token: 0x0600074A RID: 1866 RVA: 0x0000DA2B File Offset: 0x0000BC2B
	public void Server_DespawnPuck(Puck puck)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		puck.NetworkObject.Despawn(true);
		PuckManager.Logger.Info(string.Format("Despawned puck {0}", puck.NetworkObjectId));
	}

	// Token: 0x0600074B RID: 1867 RVA: 0x00031F70 File Offset: 0x00030170
	public void Server_DespawnPucks(bool includeReplay = false)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		PuckManager.Logger.Info(string.Format("Despawning {0} pucks (includeReplay: {1})", this.pucks.Count, includeReplay));
		foreach (Puck puck in this.pucks.ToList<Puck>())
		{
			if (includeReplay || !puck.IsReplay.Value)
			{
				this.Server_DespawnPuck(puck);
			}
		}
	}

	// Token: 0x0600074C RID: 1868 RVA: 0x00032010 File Offset: 0x00030210
	public void Server_SpawnPucksForPhase(GamePhase phase)
	{
		if (!NetworkManager.Singleton.IsServer)
		{
			return;
		}
		PuckManager.Logger.Info(string.Format("Spawning pucks for phase {0}", phase));
		this.puckPositions.FindAll((PuckPosition puckPosition) => puckPosition.Phase == phase).ForEach(delegate(PuckPosition puckPosition)
		{
			this.Server_SpawnPuck(puckPosition.transform.position, puckPosition.transform.rotation, false);
		});
	}

	// Token: 0x0400045E RID: 1118
	private static readonly global::Logger Logger = new global::Logger("PuckManager");

	// Token: 0x0400045F RID: 1119
	[Header("Prefabs")]
	[SerializeField]
	private Puck puckPrefab;

	// Token: 0x04000460 RID: 1120
	private List<PuckPosition> puckPositions = new List<PuckPosition>();

	// Token: 0x04000461 RID: 1121
	private List<Puck> pucks = new List<Puck>();
}
