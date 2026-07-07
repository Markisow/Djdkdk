using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;

// Token: 0x02000154 RID: 340
public class VoteManager : MonoBehaviourSingleton<VoteManager>
{
	// Token: 0x06000A62 RID: 2658 RVA: 0x0003CD98 File Offset: 0x0003AF98
	public void Server_AddVote(string name, string title, string description, PlayerTeam[] teams, float timeout, string steamId, int requiredVotes, object data = null)
	{
		Vote vote = new Vote(name, title, description, teams, timeout, steamId, requiredVotes, data);
		this.Votes.Add(vote);
		EventManager.TriggerEvent("Event_Server_OnVoteAdded", new Dictionary<string, object>
		{
			{
				"vote",
				vote
			},
			{
				"teams",
				teams
			}
		});
		Vote vote2 = vote;
		vote2.Progressed = (Action<Vote, string, bool>)Delegate.Combine(vote2.Progressed, new Action<Vote, string, bool>(this.Server_OnVoteProgressed));
		Vote vote3 = vote;
		vote3.Ended = (Action<Vote>)Delegate.Combine(vote3.Ended, new Action<Vote>(this.Server_OnVoteEnded));
		vote.Initialize();
	}

	// Token: 0x06000A63 RID: 2659 RVA: 0x0003CE38 File Offset: 0x0003B038
	public void Server_RemoveVote(Vote vote)
	{
		if (!this.Votes.Contains(vote))
		{
			return;
		}
		this.Votes.Remove(vote);
		EventManager.TriggerEvent("Event_Server_OnVoteRemoved", new Dictionary<string, object>
		{
			{
				"vote",
				vote
			}
		});
		vote.Progressed = (Action<Vote, string, bool>)Delegate.Remove(vote.Progressed, new Action<Vote, string, bool>(this.Server_OnVoteProgressed));
		vote.Ended = (Action<Vote>)Delegate.Remove(vote.Ended, new Action<Vote>(this.Server_OnVoteEnded));
		vote.Dispose();
	}

	// Token: 0x06000A64 RID: 2660 RVA: 0x0003CEC8 File Offset: 0x0003B0C8
	public Vote[] Server_GetVotesByName(string name)
	{
		return (from v in this.Votes
		where v.Name == name
		select v).ToArray<Vote>();
	}

	// Token: 0x06000A65 RID: 2661 RVA: 0x0003CF00 File Offset: 0x0003B100
	public Vote Server_GetVoteByName(string name)
	{
		return this.Votes.Find((Vote v) => v.Name == name);
	}

	// Token: 0x06000A66 RID: 2662 RVA: 0x0003CF34 File Offset: 0x0003B134
	public Vote[] Server_GetTeamVotesByName(string name, PlayerTeam team)
	{
		return (from v in this.Votes
		where v.Name == name && v.Teams.Contains(team)
		select v).ToArray<Vote>();
	}

	// Token: 0x06000A67 RID: 2663 RVA: 0x0003CF74 File Offset: 0x0003B174
	public Vote Server_GetTeamVoteByName(string name, PlayerTeam team)
	{
		return this.Votes.Find((Vote v) => v.Name == name && v.Teams.Contains(team));
	}

	// Token: 0x06000A68 RID: 2664 RVA: 0x0003CFAC File Offset: 0x0003B1AC
	[Rpc(SendTo.SpecifiedInParams, InvokePermission = RpcInvokePermission.Server, DeferLocal = true)]
	public void Server_NotifyVoteStartedRpc(string name, string description, string steamId, int inFavourVotes, int againstVotes, int requiredVotes, object data)
	{
		EventManager.TriggerEvent("Event_OnVoteStarted", new Dictionary<string, object>
		{
			{
				"name",
				name
			},
			{
				"description",
				description
			},
			{
				"steamId",
				steamId
			},
			{
				"inFavourVotes",
				inFavourVotes
			},
			{
				"againstVotes",
				againstVotes
			},
			{
				"requiredVotes",
				requiredVotes
			},
			{
				"data",
				data
			}
		});
	}

	// Token: 0x06000A69 RID: 2665 RVA: 0x00010AC4 File Offset: 0x0000ECC4
	[Rpc(SendTo.SpecifiedInParams, InvokePermission = RpcInvokePermission.Server, DeferLocal = true)]
	public void Server_NotifyVoteProgressedRpc(string name, int inFavourVotes, int againstVotes)
	{
		EventManager.TriggerEvent("Event_OnVoteProgressed", new Dictionary<string, object>
		{
			{
				"name",
				name
			},
			{
				"inFavourVotes",
				inFavourVotes
			},
			{
				"againstVotes",
				againstVotes
			}
		});
	}

	// Token: 0x06000A6A RID: 2666 RVA: 0x00010B03 File Offset: 0x0000ED03
	[Rpc(SendTo.SpecifiedInParams, InvokePermission = RpcInvokePermission.Server, DeferLocal = true)]
	public void Server_NotifyVoteEndedRpc(string name, bool succeeded)
	{
		EventManager.TriggerEvent("Event_OnVoteEnded", new Dictionary<string, object>
		{
			{
				"name",
				name
			},
			{
				"succeeded",
				succeeded
			}
		});
	}

	// Token: 0x06000A6B RID: 2667 RVA: 0x00010B31 File Offset: 0x0000ED31
	private void Server_OnVoteProgressed(Vote vote, string steamId, bool inFavour)
	{
		EventManager.TriggerEvent("Event_Server_OnVoteProgressed", new Dictionary<string, object>
		{
			{
				"vote",
				vote
			},
			{
				"steamId",
				steamId
			},
			{
				"inFavour",
				inFavour
			}
		});
	}

	// Token: 0x06000A6C RID: 2668 RVA: 0x00010B6B File Offset: 0x0000ED6B
	private void Server_OnVoteEnded(Vote vote)
	{
		this.Server_RemoveVote(vote);
	}

	// Token: 0x04000614 RID: 1556
	private static readonly Logger Logger = new Logger("VoteManager");

	// Token: 0x04000615 RID: 1557
	public List<Vote> Votes = new List<Vote>();
}
