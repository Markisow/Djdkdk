using System;
using System.Collections.Generic;
using DG.Tweening;

// Token: 0x0200012E RID: 302
public class TimeoutManager : MonoBehaviourSingleton<TimeoutManager>
{
	// Token: 0x06000892 RID: 2194 RVA: 0x00036224 File Offset: 0x00034424
	public void Dispose()
	{
		foreach (Tween t in this.steamIdTweenMap.Values)
		{
			t.Kill(false);
		}
		this.steamIdTweenMap.Clear();
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x00036288 File Offset: 0x00034488
	public void AddSteamIdTimeout(string steamId, float timeout)
	{
		if (this.steamIdTweenMap.ContainsKey(steamId))
		{
			return;
		}
		this.steamIdTweenMap[steamId] = DOVirtual.DelayedCall(timeout, delegate
		{
			this.RemoveSteamIdTimeout(steamId);
		}, true);
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0000EB6E File Offset: 0x0000CD6E
	public void RemoveSteamIdTimeout(string steamId)
	{
		if (!this.steamIdTweenMap.ContainsKey(steamId))
		{
			return;
		}
		this.steamIdTweenMap[steamId].Kill(false);
		this.steamIdTweenMap.Remove(steamId);
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0000EB9E File Offset: 0x0000CD9E
	public bool IsSteamIdTimedOut(string steamId)
	{
		return this.steamIdTweenMap.ContainsKey(steamId);
	}

	// Token: 0x0400051B RID: 1307
	private Dictionary<string, Tween> steamIdTweenMap = new Dictionary<string, Tween>();
}
