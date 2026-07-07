using System;

// Token: 0x0200008C RID: 140
public struct ServerState
{
	// Token: 0x060004BB RID: 1211 RVA: 0x0000BDA3 File Offset: 0x00009FA3
	public bool Equals(ServerState other)
	{
		return this.AuthenticationPhase == other.AuthenticationPhase && this.ServerData == other.ServerData && this.MatchData == other.MatchData;
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x00029F8C File Offset: 0x0002818C
	public override bool Equals(object obj)
	{
		if (obj is ServerState)
		{
			ServerState other = (ServerState)obj;
			return this.Equals(other);
		}
		return false;
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0000BDD1 File Offset: 0x00009FD1
	public override int GetHashCode()
	{
		return HashCode.Combine<AuthenticationPhase, ServerData, ServerMatchData>(this.AuthenticationPhase, this.ServerData, this.MatchData);
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0000BDEA File Offset: 0x00009FEA
	public override string ToString()
	{
		return string.Format("AuthenticationPhase: {0}, ServerData: {1}, MatchData: {2}", this.AuthenticationPhase, this.ServerData, this.MatchData);
	}

	// Token: 0x040002EC RID: 748
	public AuthenticationPhase AuthenticationPhase;

	// Token: 0x040002ED RID: 749
	public ServerData ServerData;

	// Token: 0x040002EE RID: 750
	public ServerMatchData MatchData;
}
