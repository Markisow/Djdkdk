using System;

// Token: 0x0200008B RID: 139
public struct PlayerState
{
	// Token: 0x060004B7 RID: 1207 RVA: 0x00029E8C File Offset: 0x0002808C
	public bool Equals(PlayerState other)
	{
		return this.AuthenticationPhase == other.AuthenticationPhase && this.PlayerData == other.PlayerData && this.PartyData == other.PartyData && this.GroupData == other.GroupData && this.MatchData == other.MatchData && this.PlayerStatistics == other.PlayerStatistics && this.Key == other.Key;
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x00029F00 File Offset: 0x00028100
	public override bool Equals(object obj)
	{
		if (obj is PlayerState)
		{
			PlayerState other = (PlayerState)obj;
			return this.Equals(other);
		}
		return false;
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0000BD72 File Offset: 0x00009F72
	public override int GetHashCode()
	{
		return HashCode.Combine<AuthenticationPhase, PlayerData, PlayerPartyData, PlayerGroupData, PlayerMatchData, PlayerStatistics, string>(this.AuthenticationPhase, this.PlayerData, this.PartyData, this.GroupData, this.MatchData, this.PlayerStatistics, this.Key);
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x00029F28 File Offset: 0x00028128
	public override string ToString()
	{
		return string.Format("AuthenticationPhase: {0}, PlayerData: {1}, PartyData: {2}, GroupData: {3}, MatchData: {4}, PlayerStatistics: {5}, Key: {6}", new object[]
		{
			this.AuthenticationPhase,
			this.PlayerData,
			this.PartyData,
			this.GroupData,
			this.MatchData,
			this.PlayerStatistics,
			this.Key
		});
	}

	// Token: 0x040002E5 RID: 741
	public AuthenticationPhase AuthenticationPhase;

	// Token: 0x040002E6 RID: 742
	public PlayerData PlayerData;

	// Token: 0x040002E7 RID: 743
	public PlayerPartyData PartyData;

	// Token: 0x040002E8 RID: 744
	public PlayerGroupData GroupData;

	// Token: 0x040002E9 RID: 745
	public PlayerMatchData MatchData;

	// Token: 0x040002EA RID: 746
	public PlayerStatistics PlayerStatistics;

	// Token: 0x040002EB RID: 747
	public string Key;
}
