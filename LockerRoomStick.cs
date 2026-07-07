using System;
using UnityEngine;

// Token: 0x02000017 RID: 23
public class LockerRoomStick : MonoBehaviour
{
	// Token: 0x0600007D RID: 125 RVA: 0x00008FCB File Offset: 0x000071CB
	public void SetSkinID(int skinID, PlayerTeam team, PlayerRole role)
	{
		((role == PlayerRole.Goalie) ? this.goalieStickMesh : this.attackerStickMesh).SetSkinID(skinID, team);
	}

	// Token: 0x0600007E RID: 126 RVA: 0x00008FE6 File Offset: 0x000071E6
	public void SetShaftTapeID(int shaftTapeID, PlayerRole role)
	{
		((role == PlayerRole.Goalie) ? this.goalieStickMesh : this.attackerStickMesh).SetShaftTapeID(shaftTapeID);
	}

	// Token: 0x0600007F RID: 127 RVA: 0x00009000 File Offset: 0x00007200
	public void SetBladeTapeID(int bladeTapeID, PlayerRole role)
	{
		((role == PlayerRole.Goalie) ? this.goalieStickMesh : this.attackerStickMesh).SetBladeTapeID(bladeTapeID);
	}

	// Token: 0x06000080 RID: 128 RVA: 0x0000901A File Offset: 0x0000721A
	public void ShowRoleStick(PlayerRole role)
	{
		this.attackerStickMesh.gameObject.SetActive(role == PlayerRole.Attacker);
		this.goalieStickMesh.gameObject.SetActive(role == PlayerRole.Goalie);
	}

	// Token: 0x0400003C RID: 60
	[Header("References")]
	[SerializeField]
	private StickMesh attackerStickMesh;

	// Token: 0x0400003D RID: 61
	[SerializeField]
	private StickMesh goalieStickMesh;
}
