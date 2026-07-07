using System;
using System.Linq;

// Token: 0x020001DA RID: 474
public static class BackendUtils
{
	// Token: 0x06000E4C RID: 3660 RVA: 0x00013C6A File Offset: 0x00011E6A
	public static PlayerBan GetActivePlayerDataBan(PlayerData playerData)
	{
		if (playerData == null)
		{
			return null;
		}
		return playerData.bans.FirstOrDefault((PlayerBan ban) => Utils.GetTimestamp() <= ban.expiresAt);
	}

	// Token: 0x06000E4D RID: 3661 RVA: 0x00013C9B File Offset: 0x00011E9B
	public static PlayerMute GetActivePlayerDataMute(PlayerData playerData)
	{
		if (playerData == null)
		{
			return null;
		}
		return playerData.mutes.FirstOrDefault((PlayerMute mute) => Utils.GetTimestamp() <= mute.expiresAt);
	}

	// Token: 0x06000E4E RID: 3662 RVA: 0x00013CCC File Offset: 0x00011ECC
	public static PlayerCooldown GetActivePlayerDataCooldown(PlayerData playerData)
	{
		if (playerData == null)
		{
			return null;
		}
		return playerData.cooldowns.FirstOrDefault((PlayerCooldown cooldown) => Utils.GetTimestamp() <= cooldown.expiresAt);
	}

	// Token: 0x06000E4F RID: 3663 RVA: 0x0004B95C File Offset: 0x00049B5C
	public static bool IsConnectedToMatchEndPoint()
	{
		Connection connection = GlobalStateManager.ConnectionState.Connection;
		EndPoint a = (connection != null) ? connection.EndPoint : null;
		PlayerMatchData matchData = BackendManager.PlayerState.MatchData;
		EndPoint b = (matchData != null) ? matchData.endPoint : null;
		return a != null && a == b;
	}
}
