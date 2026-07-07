using System;
using Unity.Netcode;

// Token: 0x0200011D RID: 285
public class ConnectionApproval
{
	// Token: 0x170000D6 RID: 214
	// (get) Token: 0x060007EC RID: 2028 RVA: 0x0000E2DE File Offset: 0x0000C4DE
	public ulong ClientID
	{
		get
		{
			return this.Request.ClientNetworkId;
		}
	}

	// Token: 0x170000D7 RID: 215
	// (get) Token: 0x060007ED RID: 2029 RVA: 0x0000E2EB File Offset: 0x0000C4EB
	public bool IsHost
	{
		get
		{
			return this.ClientID == 0UL;
		}
	}

	// Token: 0x060007EE RID: 2030 RVA: 0x0000E2F7 File Offset: 0x0000C4F7
	public void Halt()
	{
		this.IsInProgress = true;
		this.Response.Pending = this.IsInProgress;
	}

	// Token: 0x060007EF RID: 2031 RVA: 0x0000E311 File Offset: 0x0000C511
	public void Approve(PlayerData playerData)
	{
		this.PlayerData = playerData;
		this.IsApproved = true;
		this.IsInProgress = false;
		this.Response.Approved = this.IsApproved;
		this.Response.Pending = this.IsInProgress;
	}

	// Token: 0x060007F0 RID: 2032 RVA: 0x0000E34A File Offset: 0x0000C54A
	public void Reject(string reason)
	{
		this.IsApproved = false;
		this.IsInProgress = false;
		this.Response.Reason = reason;
		this.Response.Approved = this.IsApproved;
		this.Response.Pending = this.IsInProgress;
	}

	// Token: 0x040004C9 RID: 1225
	public NetworkManager.ConnectionApprovalRequest Request;

	// Token: 0x040004CA RID: 1226
	public NetworkManager.ConnectionApprovalResponse Response;

	// Token: 0x040004CB RID: 1227
	public ConnectionData ConnectionData;

	// Token: 0x040004CC RID: 1228
	public PlayerData PlayerData;

	// Token: 0x040004CD RID: 1229
	public string IpAddress;

	// Token: 0x040004CE RID: 1230
	public bool IsApproved;

	// Token: 0x040004CF RID: 1231
	public bool IsInProgress;
}
