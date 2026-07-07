using System;
using System.Collections.Generic;
using DG.Tweening;

// Token: 0x02000153 RID: 339
public class Vote
{
	// Token: 0x17000107 RID: 263
	// (get) Token: 0x06000A59 RID: 2649 RVA: 0x00010A15 File Offset: 0x0000EC15
	public int InFavourVotes
	{
		get
		{
			return this.InFavourSteamIds.Count;
		}
	}

	// Token: 0x17000108 RID: 264
	// (get) Token: 0x06000A5A RID: 2650 RVA: 0x00010A22 File Offset: 0x0000EC22
	public int AgainstVotes
	{
		get
		{
			return this.AgainstSteamIds.Count;
		}
	}

	// Token: 0x06000A5B RID: 2651 RVA: 0x0003CCB0 File Offset: 0x0003AEB0
	public Vote(string name, string title, string description, PlayerTeam[] teams, float timeout, string steamId, int requiredVotes, object data = null)
	{
		this.Name = name;
		this.Title = title;
		this.Description = description;
		this.Teams = teams;
		this.Timeout = timeout;
		this.SteamId = steamId;
		this.RequiredVotes = requiredVotes;
		this.Data = data;
	}

	// Token: 0x06000A5C RID: 2652 RVA: 0x00010A2F File Offset: 0x0000EC2F
	public void Initialize()
	{
		Tween tween = this.timeoutTween;
		if (tween != null)
		{
			tween.Kill(false);
		}
		this.timeoutTween = DOVirtual.DelayedCall(this.Timeout, delegate
		{
			this.End();
		}, true);
		this.CastVote(this.SteamId, true);
	}

	// Token: 0x06000A5D RID: 2653 RVA: 0x00010A6E File Offset: 0x0000EC6E
	public void Dispose()
	{
		Tween tween = this.timeoutTween;
		if (tween == null)
		{
			return;
		}
		tween.Kill(false);
	}

	// Token: 0x06000A5E RID: 2654 RVA: 0x0003CD18 File Offset: 0x0003AF18
	public void CastVote(string steamId, bool inFavour)
	{
		if (this.InFavourSteamIds.Contains(steamId) || this.AgainstSteamIds.Contains(steamId))
		{
			return;
		}
		if (inFavour)
		{
			this.InFavourSteamIds.Add(steamId);
		}
		else
		{
			this.AgainstSteamIds.Add(steamId);
		}
		if (this.InFavourVotes >= this.RequiredVotes)
		{
			this.End();
			return;
		}
		if (this.SteamId != steamId)
		{
			Action<Vote, string, bool> progressed = this.Progressed;
			if (progressed == null)
			{
				return;
			}
			progressed(this, steamId, inFavour);
		}
	}

	// Token: 0x06000A5F RID: 2655 RVA: 0x00010A81 File Offset: 0x0000EC81
	private void End()
	{
		this.Passed = (this.InFavourVotes >= this.RequiredVotes);
		Action<Vote> ended = this.Ended;
		if (ended == null)
		{
			return;
		}
		ended(this);
	}

	// Token: 0x04000605 RID: 1541
	private static readonly Logger Logger = new Logger("Vote");

	// Token: 0x04000606 RID: 1542
	public string Name;

	// Token: 0x04000607 RID: 1543
	public string Title;

	// Token: 0x04000608 RID: 1544
	public string Description;

	// Token: 0x04000609 RID: 1545
	public PlayerTeam[] Teams;

	// Token: 0x0400060A RID: 1546
	public float Timeout;

	// Token: 0x0400060B RID: 1547
	public string SteamId;

	// Token: 0x0400060C RID: 1548
	public int RequiredVotes;

	// Token: 0x0400060D RID: 1549
	public object Data;

	// Token: 0x0400060E RID: 1550
	public List<string> InFavourSteamIds = new List<string>();

	// Token: 0x0400060F RID: 1551
	public List<string> AgainstSteamIds = new List<string>();

	// Token: 0x04000610 RID: 1552
	public Action<Vote, string, bool> Progressed;

	// Token: 0x04000611 RID: 1553
	public Action<Vote> Ended;

	// Token: 0x04000612 RID: 1554
	public bool Passed;

	// Token: 0x04000613 RID: 1555
	private Tween timeoutTween;
}
