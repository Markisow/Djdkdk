using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// Token: 0x02000136 RID: 310
public class SpectatorManager : MonoBehaviourSingleton<SpectatorManager>
{
	// Token: 0x06000922 RID: 2338 RVA: 0x00037DE4 File Offset: 0x00035FE4
	private void Update()
	{
		int count = this.spectatorPositionSpectatorMap.Values.Count;
		if (count == 0)
		{
			return;
		}
		List<Spectator> list = this.spectatorPositionSpectatorMap.Values.ToList<Spectator>();
		for (int i = 0; i < this.spectatorUpdatesPerFrame; i++)
		{
			int index = (this.updateBatch + i) % count;
			list[index].UpdateAnimation();
		}
		this.updateBatch = (this.updateBatch + this.spectatorUpdatesPerFrame) % count;
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00037E54 File Offset: 0x00036054
	public void RegisterSpectatorPosition(SpectatorPosition position)
	{
		if (this.spectatorPositionSpectatorMap.ContainsKey(position))
		{
			return;
		}
		if (UnityEngine.Random.value > this.spectatorDensity)
		{
			return;
		}
		Spectator spectator = UnityEngine.Object.Instantiate<Spectator>(this.spectatorPrefab, position.transform.position, position.transform.rotation, base.transform);
		this.spectatorPositionSpectatorMap[position] = spectator;
		spectator.RandomizeAppearance();
		spectator.PlayAnimation(this.currentAnimation);
		spectator.LookTarget = this.currentLookTarget;
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x0000FB84 File Offset: 0x0000DD84
	public void UnregisterSpectatorPosition(SpectatorPosition position)
	{
		if (!this.spectatorPositionSpectatorMap.ContainsKey(position))
		{
			return;
		}
		UnityEngine.Object.Destroy(this.spectatorPositionSpectatorMap[position].gameObject);
		this.spectatorPositionSpectatorMap.Remove(position);
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00037ED4 File Offset: 0x000360D4
	public void SetSpectatorLookTarget(Transform lookTarget)
	{
		this.currentLookTarget = lookTarget;
		foreach (Spectator spectator in this.spectatorPositionSpectatorMap.Values)
		{
			spectator.LookTarget = this.currentLookTarget;
		}
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x00037F38 File Offset: 0x00036138
	public void SetSpectatorAnimation(string animationName)
	{
		this.currentAnimation = animationName;
		foreach (Spectator spectator in this.spectatorPositionSpectatorMap.Values)
		{
			spectator.PlayAnimation(this.currentAnimation);
		}
	}

	// Token: 0x04000569 RID: 1385
	[Header("Settings")]
	[SerializeField]
	private float spectatorDensity = 0.25f;

	// Token: 0x0400056A RID: 1386
	[SerializeField]
	private int spectatorUpdatesPerFrame = 8;

	// Token: 0x0400056B RID: 1387
	[Header("Prefabs")]
	[SerializeField]
	private Spectator spectatorPrefab;

	// Token: 0x0400056C RID: 1388
	private Dictionary<SpectatorPosition, Spectator> spectatorPositionSpectatorMap = new Dictionary<SpectatorPosition, Spectator>();

	// Token: 0x0400056D RID: 1389
	private Transform currentLookTarget;

	// Token: 0x0400056E RID: 1390
	private string currentAnimation = "Seated";

	// Token: 0x0400056F RID: 1391
	private int updateBatch;
}
